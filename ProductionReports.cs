using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Project_Product_List
{


    public partial class ProductionReports : Form
    {

        static string ConnectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;

        SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
        

        int A = 9;
        int B = 232;
        int textBoxCounter = 2;


        public ProductionReports()
        {
            InitializeComponent();
        }

        private void ProductionReports_Load(object sender, EventArgs e)
        {

            Load_Customers_To_ComboBox();
        }


        public System.Windows.Forms.TextBox addNewTextBox()
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();

            this.Controls.Add(txt);
            txt.Width = 114;
            txt.Height = 20;
            txt.Name = "textBoxRMA" + Convert.ToString(textBoxCounter);
            txt.Top = (A * 27) - 1;
            txt.Left = B + 50;
            textBoxCounter++;

            A *= 2;
            //B += 20;

            return txt;
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

                Console.WriteLine(ex.ToString());
            }

        }


        private Point newPoint(int v1, int v2)
        {
            throw new NotImplementedException();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            this.Hide();
        }
        private string MyDirectory()
        {
            //MessageBox.Show(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.UseShellExecute = true;
            pi.FileName = MyDirectory() + @"\HELP UTC TESTS\Production Reports.docx";
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

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            new ProductionReportsHistory().Show();
            this.Hide();
        }

        private void PictureBoxDone_Click(object sender, EventArgs e)
        {

            // Test the Date formats !

            //MessageBox.Show(dateTimePickerInvoiceDate.ToString());
            //dateTimePickerInvoiceDate.Format = DateTimePickerFormat.Custom;
            //dateTimePickerInvoiceDate.CustomFormat = "dd/MM/yyyy";
            //MessageBox.Show(dateTimePickerInvoiceDate.ToString());

            //
            
            if (textBoxSN.Text == "")
            {
                MessageBox.Show(" Serial Number must be writed ! ");
            }
            else
            {
                try
                {
                    Delete_Previous_Data_From_DataBaseTEMPORARY_dt();
                    Delete_Previous_Data_From_DataBase();
                    insertToTemporaryDataBase();
                    insertToDataBase();
                    MessageBox.Show("Successfully inserted into database");
                    clearFieldsAfterDone();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed");
                }
            }
        }



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
                else if (c is ComboBox)
                {
                    ((ComboBox)c).Text = "";
                }
            }
        }

        public void insertToDataBase()
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {




                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("ProductionReports_add", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@Serial_Number", textBoxSN.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TypeOfUDI", comboBoxType.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Customer_name", comboBoxCustomer.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Customer_location", textBoxLocation.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Sell_or_Rent", comboBoxSell_Rent.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@RMA_Number_1", textBoxRMA1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RMA_Number_2", textBoxRMA2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RMA_Number_3", textBoxRMA3.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@Invoice_Date", dateTimePickerInvoiceDate.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Shipment_Date", dateTimePickerShipmentDate.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Courier", comboBoxCourier.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Waybill", textBoxWaybill.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Comment", textBoxComment.Text.Trim());

                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }


        public void insertToTemporaryDataBase()
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("ProductionReports_Temporary_add", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@Serial_Number", textBoxSN.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TypeOfUDI", comboBoxType.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Customer_name", comboBoxCustomer.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Customer_location", textBoxLocation.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Sell_or_Rent", comboBoxSell_Rent.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@RMA_Number_1", textBoxRMA1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RMA_Number_2", textBoxRMA2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RMA_Number_3", textBoxRMA3.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@Invoice_Date", dateTimePickerInvoiceDate.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Shipment_Date", dateTimePickerShipmentDate.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Courier", comboBoxCourier.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Waybill", textBoxWaybill.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Comment", textBoxComment.Text.Trim());


                    sqlCmd.ExecuteNonQuery();

                    sqlCon.Close();

                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }



        public void Delete_Previous_Data_From_DataBaseTEMPORARY_dt()
        {
            SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string SN = textBoxSN.Text.Trim();

            cmd.CommandText = "DELETE FROM ProductionReports_Temporary_dt WHERE Serial_Number = '" + SN + "';";
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
        public void Delete_Previous_Data_From_DataBase()
        {
            SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string SN = textBoxSN.Text.Trim();

            cmd.CommandText = "DELETE FROM ProductionReports_dt WHERE Serial_Number = '" + SN + "';";
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


        private void PictureBoxSave_Click(object sender, EventArgs e)
        {
            if (textBoxSN.Text == "")
            {
                MessageBox.Show(" Serial Number must be writed ! ");
            }
            else
            {
                Delete_Previous_Data_From_DataBaseTEMPORARY_dt();
                insertToTemporaryDataBase();
                clearFieldsAfterDone();
            }

        }



        public void restoreTypeOfUDI()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TypeOfUDI FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxType.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCustomerName()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Customer_name FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxCustomer.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCustomerLocation()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Customer_location FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxLocation.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSellOrRent()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Sell_or_Rent FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxSell_Rent.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreRMA_NUMBER1()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT RMA_NUMBER_1 FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxRMA1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreRMA_NUMBER2()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT RMA_NUMBER_2 FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxRMA2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreRMA_NUMBER3()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT RMA_NUMBER_3 FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxRMA3.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreInvoiceDate()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Invoice_Date FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePickerInvoiceDate.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreShipmentDate()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Shipment_Date FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePickerShipmentDate.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCourier()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Courier FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxCourier.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorewaybill()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Waybill FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxWaybill.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreComment()
        {
            string SN = textBoxSN.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Comment FROM [ProductionReports_Temporary_dt] WHERE Serial_Number = '" + SN + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxComment.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void Restore_data_from_temporary_data_base()
        {
            restoreTypeOfUDI();
            restoreCustomerName();
            restoreCustomerLocation();
            restoreSellOrRent();
            restoreRMA_NUMBER1();
            restoreRMA_NUMBER2();
            restoreRMA_NUMBER3();
            restoreInvoiceDate();
            restoreShipmentDate();
            restoreCourier();
            restorewaybill();
            restoreComment();
        }

        private void PictureBoxRestore_Click(object sender, EventArgs e)
        {
            try
            {
                Restore_data_from_temporary_data_base();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void DoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxSN.Text == "")
            {
                MessageBox.Show(" Serial Number must be writed ! ");
            }
            else
            {
                try
                {
                    Delete_Previous_Data_From_DataBaseTEMPORARY_dt();
                    Delete_Previous_Data_From_DataBase();
                    insertToTemporaryDataBase();
                    insertToDataBase();
                    MessageBox.Show("Successfully inserted into database");
                    clearFieldsAfterDone();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed");

                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxSN.Text == "")
            {
                MessageBox.Show(" Serial Number must be writed ! ");
            }
            else
            {
                Delete_Previous_Data_From_DataBaseTEMPORARY_dt();
                insertToTemporaryDataBase();
                clearFieldsAfterDone();
            }
        }

        private void ComboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            fill_Address();
        }

        void fill_Address()
        {

            ///////////////////////// fill Customer Address /////////////////////////

            SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string CustomerName = comboBoxCustomer.Text.Trim();

            cmd.CommandText = "SELECT Customer_address FROM Customers_dt WHERE Customer_name = '" + CustomerName + "';";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                textBoxLocation.Text = Convert.ToString(reader[0]);
            }

            sqlConnection1.Close();
            
        }
    }
}
