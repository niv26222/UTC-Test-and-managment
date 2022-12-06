using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using Dapper;
using Image = iTextSharp.text.Image;

namespace Project_Product_List
{

    public partial class ServiceForm : Form
    {

        public List<string> imagesPath = new List<string>();
        string SavePath = Paths.Paths.SERVICE_FORM_PATH;

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
                        comboBoxSoldTo.Items.Add(dr["Name"]);
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

            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT RMA_Number FROM RMA WHERE deviceBeenFixed = '" + 0 + "'; ";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        comboBoxRMA_NUMBER.Items.Add(dr["RMA_Number"]);
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

        public void LOAD_RMA_DATE_TO_DATE_TIME_PICKER_INVOICE_DATE()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DateCreate FROM RMA WHERE RMA_Number = '" + RMA_NUMBER + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        dateTimePickerINVOICE_DATE.Text = Convert.ToString(dr[0]);
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


        public void LOAD_RMA_DATE_TO_DATE_TIME_PICKER_SERVICE_DATE()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SERVICE_DATE FROM SERVICE_FORM WHERE RMA = '" + RMA_NUMBER + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        dateTimePickerSERVICE_DATE.Text = Convert.ToString(dr[0]);
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

        

        public void LOAD_PRODUCT_TO_TEXT_BOX()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Description1 FROM RMA WHERE RMA_Number = '" + RMA_NUMBER + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxMODEL.Text = Convert.ToString(dr[0]);
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

        public void LOAD_SERIAL_NUMBER_TO_TEXT_BOX()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SerialNumber1 FROM RMA WHERE RMA_Number = '" + RMA_NUMBER + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxSerial.Text = Convert.ToString(dr[0]);
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

        public void LOAD_CUSTOMER_NAME_TO_ComboBox()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Customer_Name FROM RMA WHERE RMA_Number = '" + RMA_NUMBER + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxSoldTo.Text = Convert.ToString(dr[0]);
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

