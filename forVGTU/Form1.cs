using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace forVGTU
{
    public partial class Form1 : Form
    {
        Database dataBase = new Database();
        private bool closed = false;
        private NpgsqlConnection conn; 
        /*ApplicationForm applicationForm = new ApplicationForm();
          applicationForm.Show();
          this.Hide();*/
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (closed)
            {
                return;
            }
            else if (CheckLogin(login, password))
            {
                ShowUserRoleForm(login);
            }
        }

        string connectingString = "Server = localhost; Port = 5432; Database = ForVgtu; User Id = postgres; Password = assaq123";

        private bool CheckLogin(string login, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectingString))
            {
                string query = "SELECT COUNT(*) FROM users WHERE login = @login AND password = @password";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("login", login);
                    command.Parameters.AddWithValue("password", password);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void ShowUserRoleForm(string login)
        {
            string role = GetUserRole(login);

            if (role == "РаботникПК")
            {
                WorkForm workForm = new WorkForm();
                workForm.Show();
                this.Hide();
            }
            else if (role == "Абитуриент")
            {
                ApplicationForm applicationForm = new ApplicationForm();
                applicationForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("ошибка: неизвестная роль пользователя", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Hide();
        }

        private string GetUserRole(string login)
        {
            string role = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectingString))
            {
                string query = "SELECT role FROM users WHERE login = @login";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            role = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("не удалось получить роль пользователя", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"ошибка при получении роли пользователя: {ex.Message}", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return role;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }
    }
}
