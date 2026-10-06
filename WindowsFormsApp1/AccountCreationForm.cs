using libzkfpcsharp;
using MySqlConnector;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AccountCreationForm : Form
    {
        // =========================================================
        // DATABASE
        // =========================================================

        private const string connectionString =
            "Server=localhost;Database=vaultify;User ID=root;Password=password;";


        // =========================================================
        // LOGIN FORM
        // =========================================================

        private LoginForm loginForm;


        // =========================================================
        // FINGERPRINT MANAGER
        // =========================================================

        private FingerprintManager fingerprintManager =
            FingerprintManager.Instance;


        // =========================================================
        // ENROLLMENT
        // =========================================================

        private const int REGISTER_FINGER_COUNT = 3;


        private byte[][] RegTmps =
            new byte[REGISTER_FINGER_COUNT][];


        private byte[] RegTmp =
            new byte[2048];


        private int cbRegTmp = 0;

        private int RegisterCount = 0;

        private bool enrollmentActive = false;

        private byte[] registeredFingerprintTemplate = null;


        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public AccountCreationForm(
            LoginForm loginForm)
        {
            InitializeComponent();

            this.loginForm =
                loginForm;
        }


        // =========================================================
        // FORM LOAD
        // =========================================================

        private void AccountCreationForm_Load(
            object sender,
            EventArgs e)
        {
            // -----------------------------------------------------
            // Prepare the three fingerprint buffers.
            // -----------------------------------------------------

            for (int i = 0;
                 i < REGISTER_FINGER_COUNT;
                 i++)
            {
                RegTmps[i] =
                    new byte[2048];
            }


            RegisterCount = 0;

            cbRegTmp = 0;

            enrollmentActive = false;

            registeredFingerprintTemplate = null;


            // -----------------------------------------------------
            // Scanner was already initialized by LoginForm.
            // -----------------------------------------------------

            if (!fingerprintManager.IsConnected)
            {
                statusLabel.Text =
                    "Fingerprint scanner not connected.";

                return;
            }


            // -----------------------------------------------------
            // AccountCreationForm becomes the active consumer.
            // -----------------------------------------------------

            fingerprintManager.StartCapture(
                OnFingerprintCaptured
            );


            statusLabel.Text =
                "Scanner ready. Fingerprint is optional.";
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
            // Prevent registering a fingerprint already in use
            // -----------------------------------------------------

            if (RegisterCount == 0)
            {
                int existingEmployeeId = 0;
                int existingScore = 0;

                int identifyRet =
                    fingerprintManager.Identify(
                        capture.Template,
                        ref existingEmployeeId,
                        ref existingScore
                    );

                if (identifyRet == zkfperrdef.ZKFP_ERR_OK)
                {
                    statusLabel.Text =
                        "Fingerprint is already registered to employee ID: "
                        + existingEmployeeId
                        + ". Please use a different finger.";

                    enrollmentActive = false;

                    return;
                }
            }


            // -----------------------------------------------------
            // Automatically begin enrollment
            // -----------------------------------------------------

            if (!enrollmentActive)
            {
                enrollmentActive = true;

                RegisterCount = 0;

                cbRegTmp = 0;

                statusLabel.Text =
                    "1/3 - Fingerprint captured. "
                    + "Remove your finger and place the same finger again.";
            }


            // -----------------------------------------------------
            // Compare against previous capture
            // -----------------------------------------------------

            if (RegisterCount > 0)
            {
                int matchScore =
                    fingerprintManager.Match(
                        capture.Template,
                        RegTmps[
                            RegisterCount - 1
                        ]
                    );


                if (matchScore <= 0)
                {
                    statusLabel.Text =
                        "Different fingerprint detected. "
                        + "Match score: "
                        + matchScore
                        + ". Please use the same finger.";

                    return;
                }


                statusLabel.Text =
                    "Match score: "
                    + matchScore;
            }


            // -----------------------------------------------------
            // Store capture
            // -----------------------------------------------------

            Array.Copy(
                capture.Template,
                RegTmps[RegisterCount],
                capture.TemplateSize
            );


            RegisterCount++;


            // -----------------------------------------------------
            // Three captures
            // -----------------------------------------------------

            if (RegisterCount >=
                REGISTER_FINGER_COUNT)
            {
                int ret =
                    fingerprintManager.Merge(
                        RegTmps[0],
                        RegTmps[1],
                        RegTmps[2],
                        RegTmp,
                        ref cbRegTmp
                    );


                if (ret ==
                    zkfperrdef.ZKFP_ERR_OK)
                {
                    registeredFingerprintTemplate =
                        new byte[cbRegTmp];


                    Array.Copy(
                        RegTmp,
                        registeredFingerprintTemplate,
                        cbRegTmp
                    );


                    statusLabel.Text =
                        "Fingerprint registered successfully! "
                        + "You may now click Register.";


                    enrollmentActive = false;

                    return;
                }


                statusLabel.Text =
                    "Fingerprint merge failed. Error: "
                    + ret;


                enrollmentActive = false;

                RegisterCount = 0;

                cbRegTmp = 0;

                registeredFingerprintTemplate =
                    null;

                return;
            }


            // -----------------------------------------------------
            // More captures required
            // -----------------------------------------------------

            int remaining =
                REGISTER_FINGER_COUNT -
                RegisterCount;


            statusLabel.Text =
                RegisterCount
                + "/3 captured. "
                + "Place the same finger again. "
                + remaining
                + " capture(s) remaining.";
        }


        // =========================================================
        // REGISTER
        // =========================================================

        private void Register_Click(
            object sender,
            EventArgs e)
        {
            string firstName =
                Firstname.Text.Trim();


            string lastName =
                Lastname.Text.Trim();


            string username =
                Username.Text.Trim();


            string password =
                Password.Text;


            // -----------------------------------------------------
            // Required fields
            // -----------------------------------------------------

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Please fill in all fields.",
                    "Registration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }


            // -----------------------------------------------------
            // Password
            // -----------------------------------------------------

            string passwordHash =
                HashPassword(password);


            try
            {
                using (MySqlConnection connection =
                    new MySqlConnection(
                        connectionString))
                {
                    connection.Open();


                    string query = @"
                        INSERT INTO employees
                        (
                            first_name,
                            last_name,
                            username,
                            password_hash,
                            fingerprint_template
                        )
                        VALUES
                        (
                            @firstName,
                            @lastName,
                            @username,
                            @passwordHash,
                            @fingerprintTemplate
                        );
                    ";


                    using (MySqlCommand command =
                        new MySqlCommand(
                            query,
                            connection))
                    {
                        command.Parameters.AddWithValue(
                            "@firstName",
                            firstName
                        );


                        command.Parameters.AddWithValue(
                            "@lastName",
                            lastName
                        );


                        command.Parameters.AddWithValue(
                            "@username",
                            username
                        );


                        command.Parameters.AddWithValue(
                            "@passwordHash",
                            passwordHash
                        );


                        // -------------------------------------------------
                        // Fingerprint is optional.
                        // -------------------------------------------------

                        if (registeredFingerprintTemplate != null &&
                            registeredFingerprintTemplate.Length > 0)
                        {
                            command.Parameters.Add(
                                "@fingerprintTemplate",
                                MySqlDbType.Blob
                            ).Value =
                                registeredFingerprintTemplate;
                        }
                        else
                        {
                            command.Parameters.Add(
                                "@fingerprintTemplate",
                                MySqlDbType.Blob
                            ).Value =
                                DBNull.Value;
                        }


                        command.ExecuteNonQuery();

                        long newEmployeeId = command.LastInsertedId;

                        // -------------------------------------------------
                        // Add newly registered fingerprint to in-memory
                        // algorithm cache so it is immediately recognized.
                        // -------------------------------------------------
                        if (registeredFingerprintTemplate != null &&
                            registeredFingerprintTemplate.Length > 0 &&
                            newEmployeeId > 0)
                        {
                            fingerprintManager.AddFingerprint(
                                (int)newEmployeeId,
                                registeredFingerprintTemplate
                            );
                        }
                    }
                }


                MessageBox.Show(
                    "Employee account created successfully!",
                    "Registration Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );


                // -----------------------------------------------------
                // Reset
                // -----------------------------------------------------

                Firstname.Clear();

                Lastname.Clear();

                Username.Clear();

                Password.Clear();

                if (picFPImg.Image != null)
                {
                    picFPImg.Image.Dispose();
                    picFPImg.Image = null;
                }

                registeredFingerprintTemplate =
                    null;


                RegisterCount = 0;

                cbRegTmp = 0;

                enrollmentActive = false;


                statusLabel.Text =
                    "Scanner ready. Fingerprint is optional.";
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show(
                        "That username is already taken.",
                        "Registration Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    MessageBox.Show(
                        "A database error occurred:\n\n"
                        + ex.Message,
                        "Database Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An unexpected error occurred:\n\n"
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================================================
        // PASSWORD HASH
        // =========================================================

        private string HashPassword(
            string password)
        {
            byte[] salt =
                new byte[16];


            using (var rng =
                RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }


            byte[] hash;


            using (var pbkdf2 =
                new Rfc2898DeriveBytes(
                    password,
                    salt,
                    100000,
                    HashAlgorithmName.SHA256))
            {
                hash =
                    pbkdf2.GetBytes(32);
            }


            return
                Convert.ToBase64String(salt)
                + ":"
                + Convert.ToBase64String(hash);
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
        // FORM CLOSING
        // =========================================================

        protected override void OnFormClosing(
            FormClosingEventArgs e)
        {
            // -----------------------------------------------------
            // Do NOT shut down the SDK here.
            //
            // LoginForm owns the application-level scanner.
            // -----------------------------------------------------

            fingerprintManager.StopCapture();


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

        private void label1_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label2_Click(
            object sender,
            EventArgs e)
        {
        }

        private void textBox2_TextChanged(
            object sender,
            EventArgs e)
        {
        }

        private void label6_Click(
            object sender,
            EventArgs e)
        {
            Application.Exit();
        }

        private void statusLabel_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label8_Click(
            object sender,
            EventArgs e)
        {
            this.Close();
        }
    }
}