        public void Delete_Previous_Data_From_DataBase()
        {

            string RMA = comboBoxRMA_NUMBER.Text.Trim();

            SQLiteCommand cmd;
            SQLiteDataReader reader;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "DELETE FROM SERVICE_FORM WHERE RMA = '" + RMA + "';";
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

        public void restoreServiceFormData(string dataBaseName, TextBox textboxName, string dataBaseTableName)
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();


            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT  " + dataBaseName + " FROM " + dataBaseTableName + " WHERE RMA = '" + RMA_NUMBER + "';";//" + dataBaseTableName + "
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textboxName.Text = Convert.ToString(dr[0]);
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

 



        


        public void Done_and_Save_into_dataBase()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(General.LoadConnectionString()))
                {
                    cnn.Execute("insert into SERVICE_FORM (SOLD_TO, SERVICE_AT, RMA, MODEL, SERIAL, INVOICE_DATE, SERVICE_DATE, QTY_1, DESCRIPTION_1, PRICE_1, AMOUNT_1, MAN_PN_1, PART_REFERENCE_1, QTY_2, DESCRIPTION_2, PRICE_2, AMOUNT_2, MAN_PN_2, PART_REFERENCE_2, QTY_3, DESCRIPTION_3, PRICE_3, AMOUNT_3, MAN_PN_3, PART_REFERENCE_3, QTY_4, DESCRIPTION_4, PRICE_4, AMOUNT_4, MAN_PN_4, PART_REFERENCE_4, QTY_5, DESCRIPTION_5, PRICE_5, AMOUNT_5, MAN_PN_5, PART_REFERENCE_5, QTY_6, DESCRIPTION_6, PRICE_6, AMOUNT_6, MAN_PN_6, PART_REFERENCE_6, QTY_7, DESCRIPTION_7, PRICE_7, AMOUNT_7, MAN_PN_7, PART_REFERENCE_7, QTY_8, DESCRIPTION_8, PRICE_8, AMOUNT_8, MAN_PN_8, PART_REFERENCE_8, QTY_9, DESCRIPTION_9, PRICE_9, AMOUNT_9, MAN_PN_9, PART_REFERENCE_9, QTY_10, DESCRIPTION_10, PRICE_10, AMOUNT_10, MAN_PN_10, PART_REFERENCE_10, QTY_11, DESCRIPTION_11, PRICE_11, AMOUNT_11, MAN_PN_11, PART_REFERENCE_11, QTY_12, DESCRIPTION_12, PRICE_12, AMOUNT_12, MAN_PN_12, PART_REFERENCE_12, TOTAL_PRICE, TECHNICIAN_PERSON_1, TECHNICIAN_PERSON_DATE_1, TECHNICIAN_PERSON_HOURS_1, TECHNICIAN_PERSON_RATES_1, TECHNICIAN_PERSON_2, TECHNICIAN_PERSON_DATE_2, TECHNICIAN_PERSON_HOURS_2, TECHNICIAN_PERSON_RATES_2, TECHNICIAN_PERSON_3, TECHNICIAN_PERSON_DATE_3, TECHNICIAN_PERSON_HOURS_3, TECHNICIAN_PERSON_RATES_3, COMMENTS, SERVICE_SUPERVISED_BY, SIGNED, REASON1, REASON2, REASON3, REASON4, REASON5, REASON6, REASON7, REASON8, REASON9, REASON10, REASON11, REASON12 ) values ('" + comboBoxSoldTo.Text.Trim() + "', '" + comboBoxSERVICED_AT.Text.Trim() + "', '" + comboBoxRMA_NUMBER.Text.Trim() + "', '" + textBoxMODEL.Text.Trim() + "', '" + textBoxSerial.Text.Trim() + "', '" + dateTimePickerINVOICE_DATE.Text.Trim() + "', '" + dateTimePickerSERVICE_DATE.Text.Trim() + "', '" + textBoxQTY_1.Text.Trim() + "', '" + textBoxDESCRIPTION_1.Text.Trim() + "', '" + textBoxPRICE_1.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_1.Text.Trim() + "', '" + textBoxMAN_PAN_1.Text.Trim() + "', '" + textBoxPART_REFERENCE_1.Text.Trim() + "', '" + textBoxQTY_2.Text.Trim() + "', '" + textBoxDESCRIPTION_2.Text.Trim() + "', '" + textBoxPRICE_2.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_2.Text.Trim() + "', '" + textBoxMAN_PAN_2.Text.Trim() + "', '" + textBoxPART_REFERENCE_2.Text.Trim() + "', '" + textBoxQTY_3.Text.Trim() + "', '" + textBoxDESCRIPTION_3.Text.Trim() + "', '" + textBoxPRICE_3.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_3.Text.Trim() + "', '" + textBoxMAN_PAN_3.Text.Trim() + "', '" + textBoxPART_REFERENCE_3.Text.Trim() + "', '" + textBoxQTY_4.Text.Trim() + "', '" + textBoxDESCRIPTION_4.Text.Trim() + "', '" + textBoxPRICE_4.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_4.Text.Trim() + "', '" + textBoxMAN_PAN_4.Text.Trim() + "', '" + textBoxPART_REFERENCE_4.Text.Trim() + "', '" + textBoxQTY_5.Text.Trim() + "', '" + textBoxDESCRIPTION_5.Text.Trim() + "', '" + textBoxPRICE_5.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_5.Text.Trim() + "', '" + textBoxMAN_PAN_5.Text.Trim() + "', '" + textBoxPART_REFERENCE_5.Text.Trim() + "', '" + textBoxQTY_6.Text.Trim() + "', '" + textBoxDESCRIPTION_6.Text.Trim() + "', '" + textBoxPRICE_6.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_6.Text.Trim() + "', '" + textBoxMAN_PAN_6.Text.Trim() + "', '" + textBoxPART_REFERENCE_6.Text.Trim() + "', '" + textBoxQTY_7.Text.Trim() + "', '" + textBoxDESCRIPTION_7.Text.Trim() + "', '" + textBoxPRICE_7.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_7.Text.Trim() + "', '" + textBoxMAN_PAN_7.Text.Trim() + "', '" + textBoxPART_REFERENCE_7.Text.Trim() + "', '" + textBoxQTY_8.Text.Trim() + "', '" + textBoxDESCRIPTION_8.Text.Trim() + "', '" + textBoxPRICE_8.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_8.Text.Trim() + "', '" + textBoxMAN_PAN_8.Text.Trim() + "', '" + textBoxPART_REFERENCE_8.Text.Trim() + "', '" + textBoxQTY_9.Text.Trim() + "', '" + textBoxDESCRIPTION_9.Text.Trim() + "', '" + textBoxPRICE_9.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_9.Text.Trim() + "', '" + textBoxMAN_PAN_9.Text.Trim() + "', '" + textBoxPART_REFERENCE_9.Text.Trim() + "', '" + textBoxQTY_10.Text.Trim() + "', '" + textBoxDESCRIPTION_10.Text.Trim() + "', '" + textBoxPRICE_10.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_10.Text.Trim() + "', '" + textBoxMAN_PAN_10.Text.Trim() + "', '" + textBoxPART_REFERENCE_10.Text.Trim() + "', '" + textBoxQTY_11.Text.Trim() + "', '" + textBoxDESCRIPTION_11.Text.Trim() + "', '" + textBoxPRICE_11.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_11.Text.Trim() + "', '" + textBoxMAN_PAN_11.Text.Trim() + "', '" + textBoxPART_REFERENCE_11.Text.Trim() + "', '" + textBoxQTY_12.Text.Trim() + "', '" + textBoxDESCRIPTION_12.Text.Trim() + "', '" + textBoxPRICE_12.Text.Trim() + "', '" + textBoxPartUsedAMOUNT_12.Text.Trim() + "', '" + textBoxMAN_PAN_12.Text.Trim() + "', '" + textBoxPART_REFERENCE_12.Text.Trim() + "', '" + textBoxTOTAL_PRICE.Text.Trim() + "', '" + textBoxSERVICE_PERSON_1.Text.Trim() + "', '" + dateTimePickerDATE_1.Text.Trim() + "', '" + textBoxHOURS_1.Text.Trim() + "', '" + textBoxRATES_1.Text.Trim() + "', '" + textBoxSERVICE_PERSON_2.Text.Trim() + "', '" + dateTimePickerDATE_2.Text.Trim() + "', '" + textBoxHOURS_2.Text.Trim() + "', '" + textBoxRATES_2.Text.Trim() + "', '" + textBoxSERVICE_PERSON_3.Text.Trim() + "', '" + dateTimePickerDATE_3.Text.Trim() + "', '" + textBoxHOURS_3.Text.Trim() + "', '" + textBoxRATES_3.Text.Trim() + "', '" + textBoxCOMMENTS.Text.Trim() + "', '" + textBoxSERVICE_SUPERVISED_BY.Text.Trim() + "', '" + textBoxSIGNED.Text.Trim() + "', '" + textBoxREASON_1.Text.Trim() + "', '" + textBoxREASON_2.Text.Trim() + "', '" + textBoxREASON_3.Text.Trim() + "', '" + textBoxREASON_4.Text.Trim() + "', '" + textBoxREASON_5.Text.Trim() + "', '" + textBoxREASON_6.Text.Trim() + "', '" + textBoxREASON_7.Text.Trim() + "', '" + textBoxREASON_8.Text.Trim() + "', '" + textBoxREASON_9.Text.Trim() + "', '" + textBoxREASON_10.Text.Trim() + "', '" + textBoxREASON_11.Text.Trim() + "', '" + textBoxREASON_12.Text.Trim() + "')");
                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }




