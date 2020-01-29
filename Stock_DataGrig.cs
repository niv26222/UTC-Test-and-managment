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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlServerCe;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;

namespace Project_Product_List
{

    public partial class Stock_DataGrig : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;

        public Stock_DataGrig()
        {
            InitializeComponent();
        }

        private void Stock_DataGrig_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Stock_products_dt] ; ", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }
        public void createExcelFile()
        {
            //dataGridView1.Rows.Add();
            saveFileDialog1.InitialDirectory = "Desktop";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Excel Files(2003)|*.xlsx|Excel Files(2007)|*.xlsx |Excel Files(2013)|*.xlsx";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                //change properties of the work Book
                ExcelApp.Columns.ColumnWidth = 30;

                //Storing header part in Excel
                
                for (int i = 1; i < dataGridView1.Columns.Count+1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    ExcelApp.Cells[1, i].EntireRow.Font.Bold = true;
                    ExcelApp.Cells[1, i].Font.Color = Color.Green;
                    
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

        private void Back_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            this.Hide();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            string productSerialNumber = textBoxSerialNumber.Text.Trim();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Stock_products_dt] where SerialNumber LIKE '%" + productSerialNumber + "';", sqlCon);

                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ProductName = comboBox1.Text.Trim();


            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Stock_products_dt] where Product = '" + ProductName + "';", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
