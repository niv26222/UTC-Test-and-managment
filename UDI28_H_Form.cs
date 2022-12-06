using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Data.SQLite;
using System.Configuration;

namespace Project_Product_List
{

    public partial class UDI28_H_Form : Form
    {


        public UDI28_H_Form()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ChoosForm_H_().Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            new ChoosForm_H_().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string productSerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM UDI28 where Seriel_Code LIKE '%" + productSerialNumber + "';", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string CustomerName = comboBoxCustomerName.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM UDI28 where Customer_Name = '" + CustomerName + "';", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        public void Delete_from_udi28()
        {
            string deleteSN = textBox1.Text.Trim();

            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "DELETE FROM UDI28 WHERE Seriel_Code = '" + deleteSN + "';";
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



            MessageBox.Show("Done !");
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
        

        public void UpdateDataGrid()
        {
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM UDI28", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        private void UDI28_H_Form_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
            Load_Customers_To_ComboBox();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            Delete_from_udi28();
            UpdateDataGrid();
        }

        public void createExcelFile()
        {
            saveFileDialog1.InitialDirectory = "Desktop";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "UDI28 TESTS HISTORY";
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
            pi.FileName = Paths.Paths.UDI_28_HISTORY_HELP_FILE;

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
