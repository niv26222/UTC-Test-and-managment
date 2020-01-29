using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Project_Product_List
{
    public partial class TestPressureOpacity_H_Form : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;

        public TestPressureOpacity_H_Form()
        {
            InitializeComponent();
        }


        public void UpdateDataGrid()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [TestPressureOpacity_dt] ; ", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TestPressureOpacity_H_Form_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ChoosForm_H_().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string productSerialNumber = textBoxSerialNumber.Text.Trim();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [TestPressureOpacity_dt] where Serial LIKE '%" + productSerialNumber + "';", sqlCon);

                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }


        public void Delete_from_TestPressureOpacity()
        {
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string deleteSN = textBox1.Text.Trim();

            cmd.CommandText = "DELETE FROM TestPressureOpacity_dt WHERE Serial = '" + deleteSN + "';";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Read();
            }

            sqlConnection1.Close();
            MessageBox.Show("Done !");
        }


        private void Delete_from_tpo_Directory()
        {

            string tpo_to_delete = textBox1.Text;


            int year = DateTime.Now.Year;


            string DeletePath = "U:" + @"\" + "Test Pressure Opacity" + @"\" + year + @"\" + tpo_to_delete + ".pdf";


            if (File.Exists(DeletePath))
            {
                File.Delete(DeletePath);
            }
            else
            {
                MessageBox.Show(" File not found !");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Delete_from_tpo_Directory();
            Delete_from_TestPressureOpacity();
            MessageBox.Show("Delete Successfully !");
            UpdateDataGrid();
        }
        public void createExcelFile()
        {
            saveFileDialog1.InitialDirectory = "Desktop";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "TPO TESTS HISTORY";
            saveFileDialog1.Filter = "Excel Files(2003)|*.xlsx|Excel Files(2007)|*.xlsx |Excel Files(2013)|*.xlsx";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                //change properties of the work Book
                ExcelApp.Columns.ColumnWidth = 30;

                //Storing header part in Excel

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    ExcelApp.Cells[1, i].EntireRow.Font.Bold = true;

                }


                //Storing each row and coloumn value to excel sheet

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                MessageBox.Show("Excel File Create Successfully !");
            }
        }


        private string MyDirectory()
        {
            //MessageBox.Show(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.UseShellExecute = true;
            pi.FileName = MyDirectory() + @"\HELP UTC TESTS\Help.docx";

            p.StartInfo = pi;

            try
            {
                p.Start();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createExcelFile();
        }
    }
}
