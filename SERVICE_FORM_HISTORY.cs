using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Data.SQLite;

namespace Project_Product_List
{
    public partial class SERVICE_FORM_HISTORY : Form
    {
        string connectionString = Paths.Paths.UTC_SQL_CONNECTION_NEW;

        public SERVICE_FORM_HISTORY()
        {
            InitializeComponent();
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
                        comboBoxCustomerName.Items.Add(dr["Name"]);
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
                SQLiteCommand cmd;
                SQLiteDataReader dr;

                using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
                {
                    try
                    {
                        cmd = new SQLiteCommand();
                        cmd.CommandText = "SELECT RMA_Number FROM RMA";
                        cmd.Connection = conn;
                        conn.Open();
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            comboBox1.Items.Add(dr["RMA_Number"]);
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

        private void SERVICE_FORM_HISTORY_Load(object sender, EventArgs e)
        {
            LOAD_RMA_NUMBERS_TO_COMBO_BOX();
            Load_Customers_To_ComboBox();
            UpdateDataGrid();

        }


        public void UpdateDataGrid()
        {
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM SERVICE_FORM", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }





        private void button4_Click(object sender, EventArgs e)
        {

            string RMA_NUMBER = textBoxSerialNumber.Text.Trim();

            
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM SERVICE_FORM WHERE RMA LIKE '%" + RMA_NUMBER + "'", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }

            


        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Customer_name = comboBoxCustomerName.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM SERVICE_FORM WHERE SOLD_TO  = '" + Customer_name + "'", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }


            

        }



        public void Delete_from_ServiceForm()
        {

            string deleteRMAnumber = comboBox1.Text.Trim();

            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "DELETE FROM SERVICE_FORM WHERE Name = '" + deleteRMAnumber + "';";
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
            UpdateDataGrid();
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
            pi.FileName = Paths.Paths.SERVICE_FORM_HISTORY_HELP_FILE;
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

        private void filterWarrantyServiceForm()
        {
            //string yes = "Yes";

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM SERVICE_FORM", conn); //"SELECT * FROM RMA Where Warranty =  '" + yes + "'", conn
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        public void EOYReport()
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

        private void eOYREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterWarrantyServiceForm();
            EOYReport();
        }


        public static DateTime ConvertFromUnixTimestamp(double start, long end)
        {
            DateTime origin = new DateTime(end, DateTimeKind.Utc);
            return origin.AddSeconds(start);
        }

        private void button2_Click(object sender, EventArgs e)
        {


            string startDate = dateTimePickerStart.Text;
            string endtDate = dateTimePickerEnd.Text;


            var parsedStartDate = DateTime.Parse(startDate).ToString("dd-MM-yyyy");

            var parseEnddDate = DateTime.Parse(endtDate).ToString("dd-MM-yyyy");



            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * from SERVICE_FORM WHERE SERVICE_DATE >= '" + parsedStartDate + "' AND SERVICE_DATE <='" + parseEnddDate + "'", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
            
        }
    }
}
