using MySqlConnector;
using System;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Dashboard : Form
    {
        private int employeeId;

        public Dashboard(int employeeId)
        {
            InitializeComponent();

            this.employeeId = employeeId;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadEmployeeInformation();
        }

        private void LoadEmployeeInformation()
        {
            string connectionString =
                "Server=localhost;Database=vaultify;User ID=root;Password=password;";

            string query = @"
                SELECT
                    employee_id,
                    full_name,
                    official_title,
                    username,
                    role,
                    created_at,
                    updated_at
                FROM employees
                WHERE employee_id = @employeeId;
            ";

            try
            {
                using (MySqlConnection connection =
                    new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command =
                        new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(
                            "@employeeId",
                            employeeId
                        );

                        using (MySqlDataReader reader =
                            command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                StringBuilder employeeInfo =
                                    new StringBuilder();

                                employeeInfo.AppendLine(
                                    "Employee ID: " +
                                    reader["employee_id"].ToString()
                                );

                                employeeInfo.AppendLine(
                                    "Full Name: " +
                                    reader["full_name"].ToString()
                                );

                                employeeInfo.AppendLine(
                                    "Title: " +
                                    (reader["official_title"] != DBNull.Value ? reader["official_title"].ToString() : "N/A")
                                );

                                employeeInfo.AppendLine(
                                    "Username: " +
                                    reader["username"].ToString()
                                );

                                employeeInfo.AppendLine(
                                    "Role: " +
                                    reader["role"].ToString()
                                );

                                employeeInfo.AppendLine(
                                    "Created At: " +
                                    reader["created_at"].ToString()
                                );

                                employeeInfo.AppendLine(
                                    "Updated At: " +
                                    reader["updated_at"].ToString()
                                );

                                label1.Text =
                                    employeeInfo.ToString();

                                label1.AutoSize = true;
                            }
                            else
                            {
                                label1.Text =
                                    "Employee information could not be found.";
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    "A database error occurred:\n\n"
                    + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}