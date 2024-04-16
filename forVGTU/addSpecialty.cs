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
    public partial class addSpecialty : Form
    {
        Database database = new Database();
        public addSpecialty()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            specialtyForm magistracyForm = new specialtyForm();
            magistracyForm.Show();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var direction_name = textBox2.Text;
            database.OpenConnection();

            string query = $"insert into specialty (direction_name) values ('{direction_name}')";
            NpgsqlCommand comm = new NpgsqlCommand(query, database.GetConnection());
            comm.ExecuteNonQuery();

            database.CloseConnection();

            specialtyForm ms = new specialtyForm();
            ms.Show();
            this.Close();
        }
    }
}
