using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Project_Product_List
{
    public partial class ProductionReportsHistory : Form
    {
        static string ConnectionString = Paths.Paths.UTC_SQL_CONNECTION_NEW;
        SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);

        public ProductionReportsHistory()
        {
            InitializeComponent();
        }

        private void PictureBoxBack_Click(object sender, EventArgs e)
        {
            new Reports().Show();
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
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Name FROM CUSTOMER";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        comboBoxCustomer.Items.Add(dr["Name"]);
                    }

                    dr.Close();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void ProductionReportsHistory_Load(object sender, EventArgs e)
        {
            Load_Customers_To_ComboBox();
            UpdateDataGrid();
        }

        public void UpdateDataGrid()
        {
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM PRODUCTION_REPORT", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        private void PictureBoxRestore_Click(object sender, EventArgs e)
        {
            string SN = textBoxSN.Text.Trim();

            
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM PRODUCTION_REPORT WHERE Serial_Number LIKE '%" + SN + "'", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }

        }



        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CN = comboBoxCustomer.Text.Trim();
            
            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT * FROM PRODUCTION_REPORT WHERE Customer_name LIKE '%" + CN + "';";
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = conn;
                    conn.Open();
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Read();
                    }

                    reader.Close();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
