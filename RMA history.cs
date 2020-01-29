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

    public partial class RMA_history : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;

        public RMA_history()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            this.Hide();
        }

        public void Load_Customers_To_ComboBox()
        {
            /////load the names to combobox

            try
            {
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
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }


        }

        public void fillDataGrid()
        {

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [RMA_dt] ; ", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;

                    //foreach (DataGridViewRow row in dataGridView1.Rows)
                    //{
                    //    row.DefaultCellStyle.BackColor = Color.CornflowerBlue;
                    //}
                    sqlCon.Close(); 


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

                cmd.CommandText = "SELECT RMA_Number FROM[RMA_dt] WHERE isArrived = '" + 0 + "'; ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;


                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxUpdateStatusByRMANumber.Items.Add(Convert.ToString(reader[0]));
                }

                sqlConnection1.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }


        public void LOAD_SERIAL_NUMBERS_TO_COMBO_BOX()
        {
            /////load the S/N to combobox

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;


            // "UPDATE RMA_dt SET isArrived = '" + 1 + "'  WHERE RMA_Number = '" + RMANumber + "'; ";
            cmd.CommandText = "SELECT SerialNumber1 FROM[RMA_dt] WHERE isArrived = '" + 0 + "'; ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBoxUpdateStatus.Items.Add(Convert.ToString(reader[0]));
            }

            sqlConnection1.Close();
        }

        public void complete_the_Serial_Number_from_the_RMA_combo_box()
        {
            string RMA_NUMBER = comboBoxUpdateStatusByRMANumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SerialNumber1 FROM RMA_dt WHERE RMA_Number = '" + RMA_NUMBER + "' and isArrived = '" + 0 + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxUpdateStatus.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void complete_the_RMA_NUMBER_from_the_Serial_Number_COMBO_box()
        {
            string Serial_Number = comboBoxUpdateStatus.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT RMA_Number FROM RMA_dt WHERE SerialNumber1 = '" + Serial_Number + "' and isArrived = '" + 0 + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxUpdateStatusByRMANumber.Text = rd[0].ToString();
            }
            con.Close();
        }

        private void RMA_history_Load(object sender, EventArgs e)
        {
            LOAD_SERIAL_NUMBERS_TO_COMBO_BOX();
            LOAD_RMA_NUMBERS_TO_COMBO_BOX();
            Load_Customers_To_ComboBox();
            fillDataGrid();


        }

        private void comboBoxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string CustomerName = comboBoxCustomerName.Text.Trim();



            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [RMA_dt] where Customer_Name = '" + CustomerName + "';", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string RMA_number = textBoxRMANumber.Text.Trim();



            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [RMA_dt] where RMA_Number LIKE '%" + RMA_number + "';", sqlCon);

                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }

        public void Delete_from_RMA()
        {
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string deleteRMAnumber = comboBoxDelete.Text.Trim();

            cmd.CommandText = "DELETE FROM RMA_dt WHERE RMA_Number = '" + deleteRMAnumber + "';";
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

        private void textBoxSerialNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxDelete.Text == "")
            {
                MessageBox.Show("Please choose RMA from the list ! ");
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete this RMA?", "Delete RMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Delete_from_RMA_Directory();
                    Delete_from_RMA();
                    MessageBox.Show("Delete Successfully !");
                    fillDataGrid();

                }
            }

        }

        private void Delete_from_RMA_Directory()
        {
            int RMAyear = DateTime.Now.Year;

            string rma_number_to_delete = comboBoxDelete.Text;

            string DeletePath = "U:" + @"\" + "RMA - NEW" + @"\" + RMAyear + @"\" + rma_number_to_delete + ".pdf";

            if (File.Exists(DeletePath))
            {
                File.Delete(DeletePath);
            }
            else
            {
                MessageBox.Show(" File not found !");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void updateStatusOf_RMA_BySerialNumber()
        {

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string RMANumber = comboBoxUpdateStatus.Text.Trim();

            cmd.CommandText = "UPDATE RMA_dt SET isArrived = '" + 1 + "'  WHERE SerialNumber1 = '" + RMANumber + "' and isArrived = '" + 0 + "'; ";
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

        void updateStatusOf_RMA_By_RMANumber()
        {

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string RMANumber = comboBoxUpdateStatusByRMANumber.Text.Trim();

            cmd.CommandText = "UPDATE RMA_dt SET isArrived = '" + 1 + "'  WHERE RMA_Number = '" + RMANumber + "' and isArrived = '" + 0 + "'; ";
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


        void updateStatusOf_RMA_By_RMANumber_Undo()
        {

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string RMANumber = comboBoxUpdateStatusByRMANumber.Text.Trim();

            cmd.CommandText = "UPDATE RMA_dt SET isArrived = '" + 0 + "'  WHERE RMA_Number = '" + RMANumber + "'; ";
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

        void updateStatusOf_RMA_BySerialNumber_Undo()
        {

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string RMANumber = comboBoxUpdateStatus.Text.Trim();

            cmd.CommandText = "UPDATE RMA_dt SET isArrived = '" + 0 + "'  WHERE SerialNumber1 = '" + RMANumber + "'; ";
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
            pi.FileName = MyDirectory() + @"\HELP UTC TESTS\RMA History.docx";
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

        private void button2_Click(object sender, EventArgs e)
        {
            string RMA_SN = textBoxSerialNumber.Text.Trim();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [RMA_dt] where SerialNumber1 LIKE '%" + RMA_SN + "';", sqlCon);

                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }


        private void buttonUpdateStatus_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Confirm package is arrived", "Update status", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    updateStatusOf_RMA_BySerialNumber();
                    MessageBox.Show("Successfully Done !");
                    fillDataGrid();
                    comboBoxUpdateStatus.Items.Clear();
                    comboBoxUpdateStatusByRMANumber.Items.Clear();
                    LOAD_SERIAL_NUMBERS_TO_COMBO_BOX();
                    LOAD_RMA_NUMBERS_TO_COMBO_BOX();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }





        private void Button6_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Confirm package is arrived", "Update status", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    updateStatusOf_RMA_By_RMANumber();
                    MessageBox.Show("Successfully Done !");
                    fillDataGrid();
                    comboBoxUpdateStatusByRMANumber.Items.Clear();
                    comboBoxUpdateStatus.Items.Clear();
                    LOAD_SERIAL_NUMBERS_TO_COMBO_BOX();
                    LOAD_RMA_NUMBERS_TO_COMBO_BOX();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }


        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Undo package is arrived", "Update status", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    updateStatusOf_RMA_By_RMANumber_Undo();
                    MessageBox.Show("Done !");
                    fillDataGrid();
                    LOAD_SERIAL_NUMBERS_TO_COMBO_BOX();
                    LOAD_RMA_NUMBERS_TO_COMBO_BOX();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Undo package is arrived", "Update status", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    updateStatusOf_RMA_BySerialNumber_Undo();
                    MessageBox.Show("Done !");
                    fillDataGrid();
                    LOAD_SERIAL_NUMBERS_TO_COMBO_BOX();
                    LOAD_RMA_NUMBERS_TO_COMBO_BOX();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ComboBoxUpdateStatusByRMANumber_SelectedIndexChanged(object sender, EventArgs e)
        {

            complete_the_Serial_Number_from_the_RMA_combo_box();
            
        }

        private void comboBoxUpdateStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            complete_the_RMA_NUMBER_from_the_Serial_Number_COMBO_box();
        }
    }
}