        public void Restore_data_from_temporary_data_base()
        {
            General.ActOnDb("SELECT", "MODEL", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMODEL);
            General.ActOnDb("SELECT", "SERIAL", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxSerial);
            General.ActOnDb("SELECT", "SERVICE_DATE", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), dateTimePickerSERVICE_DATE);
            General.ActOnDb("SELECT", "INVOICE_DATE", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), dateTimePickerINVOICE_DATE);
            General.ActOnDb("SELECT", "SOLD_TO", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), comboBoxSoldTo);
            General.ActOnDb("SELECT", "QTY_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_1);
            General.ActOnDb("SELECT", "DESCRIPTION_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_1);
            General.ActOnDb("SELECT", "PRICE_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_1);
            General.ActOnDb("SELECT", "AMOUNT_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_1);
            General.ActOnDb("SELECT", "MAN_PN_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_1);
            General.ActOnDb("SELECT", "PART_REFERENCE_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_1);
            General.ActOnDb("SELECT", "QTY_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_2);
            General.ActOnDb("SELECT", "DESCRIPTION_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_2);
            General.ActOnDb("SELECT", "PRICE_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_2);
            General.ActOnDb("SELECT", "AMOUNT_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_2);
            General.ActOnDb("SELECT", "MAN_PN_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_2);
            General.ActOnDb("SELECT", "PART_REFERENCE_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_2);
            General.ActOnDb("SELECT", "QTY_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_3);
            General.ActOnDb("SELECT", "DESCRIPTION_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_3);
            General.ActOnDb("SELECT", "PRICE_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_2);
            General.ActOnDb("SELECT", "AMOUNT_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_3);
            General.ActOnDb("SELECT", "MAN_PN_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_3);
            General.ActOnDb("SELECT", "PART_REFERENCE_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_3);
            General.ActOnDb("SELECT", "QTY_4", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_4);
            General.ActOnDb("SELECT", "DESCRIPTION_4", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_4);
            General.ActOnDb("SELECT", "PRICE_4", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_4);
            General.ActOnDb("SELECT", "AMOUNT_4", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_4);
            General.ActOnDb("SELECT", "MAN_PN_4", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_4);
            General.ActOnDb("SELECT", "PART_REFERENCE_4", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_4);
            General.ActOnDb("SELECT", "QTY_5", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_5);
            General.ActOnDb("SELECT", "DESCRIPTION_5", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_5);
            General.ActOnDb("SELECT", "PRICE_5", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_5);
            General.ActOnDb("SELECT", "AMOUNT_5", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_5);
            General.ActOnDb("SELECT", "MAN_PN_5", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_5);
            General.ActOnDb("SELECT", "PART_REFERENCE_5", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_5);
            General.ActOnDb("SELECT", "QTY_6", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_6);
            General.ActOnDb("SELECT", "DESCRIPTION_6", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_6);
            General.ActOnDb("SELECT", "PRICE_6", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_6);
            General.ActOnDb("SELECT", "AMOUNT_6", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_6);
            General.ActOnDb("SELECT", "MAN_PN_6", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_6);
            General.ActOnDb("SELECT", "PART_REFERENCE_6", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_6);
            General.ActOnDb("SELECT", "QTY_7", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_7);
            General.ActOnDb("SELECT", "DESCRIPTION_7", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_7);
            General.ActOnDb("SELECT", "PRICE_7", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_7);
            General.ActOnDb("SELECT", "AMOUNT_7", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_7);
            General.ActOnDb("SELECT", "MAN_PN_7", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_7);
            General.ActOnDb("SELECT", "PART_REFERENCE_7", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_7);
            General.ActOnDb("SELECT", "QTY_8", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_8);
            General.ActOnDb("SELECT", "DESCRIPTION_8", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_8);
            General.ActOnDb("SELECT", "PRICE_8", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_8);
            General.ActOnDb("SELECT", "AMOUNT_8", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_8);
            General.ActOnDb("SELECT", "MAN_PN_8", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_8);
            General.ActOnDb("SELECT", "PART_REFERENCE_8", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_8);
            General.ActOnDb("SELECT", "QTY_9", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_9);
            General.ActOnDb("SELECT", "DESCRIPTION_9", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_9);
            General.ActOnDb("SELECT", "PRICE_9", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_9);
            General.ActOnDb("SELECT", "AMOUNT_9", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_9);
            General.ActOnDb("SELECT", "MAN_PN_9", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_9);
            General.ActOnDb("SELECT", "PART_REFERENCE_9", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_9);
            General.ActOnDb("SELECT", "QTY_10", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_10);
            General.ActOnDb("SELECT", "DESCRIPTION_10", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_10);
            General.ActOnDb("SELECT", "PRICE_10", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_10);
            General.ActOnDb("SELECT", "AMOUNT_10", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_10);
            General.ActOnDb("SELECT", "MAN_PN_10", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_10);
            General.ActOnDb("SELECT", "PART_REFERENCE_10", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_10);
            General.ActOnDb("SELECT", "QTY_11", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_11);
            General.ActOnDb("SELECT", "DESCRIPTION_11", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_11);
            General.ActOnDb("SELECT", "PRICE_11", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_11);
            General.ActOnDb("SELECT", "AMOUNT_11", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_11);
            General.ActOnDb("SELECT", "MAN_PN_11", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_11);
            General.ActOnDb("SELECT", "PART_REFERENCE_11", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_11);
            General.ActOnDb("SELECT", "QTY_12", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxQTY_12);
            General.ActOnDb("SELECT", "DESCRIPTION_12", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxDESCRIPTION_12);
            General.ActOnDb("SELECT", "PRICE_12", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPRICE_12);
            General.ActOnDb("SELECT", "AMOUNT_12", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPartUsedAMOUNT_12);
            General.ActOnDb("SELECT", "MAN_PN_12", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxMAN_PAN_12);
            General.ActOnDb("SELECT", "PART_REFERENCE_12", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxPART_REFERENCE_12);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxSERVICE_PERSON_1);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_DATE_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), dateTimePickerDATE_1);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_HOURS_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxHOURS_1);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_RATES_1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxRATES_1);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxSERVICE_PERSON_2);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_DATE_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), dateTimePickerDATE_2);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_HOURS_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxHOURS_2);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_RATES_2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxRATES_2);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxSERVICE_PERSON_3);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_DATE_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), dateTimePickerDATE_3);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_HOURS_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxHOURS_3);
            General.ActOnDb("SELECT", "TECHNICIAN_PERSON_RATES_3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxRATES_3);
            General.ActOnDb("SELECT", "COMMENTS", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxCOMMENTS);
            General.ActOnDb("SELECT", "SERVICE_SUPERVISED_BY", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxSERVICE_SUPERVISED_BY);
            General.ActOnDb("SELECT", "SIGNED", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxSIGNED);
            General.ActOnDb("SELECT", "TOTAL_PRICE", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxTOTAL_PRICE);
            General.ActOnDb("SELECT", "REASON1", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_1);
            General.ActOnDb("SELECT", "REASON2", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_2);
            General.ActOnDb("SELECT", "REASON3", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_3);
            General.ActOnDb("SELECT", "REASON4", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_4);
            General.ActOnDb("SELECT", "REASON5", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_5);
            General.ActOnDb("SELECT", "REASON6", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_6);
            General.ActOnDb("SELECT", "REASON7", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_7);
            General.ActOnDb("SELECT", "REASON8", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_8);
            General.ActOnDb("SELECT", "REASON9", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_9);
            General.ActOnDb("SELECT", "REASON10", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_10);
            General.ActOnDb("SELECT", "REASON11", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_11);
            General.ActOnDb("SELECT", "REASON12", "SERVICE_FORM", "RMA", comboBoxRMA_NUMBER.Text.Trim(), textBoxREASON_12);

        }




        private void restoreServiceFormData(string v1, DateTimePicker dateTimePickerDATE_1, string v2)
        {
            throw new NotImplementedException();
        }

        
        public void PDFCreator()
        {
            ///////////// Creating the document  /////////////

            FontFactory.RegisterDirectories();

            int CURRENT_YEAR = DateTime.Now.Year;
            int SERVICE_FORM_YEAR = dateTimePickerSERVICE_DATE.Value.Year;


            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }

            int serviceFormCounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length) + 1; // Will Retrieve count of PDF files  in directry


            string SavePathNew = SavePath + "SERVICE FORM #" + comboBoxRMA_NUMBER.Text + "_SF" + serviceFormCounter + ".pdf";



            while (File.Exists(SavePathNew))
            {
                serviceFormCounter += 1;
                SavePathNew = SavePath + "SERVICE FORM #" + comboBoxRMA_NUMBER.Text + "_SF" + serviceFormCounter + ".pdf";
            }



            Document doc = new Document(iTextSharp.text.PageSize.A4);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(SavePathNew, FileMode.Create));  ///////////////// ?


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

            PdfPTable table2 = new PdfPTable(7);
            PdfPCell cell2 = new PdfPCell();
            cell2.Colspan = 7;
            cell2.HorizontalAlignment = 7;
            table2.AddCell(cell2);
            table2.SetWidths(new int[] { 2, 4, 3, 3, 4, 2, 3 });

            table2.AddCell("QTY ");
            table2.AddCell("DESCRIPTION ");
            table2.AddCell("REASON ");
            table2.AddCell("UTC P/N ");
            table2.AddCell("PART REFERENCE ");
            table2.AddCell("PRICE ");
            table2.AddCell("AMOUNT ");


            table2.AddCell(textBoxQTY_1.Text);
            table2.AddCell(textBoxDESCRIPTION_1.Text);
            table2.AddCell(textBoxREASON_1.Text);
            table2.AddCell(textBoxMAN_PAN_1.Text);
            table2.AddCell(textBoxPART_REFERENCE_1.Text);
            table2.AddCell(textBoxPRICE_1.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_1.Text);
            //
            table2.AddCell(textBoxQTY_2.Text);
            table2.AddCell(textBoxDESCRIPTION_2.Text);
            table2.AddCell(textBoxREASON_2.Text);
            table2.AddCell(textBoxMAN_PAN_2.Text);
            table2.AddCell(textBoxPART_REFERENCE_2.Text);
            table2.AddCell(textBoxPRICE_2.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_2.Text);
            

            //
            table2.AddCell(textBoxQTY_3.Text);
            table2.AddCell(textBoxDESCRIPTION_3.Text);
            table2.AddCell(textBoxREASON_3.Text);
            table2.AddCell(textBoxMAN_PAN_3.Text);
            table2.AddCell(textBoxPART_REFERENCE_3.Text);
            table2.AddCell(textBoxPRICE_3.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_3.Text);
            

            //
            table2.AddCell(textBoxQTY_4.Text);
            table2.AddCell(textBoxDESCRIPTION_4.Text);
            table2.AddCell(textBoxREASON_4.Text);
            table2.AddCell(textBoxMAN_PAN_4.Text);
            table2.AddCell(textBoxPART_REFERENCE_4.Text);
            table2.AddCell(textBoxPRICE_4.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_4.Text);
            

            //
            table2.AddCell(textBoxQTY_5.Text);
            table2.AddCell(textBoxDESCRIPTION_5.Text);
            table2.AddCell(textBoxREASON_5.Text);
            table2.AddCell(textBoxMAN_PAN_5.Text);
            table2.AddCell(textBoxPART_REFERENCE_5.Text);
            table2.AddCell(textBoxPRICE_5.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_5.Text);
            

            //
            table2.AddCell(textBoxQTY_6.Text);
            table2.AddCell(textBoxDESCRIPTION_6.Text);
            table2.AddCell(textBoxREASON_6.Text);
            table2.AddCell(textBoxMAN_PAN_6.Text);
            table2.AddCell(textBoxPART_REFERENCE_6.Text);
            table2.AddCell(textBoxPRICE_6.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_6.Text);
            

            //
            table2.AddCell(textBoxQTY_7.Text);
            table2.AddCell(textBoxDESCRIPTION_7.Text);
            table2.AddCell(textBoxREASON_7.Text);
            table2.AddCell(textBoxMAN_PAN_7.Text);
            table2.AddCell(textBoxPART_REFERENCE_7.Text);
            table2.AddCell(textBoxPRICE_7.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_7.Text);
            

            //
            table2.AddCell(textBoxQTY_8.Text);
            table2.AddCell(textBoxDESCRIPTION_8.Text);
            table2.AddCell(textBoxREASON_8.Text);
            table2.AddCell(textBoxMAN_PAN_8.Text);
            table2.AddCell(textBoxPART_REFERENCE_8.Text);
            table2.AddCell(textBoxPRICE_8.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_8.Text);
            

            //
            table2.AddCell(textBoxQTY_9.Text);
            table2.AddCell(textBoxDESCRIPTION_9.Text);
            table2.AddCell(textBoxREASON_9.Text);
            table2.AddCell(textBoxMAN_PAN_9.Text);
            table2.AddCell(textBoxPART_REFERENCE_9.Text);
            table2.AddCell(textBoxPRICE_9.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_9.Text);
            

            //
            table2.AddCell(textBoxQTY_10.Text);
            table2.AddCell(textBoxDESCRIPTION_10.Text);
            table2.AddCell(textBoxREASON_10.Text);
            table2.AddCell(textBoxMAN_PAN_10.Text);
            table2.AddCell(textBoxPART_REFERENCE_10.Text);
            table2.AddCell(textBoxPRICE_10.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_10.Text);
            


            //
            table2.AddCell(textBoxQTY_11.Text);
            table2.AddCell(textBoxDESCRIPTION_11.Text);
            table2.AddCell(textBoxREASON_11.Text);
            table2.AddCell(textBoxMAN_PAN_11.Text);
            table2.AddCell(textBoxPART_REFERENCE_11.Text);
            table2.AddCell(textBoxPRICE_11.Text);
            table2.AddCell(textBoxPartUsedAMOUNT_11.Text);
           

            //
            table2.AddCell(textBoxQTY_12.Text);
            table2.AddCell(textBoxDESCRIPTION_12.Text);
            table2.AddCell(textBoxREASON_12.Text);
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
            ///


            
            
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



            ////////////////////// Images  ///////////////////////////////

            Paragraph Image1Lable = new Paragraph("Image 1:");
            Paragraph Image2Lable = new Paragraph("Image 2:");
            Paragraph Image3Lable = new Paragraph("Image 3:");
            Paragraph Image4Lable = new Paragraph("Image 4:");
            Paragraph Image5Lable = new Paragraph("Image 5:");
            try
            {

                if((txtImage1.Text.Length > 4) || (txtImage2.Text.Length > 4) || (txtImage3.Text.Length > 4) || (txtImage4.Text.Length > 4)|| (txtImage5.Text.Length > 4))
                {
                    for (int i = 0; i < 12; i++)
                    {
                        doc.Add(lineDown);
                    }
                    doc.Add(TopPNG);
                    doc.Add(DownPNG);

                }
                if (txtImage1.Text != "")
                {

                    string image1 = txtImage1.Text;
                    Image jpg1 = Image.GetInstance(image1);
                    //Resize image depend upon your need
                    jpg1.ScaleToFit(270f, 260f);
                    //Give space before image
                    jpg1.SpacingBefore = 10f;
                    //Give some space after the image
                    jpg1.SpacingAfter = 3f;
                    jpg1.Alignment = Element.ALIGN_CENTER;
                    doc.Add(Image1Lable);
                    doc.Add(jpg1);
                }

                if (txtImage2.Text != "")
                {
                    string image2 = txtImage2.Text;
                    Image jpg2 = Image.GetInstance(image2);
                    //Resize image depend upon your need
                    jpg2.ScaleToFit(270f, 260f);
                    //Give space before image
                    jpg2.SpacingBefore = 10f;
                    //Give some space after the image
                    jpg2.SpacingAfter = 1f;
                    jpg2.Alignment = Element.ALIGN_CENTER;
                    doc.Add(Image2Lable);
                    doc.Add(jpg2);

                }

                if (txtImage3.Text != "")
                {
                    string image3 = txtImage3.Text;
                    Image jpg3 = Image.GetInstance(image3);
                    //Resize image depend upon your need
                    jpg3.ScaleToFit(270f, 260f);
                    //Give space before image
                    jpg3.SpacingBefore = 10f;
                    //Give some space after the image
                    jpg3.SpacingAfter = 1f;
                    jpg3.Alignment = Element.ALIGN_CENTER;
                    doc.Add(Image3Lable);
                    doc.Add(jpg3);

                }

                if (txtImage4.Text != "")
                {
                    string image4 = txtImage4.Text;
                    Image jpg4 = Image.GetInstance(image4);
                    //Resize image depend upon your need
                    jpg4.ScaleToFit(270f, 260f);
                    //Give space before image
                    jpg4.SpacingBefore = 10f;
                    //Give some space after the image
                    jpg4.SpacingAfter = 1f;
                    jpg4.Alignment = Element.ALIGN_CENTER;
                    doc.Add(Image4Lable);
                    doc.Add(jpg4);

                }

                if (txtImage5.Text != "")
                {
                    string image5 = txtImage5.Text;
                    Image jpg5 = Image.GetInstance(image5);
                    //Resize image depend upon your need
                    jpg5.ScaleToFit(270f, 260f);
                    //Give space before image
                    jpg5.SpacingBefore = 10f;
                    //Give some space after the image
                    jpg5.SpacingAfter = 1f;
                    jpg5.Alignment = Element.ALIGN_CENTER;
                    doc.Add(Image5Lable);
                    doc.Add(jpg5);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

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
            
            int SERVICE_FORM_YEAR = DateTime.Now.Year;


            if (!Directory.Exists(SavePath))
            {
                try
                {
                    Directory.CreateDirectory(SavePath);
                }
                catch
                {
                    // Bring up a dialog to chose a folder path in which to open or save a file.
                    var folderBrowserDialog1 = new FolderBrowserDialog();

                    // Show the FolderBrowserDialog.
                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        SavePath = folderBrowserDialog1.SelectedPath;
                    }
                }
            }


            try
            {
                Load_Customers_To_ComboBox();
                LOAD_RMA_NUMBERS_TO_COMBO_BOX();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            comboBoxSERVICED_AT.Text = "Underwater Technologies Center Ltd.";

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

            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SerialNumber1 FROM RMA WHERE RMA_Number = '" + RMA_NUMBER + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxSerial.Text = Convert.ToString(dr[0]);
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

        public void restoreModelOnLoad()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Description1 FROM RMA WHERE RMA_Number = '" + RMA_NUMBER + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxMODEL.Text = Convert.ToString(dr[0]);
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

        public void restoreSoldToOnLoad()
        {
            string RMA_NUMBER = comboBoxRMA_NUMBER.Text.Trim();

            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Customer_Name FROM RMA WHERE RMA_Number = '" + RMA_NUMBER + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxSoldTo.Text = Convert.ToString(dr[0]);
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
                Delete_Previous_Data_From_DataBase();
                Done_and_Save_into_dataBase();
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
            pi.FileName = Paths.Paths.SERVICE_FORM_HELP_FILE;

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
                Delete_Previous_Data_From_DataBase();
                Done_and_Save_into_dataBase();
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
                    Delete_Previous_Data_From_DataBase();
                    Done_and_Save_into_dataBase();
                    PDFCreator();


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
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so Service form document will be created, but it will not be stored in the database");
                    PDFCreator();

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Delete_Previous_Data_From_DataBase();
                Done_and_Save_into_dataBase();
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

  
            string RMANumber = comboBoxRMA_NUMBER.Text.Trim();
            SQLiteCommand cmd;
            SQLiteDataReader reader;



            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "UPDATE RMA SET deviceBeenFixed = '" + 1 + "'  WHERE RMA_Number = '" + RMANumber + "'; ";
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


        private void SendToPrinter_ServiceForm()
        {


            int serviceFormCounter = (Directory.GetFiles(SavePath, "*.pdf", SearchOption.AllDirectories).Length); // Will Retrieve count of PDF files  in directry

            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";


            info.FileName = SavePath + "SERVICE FORM #" + comboBoxRMA_NUMBER.Text + "_SF" + serviceFormCounter + ".pdf";
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
                    Delete_Previous_Data_From_DataBase();
                    Done_and_Save_into_dataBase();
                    PDFCreator();


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
                    updateStatusOf_RMA_As_Fixed();
                    clearFieldsAfterDone();
                    MessageBox.Show("Done Successfully  !");

                }
                catch (SqlException Ex)
                {
                    MessageBox.Show(Ex.Message + "\n\n\nPlease note that the connection to Data Base has failed, so Service form document will be created, but it will not be stored in the database");
                    PDFCreator();

                }
            }
        }


        void update_Status_Of_RMA_As_Open()
        {

            string RMANumber = comboBoxRMA_NUMBER.Text.Trim();//   
            SQLiteCommand cmd;
            SQLiteDataReader reader;


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "UPDATE RMA SET deviceBeenFixed = '" + 0 + "'  WHERE RMA_Number = '" + RMANumber + "' and deviceBeenFixed = '" + 1 + "';";
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
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                update_Status_Of_RMA_As_Open();

                //clear fields of comboBox to prevent Duplication !
                comboBoxRMA_NUMBER.Items.Clear();

                LOAD_RMA_NUMBERS_TO_COMBO_BOX();
                MessageBox.Show("Undo Successfully !");


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

        public string GetImagePath()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                }
            }
            return filePath;
        }




        private void button7_Click(object sender, EventArgs e)
        {
            txtImage1.Text = GetImagePath();
            if (!(imagesPath.Contains(txtImage1.Text)))
            {
                imagesPath.Add(txtImage1.Text);
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {

            txtImage2.Text = GetImagePath();
            if (!(imagesPath.Contains(txtImage2.Text)))
            {
                imagesPath.Add(txtImage2.Text);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            txtImage3.Text = GetImagePath();
            if (!(imagesPath.Contains(txtImage3.Text)))
            {
                imagesPath.Add(txtImage3.Text);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtImage4.Text = GetImagePath();
            if (!(imagesPath.Contains(txtImage4.Text)))
            {
                imagesPath.Add(txtImage4.Text);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            txtImage5.Text = GetImagePath();
            if (!(imagesPath.Contains(txtImage5.Text)))
            {
                imagesPath.Add(txtImage5.Text);
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}