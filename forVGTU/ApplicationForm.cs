using Npgsql;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace forVGTU
{
    enum RowState
    {
        Existed,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class ApplicationForm : Form
    {
       Database database = new Database();
        public ApplicationForm()
        {
            InitializeComponent();
        }

        private void ApplicationForm_Load(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)// СПО
            {
                panelSPO.Visible = true;
                panelBaccalaureate.Visible = false;
                panelSpecialty.Visible = false;
                panelMagistracy.Visible = false;
                panelEge.Visible = false;
            }

            if (comboBox1.SelectedIndex == 1)//Бакалавриат
            {
                panelSPO.Visible = false;
                panelBaccalaureate.Visible = true;
                panelSpecialty.Visible = false;
                panelMagistracy.Visible = false;
                panelEge.Visible = true;
            }

            if (comboBox1.SelectedIndex == 2)//Специалитет
            {
                panelSPO.Visible = false;
                panelBaccalaureate.Visible = false;
                panelSpecialty.Visible = true;
                panelMagistracy.Visible = false;
                panelEge.Visible = true;
            }

            if (comboBox1.SelectedIndex == 3)//Магистратура
            {
                panelSPO.Visible = false;
                panelBaccalaureate.Visible = false;
                panelSpecialty.Visible = false;
                panelMagistracy.Visible = true;
                panelEge.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApplicationStatus applicationStatus = new ApplicationStatus();
            applicationStatus.Show();
            this.Close();
        }
        //TextBox1.Text =  Regex.Replace( TextBox1.Text, "\s", "")
        //Regex("^([a-zA-Z0-9])*$");

        private void button1_Click(object sender, EventArgs e)
        {
            var id = generateID();
            var education_level = comboBox1.SelectedItem.ToString();
            var fio = textBox1.Text;
            var passport = maskedTextBox3.Text;
            var snils = maskedTextBox2.Text;
            var email = textBox4.Text;
            var phone = maskedTextBox1.Text;
            var fio_parents = textBox5.Text;
            var institution = textBox2.Text;
            var direction = "";
            var status = "ПОДАНО";
            var comment = "";
            var executor = "-";
            var ege_math = "0";
            var ege_physic = "0";
            var ege_chemistry = "0";
            var ege_history = "0";
            var ege_sociality = "0";
            var ege_inform = "0";
            var ege_biology = "0";
            var ege_geography = "0";
            var ege_language = "0";
            var ege_literature = "0";
            var avarage_score_diplom = "0";
            var avarage_score = "0";
            var date = DateTime.Now.ToString("yyyyMMdd"); ;

            if (comboBox1.SelectedIndex == 0)// СПО
            {
                direction = comboBox2.Text;
                avarage_score = textBox9.Text;
            }

            if (comboBox1.SelectedIndex == 1)//Бакалавриат
            {
                direction = comboBox3.Text;
            }

            if (comboBox1.SelectedIndex == 2)//Специалитет
            {
                direction = comboBox4.Text;
            }

            if (comboBox1.SelectedIndex == 3)//Магистратура
            {
                direction = comboBox5.Text;
                avarage_score_diplom = textBox10.Text;
            }


            if (comboBox6.SelectedIndex == 0) //математика
            {
                ege_math = textBox3.Text;
            }
            if (comboBox6.SelectedIndex == 1) //физика
            {
                ege_physic = textBox3.Text;
            }
            if (comboBox7.SelectedIndex == 0) //химия
            {
                ege_chemistry = textBox6.Text;
            }
            if (comboBox7.SelectedIndex == 1) //история
            {
                ege_history = textBox6.Text;
            }
            if (comboBox8.SelectedIndex == 0) //общество
            {
                ege_sociality = textBox7.Text;
            }
            if (comboBox8.SelectedIndex == 1) //информатика
            {
                ege_inform = textBox7.Text;
            }
            if (comboBox9.SelectedIndex == 0) //биология
            {
                ege_biology = textBox8.Text;
            }
            if (comboBox9.SelectedIndex == 1) //география
            {
                ege_geography = textBox8.Text;
            }
            if (comboBox10.SelectedIndex == 0) //языки
            {
                ege_language = textBox11.Text;
            }
            if (comboBox10.SelectedIndex == 1) //литра
            {
                ege_literature = textBox11.Text;
            }



            database.OpenConnection();

            string query = $"insert into application values ('{id}', '{education_level}', '{fio}', '{passport}', '{snils}', '{email}'," +
                $" '{phone}', '{fio_parents}', '{institution}', '{direction}', '{status}', '{comment}', '{executor}',"+
                $" '{ege_math}', '{ege_physic}', '{ege_chemistry}', '{ege_history}', '{ege_sociality}', '{ege_inform}', " +
                $" '{ege_biology}', '{ege_geography}', '{ege_language}', '{ege_literature}', '{avarage_score_diplom}'," +
                $" '{avarage_score}', '{date}')";
            NpgsqlCommand comm = new NpgsqlCommand(query, database.GetConnection());

            comm.ExecuteNonQuery();
            MessageBox.Show("Товар добавлен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

            database.CloseConnection();
        }

        private string generateID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssff");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {
                MessageBox.Show("Вводите только цифры!");
                textBox3.Text = textBox3.Text.Remove(textBox4.Text.Length - 1);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, "[^0-9]"))
            {
                MessageBox.Show("Вводите только цифры!");
                textBox6.Text = textBox6.Text.Remove(textBox6.Text.Length - 1);
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox7.Text, "[^0-9]"))
            {
                MessageBox.Show("Вводите только цифры!");
                textBox7.Text = textBox7.Text.Remove(textBox7.Text.Length - 1);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox8.Text, "[^0-9]"))
            {
                MessageBox.Show("Вводите только цифры!");
                textBox8.Text = textBox8.Text.Remove(textBox8.Text.Length - 1);
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox9.Text, "[^0-9]"))
            {
                MessageBox.Show("Вводите только цифры!");
                textBox9.Text = textBox9.Text.Remove(textBox9.Text.Length - 1);
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox10.Text, "[^0-9]"))
            {
                MessageBox.Show("Вводите только цифры!");
                textBox10.Text = textBox10.Text.Remove(textBox10.Text.Length - 1);
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox11.Text, "[^0-9]"))
            {
                MessageBox.Show("Вводите только цифры!");
                textBox11.Text = textBox10.Text.Remove(textBox11.Text.Length - 1);
            }
        }
    }
}
