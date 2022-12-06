using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Project_Product_List
{
    public partial class Tracking_by_waybill_number : Form
    {
        string connectionString = Paths.Paths.UTC_SQL_CONNECTION_NEW;

        public Tracking_by_waybill_number()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Production_testingFORM().Show();
            this.Hide();
        }


        public int fill_Waybill_data_udi14()
        {
            ///Fill for udi 14 tracking
            

            string Waybill_Number = comboBoxWaybill_Number.Text.Trim();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Udi14_dt] where WaybillNumber = '" + Waybill_Number + "';", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }


            int rowCount = ((DataTable)this.dataGridView1.DataSource).Rows.Count;

            int numberOfRows = dataGridView1.Rows.Count;

            return rowCount ;
        }

        public int fill_Waybill_data_udi28()
        {
            ///Fill for udi 28 tracking

            string Waybill_Number = comboBoxWaybill_Number.Text.Trim();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Udi28_dt] where WaybillNumber = '" + Waybill_Number + "';", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView2.DataSource = dtbl;
            }

            int rowCount = ((DataTable)this.dataGridView2.DataSource).Rows.Count;


            return rowCount ;
        }

        public int fill_Waybill_data_Boat14()
        {
            ///Fill for Boat14 tracking

            string Waybill_Number = comboBoxWaybill_Number.Text.Trim();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Boat14_dt] where WaybillNumber = '" + Waybill_Number + "';", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView3.DataSource = dtbl;
            }
            int rowCount = ((DataTable)this.dataGridView3.DataSource).Rows.Count;


            return rowCount ;
        }

        public int fill_Waybill_data_Boat28()
        {
            ///Fill for Boat28 tracking

            string Waybill_Number = comboBoxWaybill_Number.Text.Trim();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Boat28_dt] where WaybillNumber = '" + Waybill_Number + "';", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView4.DataSource = dtbl;
            }
            int rowCount = ((DataTable)this.dataGridView4.DataSource).Rows.Count;


            return rowCount ;
        }

        public int fill_Waybill_data_ADCS()
        {
            ///Fill for ADCS tracking

            string Waybill_Number = comboBoxWaybill_Number.Text.Trim();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [ADCS_dt] where WaybillNumber = '" + Waybill_Number + "';", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView5.DataSource = dtbl;
            }
            int rowCount = ((DataTable)this.dataGridView5.DataSource).Rows.Count;
            
            return rowCount ;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int Counter;

            try
            {

                fill_Waybill_data_udi14();
                fill_Waybill_data_udi28();
                fill_Waybill_data_Boat14();
                fill_Waybill_data_Boat28();
                fill_Waybill_data_ADCS();

                Counter = fill_Waybill_data_udi14() + fill_Waybill_data_udi28() + fill_Waybill_data_Boat14() + fill_Waybill_data_Boat28() + fill_Waybill_data_ADCS();

                textBoxCounter.Text = Counter.ToString();
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        
        public void createExcelFileUDI14()
        {
            
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

        public void createExcelFileUDI28()
        {

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

                for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
                    ExcelApp.Cells[1, i].EntireRow.Font.Bold = true;
                }


                //Storing each row and coloumn value to excel sheet

                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();

                MessageBox.Show("Excel File Create Successfully !");
            }
        }

        public void createExcelFileBoat14()
        {

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

                for (int i = 1; i < dataGridView3.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridView3.Columns[i - 1].HeaderText;
                    ExcelApp.Cells[1, i].EntireRow.Font.Bold = true;
                }


                //Storing each row and coloumn value to excel sheet

                for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView3.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView3.Rows[i].Cells[j].Value.ToString();
                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();

                MessageBox.Show("Excel File Create Successfully !");
            }
        }

        public void createExcelFileBoat28()
        {

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

                for (int i = 1; i < dataGridView4.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridView4.Columns[i - 1].HeaderText;
                    ExcelApp.Cells[1, i].EntireRow.Font.Bold = true;
                }


                //Storing each row and coloumn value to excel sheet

                for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView4.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView4.Rows[i].Cells[j].Value.ToString();
                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();

                MessageBox.Show("Excel File Create Successfully !");
            }
        }

        public void createExcelFileADCS()
        {
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
                for (int i = 1; i < dataGridView5.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridView5.Columns[i - 1].HeaderText;
                    ExcelApp.Cells[1, i].EntireRow.Font.Bold = true;
                }


                //Storing each row and coloumn value to excel sheet
                for (int i = 0; i < dataGridView5.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView5.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView5.Rows[i].Cells[j].Value.ToString();
                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();

                MessageBox.Show("Excel File Create Successfully !");
            }
        }



        private void button7_Click(object sender, EventArgs e)
        {
            //UDI14
            createExcelFileUDI14();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //UDI28
            createExcelFileUDI28();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Boat14
            createExcelFileBoat14();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Boat28
            createExcelFileBoat28();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //ADCS
            createExcelFileADCS();
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
            pi.FileName = Paths.Paths.WAYBILL_HELP_FILE;
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



        void Load_Customers_To_ComboBox()
        {
            /////load the names to combobox

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;


            cmd.CommandText = "SELECT WaybillNumber FROM[ShipmentReceipt_dt]";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBoxWaybill_Number.Items.Add(Convert.ToString(reader[0]));
            }

            sqlConnection1.Close();
        }


        private void Tracking_by_waybill_number_Load(object sender, EventArgs e)
        {
            Load_Customers_To_ComboBox();
        }
    }
}
