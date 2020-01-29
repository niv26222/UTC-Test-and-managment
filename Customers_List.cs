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
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Project_Product_List
{
    public partial class Customers_List : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;
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
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        SqlCommand sqlCmd = new SqlCommand("Customers_add", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@Customer_name", textBoxCustomerName.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Customer_Phone", textBoxCustomerPhone.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Customer_address", textBoxCustomerAddress.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Customer_rate", comboBoxCustomerRate.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Customer_mail", textBoxCustomerMail.Text.Trim());


                        sqlCmd.ExecuteNonQuery();

                        if (rma_form != null)
                        {
                            rma_form.changeDetailsFromCustomerList(textBoxCustomerName.Text.Trim());
                        }
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
            Load_Customers();

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void Fill_Customers_in_ComboBox()
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string CustomerName = comboBoxCustomerNameToDelete.Text.Trim();

                cmd.CommandText = "SELECT Customer_name FROM[Customers_dt]";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;


                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxCustomerNameToDelete.Items.Add(Convert.ToString(reader[0]));
                }

                sqlConnection1.Close();
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }



        }

        void Load_Customers()
        {

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Customers_dt] ; ", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dataGridViewCustomers.DataSource = dtbl;
                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }
        private void Customers_List_Load(object sender, EventArgs e)
        {


            Load_Customers();

            Fill_Customers_in_ComboBox();



        }

        void Delete_Customer_From_DataBase()
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string CustomerName = comboBoxCustomerNameToDelete.Text.Trim();

                cmd.CommandText = "DELETE FROM Customers_dt WHERE Customer_name = '" + CustomerName + "';";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Read();
                }

                sqlConnection1.Close();
                MessageBox.Show("Done !");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        private void button2_Click_1(object sender, EventArgs e)
        {


            if(MessageBox.Show("Are you sure you want to delete this Customer?","Delete Customer",  MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
            {
                Delete_Customer_From_DataBase();
                MessageBox.Show("Delete Successfully !");

            }
            Load_Customers();


        }

        private void comboBoxCustomerNameToDelete_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Load_Customers();
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
    }
}
