using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Data.SQLite;
using System.Configuration;
using Dapper;

namespace Project_Product_List
{
    public partial class Invoices_FORM : Form
    {

        public Invoices_FORM()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {

        }



        void Load_Customers_To_ComboBox()
        {
            /////load the names to combobox
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

        
        void Load_number()
        {
            textBoxNUM_1.Text ="1" ;
            textBoxNUM_2.Text = "2";
            textBoxNUM_3.Text = "3";
            textBoxNUM_4.Text = "4";
            textBoxNUM_5.Text = "5";
            textBoxNUM_6.Text = "6";
            textBoxNUM_7.Text = "7";
            textBoxNUM_8.Text = "8";
            textBoxNUM_9.Text = "9";
            textBoxNUM_10.Text = "10";
            textBoxNUM_11.Text = "11";
            textBoxNUM_12.Text = "12";

        }

        void Load_Invoices()
        {
            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Invoice_Number FROM INVOICE";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        textBoxInvoiceNumber.Items.Add(dr["Invoice_Number"]);
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


        private void Invoices_FORM_Load(object sender, EventArgs e)
        {
            Load_number();
            Load_Invoices();
            Load_Customers_To_ComboBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        public void InsertToDataBase()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(General.LoadConnectionString()))
                {

                    cnn.Execute("insert into INVOICE (Invoice_Number, Date, Customer_Name, Comment, NUM_1, CAT_NUMBER_1, DESCRIPTION_1, QUANTITY_1, PRICE_1, TOTAL_1, NUM_2, CAT_NUMBER_2, DESCRIPTION_2, QUANTITY_2, PRICE_2, TOTAL_2, NUM_3, CAT_NUMBER_3, DESCRIPTION_3, QUANTITY_3, PRICE_3, TOTAL_3, NUM_4, CAT_NUMBER_4, DESCRIPTION_4, QUANTITY_4, PRICE_4, TOTAL_4, NUM_5, CAT_NUMBER_5, DESCRIPTION_5, QUANTITY_5, PRICE_5, TOTAL_5, NUM_6, CAT_NUMBER_6, DESCRIPTION_6, QUANTITY_6, PRICE_6, TOTAL_6, NUM_7, CAT_NUMBER_7, DESCRIPTION_7, QUANTITY_7, PRICE_7, TOTAL_7, NUM_8, CAT_NUMBER_8, DESCRIPTION_8, QUANTITY_8, PRICE_8, TOTAL_8, NUM_9, CAT_NUMBER_9, DESCRIPTION_9, QUANTITY_9, PRICE_9, TOTAL_9, NUM_10, CAT_NUMBER_10, DESCRIPTION_10, QUANTITY_10, PRICE_10, TOTAL_10, NUM_11, CAT_NUMBER_11, DESCRIPTION_11, QUANTITY_11, PRICE_11, TOTAL_11, NUM_12, CAT_NUMBER_12, DESCRIPTION_12, QUANTITY_12, PRICE_12, TOTAL_12, AllTotal) values ('" + textBoxInvoiceNumber.Text.Trim() + "', '" + dateTimePicker1.Text.Trim() + "', '" + comboBoxCustomerName.Text.Trim() + "', '" + textBoxComment.Text.Trim() + "','" + textBoxNUM_1.Text.Trim() + "','" + textBox_CAT_NUMBER_1.Text.Trim() + "','" + textBoxDESCRIPTION_1.Text.Trim() + "', '" + textBoxQUANTITY_1.Text.Trim() + "', '" + textBoxPRICE_1.Text.Trim() + "','" + textBoxTOTAL_1.Text.Trim() + "', '" + textBoxNUM_2.Text.Trim() + "','" + textBox_CAT_NUMBER_2.Text.Trim() + "','" + textBoxDESCRIPTION_2.Text.Trim() + "', '" + textBoxQUANTITY_2.Text.Trim() + "', '" + textBoxPRICE_2.Text.Trim() + "','" + textBoxTOTAL_2.Text.Trim() + "' , '" + textBoxNUM_3.Text.Trim() + "','" + textBox_CAT_NUMBER_3.Text.Trim() + "','" + textBoxDESCRIPTION_3.Text.Trim() + "', '" + textBoxQUANTITY_3.Text.Trim() + "', '" + textBoxPRICE_3.Text.Trim() + "','" + textBoxTOTAL_3.Text.Trim() + "', '" + textBoxNUM_4.Text.Trim() + "','" + textBox_CAT_NUMBER_4.Text.Trim() + "','" + textBoxDESCRIPTION_4.Text.Trim() + "', '" + textBoxQUANTITY_4.Text.Trim() + "', '" + textBoxPRICE_4.Text.Trim() + "','" + textBoxTOTAL_4.Text.Trim() + "' , '" + textBoxNUM_5.Text.Trim() + "','" + textBox_CAT_NUMBER_5.Text.Trim() + "','" + textBoxDESCRIPTION_5.Text.Trim() + "', '" + textBoxQUANTITY_5.Text.Trim() + "', '" + textBoxPRICE_5.Text.Trim() + "','" + textBoxTOTAL_5.Text.Trim() + "', '" + textBoxNUM_6.Text.Trim() + "','" + textBox_CAT_NUMBER_6.Text.Trim() + "','" + textBoxDESCRIPTION_6.Text.Trim() + "', '" + textBoxQUANTITY_6.Text.Trim() + "', '" + textBoxPRICE_6.Text.Trim() + "','" + textBoxTOTAL_6.Text.Trim() + "' , '" + textBoxNUM_7.Text.Trim() + "','" + textBox_CAT_NUMBER_7.Text.Trim() + "','" + textBoxDESCRIPTION_7.Text.Trim() + "', '" + textBoxQUANTITY_7.Text.Trim() + "', '" + textBoxPRICE_7.Text.Trim() + "','" + textBoxTOTAL_7.Text.Trim() + "', '" + textBoxNUM_8.Text.Trim() + "','" + textBox_CAT_NUMBER_8.Text.Trim() + "','" + textBoxDESCRIPTION_8.Text.Trim() + "', '" + textBoxQUANTITY_8.Text.Trim() + "', '" + textBoxPRICE_8.Text.Trim() + "','" + textBoxTOTAL_8.Text.Trim() + "', '" + textBoxNUM_9.Text.Trim() + "','" + textBox_CAT_NUMBER_9.Text.Trim() + "','" + textBoxDESCRIPTION_9.Text.Trim() + "', '" + textBoxQUANTITY_9.Text.Trim() + "', '" + textBoxPRICE_9.Text.Trim() + "','" + textBoxTOTAL_9.Text.Trim() + "', '" + textBoxNUM_10.Text.Trim() + "','" + textBox_CAT_NUMBER_10.Text.Trim() + "','" + textBoxDESCRIPTION_10.Text.Trim() + "', '" + textBoxQUANTITY_10.Text.Trim() + "', '" + textBoxPRICE_10.Text.Trim() + "','" + textBoxTOTAL_10.Text.Trim() + "', '" + textBoxNUM_11.Text.Trim() + "','" + textBox_CAT_NUMBER_11.Text.Trim() + "','" + textBoxDESCRIPTION_11.Text.Trim() + "', '" + textBoxQUANTITY_11.Text.Trim() + "', '" + textBoxPRICE_11.Text.Trim() + "','" + textBoxTOTAL_11.Text.Trim() + "', '" + textBoxNUM_12.Text.Trim() + "','" + textBox_CAT_NUMBER_12.Text.Trim() + "','" + textBoxDESCRIPTION_12.Text.Trim() + "', '" + textBoxQUANTITY_12.Text.Trim() + "', '" + textBoxPRICE_12.Text.Trim() + "','" + textBoxTOTAL_12.Text.Trim() + "', '" + textBoxAllTotal.Text.Trim() + "')");

                }
                MessageBox.Show("Done Successfully !");


            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
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
            }
        }


