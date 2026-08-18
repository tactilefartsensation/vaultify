using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class AccountCreationForm : Form
    {
        public AccountCreationForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            LoginForm accountForm = new LoginForm();

            accountForm.FormClosed += (s, args) => this.Show();

            this.Hide();
            accountForm.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void Register_Click(object sender, EventArgs e)
        {
            string firstName = Firstname.Text.Trim();
            string lastName = Lastname.Text.Trim();
            string username = Username.Text.Trim();
            string password = Password.Text;

            // Check for empty fields
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

            // Hash the password
            string passwordHash = HashPassword(password);

            // MySQL connection
            string connectionString =
                "Server=localhost;Database=vaultify;User ID=root;Password=password;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                INSERT INTO employees
                (
                    first_name,
                    last_name,
                    username,
                    password_hash
                )
                VALUES
                (
                    @firstName,
                    @lastName,
                    @username,
                    @passwordHash
                );";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", firstName);
                        command.Parameters.AddWithValue("@lastName", lastName);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@passwordHash", passwordHash);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Employee account created successfully!",
                    "Registration Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Clear the textboxes
                Firstname.Clear();
                Lastname.Clear();
                Username.Clear();
                Password.Clear();
            }
            catch (MySqlException ex)
            {
                // MySQL error 1062 = duplicate value for a UNIQUE column
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
                        "A database error occurred:\n\n" + ex.Message,
                        "Database Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An unexpected error occurred:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private string HashPassword(string password)
        {
            // Generate a 16-byte salt (compatible with .NET Framework 4.8)
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a 32-byte subkey using PBKDF2 with SHA-256
            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(32);
            }

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

    }


}
