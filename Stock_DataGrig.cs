using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Data.SQLite;
using System.Configuration;

namespace Project_Product_List
{

    public partial class Stock_DataGrig : Form
    {
        

        public Stock_DataGrig()
        {
            InitializeComponent();
        }

        private void Stock_DataGrig_Load(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM STOCK", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
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


            
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM STOCK WHERE SerialNumber1 LIKE '%" + productSerialNumber + "'", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ProductName = comboBox1.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM STOCK WHERE Product LIKE '%" + ProductName + "'", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
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
            pi.FileName = @"P:\Archive\HELP UTC TESTS\Help.docx";

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
