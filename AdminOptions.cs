using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Windows.Forms;

namespace Project_Product_List
{
    public partial class AdminOptions : Form
    {


        public AdminOptions()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        void Delete_DataBase(string db_name, string name_in_db, string textBox_name)
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(Paths.Paths.UTC_SQL_CONNECTION_NEW);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                
                cmd.CommandText = "DELETE FROM " + db_name + " WHERE " + name_in_db + " = '" + textBox_name.Trim() + "';";

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


        void DeleteFromDirectory(string textBox_name, string type)
        {

            string AT_number_to_delete = textBox_Acceptance_test.Text;

            string DeletePath = Paths.Paths.ADMIN_OPTIONS_ROOT_PATH + type + @"\" + AT_number_to_delete + ".pdf";

            if (File.Exists(DeletePath))
            {
                File.Delete(DeletePath);

                if (!(File.Exists(DeletePath)))
                {
                    MessageBox.Show("Delete Successfully from Directory!");
                }
                else
                {
                    MessageBox.Show(" Unable delete file !");
                }

            }
            else
            {
                MessageBox.Show(" File not found !");
            }
        }


        void update_Status_Of_RMA_As_OPEN()
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(Paths.Paths.UTC_SQL_CONNECTION_NEW);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string RMANumber = textBoxRMA_Number.Text.Trim();//   
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

        void update_Status_Of_RMA_As_CLOSE()
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(Paths.Paths.UTC_SQL_CONNECTION_NEW);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string RMANumber = textBoxRMA_Number.Text.Trim();//   
                cmd.CommandText = "UPDATE RMA_dt SET deviceBeenFixed = '" + 1 + "'  WHERE RMA_Number = '" + RMANumber + "' and deviceBeenFixed = '" + 0 + "';";

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

        void update_Status_Of_RMA_As_ARRIVED()
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(Paths.Paths.UTC_SQL_CONNECTION_NEW);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string RMANumber = textBoxRMA_Number.Text.Trim();//   
                cmd.CommandText = "UPDATE RMA_dt SET isArrived = '" + 1 + "'  WHERE RMA_Number = '" + RMANumber + "' and isArrived = '" + 0 + "';";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                
                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Read();
                }

                sqlConnection1.Close();

