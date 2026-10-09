using libzkfpcsharp;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        private const string connectionString =
            "Server=localhost;Database=vaultify;User ID=root;Password=password;";

        private FingerprintManager fingerprintManager =
            FingerprintManager.Instance;

        private class StaffAccountItem
        {
            public int EmployeeId { get; set; }
            public string FullName { get; set; }
            public string Username { get; set; }
            public string Role { get; set; }
            public string Title { get; set; }
            public bool HasFingerprint { get; set; }

            public override string ToString()
            {
                if (EmployeeId == 0)
                {
                    return FullName;
                }

                string display = FullName;
                if (!string.IsNullOrWhiteSpace(Title))
                {
                    display += " (" + Title + ")";
                }

                display += " - " + Role;
                return display;
            }
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            lblFooterStatus.Text = "● Initializing ZKTeco Hardware Engine...";
            lblScannerDevice.Text = "Initializing optical sensor...";
            lblScannerInstruction.Text = "Please wait while biometric engine starts...";

            string error = null;

            bool success = await Task.Run(() =>
            {
                return fingerprintManager.Initialize(out error);
            });

            if (!success)
            {
                lblFooterStatus.Text = "● Scanner Offline / Not Connected • Subic Treasury OS v2.4";
                lblScannerDevice.Text = "Optical Scanner Offline";
                lblScannerInstruction.Text = error ?? "Connect scanner or use Password Login tab.";
                lblScannerDevice.ForeColor = Color.FromArgb(170, 50, 40);
                progressBarScanner.Value = 0;
            }
            else
            {
                lblFooterStatus.Text = "● Hardware Sensor Ready • Subic Treasury OS v2.4";
                lblScannerDevice.Text = "Optical Scanner Ready (ZKTeco Live20R)";
                lblScannerInstruction.Text = "Click scanner or button below to authenticate.";
                progressBarScanner.Value = 100;

                // Load all registered fingerprints into in-memory cache
                LoadFingerprintDatabase();

                // Start capture loop
                fingerprintManager.StartCapture(OnFingerprintCaptured);
            }

            // Populate Staff Account dropdown
            LoadRegisteredOfficersList();
        }

        // =========================================================
        // LOAD REGISTERED OFFICERS LIST
        // =========================================================

        public void LoadRegisteredOfficersList()
        {
            try
            {
                cboStaff.Items.Clear();

                // Item 0: Auto-Detect
                cboStaff.Items.Add(new StaffAccountItem
                {
                    EmployeeId = 0,
                    FullName = "[ Auto-Detect Any Registered Staff ]",
                    Username = "auto",
                    Role = "All Roles",
                    Title = "Biometrics Enrolled",
                    HasFingerprint = true
                });

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT employee_id, full_name, official_title, username, role, fingerprint_template
                        FROM employees
                        WHERE status = 'ACTIVE'
                        ORDER BY full_name ASC;
                    ";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int empId = reader.GetInt32("employee_id");
                                string fullName = reader["full_name"] != DBNull.Value ? reader["full_name"].ToString() : "Officer";
                                string title = reader["official_title"] != DBNull.Value ? reader["official_title"].ToString() : "";
                                string username = reader["username"].ToString();
                                string role = reader["role"].ToString();
                                bool hasFp = reader["fingerprint_template"] != DBNull.Value;

                                cboStaff.Items.Add(new StaffAccountItem
                                {
                                    EmployeeId = empId,
                                    FullName = fullName,
                                    Title = title,
                                    Username = username,
                                    Role = role,
                                    HasFingerprint = hasFp
                                });
                            }
                        }
                    }
                }

                if (cboStaff.Items.Count > 0)
                {
                    cboStaff.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading officers list: " + ex.Message);
            }
        }

        private void cboStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStaff.SelectedItem is StaffAccountItem item)
            {
                if (item.EmployeeId == 0)
                {
                    lblStaffInfo.Text = "Officer: Auto-Detect • Role: All Registered Staff (Biometrics Enrolled)";
                }
                else
                {
                    string fpStatus = item.HasFingerprint ? "Biometrics Enrolled" : "Password Only";
                    lblStaffInfo.Text = "Officer: " + item.FullName + " • Role: " + item.Role + " (" + fpStatus + ")";
                }
            }
        }

        // =========================================================
        // LOAD FINGERPRINTS FROM MYSQL INTO CACHE
        // =========================================================

        private void LoadFingerprintDatabase()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT employee_id, fingerprint_template
                        FROM employees
                        WHERE fingerprint_template IS NOT NULL AND status = 'ACTIVE';
                    ";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int employeeId = reader.GetInt32("employee_id");
                                byte[] template = (byte[])reader["fingerprint_template"];

                                int ret = fingerprintManager.AddFingerprint(employeeId, template);
                                if (ret != zkfperrdef.ZKFP_ERR_OK)
                                {
                                    System.Diagnostics.Debug.WriteLine(
                                        "Could not load fingerprint for employee " + employeeId + ". Error: " + ret
                                    );
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading fingerprints from DB: " + ex.Message);
            }
        }

        // =========================================================
        // FINGERPRINT CAPTURE CALLBACK
        // =========================================================

        private void OnFingerprintCaptured(FingerprintCapture capture)
        {
            if (IsDisposed || !IsHandleCreated)
                return;

            try
            {
                BeginInvoke(new Action(() =>
                {
                    ProcessFingerprint(capture);
                }));
            }
            catch
            {
                // Form is closing.
            }
        }

        // =========================================================
        // PROCESS FINGERPRINT
        // =========================================================

        private void ProcessFingerprint(FingerprintCapture capture)
        {
            if (IsDisposed) return;

            // Show captured image in sensor preview box
            try
            {
                Bitmap bitmap = CreateFingerprintBitmap(
                    capture.Image,
                    fingerprintManager.ImageWidth,
                    fingerprintManager.ImageHeight
                );

                if (picFPImg.Image != null)
                {
                    picFPImg.Image.Dispose();
                }

                picFPImg.Image = bitmap;
                picFPImg.Invalidate();
            }
            catch
            {
            }

            int employeeId = 0;
            int score = 0;

            int ret = fingerprintManager.Identify(
                capture.Template,
                ref employeeId,
                ref score
            );

            if (ret == zkfperrdef.ZKFP_ERR_OK && employeeId > 0)
            {
                lblScannerInstruction.Text = "Fingerprint authenticated! Score: " + score;
                lblScannerInstruction.ForeColor = Color.FromArgb(20, 120, 70);

                // Highlight employee in combobox
                for (int i = 0; i < cboStaff.Items.Count; i++)
                {
                    if (cboStaff.Items[i] is StaffAccountItem item && item.EmployeeId == employeeId)
                    {
                        cboStaff.SelectedIndex = i;
                        break;
                    }
                }

                LoginWithFingerprint(employeeId);
            }
            else
            {
                lblScannerInstruction.Text = "Fingerprint not recognized. Score: " + score + ". Please try again.";
                lblScannerInstruction.ForeColor = Color.FromArgb(170, 50, 40);
            }
        }

        // =========================================================
        // FINGERPRINT LOGIN SUCCESS
        // =========================================================

        private void LoginWithFingerprint(int employeeId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT employee_id, full_name, role
                        FROM employees
                        WHERE employee_id = @employeeId AND status = 'ACTIVE';
                    ";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@employeeId", employeeId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                OpenDashboard(employeeId);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Account found but is currently inactive.",
                                    "Access Denied",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Fingerprint login failed:\n\n" + ex.Message,
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // PASSWORD LOGIN
        // =========================================================

        private void btnLoginPassword_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Please enter both username and password.",
                    "Input Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT employee_id, password_hash
                        FROM employees
                        WHERE username = @username AND status = 'ACTIVE';
                    ";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "Invalid username or access key.",
                                    "Authentication Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );
                                return;
                            }

                            int employeeId = reader.GetInt32("employee_id");
                            string storedHash = reader.GetString("password_hash");

                            if (!VerifyPassword(password, storedHash))
                            {
                                MessageBox.Show(
                                    "Invalid username or access key.",
                                    "Authentication Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );
                                return;
                            }

                            OpenDashboard(employeeId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Login failed:\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // PASSWORD VERIFICATION HELPER
        // =========================================================

        private bool VerifyPassword(string password, string storedPassword)
        {
            try
            {
                string[] parts = storedPassword.Split(':');
                if (parts.Length != 2) return false;

                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] storedHash = Convert.FromBase64String(parts[1]);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
                {
                    byte[] computedHash = pbkdf2.GetBytes(32);
                    if (computedHash.Length != storedHash.Length) return false;

                    for (int i = 0; i < computedHash.Length; i++)
                    {
                        if (computedHash[i] != storedHash[i]) return false;
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // =========================================================
        // OPEN DASHBOARD
        // =========================================================

        private void OpenDashboard(int employeeId)
        {
            fingerprintManager.StopCapture();

            Dashboard dashboard = new Dashboard(employeeId);
            dashboard.FormClosed += (s, args) =>
            {
                this.Show();
                LoadRegisteredOfficersList();
                fingerprintManager.StartCapture(OnFingerprintCaptured);
                lblScannerInstruction.Text = "Click scanner or button below to authenticate.";
                lblScannerInstruction.ForeColor = Color.FromArgb(100, 120, 130);
            };

            this.Hide();
            dashboard.Show();
        }

        // =========================================================
        // TAB SWITCHING
        // =========================================================

        private void btnTabBiometric_Click(object sender, EventArgs e)
        {
            panelBiometricCard.Visible = true;
            panelPasswordCard.Visible = false;

            btnTabBiometric.BackColor = Color.FromArgb(46, 117, 107);
            btnTabBiometric.ForeColor = Color.White;

            btnTabPassword.BackColor = Color.FromArgb(225, 233, 238);
            btnTabPassword.ForeColor = Color.FromArgb(55, 80, 95);

            if (fingerprintManager.IsConnected)
            {
                fingerprintManager.StartCapture(OnFingerprintCaptured);
            }
        }

        private void btnTabPassword_Click(object sender, EventArgs e)
        {
            panelBiometricCard.Visible = false;
            panelPasswordCard.Visible = true;

            btnTabPassword.BackColor = Color.FromArgb(46, 117, 107);
            btnTabPassword.ForeColor = Color.White;

            btnTabBiometric.BackColor = Color.FromArgb(225, 233, 238);
            btnTabBiometric.ForeColor = Color.FromArgb(55, 80, 95);

            txtUsername.Focus();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnScanBiometric_Click(object sender, EventArgs e)
        {
            lblScannerInstruction.Text = "Scanner Active. Place registered finger firmly on the sensor.";
            lblScannerInstruction.ForeColor = Color.FromArgb(46, 117, 107);
            progressBarScanner.Value = 100;
        }

        private void picFPImg_Click(object sender, EventArgs e)
        {
            btnScanBiometric_Click(sender, e);
        }

        // =========================================================
        // OPEN ACCOUNT CREATION / BIOMETRIC ENROLLMENT
        // =========================================================

        private void btnNewStaff_Click(object sender, EventArgs e)
        {
            fingerprintManager.StopCapture();

            AccountCreationForm accountForm = new AccountCreationForm(this);
            accountForm.FormClosed += (s, args) =>
            {
                this.Show();
                LoadRegisteredOfficersList();
                if (fingerprintManager.IsConnected)
                {
                    fingerprintManager.StartCapture(OnFingerprintCaptured);
                    lblScannerInstruction.Text = "Click scanner or button below to authenticate.";
                    lblScannerInstruction.ForeColor = Color.FromArgb(100, 120, 130);
                }
            };

            this.Hide();
            accountForm.Show();
        }

        // =========================================================
        // GDI+ VECTOR PAINTING (Crisp sharp graphics)
        // =========================================================

        private void picHeaderIcon_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(235, 245, 250), 2.5f))
            using (Brush brush = new SolidBrush(Color.FromArgb(235, 245, 250)))
            {
                // Pediment (Triangle Roof)
                Point[] roof = new Point[]
                {
                    new Point(26, 8),
                    new Point(6, 22),
                    new Point(46, 22)
                };
                g.FillPolygon(brush, roof);

                // Entablature (Roof beam)
                g.FillRectangle(brush, 4, 23, 44, 4);

                // Four Pillars
                int[] pillars = { 7, 18, 29, 40 };
                foreach (int px in pillars)
                {
                    g.FillRectangle(brush, px, 29, 5, 20);
                }

                // Base steps
                g.FillRectangle(brush, 4, 50, 44, 4);
                g.FillRectangle(brush, 2, 55, 48, 4);
            }
        }

        private void picFPImg_Paint(object sender, PaintEventArgs e)
        {
            if (picFPImg.Image != null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = picFPImg.Width / 2;
            int cy = picFPImg.Height / 2;

            // Draw lock / hand scanner target
            using (Pen pen = new Pen(Color.FromArgb(140, 170, 185), 2f))
            using (Brush bodyBrush = new SolidBrush(Color.FromArgb(215, 230, 238)))
            using (Brush lockBrush = new SolidBrush(Color.FromArgb(90, 125, 140)))
            {
                // Scanner target circle
                g.DrawEllipse(pen, cx - 45, cy - 45, 90, 90);

                // Shackle of lock
                using (Pen shacklePen = new Pen(Color.FromArgb(90, 125, 140), 4f))
                {
                    g.DrawArc(shacklePen, cx - 14, cy - 26, 28, 28, 180, 180);
                }

                // Body of lock
                g.FillRectangle(lockBrush, cx - 18, cy - 12, 36, 28);

                // Keyhole
                using (Brush holeBrush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(holeBrush, cx - 4, cy - 4, 8, 8);
                    g.FillRectangle(holeBrush, cx - 2, cy, 4, 8);
                }
            }
        }

        // =========================================================
        // BITMAP CONVERSION
        // =========================================================

        private Bitmap CreateFingerprintBitmap(byte[] buffer, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            ColorPalette palette = bitmap.Palette;

            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }

            bitmap.Palette = palette;

            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format8bppIndexed
            );

            System.Runtime.InteropServices.Marshal.Copy(
                buffer,
                0,
                data.Scan0,
                width * height
            );

            bitmap.UnlockBits(data);
            return bitmap;
        }
    }
}