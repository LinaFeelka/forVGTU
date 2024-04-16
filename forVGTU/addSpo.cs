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
    public partial class addSpo : Form
    {
        Database database = new Database();
        public addSpo()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            spoForm spoForm = new spoForm();
            spoForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var direction_name = textBox2.Text;
            database.OpenConnection();

            string query = $"insert into spo (direction_name) values ('{direction_name}')";
            NpgsqlCommand comm = new NpgsqlCommand(query, database.GetConnection());
            comm.ExecuteNonQuery();

            database.CloseConnection();

            spoForm spoForm = new spoForm();
            spoForm.Show();
            this.Close();
        }
    }
}