                MessageBox.Show("The RMA is Arrived.\n");
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        void update_Status_Of_RMA_As_UNARRIVED()
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(Paths.Paths.UTC_SQL_CONNECTION_NEW);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string RMANumber = textBoxRMA_Number.Text.Trim();//   
                cmd.CommandText = "UPDATE RMA_dt SET isArrived = '" + 0 + "'  WHERE RMA_Number = '" + RMANumber + "' and isArrived = '" + 1 + "';";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;


                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Read();
                }

                sqlConnection1.Close();

                MessageBox.Show("The RMA is UNArrived.\n");
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void Delete_from_RMA_Directory()
        {
            int RMAyear = DateTime.Now.Year;

            string rma_number_to_delete = textBoxRMANumber.Text;

            string DeletePath = Paths.Paths.RMA_ROOT_PATH + rma_number_to_delete + ".pdf";

            if (File.Exists(DeletePath))
            {
                File.Delete(DeletePath);
                
                if (!(File.Exists(DeletePath)))
                {
                    MessageBox.Show("Delete Successfully from Directory!");
                }
                else
                {
                    MessageBox.Show(" Unable delete file !");
                }

            }
            else
            {
                MessageBox.Show(" File not found !");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this RMA?", "Delete RMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Delete_from_RMA_Directory();
                Delete_DataBase("RMA_dt", "RMA_Number", textBoxRMANumber.Text);
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are you sure you want to delete this service form ?", "Delete Service Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                Delete_from_ServiceForm_from_Directory();
                Delete_DataBase("ServiceForm_dt", "SERIAL", textBox_service_form.Text);
                
            }
        }

        private void Delete_from_ServiceForm_from_Directory()
        {

            string ServiceForm_from_to_delete = textBox_service_form.Text;


            int RMAyear = DateTime.Now.Year;

            
            string DeletePath = Paths.Paths.SERVICE_FORM_PATH + ServiceForm_from_to_delete + ".pdf";


            if (File.Exists(DeletePath))
            {
                File.Delete(DeletePath);

                if (!(File.Exists(DeletePath)))
                {
                    MessageBox.Show("Delete Successfully from Directory!");
                }
                else
                {
                    MessageBox.Show(" Unable delete file !");
                }

            }
            else
            {
                MessageBox.Show(" File not found !");
            }


        }

        private void Delete_from_tpo_Directory()
        {

            string tpo_to_delete = textBox_TPO.Text;


            int year = DateTime.Now.Year;


            string DeletePath = Paths.Paths.TEST_PRESSURE_OPACITY_PATH + tpo_to_delete + ".pdf";


            if (File.Exists(DeletePath))
            {
                File.Delete(DeletePath);

                if (!(File.Exists(DeletePath)))
                {
                    MessageBox.Show("Delete Successfully from Directory!");
                }
                else
                {
                    MessageBox.Show(" Unable delete file !");
                }

            }
            else
            {
                MessageBox.Show(" File not found !");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this TPO ?", "Delete TPO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Delete_from_tpo_Directory();
                Delete_DataBase("TestPressureOpacity_dt", "Serial", textBox_TPO.Text);
                
                

            }
        }

        private void Delete_from_tpr_Directory()
        {

            string tpr_to_delete = textBox_TPR.Text;


            int year = DateTime.Now.Year;


            string DeletePath = Paths.Paths.TEST_PRESSURE_REPORT_PATH + tpr_to_delete + ".pdf";


            if (File.Exists(DeletePath))
            {
                File.Delete(DeletePath);

                if (!(File.Exists(DeletePath)))
                {
                    MessageBox.Show("Delete Successfully from Directory!");
                }
                else
                {
                    MessageBox.Show(" Unable delete file !");
                }

            }
            else
            {
                MessageBox.Show(" File not found !");
            }

        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this TPR ?", "Delete TPR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Delete_from_tpr_Directory();
                Delete_DataBase("TestPressureReport_dt", "serialNumber", textBox_TPR.Text);
                

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBoxType.Text == "")
            {
                MessageBox.Show("Please choose type from comboBox");
            }
            else
            {
                switch (comboBoxType.Text)
                {
                    case "UDI 14":
                        
                        //DeleteFromDirectory(textBox_Acceptance_test.Text, "UDI-14");
                        Delete_DataBase("Udi14_dt", "Seriel_Code", textBox_Acceptance_test.Text);
                        MessageBox.Show("Delete Successfully !");
                        break;

                    case "UDI 28":                     
                        //DeleteFromDirectory(textBox_Acceptance_test.Text, "UDI-28");
                        Delete_DataBase("Udi28_dt", "Seriel_Code", textBox_Acceptance_test.Text);
                        MessageBox.Show("Delete Successfully !");
                        break;

                    case "UDI BOAT 14":  
                        //DeleteFromDirectory(textBox_Acceptance_test.Text, "BOAT UNIT 14");
                        Delete_DataBase("Boat14_dt", "Seriel_Code", textBox_Acceptance_test.Text);
                        MessageBox.Show("Delete Successfully !");
                        break;

                    case "UDI BOAT 28":  
                        //DeleteFromDirectory(textBox_Acceptance_test.Text, "BOAT UNIT 28");
                        Delete_DataBase("Boat28_dt", "Seriel_Code", textBox_Acceptance_test.Text);
                        MessageBox.Show("Delete Successfully !");
                        break;

                    case "ADCS":                     
                        //DeleteFromDirectory(textBox_Acceptance_test.Text, "ADCS");
                        Delete_DataBase("ADCS_dt", "Seriel_Code", textBox_Acceptance_test.Text);
                        MessageBox.Show("Delete Successfully !");
                        break;

                    default:
                        MessageBox.Show("UNKNOWN TYPE OF UDI !");
                        break;
                }
            }

            
        }

        private void AdminOptions_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lable1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxRMANumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            switch (comboBox_Command.Text)
            {
                case "OPEN":
                    if (MessageBox.Show("Are you sure you want to OPEN this RMA ?", "OPEN RMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        update_Status_Of_RMA_As_OPEN();
                    }
                    break;
                case "CLOSE":
                    if (MessageBox.Show("Are you sure you want to CLOSE this RMA ?", "CLOSE RMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        update_Status_Of_RMA_As_CLOSE();
                    }
                    break;
                case "MARK AS ARRIVED":
                    if (MessageBox.Show("Are you sure you want to MARK AS ARRIVED RMA ?", "MARK AS ARRIVED", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        update_Status_Of_RMA_As_ARRIVED();
                    }
                    break;
                case "MARK AS NOT ARRIVED":
                    if (MessageBox.Show("Are you sure you want to MARK AS UNARRIVED RMA ??", "MARK AS UNARRIVED", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        update_Status_Of_RMA_As_UNARRIVED();
                    }
                    break;
                default:
                    MessageBox.Show("Undefined command");
                    break;

            }
        }
    }
}

       