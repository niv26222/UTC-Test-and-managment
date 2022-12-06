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
    public partial class BoatUnit28_H_Form : Form
    {
        string connectionString = Paths.Paths.UTC_SQL_CONNECTION_NEW;

        public BoatUnit28_H_Form()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ChoosForm_H_().Show();
            this.Hide();
        }

        private void labelSerialNumber_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxSerialNumber_TextChanged(object sender, EventArgs e)
        {

        }

        void Load_Customers_To_ComboBox()
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


        public void UpdateDataGrid()
        {
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM BOAT28", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        private void RMA_BoatUnit_28_Form_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
            //Load_Customers_To_ComboBox();


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
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM BOAT28 where Seriel_Code LIKE '%" + productSerialNumber + "';", conn);
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
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM BOAT28 where Customer_Name = '" + CustomerName + "';", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void Delete_from_boat28()
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
                    cmd.CommandText = "DELETE FROM BOAT28 WHERE Seriel_Code = '" + deleteSN + "';";
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


        public void createExcelFile()
        {
            saveFileDialog1.InitialDirectory = "Desktop";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "UDI BOAT 28 TESTS HISTORY";
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


        private void button1_Click(object sender, EventArgs e)
        {
            Delete_from_boat28();
            UpdateDataGrid();
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
            pi.FileName = Paths.Paths.UDI_BOAT_28_HISTORY_HELP_FILE;
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
