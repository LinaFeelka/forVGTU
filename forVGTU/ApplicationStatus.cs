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
    public partial class ApplicationStatus : Form
    {
        Database database = new Database();
        public ApplicationStatus()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicationForm frm = new ApplicationForm();
            frm.Show();
            this.Close();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("fio", "ФИО");                                    //0
            dataGridView1.Columns.Add("direction", "направлениe");                      //1
            dataGridView1.Columns.Add("status", "Статус");                              //2
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2));
        }
        private void RefreshDataGrid(DataGridView dgw)
        {
            database.OpenConnection();

            string query = $"select fio, direction, status from application";
            NpgsqlCommand comm = new NpgsqlCommand(query, database.GetConnection());

            IDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();

            database.CloseConnection();
        }

        private void ApplicationStatus_Load(object sender, EventArgs e)
        {
            button2.Visible = false;
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            database.OpenConnection();

            string query = $"select fio, direction, status from application where concat (fio) like '%" + textBox1.Text + "%'";
            NpgsqlCommand comm = new NpgsqlCommand(query, database.GetConnection());
            comm.ExecuteNonQuery();

            IDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();

            database.CloseConnection();
        }
        private int clickedRowIndex = -1;
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Сохраняем индекс строки, на которую нажал пользователь правой кнопкой мыши
                clickedRowIndex = e.RowIndex;

                // Показываем panel1
                button2.Visible = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (clickedRowIndex >= 0 && clickedRowIndex < dataGridView1.Rows.Count)
            {
                string fio = dataGridView1.Rows[clickedRowIndex].Cells[0].Value.ToString();
                string direction = dataGridView1.Rows[clickedRowIndex].Cells[1].Value.ToString();


                NpgsqlConnection npgsqlConnection = new NpgsqlConnection("Server = localhost; port = 5432; Database = application; User Id = postgres; Password = assaq123");

                PrintApp formOrdersAdmin = new PrintApp(fio, direction, npgsqlConnection);
                formOrdersAdmin.Show();
            }
        }
    }
}
