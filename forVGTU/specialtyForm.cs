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
    public partial class specialtyForm : Form
    {
        Database database = new Database();
        public specialtyForm()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "ID");                                    //0
            dataGridView1.Columns.Add("direction_name", "Наименование");              //1
            dataGridView1.Columns.Add("isNew", string.Empty);                         //2
            dataGridView1.Columns["isNew"].Visible = false;
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), RowState.ModifiedNew);
        }
        private void RefreshDataGrid(DataGridView dgw)
        {
            database.OpenConnection();

            string query = $"select * from specialty";
            NpgsqlCommand comm = new NpgsqlCommand(query, database.GetConnection());

            IDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();

            database.CloseConnection();
        }

        int selectedRow;
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
            }
        }
        private void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Edit()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = textBox1.Text;
            var direction_name = textBox2.Text;


            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(id, direction_name);
                dataGridView1.Rows[selectedRowIndex].Cells[2].Value = RowState.Modified;
            }

        }
        private void DeleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[2].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[2].Value = RowState.Deleted;
        }

        private void Update()
        {
            database.OpenConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[2].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var deleteQuery = $"delete from specialty where id = {id}";

                    var comm = new NpgsqlCommand(deleteQuery, database.GetConnection());
                    comm.ExecuteNonQuery();

                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var direction_name = dataGridView1.Rows[index].Cells[1].Value.ToString();

                    var changeQuery = $"update specialty set direction_name = '{direction_name}' where id = '{id}'";

                    var comm = new NpgsqlCommand(changeQuery, database.GetConnection());
                    comm.ExecuteNonQuery();
                }
            }
            database.CloseConnection();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            addSpecialty addSpo = new addSpecialty();
            addSpo.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Edit();
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteRow();
            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            WorkForm workForm = new WorkForm();
            workForm.Show();
            this.Close();
        }

        private void magistracyForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void specialtyForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
    }
}
