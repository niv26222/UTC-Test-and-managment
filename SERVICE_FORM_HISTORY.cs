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
    public partial class SERVICE_FORM_HISTORY : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;

        public SERVICE_FORM_HISTORY()
        {
            InitializeComponent();
        }


        public void Load_Customers_To_ComboBox()
        {
            /////load the names to combobox

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string CustomerName = comboBoxCustomerName.Text.Trim();

            cmd.CommandText = "SELECT Customer_name FROM[Customers_dt]";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBoxCustomerName.Items.Add(Convert.ToString(reader[0]));
            }

            sqlConnection1.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            this.Hide();
        }

        public void fillDataGrig()
        {


            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [ServiceForm_dt] ; ", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;
                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }

        public void LOAD_RMA_NUMBERS_TO_COMBO_BOX()
        {
            /////load the names to combobox


            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT RMA_Number FROM[RMA_dt]";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;


                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(reader[0]));
                }

                sqlConnection1.Close();
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }



        }

        private void SERVICE_FORM_HISTORY_Load(object sender, EventArgs e)
        {
            LOAD_RMA_NUMBERS_TO_COMBO_BOX();
            Load_Customers_To_ComboBox();
            fillDataGrig();

        }

        private void button4_Click(object sender, EventArgs e)
        {

            string RMA_NUMBER = textBoxSerialNumber.Text.Trim();



            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [ServiceForm_dt] where RMA LIKE '%" + RMA_NUMBER + "';", sqlCon);

                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Customer_name = comboBoxCustomerName.Text.Trim();


            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [ServiceForm_dt] where SOLD_TO = '" + Customer_name + "';", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }


        public void fillDataGrid()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [ServiceForm_dt] ; ", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }

        public void Delete_from_ServiceForm()
        {
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string deleteRMAnumber = comboBox1.Text.Trim();

            cmd.CommandText = "DELETE FROM ServiceForm_dt WHERE RMA = '" + deleteRMAnumber + "';";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Read();
            }

            sqlConnection1.Close();
            
        }


        private void Delete_from_ServiceForm_from_Directory()
        {
            string ServiceForm_from_to_delete = comboBox1.Text;

            int RMAyear = DateTime.Now.Year;

            string DeletePath = "U:" + @"\" + "Service form - NEW" + @"\" + RMAyear + @"\" + ServiceForm_from_to_delete + ".pdf";

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
            Delete_from_ServiceForm_from_Directory();
            Delete_from_ServiceForm();
            MessageBox.Show("Delete Successfully !");
            fillDataGrid();
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
            pi.FileName =MyDirectory() + @"\HELP UTC TESTS\Help.docx";
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
