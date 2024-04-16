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
    public partial class PrintApp : Form
    {
        Database database = new Database();
        private string fio;
        private string direction;
        public PrintApp(string fio, string direction, Npgsql.NpgsqlConnection npgsqlConnection)
        {
            InitializeComponent();

            this.fio = fio;
            this.direction = direction;
        }

        private void PrintApp_Load(object sender, EventArgs e)
        {
            richTextBox1.Text =
                                $"                  Заявление\n\n" +
                                $"Я, {this.fio} подал документы в ВУЗ\n\n" +
                                $"На направления подготовки: {this.direction}\n\n" +
                                $"Дата заявления: {DateTime.Now:dd/MM/yyyy} \n\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicationStatus applicationStatus = new ApplicationStatus();
            applicationStatus.Show();
            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Times New Roman", 16, FontStyle.Regular), Brushes.Black, new Point(10, 10));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }
}
