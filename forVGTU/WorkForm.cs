using Npgsql;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace forVGTU
{
    public partial class WorkForm : Form
    {
        Database database = new Database();
        public WorkForm()
        {
            InitializeComponent();
        }

        private void WorkForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "ID");                                      //0
            dataGridView1.Columns.Add("education_level", "уровень образования");        //1
            dataGridView1.Columns.Add("fio", "ФИО");                                    //2
            dataGridView1.Columns.Add("passport", "Паспорт");                           //3
            dataGridView1.Columns.Add("snils", "СНИЛС");                                //4
            dataGridView1.Columns.Add("email", "почтa");                                //5
            dataGridView1.Columns.Add("phone", "Телефон");                              //6
            dataGridView1.Columns.Add("fio_parents", "ФИО представителя");              //7
            dataGridView1.Columns.Add("institution", "Учебное заведение");              //8
            dataGridView1.Columns.Add("direction", "направлениe");                      //9
            dataGridView1.Columns.Add("status", "Статус");                              //10
            dataGridView1.Columns.Add("comment", "Комментарий");                        //11
            dataGridView1.Columns.Add("executor", "Исполнитель");                       //12
            dataGridView1.Columns.Add("ege_math", "ЕГЭ математика");                    //13 int
            dataGridView1.Columns.Add("ege_physic", "ЕГЭ физика");                      //14 int
            dataGridView1.Columns.Add("ege_chemistry", "ЕГЭ химия");                    //15 int
            dataGridView1.Columns.Add("ege_history", "ЕГЭ история");                    //16 int
            dataGridView1.Columns.Add("ege_sociality", "ЕГЭ  обществознание");          //17 int
            dataGridView1.Columns.Add("ege_inform", "ЕГЭ  информатика");                //18 int
            dataGridView1.Columns.Add("ege_biology", "ЕГЭ  биология");                  //19 int
            dataGridView1.Columns.Add("ege_geography", "ЕГЭ география");                //20 int
            dataGridView1.Columns.Add("ege_language", "ЕГЭ языки");                     //21 int
            dataGridView1.Columns.Add("ege_literature", "ЕГЭ литература");              //22 int
            dataGridView1.Columns.Add("avarage_score_diplom", "Средний балл(Диплом)");  //23 int
            dataGridView1.Columns.Add("avarage_score", "Средний балл");                 //24 int
            dataGridView1.Columns.Add("date", "Дата подачи");                           //25 date
            dataGridView1.Columns.Add("isNew", string.Empty);                           //26
            dataGridView1.Columns["isNew"].Visible = false;
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt64(0), record.GetString(1), record.GetString(2), record.GetInt64(3),
                record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7),
                record.GetString(8), record.GetString(9), record.GetString(10), record.GetString(11),
                record.GetString(12), record.GetInt32(13), record.GetInt32(14), record.GetInt32(15),
                record.GetInt32(16), record.GetInt32(17), record.GetInt32(18), record.GetInt32(19),
                record.GetInt32(20), record.GetInt32(21), record.GetInt32(22), record.GetInt32(23),
                record.GetInt32(24), record.GetDateTime(25), RowState.ModifiedNew);
        }
        private void RefreshDataGrid(DataGridView dgw) 
        { 
            dgw.Rows.Clear();

            database.OpenConnection();

            string query = $"select * from application";
            NpgsqlCommand comm = new NpgsqlCommand(query, database.GetConnection());
            IDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();

            database.CloseConnection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            spoForm spoForm = new spoForm();
            spoForm.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baccalaureateForm baccalaureateForm = new baccalaureateForm();
            baccalaureateForm.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            magistracyForm magistracyForm = new magistracyForm();
            magistracyForm.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            specialtyForm specialtyForm = new specialtyForm();  
            specialtyForm.Show();
            this.Close();
        }

        int selectedRow = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (selectedRow >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox3.Text = row.Cells[0].Value.ToString();
                comboBox1.Text = row.Cells[10].Value.ToString();
                textBox1.Text = row.Cells[12].Value.ToString();
                textBox2.Text = row.Cells[11].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveAppl();
            RefreshDataGrid(dataGridView1);
        }

        private void Edit ()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = textBox3.Text;
            var executor = textBox1.Text;
            var status = comboBox1.Text;
            var comment = textBox2.Text;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(id, status, comment, executor);
                dataGridView1.Rows[selectedRowIndex].Cells[26].Value = RowState.Modified;
            }
        }
        private void SaveAppl()
        {
            var index = dataGridView1.CurrentCell.RowIndex;

            database.OpenConnection();
                var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                var executor = textBox1.Text;
                var status = comboBox1.Text;
                var comment = textBox2.Text;

                var changeQuery = $"update application set executor = '{executor}', status = '{status}', comment = '{comment}' where id = '{id}'";

                var comm = new NpgsqlCommand(changeQuery, database.GetConnection());
                comm.ExecuteNonQuery();
                database.CloseConnection();
            
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
