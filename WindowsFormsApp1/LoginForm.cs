using libzkfpcsharp;
using MySqlConnector;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
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


        public LoginForm()
        {
            InitializeComponent();
        }


        // =========================================================
        // FORM LOAD
        // =========================================================

        private async void LoginForm_Load(
            object sender,
            EventArgs e)
        {
            statusLabel.Text =
                "Starting fingerprint scanner...";


            // -----------------------------------------------------
            // Initialize scanner in background.
            //
            // This prevents the UI from freezing while the SDK
            // initializes.
            // -----------------------------------------------------

            string error = null;


            bool success =
                await Task.Run(
                    () =>
                    {
                        return fingerprintManager.Initialize(
                            out error
                        );
                    }
                );


            if (!success)
            {
                statusLabel.Text =
                    error;

                return;
            }


            // -----------------------------------------------------
            // Load all registered fingerprints from MySQL.
            // -----------------------------------------------------

            LoadFingerprintDatabase();


            // -----------------------------------------------------
            // Start receiving fingerprint captures.
            // -----------------------------------------------------

            fingerprintManager.StartCapture(
                OnFingerprintCaptured
            );


            statusLabel.Text =
                "Fingerprint scanner ready.";
        }


        // =========================================================
        // LOAD FINGERPRINTS FROM MYSQL
        // =========================================================

        private void LoadFingerprintDatabase()
        {
            try
            {
                using (MySqlConnection connection =
                    new MySqlConnection(
                        connectionString))
                {
                    connection.Open();


                    string query = @"
                        SELECT employee_id, fingerprint_template
                        FROM employees
                        WHERE fingerprint_template IS NOT NULL;
                    ";


                    using (MySqlCommand command =
                        new MySqlCommand(
                            query,
                            connection))
                    {
                        using (MySqlDataReader reader =
                            command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int employeeId =
                                    reader.GetInt32(
                                        "employee_id"
                                    );


                                byte[] template =
                                    (byte[])reader[
                                        "fingerprint_template"
                                    ];


                                int ret =
                                    fingerprintManager.AddFingerprint(
                                        employeeId,
                                        template
                                    );


                                if (ret !=
                                    zkfperrdef.ZKFP_ERR_OK)
                                {
                                    System.Diagnostics.Debug.WriteLine(
                                        "Could not load fingerprint for employee "
                                        + employeeId
                                        + ". Error: "
                                        + ret
                                    );
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load fingerprint database:\n\n"
                    + ex.Message,
                    "Fingerprint Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================================================
        // FINGERPRINT CAPTURE
        // =========================================================

        private void OnFingerprintCaptured(
            FingerprintCapture capture)
        {
            if (IsDisposed ||
                !IsHandleCreated)
            {
                return;
            }


            try
            {
                BeginInvoke(
                    new Action(
                        () =>
                        {
                            ProcessFingerprint(
                                capture
                            );
                        }
                    )
                );
            }
            catch
            {
                // Form is closing.
            }
        }


        // =========================================================
        // PROCESS FINGERPRINT
        // =========================================================

        private void ProcessFingerprint(
            FingerprintCapture capture)
        {
            if (IsDisposed)
                return;


            // -----------------------------------------------------
            // Show fingerprint image
            // -----------------------------------------------------

            try
            {
                Bitmap bitmap =
                    CreateFingerprintBitmap(
                        capture.Image,
                        fingerprintManager.ImageWidth,
                        fingerprintManager.ImageHeight
                    );


                if (picFPImg.Image != null)
                {
                    picFPImg.Image.Dispose();
                }


                picFPImg.Image =
                    bitmap;
            }
            catch
            {
            }


            // -----------------------------------------------------
            // Identify employee
            // -----------------------------------------------------

            int employeeId = 0;
            int score = 0;


            int ret =
                fingerprintManager.Identify(
                    capture.Template,
                    ref employeeId,
                    ref score
                );


            if (ret ==
                zkfperrdef.ZKFP_ERR_OK)
            {
                statusLabel.Text =
                    "Fingerprint matched. Score: "
                    + score;


                LoginWithFingerprint(
                    employeeId
                );
            }
            else
            {
                statusLabel.Text =
                    "Fingerprint not recognized. Score: "
                    + score;
            }
        }


        // =========================================================
        // FINGERPRINT LOGIN
        // =========================================================

        private void LoginWithFingerprint(
            int employeeId)
        {
            try
            {
                using (MySqlConnection connection =
                    new MySqlConnection(
                        connectionString))
                {
                    connection.Open();


                    string query = @"
                        SELECT employee_id
                        FROM employees
                        WHERE employee_id = @employeeId;
                    ";


                    using (MySqlCommand command =
                        new MySqlCommand(
                            query,
                            connection))
                    {
                        command.Parameters.AddWithValue(
                            "@employeeId",
                            employeeId
                        );


                        object result =
                            command.ExecuteScalar();


                        if (result != null)
                        {
                            OpenDashboard(
                                employeeId
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Fingerprint login failed:\n\n"
                    + ex.Message,
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================================================
        // PASSWORD LOGIN
        // =========================================================

        private void Login_Click_1(
            object sender,
            EventArgs e)
        {
            string username =
                Username.Text.Trim();


            string password =
                Password.Text;


            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Please enter your username and password.",
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }


            try
            {
                using (MySqlConnection connection =
                    new MySqlConnection(
                        connectionString))
                {
                    connection.Open();


                    string query = @"
                        SELECT employee_id, password_hash
                        FROM employees
                        WHERE username = @username;
                    ";


                    using (MySqlCommand command =
                        new MySqlCommand(
                            query,
                            connection))
                    {
                        command.Parameters.AddWithValue(
                            "@username",
                            username
                        );


                        using (MySqlDataReader reader =
                            command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "Invalid username or password.",
                                    "Login Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );

                                return;
                            }


                            int employeeId =
                                reader.GetInt32(
                                    "employee_id"
                                );


                            string storedHash =
                                reader.GetString(
                                    "password_hash"
                                );


                            if (!VerifyPassword(
                                password,
                                storedHash))
                            {
                                MessageBox.Show(
                                    "Invalid username or password.",
                                    "Login Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );

                                return;
                            }


                            OpenDashboard(
                                employeeId
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Login failed:\n\n"
                    + ex.Message,
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================================================
        // PASSWORD VERIFICATION
        // =========================================================

        private bool VerifyPassword(
            string password,
            string storedPassword)
        {
            try
            {
                string[] parts =
                    storedPassword.Split(':');


                if (parts.Length != 2)
                    return false;


                byte[] salt =
                    Convert.FromBase64String(
                        parts[0]
                    );


                byte[] storedHash =
                    Convert.FromBase64String(
                        parts[1]
                    );


                using (var pbkdf2 =
                    new Rfc2898DeriveBytes(
                        password,
                        salt,
                        100000,
                        HashAlgorithmName.SHA256))
                {
                    byte[] computedHash =
                        pbkdf2.GetBytes(32);


                    if (computedHash.Length !=
                        storedHash.Length)
                    {
                        return false;
                    }


                    bool result = true;


                    for (int i = 0;
                         i < computedHash.Length;
                         i++)
                    {
                        if (computedHash[i] !=
                            storedHash[i])
                        {
                            result = false;
                        }
                    }


                    return result;
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

        private void OpenDashboard(
            int employeeId)
        {
            // Stop delivering fingerprint captures.
            fingerprintManager.StopCapture();


            Dashboard dashboard =
                new Dashboard(
                    employeeId
                );


            dashboard.FormClosed +=
                (s, args) =>
                {
                    this.Show();


                    fingerprintManager.StartCapture(
                        OnFingerprintCaptured
                    );


                    statusLabel.Text =
                        "Fingerprint scanner ready.";
                };


            this.Hide();

            dashboard.Show();
        }


        // =========================================================
        // ACCOUNT CREATION
        // =========================================================

        private void linkLabel1_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            // -----------------------------------------------------
            // VERY IMPORTANT:
            //
            // LoginForm no longer receives fingerprints.
            // We do NOT terminate the scanner.
            // -----------------------------------------------------

            fingerprintManager.StopCapture();


            AccountCreationForm accountForm =
                new AccountCreationForm(
                    this
                );


            accountForm.FormClosed +=
                (s, args) =>
                {
                    this.Show();


                    fingerprintManager.StartCapture(
                        OnFingerprintCaptured
                    );


                    statusLabel.Text =
                        "Fingerprint scanner ready.";
                };


            this.Hide();

            accountForm.Show();
        }


        // =========================================================
        // BITMAP
        // =========================================================

        private Bitmap CreateFingerprintBitmap(
            byte[] buffer,
            int width,
            int height)
        {
            Bitmap bitmap =
                new Bitmap(
                    width,
                    height,
                    PixelFormat.Format8bppIndexed
                );


            ColorPalette palette =
                bitmap.Palette;


            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] =
                    Color.FromArgb(
                        i,
                        i,
                        i
                    );
            }


            bitmap.Palette =
                palette;


            BitmapData data =
                bitmap.LockBits(
                    new Rectangle(
                        0,
                        0,
                        width,
                        height
                    ),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format8bppIndexed
                );


            try
            {
                for (int y = 0;
                     y < height;
                     y++)
                {
                    Marshal.Copy(
                        buffer,
                        y * width,
                        IntPtr.Add(
                            data.Scan0,
                            y * data.Stride
                        ),
                        width
                    );
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }


            return bitmap;
        }


        // =========================================================
        // CLEANUP
        // =========================================================

        protected override void OnFormClosing(
            FormClosingEventArgs e)
        {
            fingerprintManager.Shutdown();


            if (picFPImg.Image != null)
            {
                picFPImg.Image.Dispose();

                picFPImg.Image = null;
            }


            base.OnFormClosing(e);
        }


        // =========================================================
        // EXISTING EVENTS
        // =========================================================

        private void textBox1_TextChanged(
            object sender,
            EventArgs e)
        {
        }

        private void label1_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label4_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label3_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label5_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label6_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label7_Click(
            object sender,
            EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(
            object sender,
            EventArgs e)
        {
        }

        private void statusLabel_Click(
            object sender,
            EventArgs e)
        {
        }

        
    }
}