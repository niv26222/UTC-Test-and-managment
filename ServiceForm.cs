using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace Project_Product_List
{
    
    public partial class ServiceForm : Form
    {
        string ConnectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;

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

        public void LOAD_Customers_TO_ComboBox()
        {
            /////load the names to combobox.
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string CustomerName = comboBoxSoldTo.Text.Trim();

                cmd.CommandText = "SELECT Customer_name FROM[Customers_dt]";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxSoldTo.Items.Add(Convert.ToString(reader[0]));
                }
                sqlConnection1.Close();
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
                SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;


                cmd.CommandText = "SELECT RMA_Number FROM[RMA_dt] WHERE deviceBeenFixed = '" + 0 + "'; ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;


                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxRMA_NUMBER.Items.Add(Convert.ToString(reader[0]));
                }

                sqlConnection1.Close();
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }

        public void LOAD_RMA_DATE_TO_DATE_TIME_PICKER_INVOICE_DATE()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DateCreate FROM [RMA_dt] WHERE RMA_Number = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePickerINVOICE_DATE.Text = rd[0].ToString();
            }
        }


        public void LOAD_RMA_DATE_TO_DATE_TIME_PICKER_SERVICE_DATE()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SERVICE_DATE FROM [ServiceForm_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePickerSERVICE_DATE.Text = rd[0].ToString();
            }
        }



        public void LOAD_PRODUCT_TO_TEXT_BOX()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Description1 FROM [RMA_dt] WHERE RMA_Number = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMODEL.Text = rd[0].ToString();
            }
        }

        public void LOAD_SERIAL_NUMBER_TO_TEXT_BOX()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SerialNumber1 FROM [RMA_dt] WHERE RMA_Number = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                textBoxSerial.Text = rd[0].ToString();
            }
        }

        public void LOAD_CUSTOMER_NAME_TO_ComboBox()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Customer_Name FROM [RMA_dt] WHERE RMA_Number = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();


            while (rd.Read())
            {
                comboBoxSoldTo.Text = Convert.ToString(rd[0]);
            }

        }

        public void Save_into_temporary_dataBase()
        {

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("ServiceForm_TEMPORARY_add", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;



                    sqlCmd.Parameters.AddWithValue("@SOLD_TO", comboBoxSoldTo.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@SERVICE_AT", comboBoxSERVICED_AT.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RMA", comboBoxRMA_NUMBER.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MODEL", textBoxMODEL.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@SERIAL", textBoxSerial.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@INVOICE_DATE", dateTimePickerINVOICE_DATE.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@SERVICE_DATE", dateTimePickerSERVICE_DATE.Text.Trim());


                    sqlCmd.Parameters.AddWithValue("@QTY_1", textBoxQTY_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_1", textBoxDESCRIPTION_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_1", textBoxPRICE_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_1", textBoxPartUsedAMOUNT_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_1", textBoxMAN_PAN_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_1", textBoxPART_REFERENCE_1.Text.Trim());


                    sqlCmd.Parameters.AddWithValue("@QTY_2", textBoxQTY_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_2", textBoxDESCRIPTION_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_2", textBoxPRICE_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_2", textBoxPartUsedAMOUNT_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_2", textBoxMAN_PAN_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_2", textBoxPART_REFERENCE_2.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_3", textBoxQTY_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_3", textBoxDESCRIPTION_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_3", textBoxPRICE_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_3", textBoxPartUsedAMOUNT_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_3", textBoxMAN_PAN_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_3", textBoxPART_REFERENCE_3.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_4", textBoxQTY_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_4", textBoxDESCRIPTION_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_4", textBoxPRICE_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_4", textBoxPartUsedAMOUNT_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_4", textBoxMAN_PAN_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_4", textBoxPART_REFERENCE_4.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_5", textBoxQTY_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_5", textBoxDESCRIPTION_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_5", textBoxPRICE_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_5", textBoxPartUsedAMOUNT_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_5", textBoxMAN_PAN_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_5", textBoxPART_REFERENCE_5.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_6", textBoxQTY_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_6", textBoxDESCRIPTION_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_6", textBoxPRICE_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_6", textBoxPartUsedAMOUNT_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_6", textBoxMAN_PAN_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_6", textBoxPART_REFERENCE_6.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_7", textBoxQTY_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_7", textBoxDESCRIPTION_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_7", textBoxPRICE_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_7", textBoxPartUsedAMOUNT_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_7", textBoxMAN_PAN_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_7", textBoxPART_REFERENCE_7.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_8", textBoxQTY_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_8", textBoxDESCRIPTION_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_8", textBoxPRICE_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_8", textBoxPartUsedAMOUNT_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_8", textBoxMAN_PAN_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_8", textBoxPART_REFERENCE_8.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_9", textBoxQTY_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_9", textBoxDESCRIPTION_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_9", textBoxPRICE_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_9", textBoxPartUsedAMOUNT_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_9", textBoxMAN_PAN_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_9", textBoxPART_REFERENCE_9.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_10", textBoxQTY_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_10", textBoxDESCRIPTION_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_10", textBoxPRICE_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_10", textBoxPartUsedAMOUNT_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_10", textBoxMAN_PAN_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_10", textBoxPART_REFERENCE_10.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_11", textBoxQTY_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_11", textBoxDESCRIPTION_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_11", textBoxPRICE_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_11", textBoxPartUsedAMOUNT_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_11", textBoxMAN_PAN_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_11", textBoxPART_REFERENCE_11.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_12", textBoxQTY_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_12", textBoxDESCRIPTION_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_12", textBoxPRICE_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_12", textBoxPartUsedAMOUNT_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_12", textBoxMAN_PAN_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_12", textBoxPART_REFERENCE_12.Text.Trim());


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    sqlCmd.Parameters.AddWithValue("@TOTAL_PRICE", textBoxTOTAL_PRICE.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_1", textBoxSERVICE_PERSON_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_DATE_1", dateTimePickerDATE_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_HOURS_1", textBoxHOURS_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_RATES_1", textBoxRATES_1.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_2", textBoxSERVICE_PERSON_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_DATE_2", dateTimePickerDATE_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_HOURS_2", textBoxHOURS_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_RATES_2", textBoxRATES_2.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_3", textBoxSERVICE_PERSON_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_DATE_3", dateTimePickerDATE_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_HOURS_3", textBoxHOURS_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_RATES_3", textBoxRATES_3.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@COMMENTS", textBoxCOMMENTS.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@SERVICE_SUPERVISED_BY", textBoxSERVICE_SUPERVISED_BY.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@SIGNED", textBoxSIGNED.Text.Trim());

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
            string RMA = comboBoxRMA_NUMBER.Text.Trim();

            cmd.CommandText = "DELETE FROM ServiceForm_TEMPORARY_dt WHERE RMA = '" + RMA + "';";
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


        public void restoreServiceFormData(string dataBaseName, TextBox textboxName, string dataBaseTableName)
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();


            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT  " + dataBaseName + " FROM " + dataBaseTableName + " WHERE RMA = '" + RMA_NUMBER + "';";//" + dataBaseTableName + "
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textboxName.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMODEL()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MODEL FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMODEL.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSERIAL()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SERIAL FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxSerial.Text = rd[0].ToString();
            }
            con.Close();
        }
        
        public void restoreQTY_1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_1.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_2.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_3()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_3()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_3()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_3()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN3()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_3()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_3.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_4()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_4 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_4()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_4 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_4()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_4 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_4()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_4 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN4()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_4 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_4()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_4 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_4.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_5()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_5 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_5()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_5 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_5()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_5 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_5()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_5 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN5()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_5 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_5.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_5()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_5 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_5.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_6()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_6 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_6()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_6 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_6()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_6 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_6()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_6 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN6()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_6 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_6.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_6()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_6 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_6.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_7()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_7 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_7()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_7 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_7()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_7 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_7()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_7 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN7()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_7 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_7.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_7()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_7 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_7.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_8()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_8 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_8()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_8 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_8()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_8 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_8()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_8 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN8()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_8 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_8.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_8()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_8 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_8.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_9()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_9 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_9()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_9 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_9()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_9 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_9()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_9 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN9()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_9 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_9.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_9()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_9 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_9.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_10()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_10 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_10()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_10 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_10()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_10 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_10()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_10 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN10()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_10 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_10.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_10()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_10 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_10.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_11()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_11 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_11()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_11 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_11()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_11 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_11()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_11 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN11()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_11 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_11.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_11()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_11 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_11.Text = rd[0].ToString();
            }
            con.Close();
        }
        //
        public void restoreQTY_12()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT QTY_12 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxQTY_12.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDESCRIPTION_12()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DESCRIPTION_12 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxDESCRIPTION_12.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePRICE_12()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PRICE_12 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPRICE_12.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAMOUNT_12()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AMOUNT_12 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPartUsedAMOUNT_12.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreMAN_PN12()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MAN_PN_12 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMAN_PAN_12.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restorePART_REFERENCE_12()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT PART_REFERENCE_12 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxPART_REFERENCE_12.Text = rd[0].ToString();
            }
            con.Close();
        }
        //


        public void restoreTECHNICIAN_PERSON_1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxSERVICE_PERSON_1.Text = rd[0].ToString();
            }
            con.Close();
        }//

        public void restoreTECHNICIAN_PERSON_DATE_1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_DATE_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePickerDATE_1.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreServiceDate1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SERVICE_DATE FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePickerSERVICE_DATE.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreInvoiceDate1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT INVOICE_DATE FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePickerINVOICE_DATE.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreSoldTo()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SOLD_TO FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxSoldTo.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restorFromLoad_SoldTo()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SOLD_TO FROM [ServiceForm_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxSoldTo.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreTECHNICIAN_PERSON_HOURS_1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_HOURS_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxHOURS_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTECHNICIAN_PERSON_RATES_1()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_RATES_1 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxRATES_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTECHNICIAN_PERSON_2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxSERVICE_PERSON_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTECHNICIAN_PERSON_DATE_2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_DATE_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePickerDATE_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTECHNICIAN_PERSON_HOURS_2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_HOURS_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxHOURS_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTECHNICIAN_PERSON_RATES_2()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_RATES_2 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxRATES_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTECHNICIAN_PERSON_3()/////////////////////////////////////////////////////////////
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxSERVICE_PERSON_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTECHNICIAN_PERSON_DATE_3()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_DATE_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePickerDATE_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTECHNICIAN_PERSON_HOURS_3()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_HOURS_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxHOURS_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTECHNICIAN_PERSON_RATES_3()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TECHNICIAN_PERSON_RATES_3 FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxRATES_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCOMMENTS()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT COMMENTS FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxCOMMENTS.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSERVICE_SUPERVISED_BY()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SERVICE_SUPERVISED_BY FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxSERVICE_SUPERVISED_BY.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSIGNED()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SIGNED FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxSIGNED.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTOTAL_PRICE()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOTAL_PRICE FROM [ServiceForm_TEMPORARY_dt] WHERE RMA = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxTOTAL_PRICE.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void Done_and_Save_into_dataBase()
        {


            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("ServiceForm_add", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;


                    sqlCmd.Parameters.AddWithValue("@SOLD_TO", comboBoxSoldTo.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@SERVICE_AT", comboBoxSERVICED_AT.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RMA", comboBoxRMA_NUMBER.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MODEL", textBoxMODEL.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@SERIAL", textBoxSerial.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@INVOICE_DATE", dateTimePickerINVOICE_DATE.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@SERVICE_DATE", dateTimePickerSERVICE_DATE.Text.Trim());


                    sqlCmd.Parameters.AddWithValue("@QTY_1", textBoxQTY_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_1", textBoxDESCRIPTION_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_1", textBoxPRICE_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_1", textBoxPartUsedAMOUNT_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_1", textBoxMAN_PAN_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_1", textBoxPART_REFERENCE_1.Text.Trim());


                    sqlCmd.Parameters.AddWithValue("@QTY_2", textBoxQTY_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_2", textBoxDESCRIPTION_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_2", textBoxPRICE_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_2", textBoxPartUsedAMOUNT_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_2", textBoxMAN_PAN_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_2", textBoxPART_REFERENCE_2.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_3", textBoxQTY_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_3", textBoxDESCRIPTION_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_3", textBoxPRICE_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_3", textBoxPartUsedAMOUNT_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_3", textBoxMAN_PAN_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_3", textBoxPART_REFERENCE_3.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_4", textBoxQTY_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_4", textBoxDESCRIPTION_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_4", textBoxPRICE_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_4", textBoxPartUsedAMOUNT_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_4", textBoxMAN_PAN_4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_4", textBoxPART_REFERENCE_4.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_5", textBoxQTY_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_5", textBoxDESCRIPTION_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_5", textBoxPRICE_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_5", textBoxPartUsedAMOUNT_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_5", textBoxMAN_PAN_5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_5", textBoxPART_REFERENCE_5.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_6", textBoxQTY_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_6", textBoxDESCRIPTION_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_6", textBoxPRICE_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_6", textBoxPartUsedAMOUNT_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_6", textBoxMAN_PAN_6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_6", textBoxPART_REFERENCE_6.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_7", textBoxQTY_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_7", textBoxDESCRIPTION_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_7", textBoxPRICE_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_7", textBoxPartUsedAMOUNT_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_7", textBoxMAN_PAN_7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_7", textBoxPART_REFERENCE_7.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_8", textBoxQTY_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_8", textBoxDESCRIPTION_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_8", textBoxPRICE_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_8", textBoxPartUsedAMOUNT_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_8", textBoxMAN_PAN_8.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_8", textBoxPART_REFERENCE_8.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_9", textBoxQTY_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_9", textBoxDESCRIPTION_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_9", textBoxPRICE_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_9", textBoxPartUsedAMOUNT_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_9", textBoxMAN_PAN_9.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_9", textBoxPART_REFERENCE_9.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_10", textBoxQTY_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_10", textBoxDESCRIPTION_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_10", textBoxPRICE_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_10", textBoxPartUsedAMOUNT_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_10", textBoxMAN_PAN_10.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_10", textBoxPART_REFERENCE_10.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_11", textBoxQTY_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_11", textBoxDESCRIPTION_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_11", textBoxPRICE_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_11", textBoxPartUsedAMOUNT_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_11", textBoxMAN_PAN_11.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_11", textBoxPART_REFERENCE_11.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@QTY_12", textBoxQTY_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DESCRIPTION_12", textBoxDESCRIPTION_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PRICE_12", textBoxPRICE_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@AMOUNT_12", textBoxPartUsedAMOUNT_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MAN_PN_12", textBoxMAN_PAN_12.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@PART_REFERENCE_12", textBoxPART_REFERENCE_12.Text.Trim());


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    sqlCmd.Parameters.AddWithValue("@TOTAL_PRICE", textBoxTOTAL_PRICE.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_1", textBoxSERVICE_PERSON_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_DATE_1", dateTimePickerDATE_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_HOURS_1", textBoxHOURS_1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_RATES_1", textBoxRATES_1.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_2", textBoxSERVICE_PERSON_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_DATE_2", dateTimePickerDATE_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_HOURS_2", textBoxHOURS_2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_RATES_2", textBoxRATES_2.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_3", textBoxSERVICE_PERSON_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_DATE_3", dateTimePickerDATE_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_HOURS_3", textBoxHOURS_3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@TECHNICIAN_PERSON_RATES_3", textBoxRATES_3.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@COMMENTS", textBoxCOMMENTS.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@SERVICE_SUPERVISED_BY", textBoxSERVICE_SUPERVISED_BY.Text.Trim());

                    sqlCmd.Parameters.AddWithValue("@SIGNED", textBoxSIGNED.Text.Trim());

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close(); ///////////
                    MessageBox.Show(" Done successfully !");

                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }

        public void Restore_data_from_temporary_data_base()
        {


            restoreMODEL();
            restoreSERIAL();
            restoreQTY_1();
            restoreDESCRIPTION_1();
            restoreServiceDate1();
            restoreInvoiceDate1();
            restoreSoldTo();
            restorePRICE_1();
            restoreAMOUNT_1();
            restoreMAN_PN1();
            restorePART_REFERENCE_1();
            restoreQTY_2();
            restoreDESCRIPTION_2();
            restorePRICE_2();
            restoreAMOUNT_2();
            restoreMAN_PN2();
            restorePART_REFERENCE_2();
            restoreQTY_3();
            restoreDESCRIPTION_3();
            restorePRICE_3();
            restoreAMOUNT_3();
            restoreMAN_PN3();
            restorePART_REFERENCE_3();
            restoreQTY_4();
            restoreDESCRIPTION_4();
            restorePRICE_4();
            restoreAMOUNT_4();
            restoreMAN_PN4();
            restorePART_REFERENCE_4();
            restoreQTY_5();
            restoreDESCRIPTION_5();
            restorePRICE_5();
            restoreAMOUNT_5();
            restoreMAN_PN5();
            restorePART_REFERENCE_5();
            restoreQTY_6();
            restoreDESCRIPTION_6();
            restorePRICE_6();
            restoreAMOUNT_6();
            restoreMAN_PN6();
            restorePART_REFERENCE_6();
            restoreQTY_7();
            restoreDESCRIPTION_7();
            restorePRICE_7();
            restoreAMOUNT_7();
            restoreMAN_PN7();
            restorePART_REFERENCE_7();
            restoreQTY_8();
            restoreDESCRIPTION_8();
            restorePRICE_8();
            restoreAMOUNT_8();
            restoreMAN_PN8();
            restorePART_REFERENCE_8();
            restoreQTY_9();
            restoreDESCRIPTION_9();
            restorePRICE_9();
            restoreAMOUNT_9();
            restoreMAN_PN9();
            restorePART_REFERENCE_9();
            restoreQTY_10();
            restoreDESCRIPTION_10();
            restorePRICE_10();
            restoreAMOUNT_10();
            restoreMAN_PN10();
            restorePART_REFERENCE_10();
            restoreQTY_11();
            restoreDESCRIPTION_11();
            restorePRICE_11();
            restoreAMOUNT_11();
            restoreMAN_PN11();
            restorePART_REFERENCE_11();
            restoreQTY_12();
            restoreDESCRIPTION_12();
            restorePRICE_12();
            restoreAMOUNT_12();
            restoreMAN_PN12();
            restorePART_REFERENCE_12();

            restoreTECHNICIAN_PERSON_1();
            restoreTECHNICIAN_PERSON_DATE_1();
            restoreTECHNICIAN_PERSON_HOURS_1();
            restoreTECHNICIAN_PERSON_RATES_1();


            restoreTECHNICIAN_PERSON_2();
            restoreTECHNICIAN_PERSON_DATE_2();
            restoreTECHNICIAN_PERSON_HOURS_2();
            restoreTECHNICIAN_PERSON_RATES_2();

            restoreTECHNICIAN_PERSON_3();
            restoreTECHNICIAN_PERSON_DATE_3();
            restoreTECHNICIAN_PERSON_HOURS_3();
            restoreTECHNICIAN_PERSON_RATES_3();

            restoreCOMMENTS();
            restoreSERVICE_SUPERVISED_BY();
            restoreSIGNED();
            restoreTOTAL_PRICE();


        }

        private void restoreServiceFormData(string v1, DateTimePicker dateTimePickerDATE_1, string v2)
        {
            throw new NotImplementedException();
        }

        public void Delete_Previous_Data_From_DataBase_Preventing_duplication_of_information_in_ServiceForm_dt()
        {

            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string RMA = comboBoxRMA_NUMBER.Text.Trim();

                cmd.CommandText = "DELETE FROM ServiceForm_dt WHERE RMA = '" + RMA + "';";
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
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }


        }

        public void service_form_to_pdf()
        {
            ///////////// Creating the document  /////////////

            FontFactory.RegisterDirectories();


            int SERVICE_FORM_YEAR = dateTimePickerSERVICE_DATE.Value.Year;

            string SavePath = "U:" + @"\" + "Service form - NEW" + @"\" + SERVICE_FORM_YEAR + @"\";

            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }

            int serviceFormCounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) + 1; // Will Retrieve count of PDF files  in directry


            SavePath = "U:" + @"\" + "Service form - NEW" + @"\" + SERVICE_FORM_YEAR + @"\" + "SERVICE FORM #" + comboBoxRMA_NUMBER.Text + "_SF" + serviceFormCounter + ".pdf";



            while (File.Exists(SavePath))
            {
                serviceFormCounter += 1;
                SavePath = "U:" + @"\" + "Service form - NEW" + @"\" + SERVICE_FORM_YEAR + @"\" + "SERVICE FORM #" + comboBoxRMA_NUMBER.Text + "_SF" + serviceFormCounter + ".pdf";
            }



            Document doc = new Document(iTextSharp.text.PageSize.A4);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(SavePath, FileMode.Create));  ///////////////// ?


            doc.Open();


            /////////////////////////////////////////////////////


            // UTC LOGO 
            iTextSharp.text.Image logoJPG = iTextSharp.text.Image.GetInstance("UTC_LOGO.png");
            logoJPG.ScalePercent(10f);
            logoJPG.SetAbsolutePosition(doc.PageSize.Width - 36f - 72f, doc.PageSize.Height - 36f - 50f);

            /////////////////////////////////////////////////////


            // Signature logo        *************
            iTextSharp.text.Image SignaturePNG = iTextSharp.text.Image.GetInstance("SignatureutcPNG.png");
            SignaturePNG.ScalePercent(89f);
            SignaturePNG.SetAbsolutePosition(doc.PageSize.Width - 170f - 72f, doc.PageSize.Height - 320f - 450f);


            /////////////////////////////////////////////////////




            // Down View        *************

            iTextSharp.text.Image DownPNG = iTextSharp.text.Image.GetInstance("Down.png");
            DownPNG.ScalePercent(55f);
            DownPNG.SetAbsolutePosition(doc.PageSize.Width - 400f - (200f), doc.PageSize.Height - 930f - 50f);

            /////////////////////////////////////////////////////////


            //// Top View        *************

            iTextSharp.text.Image TopPNG = iTextSharp.text.Image.GetInstance("Top.png");
            TopPNG.ScalePercent(55f);
            TopPNG.SetAbsolutePosition(doc.PageSize.Width - 400f - (200f), doc.PageSize.Height - 270f - 50f);

            ///////////////////////////////////////////////////////



            Chunk headLine = new Chunk("Underwater Technologies Center Ltd", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLUE));



            Paragraph CNParagraph = new Paragraph("C/N 513369199");
            Chunk AddressWithLine = new Chunk("Address:\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
            AddressWithLine.SetUnderline(0.5f, -1.5f);
            Chunk TOTAL_PRICE = new Chunk("TOTAL PRICE: " + textBoxTOTAL_PRICE.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));

            Chunk COMMENT = new Chunk("COMMENT: " + textBoxCOMMENTS.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
            Chunk SERVICE_SUPERVISED_BY = new Chunk("SERVICE SUPERVISED BY: " + textBoxSERVICE_SUPERVISED_BY.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
            Chunk SIGNED = new Chunk("\nSIGNED: " + textBoxSIGNED.Text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));

            Chunk AddressParagraph = new Chunk("\n\n\n\n\n\n               8 Omarim St., Baran building,Industrial zone. P.O .Box 944, Omer, Israel 84965.\n                                                  Tel: +972-722153153 Fax: +972-86900466", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
            Paragraph To = new Paragraph("SOLD TO: " + comboBoxSoldTo.Text);
            Paragraph SERVICE_AT = new Paragraph("SERVICE AT: " + comboBoxSERVICED_AT.Text);

            Paragraph line = new Paragraph("------------------------------------------------------------------------------------------------------------");

            Chunk SERVICE_FORM = new Chunk("SERVICE FORM\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
            Chunk PARTS_USED = new Chunk("PARTS USED\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));


            Paragraph lineDown = new Paragraph("\n");





            //////////////////////   table1   ///////////////////////////////

            PdfPTable table1 = new PdfPTable(5);
            PdfPCell cell1 = new PdfPCell();
            cell1.Colspan = 5;
            cell1.HorizontalAlignment = 5;
            table1.AddCell(cell1);


            table1.AddCell("RMA - No ");
            table1.AddCell("MODEL ");
            table1.AddCell("SERIAL ");
            table1.AddCell("INVOICE DATE ");
            table1.AddCell("SERVICE DATE ");

            table1.AddCell(comboBoxRMA_NUMBER.Text);
            table1.AddCell(textBoxMODEL.Text);
            table1.AddCell(textBoxSerial.Text);
            table1.AddCell(dateTimePickerINVOICE_DATE.Value.ToString("dd-MM-yyyy"));
            table1.AddCell(dateTimePickerSERVICE_DATE.Value.ToString("dd-MM-yyyy"));
            table1.WidthPercentage = 100f;


            ////////////////////// END table1   ///////////////////////////////




            //////////////////////   table2   ///////////////////////////////

            PdfPTable table2 = new PdfPTable(6);
            PdfPCell cell2 = new PdfPCell();
            cell2.Colspan = 6;
            cell2.HorizontalAlignment = 6;
            table2.AddCell(cell2);
            table2.SetWidths(new int[] { 2, 7, 3, 3, 2, 3 });

            table2.AddCell("QTY ");
            table2.AddCell("DESCRIPTION ");
            table2.AddCell("UTC P/N ");
            table2.AddCell("PART REFERENCE ");
            table2.AddCell("PRICE ");
            table2.AddCell("AMOUNT ");


            table2.AddCell(textBoxQTY_1.Text);
            table2.AddCell(textBoxDESCRIPTION_1.Text);
            table2.AddCell(textBoxMAN_PAN_1.Text);
            table2.AddCell(textBoxPART_REFERENCE_1.Text);
            table2.AddCell(textBoxPRICE_1.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_1.Text);
            //
            table2.AddCell(textBoxQTY_2.Text);
            table2.AddCell(textBoxDESCRIPTION_2.Text);
            table2.AddCell(textBoxMAN_PAN_2.Text);
            table2.AddCell(textBoxPART_REFERENCE_2.Text);
            table2.AddCell(textBoxPRICE_2.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_2.Text);
            //
            table2.AddCell(textBoxQTY_3.Text);
            table2.AddCell(textBoxDESCRIPTION_3.Text);
            table2.AddCell(textBoxMAN_PAN_3.Text);
            table2.AddCell(textBoxPART_REFERENCE_3.Text);
            table2.AddCell(textBoxPRICE_3.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_3.Text);
            //
            table2.AddCell(textBoxQTY_4.Text);
            table2.AddCell(textBoxDESCRIPTION_4.Text);
            table2.AddCell(textBoxMAN_PAN_4.Text);
            table2.AddCell(textBoxPART_REFERENCE_4.Text);
            table2.AddCell(textBoxPRICE_4.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_4.Text);
            //
            table2.AddCell(textBoxQTY_5.Text);
            table2.AddCell(textBoxDESCRIPTION_5.Text);
            table2.AddCell(textBoxMAN_PAN_5.Text);
            table2.AddCell(textBoxPART_REFERENCE_5.Text);
            table2.AddCell(textBoxPRICE_5.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_5.Text);
            //
            table2.AddCell(textBoxQTY_6.Text);
            table2.AddCell(textBoxDESCRIPTION_6.Text);
            table2.AddCell(textBoxMAN_PAN_6.Text);
            table2.AddCell(textBoxPART_REFERENCE_6.Text);
            table2.AddCell(textBoxPRICE_6.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_6.Text);
            //
            table2.AddCell(textBoxQTY_7.Text);
            table2.AddCell(textBoxDESCRIPTION_7.Text);
            table2.AddCell(textBoxMAN_PAN_7.Text);
            table2.AddCell(textBoxPART_REFERENCE_7.Text);
            table2.AddCell(textBoxPRICE_7.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_7.Text);
            //
            table2.AddCell(textBoxQTY_8.Text);
            table2.AddCell(textBoxDESCRIPTION_8.Text);
            table2.AddCell(textBoxMAN_PAN_8.Text);
            table2.AddCell(textBoxPART_REFERENCE_8.Text);
            table2.AddCell(textBoxPRICE_8.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_8.Text);
            //
            table2.AddCell(textBoxQTY_9.Text);
            table2.AddCell(textBoxDESCRIPTION_9.Text);
            table2.AddCell(textBoxMAN_PAN_9.Text);
            table2.AddCell(textBoxPART_REFERENCE_9.Text);
            table2.AddCell(textBoxPRICE_9.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_9.Text);
            //
            table2.AddCell(textBoxQTY_10.Text);
            table2.AddCell(textBoxDESCRIPTION_10.Text);
            table2.AddCell(textBoxMAN_PAN_10.Text);
            table2.AddCell(textBoxPART_REFERENCE_10.Text);
            table2.AddCell(textBoxPRICE_10.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_10.Text);

            //
            table2.AddCell(textBoxQTY_11.Text);
            table2.AddCell(textBoxDESCRIPTION_11.Text);
            table2.AddCell(textBoxMAN_PAN_11.Text);
            table2.AddCell(textBoxPART_REFERENCE_11.Text);
            table2.AddCell(textBoxPRICE_11.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_11.Text);
            //
            table2.AddCell(textBoxQTY_12.Text);
            table2.AddCell(textBoxDESCRIPTION_12.Text);
            table2.AddCell(textBoxMAN_PAN_12.Text);
            table2.AddCell(textBoxPART_REFERENCE_12.Text);
            table2.AddCell(textBoxPRICE_12.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_12.Text);
            //

            table2.WidthPercentage = 100f;


            ////////////////////// END table2   ///////////////////////////////

            //////////////////////   table3   ///////////////////////////////

            PdfPTable table3 = new PdfPTable(4);
            PdfPCell cell3 = new PdfPCell();
            cell3.Colspan = 4;
            cell3.HorizontalAlignment = 4;
            table3.AddCell(cell3);


            table3.AddCell("SERVICE PERSON");
            table3.AddCell("DATE ");
            table3.AddCell("HOURS ");
            table3.AddCell("RATE ");


            table3.AddCell(textBoxSERVICE_PERSON_1.Text);
            if (textBoxSERVICE_PERSON_1.Text != "")
            {
                table3.AddCell(dateTimePickerDATE_1.Value.ToString("dd-MM-yyyy"));
            }

            table3.AddCell(textBoxHOURS_1.Text);
            table3.AddCell(textBoxRATES_1.Text);
            //

            table3.AddCell(textBoxSERVICE_PERSON_2.Text);
            if (textBoxSERVICE_PERSON_2.Text != "")
            {
                table3.AddCell(dateTimePickerDATE_2.Value.ToString("dd-MM-yyyy"));
            }
            table3.AddCell(textBoxHOURS_2.Text);
            table3.AddCell(textBoxRATES_2.Text);
            //

            table3.AddCell(textBoxSERVICE_PERSON_3.Text);
            if (textBoxSERVICE_PERSON_3.Text != "")
            {
                table3.AddCell(dateTimePickerDATE_3.Value.ToString("dd-MM-yyyy"));
            }

            table3.AddCell(textBoxHOURS_3.Text);
            table3.AddCell(textBoxRATES_3.Text);

            //

            if (textBoxSERVICE_PERSON_1.Text == "")
            {
                dateTimePickerDATE_1.Text = "";
            }
            if (textBoxSERVICE_PERSON_2.Text == "")
            {
                dateTimePickerDATE_2.Text = "";
            }
            if (textBoxSERVICE_PERSON_3.Text == "")
            {
                dateTimePickerDATE_3.Text = "";
            }


            table3.WidthPercentage = 100f;


            ////////////////////// END table3   ///////////////////////////////


            ////////////////////// Page design  //////////////////////


            doc.Add(TopPNG);
            doc.Add(DownPNG);
            doc.Add(SignaturePNG);
            doc.Add(logoJPG);
            doc.Add(headLine);
            doc.Add(CNParagraph);
            doc.Add(line);
            doc.Add(SERVICE_FORM);
            doc.Add(SERVICE_AT);
            doc.Add(To);
            doc.Add(line);
            doc.Add(table1);
            doc.Add(lineDown);
            doc.Add(PARTS_USED);
            doc.Add(table2);
            doc.Add(TOTAL_PRICE);
            doc.Add(lineDown);
            doc.Add(table3);
            doc.Add(lineDown);
            doc.Add(COMMENT);
            doc.Add(lineDown);
            doc.Add(SERVICE_SUPERVISED_BY);
            doc.Add(SIGNED);
            doc.Add(AddressParagraph);

            doc.Close();

            MessageBox.Show("Service form Create Successfully !");

        }

        private PdfPCell getNormalCell(string v1, object p, int v2)
        {
            throw new NotImplementedException();
        }



        public ServiceForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }



        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {

            int SERVICE_FORM_YEAR = dateTimePickerSERVICE_DATE.Value.Year;

            string SavePath = "U:" + @"\" + "Service form - NEW" + @"\" + SERVICE_FORM_YEAR + @"\";

            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }


            try
            {
                LOAD_Customers_TO_ComboBox();
                LOAD_RMA_NUMBERS_TO_COMBO_BOX();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePickerINVOICE_DATE_ValueChanged(object sender, EventArgs e)
        {

        }
        public void restoreSerialOnLoad()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SerialNumber1 FROM [RMA_dt] WHERE RMA_Number = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();
            

            while (rd.Read())
            {
                textBoxSerial.Text = rd[0].ToString();
            }



            con.Close();
        }

        public void restoreModelOnLoad()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Description1 FROM [RMA_dt] WHERE RMA_Number = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxMODEL.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSoldToOnLoad()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Customer_Name FROM [RMA_dt] WHERE RMA_Number = '" + RMA_NUMBER + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxSoldTo.Text = rd[0].ToString();
            }
            con.Close();
        }


        private void comboBoxRMA_NUMBER_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBoxSERVICED_AT.Text = "Underwater Technologies Center Ltd.";    
                LOAD_RMA_DATE_TO_DATE_TIME_PICKER_INVOICE_DATE();
                LOAD_RMA_DATE_TO_DATE_TIME_PICKER_SERVICE_DATE();
                LOAD_PRODUCT_TO_TEXT_BOX();
                restoreSerialOnLoad();
                restoreModelOnLoad();
                restoreSoldToOnLoad();
            }

            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void comboBoxSoldTo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDESCRIPTION_9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
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

        private void label111_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_Previous_Data_From_DataBaseTEMPORARY_dt();
                Save_into_temporary_dataBase();
                MessageBox.Show(" Save successfully !");
                new ChooseTestForm().Show();
                this.Hide();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
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
            pi.FileName = MyDirectory() + @"\HELP UTC TESTS\SERVICE FORM.docx";

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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_Previous_Data_From_DataBaseTEMPORARY_dt();
                Save_into_temporary_dataBase();
                MessageBox.Show(" Save successfully !");
                clearFieldsAfterDone();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you Done with this Service form ?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Delete_Previous_Data_From_DataBaseTEMPORARY_dt();
                    Delete_Previous_Data_From_DataBase_Preventing_duplication_of_information_in_ServiceForm_dt();
                    Save_into_temporary_dataBase();
                    Done_and_Save_into_dataBase();
                    service_form_to_pdf();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_ServiceForm();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }

                    }
                    MessageBox.Show("Successfully Done !");
                    updateStatusOf_RMA_As_Fixed();
                    clearFieldsAfterDone();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so Service form document will be created, but it will not be stored in the Base database");
                    service_form_to_pdf();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_ServiceForm();
                        }
                        catch (Exception Ex2)
                        {
                            MessageBox.Show(Ex2.Message);
                        }

                    }
                    MessageBox.Show("Successfully Done !");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_Previous_Data_From_DataBaseTEMPORARY_dt();
                Save_into_temporary_dataBase();
                MessageBox.Show(" Save successfully !");
                clearFieldsAfterDone();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }


        void updateStatusOf_RMA_As_Fixed()
        {

            SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string RMANumber = comboBoxRMA_NUMBER.Text.Trim();

            cmd.CommandText = "UPDATE RMA_dt SET deviceBeenFixed = '" + 1 + "'  WHERE RMA_Number = '" + RMANumber + "'; ";
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


        private void SendToPrinter_ServiceForm()
        {
            int serviceFormyear = dateTimePickerSERVICE_DATE.Value.Year;

            string SavePath = "U:" + @"\" + "Service form - NEW" + @"\" + serviceFormyear + @"\";

            int serviceFormCounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) ; // Will Retrieve count of PDF files  in directry

            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            

            info.FileName = "U:" + @"\" + "Service form - NEW" + @"\" + serviceFormyear + @"\" + "SERVICE FORM #" + comboBoxRMA_NUMBER.Text + "_SF" + serviceFormCounter + ".pdf";
            //MessageBox.Show(info.FileName);
            //return;


            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            long ticks = -1;
            while (ticks != p.TotalProcessorTime.Ticks)
            {
                ticks = p.TotalProcessorTime.Ticks;
                Thread.Sleep(1000);
            }

            if (false == p.CloseMainWindow())
            {
                p.Kill();
            }

        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you Done with this Service form ?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Delete_Previous_Data_From_DataBaseTEMPORARY_dt();
                    Delete_Previous_Data_From_DataBase_Preventing_duplication_of_information_in_ServiceForm_dt();
                    Save_into_temporary_dataBase();
                    Done_and_Save_into_dataBase();
                    service_form_to_pdf();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_ServiceForm();
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }
                        
                    }
                    MessageBox.Show("Successfully Done !");
                    updateStatusOf_RMA_As_Fixed();
                    clearFieldsAfterDone();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so Service form document will be created, but it will not be stored in the Base database");
                    service_form_to_pdf();
                    if (MessageBox.Show("Do you want to send the document to print?", "Send to printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            SendToPrinter_ServiceForm();
                        }
                        catch (Exception Ex2)
                        {
                            MessageBox.Show(Ex2.Message);
                        }
                        
                    }
                    MessageBox.Show("Successfully Done !");
                }
            }
        }


        void update_Status_Of_RMA_As_Open()
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string RMANumber = comboBoxRMA_NUMBER.Text.Trim();//   
                cmd.CommandText = "UPDATE RMA_dt SET deviceBeenFixed = '" + 0 + "'  WHERE RMA_Number = '" + RMANumber + "' and deviceBeenFixed = '" + 1 + "';";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;


                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Read();
                }

                sqlConnection1.Close();

                MessageBox.Show("The RMA is reopened.\nYou can edit the continuation again.\nPlease note that once the file is closed again, a new document will be created,\nand the previous document will be deleted.");
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                update_Status_Of_RMA_As_Open();

                //clear fields of comboBox to prevent Duplication !
                comboBoxRMA_NUMBER.Items.Clear();

                LOAD_RMA_NUMBERS_TO_COMBO_BOX();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSERVICE_SUPERVISED_BY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBoxSERVICE_SUPERVISED_BY.Text == "Niv")
            {
                textBoxSIGNED.Text = "Niv Ben Abat";
            }
            else if (textBoxSERVICE_SUPERVISED_BY.Text == "Alona")
            {
                textBoxSIGNED.Text = "Alona Moiseev";
            }
        }
    }
}
