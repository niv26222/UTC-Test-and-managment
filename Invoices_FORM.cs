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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlServerCe;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;


namespace Project_Product_List
{
    public partial class Invoices_FORM : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;

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

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string CustomerName = comboBoxCustomerName.Text.Trim();

            cmd.CommandText = "SELECT Invoice_Number FROM[Invoice_dt]";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                textBoxInvoiceNumber.Items.Add(Convert.ToString(reader[0]));
            }

            sqlConnection1.Close();
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

        public void InsertIntoDate()
        {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("Invoice_add", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@Invoice_Number", textBoxInvoiceNumber.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Date", dateTimePicker1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Customer_Name", comboBoxCustomerName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Comment", textBoxComment.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_1", textBoxNUM_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_1", textBox_CAT_NUMBER_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_1", textBoxDESCRIPTION_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_1", textBoxQUANTITY_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_1", textBoxPRICE_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_1", textBoxTOTAL_1.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_2", textBoxNUM_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_2", textBox_CAT_NUMBER_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_2", textBoxDESCRIPTION_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_2", textBoxQUANTITY_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_2", textBoxPRICE_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_2", textBoxTOTAL_2.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_3", textBoxNUM_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_3", textBox_CAT_NUMBER_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_3", textBoxDESCRIPTION_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_3", textBoxQUANTITY_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_3", textBoxPRICE_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_3", textBoxTOTAL_3.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_4", textBoxNUM_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_4", textBox_CAT_NUMBER_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_4", textBoxDESCRIPTION_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_4", textBoxQUANTITY_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_4", textBoxPRICE_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_4", textBoxTOTAL_4.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_5", textBoxNUM_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_5", textBox_CAT_NUMBER_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_5", textBoxDESCRIPTION_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_5", textBoxQUANTITY_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_5", textBoxPRICE_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_5", textBoxTOTAL_5.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_6", textBoxNUM_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_6", textBox_CAT_NUMBER_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_6", textBoxDESCRIPTION_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_6", textBoxQUANTITY_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_6", textBoxPRICE_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_6", textBoxTOTAL_6.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_7", textBoxNUM_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_7", textBox_CAT_NUMBER_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_7", textBoxDESCRIPTION_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_7", textBoxQUANTITY_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_7", textBoxPRICE_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_7", textBoxTOTAL_7.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_8", textBoxNUM_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_8", textBox_CAT_NUMBER_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_8", textBoxDESCRIPTION_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_8", textBoxQUANTITY_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_8", textBoxPRICE_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_8", textBoxTOTAL_8.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_9", textBoxNUM_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_9", textBox_CAT_NUMBER_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_9", textBoxDESCRIPTION_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_9", textBoxQUANTITY_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_9", textBoxPRICE_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_9", textBoxTOTAL_9.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_10", textBoxNUM_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_10", textBox_CAT_NUMBER_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_10", textBoxDESCRIPTION_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_10", textBoxQUANTITY_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_10", textBoxPRICE_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_10", textBoxTOTAL_10.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_11", textBoxNUM_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_11", textBox_CAT_NUMBER_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_11", textBoxDESCRIPTION_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_11", textBoxQUANTITY_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_11", textBoxPRICE_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_11", textBoxTOTAL_11.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@NUM_12", textBoxNUM_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CAT_NUMBER_12", textBox_CAT_NUMBER_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_12", textBoxDESCRIPTION_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QUANTITY_12", textBoxQUANTITY_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_12", textBoxPRICE_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TOTAL_12", textBoxTOTAL_12.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@AllTotal", textBoxAllTotal.Text.Trim());


                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();


                    MessageBox.Show("Done Successfully !");

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
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            cmd.CommandText = "DELETE FROM Invoice_dt WHERE Invoice_Number = '" + InvoiceNumber + "';";
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
                MessageBox.Show("Invoice Number must be writed !");
            }
            else
            {
                try
                {
                    Delete_Previous_Data_From_DataBase();
                    InsertIntoDate();
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
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Date FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePicker1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCustomerName()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Customer_Name FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxCustomerName.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreComment()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Comment FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxComment.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreNUM_1()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_1 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_2()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_2 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_3()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_3 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_4()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_4 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_5()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_5 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_6()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_6 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_7()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_7 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_8()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_8 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_9()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_9 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_10()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_10 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_11()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_11 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreNUM_12()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NUM_12 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxNUM_12.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreCAT_NUMBER_1()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_1 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_2()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_2 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_3()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_3 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_4()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_4 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_5()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_5 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_6()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_6 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_7()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_7 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_8()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_8 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_9()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_9 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_10()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_10 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_11()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_11 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCAT_NUMBER_12()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CAT_NUMBER_12 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBox_CAT_NUMBER_12.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreDESCRIPTION_1()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_1 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_2()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_2 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_3()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_3 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_4()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_4 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_5()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_5 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_6()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_6 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_7()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_7 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_8()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_8 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_9()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_9 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_10()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_10 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_11()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_11 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_12()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_12 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_12.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreQUANTITY_1()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_1 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_2()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_2 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_3()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_3 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_4()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_4 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_5()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_5 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_6()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_6 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_7()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_7 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_8()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_8 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_9()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_9 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_10()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_10 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_11()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_11 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreQUANTITY_12()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QUANTITY_12 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQUANTITY_12.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restorePRICE_1()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_1 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_2()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_2 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_3()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_3 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_4()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_4 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_5()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_5 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_5.Text = rd[0].ToString();
            }
            con.Close();



        }

        public void restorePRICE_6()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_6 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_7()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_7 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_8()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_8 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_9()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_9 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_10()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_10 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_11()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_11 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_12()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_12 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_12.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreTOTAL_1()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_1 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_2()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_2 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_3()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_3 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_4()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_4 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_5()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_5 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_6()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_6 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_7()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_7 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_8()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_8 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_9()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_9 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_10()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_10 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_11()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_11 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_12()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_12 FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_12.Text = rd[0].ToString();
            }
            con.Close();
        }
        
        public void restoreAllTotal()
        {
            string InvoiceNumber = textBoxInvoiceNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AllTotal FROM [Invoice_dt] WHERE Invoice_Number = '" + InvoiceNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxAllTotal.Text = rd[0].ToString();
            }
            con.Close();
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

        public void LOAD_Invoice_NUMBERS_TO_COMBO_BOX()
        {
            /////load the names to combobox

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT Invoice_Number FROM[Invoice_dt]";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                textBoxInvoiceNumber.Items.Add(Convert.ToString(reader[0]));
            }

            sqlConnection1.Close();
        }



        private void Invoices_FORM_Load_1(object sender, EventArgs e)
        {
            LOAD_Invoice_NUMBERS_TO_COMBO_BOX();
            Load_Customers_To_ComboBox();
        }
    }
}
