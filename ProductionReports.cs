using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Dapper;

namespace Project_Product_List
{


    public partial class ProductionReports : Form
    {
        
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


        public TextBox addNewTextBox()
        {
            TextBox txt = new TextBox();

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

        private Point newPoint(int v1, int v2)
        {
            throw new NotImplementedException();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            new Form2().Show();
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
            pi.FileName = @"P:\Archive\HELP UTC TESTS\Production Reports.docx";
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
            if (textBoxSN.Text == "")
            {
                MessageBox.Show(" Serial Number must be writed ! ");
            }
            else
            {
                try
                {
                    //Delete_Previous_Data_From_DataBase();
                    General.ActOnDb("DELETE", "", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), null);
                    InsertToDataBase();
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

        public void InsertToDataBase()
        {

            try
            {
                using (IDbConnection cnn = new SQLiteConnection(General.LoadConnectionString()))
                {
                    cnn.Execute("insert into PRODUCTION_REPORT(Serial_Number, TypeOfUDI, Customer_name, Customer_location, Sell_or_Rent, RMA_Number_1, RMA_Number_2, RMA_Number_3, Invoice_Date, Shipment_Date, Courier, Waybill, Comment) values ('" + textBoxSN.Text.Trim() + "','" + comboBoxType.Text.Trim() + "','" + comboBoxCustomer.Text.Trim() + "','" + textBoxLocation.Text.Trim() + "','" + comboBoxSell_Rent.Text.Trim() + "','" + textBoxRMA1.Text.Trim() + "','" + textBoxRMA2.Text.Trim() + "','" + textBoxRMA3.Text.Trim() + "','" + dateTimePickerInvoiceDate.Text.Trim() + "','" + dateTimePickerShipmentDate.Text.Trim() + "','" + comboBoxCourier.Text.Trim() + "','" + textBoxWaybill.Text.Trim() + "','" + textBoxComment.Text.Trim() + "')");
                }

                MessageBox.Show("Done Successfully !");

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }




        public void Delete_Previous_Data_From_DataBase()
        {
            SQLiteCommand cmd;
            SQLiteDataReader reader;
            string SN = textBoxSN.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "DELETE FROM PRODUCTION_REPORT WHERE Serial_Number = '" + SN + "';";
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


        private void PictureBoxSave_Click(object sender, EventArgs e)
        {
            if (textBoxSN.Text == "")
            {
                MessageBox.Show(" Serial Number must be writed ! ");
            }
            else
            {
                Delete_Previous_Data_From_DataBase();
                InsertToDataBase();
                clearFieldsAfterDone();
            }

        }



        private void PictureBoxRestore_Click(object sender, EventArgs e)
        {
            try
            {
                General.ActOnDb("SELECT", "TypeOfUDI", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), comboBoxType);
                General.ActOnDb("SELECT", "Customer_name", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), comboBoxCustomer);
                General.ActOnDb("SELECT", "Customer_location", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), textBoxLocation);
                General.ActOnDb("SELECT", "Sell_or_Rent", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), comboBoxSell_Rent);
                General.ActOnDb("SELECT", "RMA_Number_1", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), textBoxRMA1);
                General.ActOnDb("SELECT", "RMA_Number_2", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), textBoxRMA2);
                General.ActOnDb("SELECT", "RMA_Number_3", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), textBoxRMA3);
                General.ActOnDb("SELECT", "Invoice_Date", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), dateTimePickerInvoiceDate);
                General.ActOnDb("SELECT", "Shipment_Date", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), dateTimePickerShipmentDate);
                General.ActOnDb("SELECT", "Courier", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), comboBoxCourier);
                General.ActOnDb("SELECT", "Waybill", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), textBoxWaybill);
                General.ActOnDb("SELECT", "Comment", "PRODUCTION_REPORT", "Serial_Number", textBoxSN.Text.Trim(), textBoxComment);  
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
                    Delete_Previous_Data_From_DataBase();
                    InsertToDataBase();
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
                Delete_Previous_Data_From_DataBase();
                InsertToDataBase();
                clearFieldsAfterDone();
            }
        }

        private void ComboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            fill_Address();
        }

        void fill_Address()
        {

            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string CustomerName = comboBoxCustomer.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Address FROM CUSTOMER WHERE Name = '" + CustomerName + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxLocation.Text = Convert.ToString(dr[0]);
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
    }
}
