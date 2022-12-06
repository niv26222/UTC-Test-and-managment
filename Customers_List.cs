using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Data.SQLite;
using Dapper;

namespace Project_Product_List
{
    public partial class Customers_List : Form
    {
        RMA_FORM rma_form = null;


        void clearFieldsAfterDone()
        {
            //clean all Fields again for new test
            foreach (Control c in Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                else if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
            }
        }

        public Customers_List(RMA_FORM rma_form)
        {
            this.rma_form = rma_form;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            this.Hide();
        }

        


        public void insertCustomerToDataBase()
        {
            if (textBoxCustomerName.Text == "")
            {
                MessageBox.Show("Customer Name must be writed !");
            }
            else
            {
                try
                {
                    using (IDbConnection cnn = new SQLiteConnection(General.LoadConnectionString()))
                    {

                        cnn.Execute("insert into CUSTOMER (Name, Phone_Number, Mail, Address) values ('" + textBoxCustomerName.Text.Trim() + "', '" + textBoxCustomerPhone.Text.Trim() + "', '" + textBoxCustomerMail.Text.Trim() + "', '" + textBoxCustomerAddress.Text.Trim() + "')");


                    }
                }
                catch (SqlException ex)
                {

                    Console.WriteLine(ex.ToString());
                }

            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            insertCustomerToDataBase();
            MessageBox.Show("Done Successfully !");
            clearFieldsAfterDone();
            UpdateDataGrid();

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                        comboBoxCustomerNameToDelete.Items.Add(dr["Name"]);
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
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM CUSTOMER", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridViewCustomers.DataSource = dset.Tables[0];
                conn.Close();
            }
        }



        private void Customers_List_Load(object sender, EventArgs e)
        {


            UpdateDataGrid();
            Load_Customers_To_ComboBox();



        }

        void Delete_Customer_From_DataBase()
        {



            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "DELETE FROM CUSTOMER WHERE Name = '" + comboBoxCustomerNameToDelete.Text.Trim() + "';";
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


        private void button2_Click_1(object sender, EventArgs e)
        {


            if(MessageBox.Show("Are you sure you want to delete this Customer?","Delete Customer",  MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
            {
                Delete_Customer_From_DataBase();
                MessageBox.Show("Delete Successfully !");

            }
            UpdateDataGrid();


        }

        private void comboBoxCustomerNameToDelete_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        public void createExcelFile()
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

                for (int i = 1; i < dataGridViewCustomers.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridViewCustomers.Columns[i - 1].HeaderText;
                    ExcelApp.Cells[1, i].EntireRow.Font.Bold = true;

                }


                //Storing each row and coloumn value to excel sheet
                for (int i = 0; i < dataGridViewCustomers.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewCustomers.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridViewCustomers.Rows[i].Cells[j].Value.ToString();
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
            pi.FileName = Paths.Paths.MAIN_MENU_HELP_FILE;
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
