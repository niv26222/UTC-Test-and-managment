using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Product_List
{
    public partial class ProductionReportsHistory : Form
    {
        static string ConnectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;
        SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);

        public ProductionReportsHistory()
        {
            InitializeComponent();
        }

        private void PictureBoxBack_Click(object sender, EventArgs e)
        {
            new ProductionReports().Show();
            this.Hide();
        }

        public void createExcelFile()
        {

            saveFileDialog1.InitialDirectory = "Desktop";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "Production Reports " + comboBoxCustomer.Text;
            saveFileDialog1.Filter = "Excel Files(2013)|*.xlsx|Excel Files(2007)|*.xlsx|Excel Files(2003)|*.xlsx";

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
                    ExcelApp.Cells[1, i].Font.Color = System.Drawing.Color.Red;
                    
                }




                //ExcelApp.Cells[1, 0].Value = "111";


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

        private void PictureBoxExcel_Click(object sender, EventArgs e)
        {
            createExcelFile();
        }

        void Load_Customers_To_ComboBox()
        {
            /////load the names to combobox
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                //string CustomerName = comboBoxCustomerName.Text.Trim();

                cmd.CommandText = "SELECT Customer_name FROM[Customers_dt]";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;


                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxCustomer.Items.Add(Convert.ToString(reader[0]));
                }

                sqlConnection1.Close();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        private void ProductionReportsHistory_Load(object sender, EventArgs e)
        {
            Load_Customers_To_ComboBox();
            fillDataGrid();
        }

        public void fillDataGrid()
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [ProductionReports_dt] ; ", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }

        private void PictureBoxRestore_Click(object sender, EventArgs e)
        {
            string SN = textBoxSN.Text.Trim();



            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [ProductionReports_dt] where Serial_Number LIKE '%" + SN + "';", sqlCon);

                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }



        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CN = comboBoxCustomer.Text.Trim();

            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [ProductionReports_dt] where Customer_name LIKE '%" + CN + "';", sqlCon);

                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }
    }
}
