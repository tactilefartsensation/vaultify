using libzkfpcsharp;
using MySqlConnector;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AccountCreationForm : Form
    {
        private const string connectionString =
            "Server=localhost;Database=vaultify;User ID=root;Password=password;";

        private LoginForm loginForm;
        private FingerprintManager fingerprintManager = FingerprintManager.Instance;

        private const int REGISTER_FINGER_COUNT = 3;
        private byte[][] RegTmps = new byte[REGISTER_FINGER_COUNT][];
        private byte[] RegTmp = new byte[2048];
        private int cbRegTmp = 0;
        private int RegisterCount = 0;
        private bool enrollmentActive = false;
        private byte[] registeredFingerprintTemplate = null;

        public AccountCreationForm(LoginForm loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private void AccountCreationForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < REGISTER_FINGER_COUNT; i++)
            {
                RegTmps[i] = new byte[2048];
            }

            RegisterCount = 0;
            cbRegTmp = 0;
            enrollmentActive = false;
            registeredFingerprintTemplate = null;
            progressBarFP.Value = 0;

            // Check how many accounts exist to advise on role assignment
            CheckAccountRoleState();

            if (!fingerprintManager.IsConnected)
            {
                lblSensorDevice.Text = "Sensor: Disconnected / Offline";
                lblSensorDevice.ForeColor = Color.FromArgb(180, 45, 35);
                lblFPStatus.Text = "Fingerprint scanner not connected.";
                return;
            }

            // Start capture loop for enrollment
            fingerprintManager.StartCapture(OnFingerprintCaptured);

            lblSensorDevice.Text = "Sensor: ZKTeco Live20R (500 DPI Optical)";
            lblFPStatus.Text = "Status: Hardware Online. Place finger firmly on sensor.";
            lblFPTemplate.Text = "Template: [Not Registered]";
        }

        private void CheckAccountRoleState()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM employees;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        long count = Convert.ToInt64(command.ExecuteScalar());
                        if (count == 0)
                        {
                            lblRoleNotice.Text = "★ Initial Setup: No accounts exist yet. This first account will automatically be created as SysAdmin.";
                            lblRoleNotice.BackColor = Color.FromArgb(232, 246, 238);
                            lblRoleNotice.ForeColor = Color.FromArgb(25, 105, 50);
                        }
                        else
                        {
                            lblRoleNotice.Text = "ℹ Standard Staff Registration: New accounts are designated as Collector. Role assignments and tax brackets are configured by SysAdmin / Supervisor.";
                            lblRoleNotice.BackColor = Color.FromArgb(236, 244, 248);
                            lblRoleNotice.ForeColor = Color.FromArgb(35, 75, 95);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error checking account count: " + ex.Message);
            }
        }

        // =========================================================
        // TOGGLE SHOW PASSWORDS
        // =========================================================

        private void chkShowPasswords_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPasswords.Checked;
            txtConfirmPassword.UseSystemPasswordChar = !chkShowPasswords.Checked;
        }

        // =========================================================
        // RE-START / ENROLL BUTTON
        // =========================================================

        private void btnScanEnroll_Click(object sender, EventArgs e)
        {
            RegisterCount = 0;
            cbRegTmp = 0;
            enrollmentActive = true;
            registeredFingerprintTemplate = null;
            progressBarFP.Value = 0;

            if (picFPImg.Image != null)
            {
                picFPImg.Image.Dispose();
                picFPImg.Image = null;
            }
            picFPImg.Invalidate();

            lblFPTemplate.Text = "Template: [Enrolling 0/3]";
            lblFPTemplate.ForeColor = Color.FromArgb(100, 120, 130);
            lblFPStatus.Text = "Place your desired finger firmly on the sensor (1 of 3).";
            lblFPStatus.ForeColor = Color.FromArgb(46, 117, 107);
        }

        // =========================================================
        // CAPTURE CALLBACK
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
            }
        }

        // =========================================================
        // PROCESS FINGERPRINT ENROLLMENT
        // =========================================================

        private void ProcessFingerprint(FingerprintCapture capture)
        {
            if (IsDisposed) return;

            // Display captured image
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

            // -----------------------------------------------------
            // Check duplicate fingerprint across existing staff
            // -----------------------------------------------------
            if (RegisterCount == 0)
            {
                int existingEmployeeId = 0;
                int existingScore = 0;

                int identifyRet = fingerprintManager.Identify(
                    capture.Template,
                    ref existingEmployeeId,
                    ref existingScore
                );

                if (identifyRet == zkfperrdef.ZKFP_ERR_OK && existingEmployeeId > 0)
                {
                    lblFPStatus.Text = "Fingerprint is already registered to Employee ID: " + existingEmployeeId + ". Please use a different finger.";
                    lblFPStatus.ForeColor = Color.FromArgb(180, 45, 35);
                    enrollmentActive = false;
                    return;
                }
            }

            // -----------------------------------------------------
            // Begin enrollment cycle
            // -----------------------------------------------------
            if (!enrollmentActive)
            {
                enrollmentActive = true;
                RegisterCount = 0;
                cbRegTmp = 0;
            }

            // Compare against previous scan in this session
            if (RegisterCount > 0)
            {
                int matchScore = fingerprintManager.Match(
                    capture.Template,
                    RegTmps[RegisterCount - 1]
                );

                if (matchScore <= 0)
                {
                    lblFPStatus.Text = "Different finger detected (Score: " + matchScore + "). Please press the SAME finger again.";
                    lblFPStatus.ForeColor = Color.FromArgb(180, 45, 35);
                    return;
                }
            }

            // Store capture in buffer
            Array.Copy(
                capture.Template,
                RegTmps[RegisterCount],
                capture.TemplateSize
            );

            RegisterCount++;
            progressBarFP.Value = RegisterCount;

            // 3 captures reached: merge them
            if (RegisterCount >= REGISTER_FINGER_COUNT)
            {
                int ret = fingerprintManager.Merge(
                    RegTmps[0],
                    RegTmps[1],
                    RegTmps[2],
                    RegTmp,
                    ref cbRegTmp
                );

                if (ret == zkfperrdef.ZKFP_ERR_OK && cbRegTmp > 0)
                {
                    registeredFingerprintTemplate = new byte[cbRegTmp];
                    Array.Copy(RegTmp, registeredFingerprintTemplate, cbRegTmp);

                    lblFPTemplate.Text = "Template: [3/3 Captured - Merged OK]";
                    lblFPTemplate.ForeColor = Color.FromArgb(25, 115, 60);

                    lblFPStatus.Text = "Biometric enrolled successfully! Click 'Complete Registration'.";
                    lblFPStatus.ForeColor = Color.FromArgb(25, 115, 60);

                    enrollmentActive = false;
                    return;
                }

                // Merge failed
                lblFPStatus.Text = "Fingerprint merge failed (Error: " + ret + "). Please click 'Scan / Enroll' to try again.";
                lblFPStatus.ForeColor = Color.FromArgb(180, 45, 35);

                enrollmentActive = false;
                RegisterCount = 0;
                cbRegTmp = 0;
                registeredFingerprintTemplate = null;
                progressBarFP.Value = 0;
                return;
            }

            // More captures needed
            int remaining = REGISTER_FINGER_COUNT - RegisterCount;
            lblFPTemplate.Text = "Template: [Enrolling " + RegisterCount + "/3]";
            lblFPStatus.Text = RegisterCount + "/3 captured. Lift finger and place the SAME finger again (" + remaining + " remaining).";
            lblFPStatus.ForeColor = Color.FromArgb(46, 117, 107);
        }

        // =========================================================
        // COMPLETE REGISTRATION / CREATE ACCOUNT
        // =========================================================

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullNameInput = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Field validations
            if (string.IsNullOrWhiteSpace(fullNameInput))
            {
                MessageBox.Show(
                    "Please enter the Officer's Full Name and Official Title.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show(
                    "Please enter a desired username.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtUsername.Focus();
                return;
            }

            if (password.Length < 4)
            {
                MessageBox.Show(
                    "Password must be at least 4 characters long.",
                    "Weak Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show(
                    "Password and Confirm Password do not match. Please re-enter.",
                    "Password Mismatch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtConfirmPassword.Focus();
                return;
            }

            if (registeredFingerprintTemplate == null || registeredFingerprintTemplate.Length == 0)
            {
                MessageBox.Show(
                    "Biometric Fingerprint Enrollment is mandatory.\nPlease complete 3 scans on the sensor before creating an account.",
                    "Biometrics Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Parse Full Name and Optional Title if separated by hyphen/parenthesis
            string fullName = fullNameInput;
            string officialTitle = "Staff Officer";

            if (fullNameInput.Contains("-"))
            {
                string[] parts = fullNameInput.Split(new char[] { '-' }, 2);
                fullName = parts[0].Trim();
                officialTitle = parts[1].Trim();
            }
            else if (fullNameInput.Contains("("))
            {
                int start = fullNameInput.IndexOf('(');
                int end = fullNameInput.IndexOf(')');
                if (end > start)
                {
                    officialTitle = fullNameInput.Substring(start + 1, end - start - 1).Trim();
                    fullName = (fullNameInput.Substring(0, start) + fullNameInput.Substring(end + 1)).Trim();
                }
            }

            // Hash password with PBKDF2
            string passwordHash = HashPassword(password);

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if this is the very first account in the system
                    string role = "COLLECTOR";
                    using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM employees;", connection))
                    {
                        long existingCount = Convert.ToInt64(countCmd.ExecuteScalar());
                        if (existingCount == 0)
                        {
                            role = "SYSADMIN";
                        }
                    }

                    string insertQuery = @"
                        INSERT INTO employees
                        (
                            full_name,
                            official_title,
                            username,
                            password_hash,
                            fingerprint_template,
                            role,
                            status
                        )
                        VALUES
                        (
                            @fullName,
                            @officialTitle,
                            @username,
                            @passwordHash,
                            @fingerprintTemplate,
                            @role,
                            'ACTIVE'
                        );
                    ";

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@fullName", fullName);
                        command.Parameters.AddWithValue("@officialTitle", officialTitle);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@passwordHash", passwordHash);
                        command.Parameters.AddWithValue("@role", role);

                        command.Parameters.Add(
                            "@fingerprintTemplate",
                            MySqlDbType.Blob
                        ).Value = registeredFingerprintTemplate;

                        command.ExecuteNonQuery();

                        long newEmployeeId = command.LastInsertedId;

                        // Add immediately to active ZKTeco in-memory identification cache
                        if (newEmployeeId > 0 && registeredFingerprintTemplate != null)
                        {
                            fingerprintManager.AddFingerprint(
                                (int)newEmployeeId,
                                registeredFingerprintTemplate
                            );
                        }

                        // Success notification
                        string roleDesc = role == "SYSADMIN" ? "System Administrator (SysAdmin)" : "Revenue Collector";
                        MessageBox.Show(
                            "Treasury Staff Account successfully registered!\n\n"
                            + "Officer: " + fullName + " (" + officialTitle + ")\n"
                            + "Username: " + username + "\n"
                            + "Assigned Role: " + roleDesc + "\n\n"
                            + "You can now log in immediately using either your registered fingerprint or password.",
                            "Account Registration Successful",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        this.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // Unique key violation
                {
                    MessageBox.Show(
                        "The username '" + username + "' is already taken. Please choose another username.",
                        "Username Unavailable",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtUsername.Focus();
                }
                else
                {
                    MessageBox.Show(
                        "Database Error:\n\n" + ex.Message,
                        "Registration Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected Error:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // CANCEL / BACK BUTTON
        // =========================================================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =========================================================
        // PASSWORD HASHING (PBKDF2)
        // =========================================================

        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }

        // =========================================================
        // GDI+ VECTOR PAINTING (Crisp sharp graphics)
        // =========================================================

        private void picHeaderIcon_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Brush brush = new SolidBrush(Color.FromArgb(235, 245, 250)))
            {
                Point[] roof = new Point[]
                {
                    new Point(25, 6),
                    new Point(5, 20),
                    new Point(45, 20)
                };
                g.FillPolygon(brush, roof);
                g.FillRectangle(brush, 4, 21, 42, 4);

                int[] pillars = { 7, 18, 29, 39 };
                foreach (int px in pillars)
                {
                    g.FillRectangle(brush, px, 27, 4, 18);
                }

                g.FillRectangle(brush, 4, 46, 42, 4);
                g.FillRectangle(brush, 2, 51, 46, 4);
            }
        }

        private void picFPImg_Paint(object sender, PaintEventArgs e)
        {
            if (picFPImg.Image != null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = picFPImg.Width / 2;
            int cy = picFPImg.Height / 2;

            using (Pen pen = new Pen(Color.FromArgb(145, 175, 190), 2f))
            using (Brush lockBrush = new SolidBrush(Color.FromArgb(90, 125, 140)))
            {
                g.DrawEllipse(pen, cx - 45, cy - 45, 90, 90);

                using (Pen shacklePen = new Pen(Color.FromArgb(90, 125, 140), 4f))
                {
                    g.DrawArc(shacklePen, cx - 14, cy - 26, 28, 28, 180, 180);
                }

                g.FillRectangle(lockBrush, cx - 18, cy - 12, 36, 28);

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