        public void Delete_Previous_Data_From_DataBase()
        {

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "DELETE FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();

                    dr.Close();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



        }

        public void calcAll()
        {
            double txt1Value, txt2Value, txt3Value, txt4Value, txt5Value, txt6Value, txt7Value, txt8Value, txt9Value, txt10Value, txt11Value, txt12Value, total = 0;

            double.TryParse(textBoxTOTAL_1.Text, out txt1Value);
            double.TryParse(textBoxTOTAL_2.Text, out txt2Value);
            double.TryParse(textBoxTOTAL_3.Text, out txt3Value);
            double.TryParse(textBoxTOTAL_4.Text, out txt4Value);
            double.TryParse(textBoxTOTAL_5.Text, out txt5Value);
            double.TryParse(textBoxTOTAL_6.Text, out txt6Value);
            double.TryParse(textBoxTOTAL_7.Text, out txt7Value);
            double.TryParse(textBoxTOTAL_8.Text, out txt8Value);
            double.TryParse(textBoxTOTAL_9.Text, out txt9Value);
            double.TryParse(textBoxTOTAL_10.Text, out txt10Value);
            double.TryParse(textBoxTOTAL_11.Text, out txt11Value);
            double.TryParse(textBoxTOTAL_12.Text, out txt12Value);

            total = txt1Value + txt2Value + txt3Value + txt4Value + txt5Value + txt6Value + txt7Value + txt8Value + txt9Value + txt10Value + txt11Value + txt12Value;

            textBoxAllTotal.Text = total.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBoxInvoiceNumber.Text == "" || comboBoxCustomerName.Text == "")
            {
                MessageBox.Show("Invoice Number and customer name must be writed !");
            }
            else
            {
                try
                {
                    Delete_Previous_Data_From_DataBase();
                    InsertToDataBase();
                    clearFieldsAfterDone();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        
        private void button5_Click_1(object sender, EventArgs e)
        {
            
            double txt1Value, txt2Value, total = 0;
            double.TryParse(textBoxQUANTITY_1.Text, out txt1Value);
            double.TryParse(textBoxPRICE_1.Text, out txt2Value);

            total = txt1Value * txt2Value;
            textBoxTOTAL_1.Text = total.ToString();
            calcAll();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_2.Text, out txt1Value);
            double.TryParse(textBoxPRICE_2.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_2.Text = total.ToString();
            calcAll();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_3.Text, out txt1Value);
            double.TryParse(textBoxPRICE_3.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_3.Text = total.ToString();
            calcAll();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_4.Text, out txt1Value);
            double.TryParse(textBoxPRICE_4.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_4.Text = total.ToString();
            calcAll();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_5.Text, out txt1Value);
            double.TryParse(textBoxPRICE_5.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_5.Text = total.ToString();
            calcAll();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_6.Text, out txt1Value);
            double.TryParse(textBoxPRICE_6.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_6.Text = total.ToString();
            calcAll();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_7.Text, out txt1Value);
            double.TryParse(textBoxPRICE_7.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_7.Text = total.ToString();
            calcAll();
        }

        private void button11_Click(object sender, EventArgs e)
        {
           double txt1Value, txt2Value, total= 0;
            

            double.TryParse(textBoxQUANTITY_8.Text, out txt1Value);
            double.TryParse(textBoxPRICE_8.Text, out txt2Value);
            
            total = txt1Value  * txt2Value;


            textBoxTOTAL_8.Text = total.ToString();

            calcAll();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_9.Text, out txt1Value);
            double.TryParse(textBoxPRICE_9.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_9.Text = total.ToString();

            calcAll();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_10.Text, out txt1Value);
            double.TryParse(textBoxPRICE_10.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_10.Text = total.ToString();

            calcAll();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_11.Text, out txt1Value);
            double.TryParse(textBoxPRICE_11.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_11.Text = total.ToString();
            calcAll();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            double txt1Value, txt2Value, total = 0;


            double.TryParse(textBoxQUANTITY_12.Text, out txt1Value);
            double.TryParse(textBoxPRICE_12.Text, out txt2Value);

            total = txt1Value * txt2Value;


            textBoxTOTAL_12.Text = total.ToString();
            calcAll();
        }

        
        public void Restore_data_from_data_base()
        {
            restoreDate();
            restoreCustomerName();
            restoreComment();

            restoreNUM_1();
            restoreNUM_2();
            restoreNUM_3();
            restoreNUM_4();
            restoreNUM_5();
            restoreNUM_6();
            restoreNUM_7();
            restoreNUM_8();
            restoreNUM_9();
            restoreNUM_10();
            restoreNUM_11();
            restoreNUM_12();


            restoreDESCRIPTION_1();
            restoreDESCRIPTION_2();
            restoreDESCRIPTION_3();
            restoreDESCRIPTION_4();
            restoreDESCRIPTION_5();
            restoreDESCRIPTION_6();
            restoreDESCRIPTION_7();
            restoreDESCRIPTION_8();
            restoreDESCRIPTION_9();
            restoreDESCRIPTION_10();
            restoreDESCRIPTION_11();
            restoreDESCRIPTION_12();


            restoreCAT_NUMBER_1();
            restoreCAT_NUMBER_2();
            restoreCAT_NUMBER_3();
            restoreCAT_NUMBER_4();
            restoreCAT_NUMBER_5();
            restoreCAT_NUMBER_6();
            restoreCAT_NUMBER_7();
            restoreCAT_NUMBER_8();
            restoreCAT_NUMBER_9();
            restoreCAT_NUMBER_10();
            restoreCAT_NUMBER_11();
            restoreCAT_NUMBER_12();


            restoreQUANTITY_1();
            restoreQUANTITY_2();
            restoreQUANTITY_3();
            restoreQUANTITY_4();
            restoreQUANTITY_5();
            restoreQUANTITY_5();
            restoreQUANTITY_6();
            restoreQUANTITY_7();
            restoreQUANTITY_8();
            restoreQUANTITY_9();
            restoreQUANTITY_10();
            restoreQUANTITY_11();
            restoreQUANTITY_12();


            restorePRICE_1();
            restorePRICE_2();
            restorePRICE_3();
            restorePRICE_4();
            restorePRICE_5();
            restorePRICE_6();
            restorePRICE_7();
            restorePRICE_8();
            restorePRICE_9();
            restorePRICE_10();
            restorePRICE_11();
            restorePRICE_12();


            restoreTOTAL_1();
            restoreTOTAL_2();
            restoreTOTAL_3();
            restoreTOTAL_4();
            restoreTOTAL_5();
            restoreTOTAL_6();
            restoreTOTAL_7();
            restoreTOTAL_8();
            restoreTOTAL_9();
            restoreTOTAL_10();
            restoreTOTAL_11();
            restoreTOTAL_12();

            restoreAllTotal();



        }

        public void restoreDate()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Date FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        dateTimePicker1.Text = Convert.ToString(dr[0]);
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

        public void restoreCustomerName()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Customer_Name FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxCustomerName.Text = Convert.ToString(dr[0]);
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

        public void restoreComment()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Comment FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxComment.Text = Convert.ToString(dr[0]);
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


        public void restoreNUM_1()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_1 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_1.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_2()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_2 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_2.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_3()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_3 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_3.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_4()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_4 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_4.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_5()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_5 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_5.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_6()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_6 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_6.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_7()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_7 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_7.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_8()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_8 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_8.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_9()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_9 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_9.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_10()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_10 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_10.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_11()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_11 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_11.Text = Convert.ToString(dr[0]);
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

        public void restoreNUM_12()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT NUM_12 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxNUM_12.Text = Convert.ToString(dr[0]);
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


        public void restoreCAT_NUMBER_1()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_1 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_1.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_2()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_2 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_2.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_3()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_3 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_3.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_4()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_4 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_4.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_5()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_5 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_5.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_6()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_6 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_6.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_7()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_7 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_7.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_8()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_8 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_8.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_9()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_9 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_9.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_10()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_10 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_10.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_11()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_11 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_11.Text = Convert.ToString(dr[0]);
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

        public void restoreCAT_NUMBER_12()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CAT_NUMBER_12 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBox_CAT_NUMBER_12.Text = Convert.ToString(dr[0]);
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


        public void restoreDESCRIPTION_1()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_1 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_1.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_2()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_2 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_2.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_3()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_3 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_3.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_4()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_4 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_4.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_5()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_5 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_5.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_6()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_6 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_6.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_7()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_7 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_7.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_8()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_8 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_8.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_9()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_9 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_9.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_10()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_10 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_10.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_11()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_11 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_11.Text = Convert.ToString(dr[0]);
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

        public void restoreDESCRIPTION_12()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DESCRIPTION_12 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxDESCRIPTION_12.Text = Convert.ToString(dr[0]);
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


        public void restoreQUANTITY_1()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_1 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_1.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_2()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_2 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_2.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_3()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_3 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_3.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_4()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_4 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_4.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_5()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_5 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_5.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_6()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_6 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_6.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_7()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_7 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_7.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_8()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_8 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_8.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_9()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_9 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_9.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_10()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_10 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_10.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_11()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_11 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_11.Text = Convert.ToString(dr[0]);
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

        public void restoreQUANTITY_12()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT QUANTITY_12 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxQUANTITY_12.Text = Convert.ToString(dr[0]);
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


        public void restorePRICE_1()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_1 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_1.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_2()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_2 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_2.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_3()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_3 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_3.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_4()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_4 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_4.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_5()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_5 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_5.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_6()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_6 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_6.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_7()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_7 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_7.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_8()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_8 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_8.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_9()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_9 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_9.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_10()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_10 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_10.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_11()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_11 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_11.Text = Convert.ToString(dr[0]);
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

        public void restorePRICE_12()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT PRICE_12 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxPRICE_12.Text = Convert.ToString(dr[0]);
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


        public void restoreTOTAL_1()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_1 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_1.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_2()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_2 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_2.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_3()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_3 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_3.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_4()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_4 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_4.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_5()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_5 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_5.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_6()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_6 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_6.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_7()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_7 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_7.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_8()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_8 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_8.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_9()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_9 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_9.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_10()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_10 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_10.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_11()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_11 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_11.Text = Convert.ToString(dr[0]);
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

        public void restoreTOTAL_12()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT TOTAL_12 FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxTOTAL_12.Text = Convert.ToString(dr[0]);
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
        
        public void restoreAllTotal()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT AllTotal FROM INVOICE WHERE Invoice_Number = '" + InvoiceNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxAllTotal.Text = Convert.ToString(dr[0]);
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


        private void button17_Click(object sender, EventArgs e)
        {
            
            if (textBoxInvoiceNumber.Text != "")
            {
                try
                {
                    Restore_data_from_data_base();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invoice Number must be writed");
            }
        }
        

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            pi.FileName = @"P:\Archive\HELP UTC TESTS\Help.docx";
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

        public void LOAD_Invoice_NUMBERS_TO_COMBO_BOX()
        {

            /////load the names to combobox
            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Invoice_Number FROM INVOICE";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        textBoxInvoiceNumber.Items.Add(dr["Invoice_Number"]);
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



        private void Invoices_FORM_Load_1(object sender, EventArgs e)
        {
            LOAD_Invoice_NUMBERS_TO_COMBO_BOX();
            Load_Customers_To_ComboBox();
        }
    }
}
