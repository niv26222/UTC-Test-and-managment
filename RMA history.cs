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

    public partial class RMA_history : Form
    {
        string connectionString = Paths.Paths.UTC_SQL_CONNECTION_NEW;

        public RMA_history()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            this.Hide();
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
                        cmd.CommandText = "SELECT RMA_Number FROM RMA WHERE isArrived = '" + 0 + "'; ";
                        cmd.Connection = conn;
                        conn.Open();
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            comboBoxCustomerName.Items.Add(dr["RMA_Number"]);
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


        public void LOAD_SERIAL_NUMBERS_TO_COMBO_BOX()
        {


                /////load the names to combobox
                SQLiteCommand cmd;
                SQLiteDataReader dr;

                using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
                {
                    try
                    {
                        cmd = new SQLiteCommand();
                        cmd.CommandText = "SELECT SerialNumber1 FROM RMA WHERE isArrived = '" + 0 + "'; ";
                        cmd.Connection = conn;
                        conn.Open();
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            comboBoxCustomerName.Items.Add(dr["SerialNumber1"]);
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
            UpdateDataGrid();
        }

      

        public void UpdateDataGrid()
        {
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM RMA", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        private void comboBoxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string CustomerName = comboBoxCustomerName.Text.Trim();




            string RMA_SN = textBoxSerialNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM RMA WHERE Customer_Name  = '" + CustomerName + "'", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }


            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string RMA_number = textBoxRMANumber.Text.Trim();

            
            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM RMA WHERE Name = '" + RMA_number + "'", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }

            
        }

        public void Delete_from_RMA()
        {

            string deleteRMAnumber = comboBoxDelete.Text.Trim();

            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "DELETE FROM RMA WHERE RMA_Number = '" + deleteRMAnumber + "';";
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
                    UpdateDataGrid();

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



            string RMANumber = comboBoxUpdateStatusByRMANumber.Text.Trim();



            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "UPDATE RMA SET isArrived = '" + 1 + "'  WHERE SerialNumber1 = '" + RMANumber + "' and isArrived = '" + 0 + "'; ";
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

        void updateStatusOf_RMA_By_RMANumber()
        {

            string RMANumber = comboBoxUpdateStatusByRMANumber.Text.Trim();



            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "UPDATE RMA SET isArrived = '" + 1 + "'  WHERE RMA_Number = '" + RMANumber + "' and isArrived = '" + 0 + "'; ";
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


        void updateStatusOf_RMA_By_RMANumber_Undo()
        {

            string RMANumber = comboBoxUpdateStatusByRMANumber.Text.Trim();
            

            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "UPDATE RMA SET isArrived = '" + 0 + "'  WHERE RMA_Number = '" + RMANumber + "'; ";
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

        void updateStatusOf_RMA_BySerialNumber_Undo()
        {

            string RMANumber = comboBoxUpdateStatus.Text.Trim();
            

            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "UPDATE RMA SET isArrived = '" + 0 + "'  WHERE SerialNumber1 = '" + RMANumber + "'; ";
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
            pi.FileName = Paths.Paths.RMA_HISTORY_HELP_FILE;

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

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {

                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM RMA WHERE SerialNumber1 LIKE '%" + RMA_SN + "'", conn);
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
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
                    UpdateDataGrid();
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
                    UpdateDataGrid();
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
                    UpdateDataGrid();
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
                    UpdateDataGrid();
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

        private void comboBoxUpdateStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            complete_the_RMA_NUMBER_from_the_Serial_Number_COMBO_box();
        }



        private void filterWarrantyRMAs()
        {
            string yes = "Yes";

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM RMA", conn); //"SELECT * FROM RMA Where Warranty =  '" + yes + "'", conn
                DataSet dset = new DataSet();
                adapter.Fill(dset, "info");
                dataGridView1.DataSource = dset.Tables[0];
                conn.Close();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            filterWarrantyRMAs();
            EOYReport();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSerialNumber_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
