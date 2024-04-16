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

namespace forVGTU
{
    public partial class Registration : Form
    {
        Database database = new Database();
        public Registration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.OpenConnection();

            var login = textBox1.Text;
            var password = textBox2.Text;

            string query = $"insert into users values ('{login}', '{password}', 'Абитуриент')";
            NpgsqlCommand comm = new NpgsqlCommand(query, database.GetConnection());
            comm.ExecuteNonQuery();

            MessageBox.Show("Регистрация прошла успешно");
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();

            database.CloseConnection();
        }
    }
}
