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
using System.Reflection;

namespace Project_Product_List
{


    public partial class Add_new_product_Form : Form
    {
        string connectionString = Constants.Constants.UTC_SQL_CONNECTION_NEW;
        //connectionString
        public Add_new_product_Form()
        {
            InitializeComponent();
        }

        private void Add_new_product_Form_Load(object sender, EventArgs e)
        {
            LOAD_SerialNumber_TO_ComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Production_testingFORM().Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lable1_Click(object sender, EventArgs e)
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {
            new Production_testingFORM().Show();
            this.Hide();
        }

        public void restorecomboBoxProduct()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Product FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxProduct.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreAssemblyPersonName()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT AssemblyPersonName FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxAssemblyPersonName.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCommunicationWithThePC()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CommunicationWithThePC FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxCommunicationWithThePC.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCircleVersion()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CircleVersion FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxCircleVersion.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSLAVEVersion()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SLAVEVersion FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxSLAVEVersion.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreBOOTVersion()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT BOOTVersion FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxBOOTVersion.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSoftwareVersion()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SoftwareVersion FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                textBoxSoftwareVersion.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreResetMemory()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT ResetMemory FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxResetMemory.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreDepthGaugeMbar()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT DepthGaugeMbar FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxDepthGauge_mbar.Text = rd[0].ToString();
            }
            con.Close();
        }


        public void restoreSubmergingCalibration()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SubmergingCalibration FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxSubmergingCalibration.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSystemCalibration()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SystemCalibration FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxSystemCalibration.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCustomer()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Customer FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxCustomer.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTest1()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Test1 FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxTest_1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreFirstTremor()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT FirstTremor FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxFirstTremor.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreFirstSubmerging()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT FirstSubmerging FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxFirstSubmerging.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTest2()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Test2 FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxTest_2.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSecondTremor()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SecondTremor FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxsecondTremor.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreSecondSubmerging()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT SecondSubmerging FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxsecondSubmerging.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTest3()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Test3 FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxTest_3.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreThirdTremor()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT ThirdTremor FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxThirdTremor.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreThirdSubmerging()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT ThirdSubmerging FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxThirdSubmerging.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreTest4()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Test4 FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                comboBoxTest4.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreCompletionDateOfTests()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CompletionDateOfTests FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dateTimePicker1.Text = rd[0].ToString();
            }
            con.Close();
        }

        public void restoreComment()
        {
            string SerialNumber = textBoxSerialNumber.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Comment FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + SerialNumber + "';";
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
            restorecomboBoxProduct();
            restoreAssemblyPersonName();
            restoreCommunicationWithThePC();
            restoreCircleVersion();
            restoreSLAVEVersion();
            restoreBOOTVersion();
            restoreSoftwareVersion();
            restoreResetMemory();
            restoreDepthGaugeMbar();
            restoreSubmergingCalibration();
            restoreSystemCalibration();
            restoreCustomer();
            restoreTest1();
            restoreFirstTremor();
            restoreFirstSubmerging();
            restoreTest2();
            restoreSecondTremor();
            restoreSecondSubmerging();
            restoreTest3();
            restoreThirdTremor();
            restoreThirdSubmerging();
            restoreTest4();
            restoreCompletionDateOfTests();
            restoreComment();

        }
        
        public void Delete_Previous_Data_From_DataBase()
        {
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string StockSerialNumber = textBoxSerialNumber.Text.Trim();

            cmd.CommandText = "DELETE FROM Stock_products_TEMPORARY_dt WHERE SerialNumber = '" + StockSerialNumber + "';";
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

        public void insetToTemporaryDataBase()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("Stock_products_TEMPORARY_add", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@Product", comboBoxProduct.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SerialNumber", textBoxSerialNumber.Text.Trim());


                sqlCmd.Parameters.AddWithValue("@AssemblyPersonName", textBoxAssemblyPersonName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@CommunicationWithThePC", comboBoxCommunicationWithThePC.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@CircleVersion", textBoxCircleVersion.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SLAVEVersion", textBoxSLAVEVersion.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@BOOTVersion", textBoxBOOTVersion.Text.Trim());

                sqlCmd.Parameters.AddWithValue("@SoftwareVersion", textBoxSoftwareVersion.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@ResetMemory", comboBoxResetMemory.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@DepthGaugeMbar", comboBoxDepthGauge_mbar.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SubmergingCalibration", comboBoxSubmergingCalibration.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SystemCalibration", comboBoxSystemCalibration.Text.Trim());
                
                sqlCmd.Parameters.AddWithValue("@Customer", comboBoxCustomer.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Test1", comboBoxTest_1.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@FirstTremor", comboBoxFirstTremor.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@FirstSubmerging", comboBoxFirstSubmerging.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Test2", comboBoxTest_2.Text.Trim());
                
                sqlCmd.Parameters.AddWithValue("@SecondTremor", comboBoxsecondTremor.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SecondSubmerging", comboBoxsecondSubmerging.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Test3", comboBoxTest_3.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@ThirdTremor", comboBoxThirdTremor.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@ThirdSubmerging", comboBoxThirdSubmerging.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Test4", comboBoxTest4.Text.Trim());

                sqlCmd.Parameters.AddWithValue("@CompletionDateOfTests", dateTimePicker1.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Comment", textBoxComment.Text.Trim());
                
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
        }

        public void insetToDataBase()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("Stock_products_add", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@Product", comboBoxProduct.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SerialNumber", textBoxSerialNumber.Text.Trim());


                sqlCmd.Parameters.AddWithValue("@AssemblyPersonName", textBoxAssemblyPersonName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@CommunicationWithThePC", comboBoxCommunicationWithThePC.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@CircleVersion", textBoxCircleVersion.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SLAVEVersion", textBoxSLAVEVersion.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@BOOTVersion", textBoxBOOTVersion.Text.Trim());

                sqlCmd.Parameters.AddWithValue("@SoftwareVersion", textBoxSoftwareVersion.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@ResetMemory", comboBoxResetMemory.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@DepthGaugeMbar", comboBoxDepthGauge_mbar.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SubmergingCalibration", comboBoxSubmergingCalibration.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SystemCalibration", comboBoxSystemCalibration.Text.Trim());

                sqlCmd.Parameters.AddWithValue("@Customer", comboBoxCustomer.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Test1", comboBoxTest_1.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@FirstTremor", comboBoxFirstTremor.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@FirstSubmerging", comboBoxFirstSubmerging.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Test2", comboBoxTest_2.Text.Trim());

                sqlCmd.Parameters.AddWithValue("@SecondTremor", comboBoxsecondTremor.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@SecondSubmerging", comboBoxsecondSubmerging.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Test3", comboBoxTest_3.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@ThirdTremor", comboBoxThirdTremor.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@ThirdSubmerging", comboBoxThirdSubmerging.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Test4", comboBoxTest4.Text.Trim());

                sqlCmd.Parameters.AddWithValue("@CompletionDateOfTests", dateTimePicker1.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Comment", textBoxComment.Text.Trim());

                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
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

        public void LOAD_SerialNumber_TO_ComboBox()
        {
            
            SqlConnection sqlConnection1 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT SerialNumber FROM Stock_products_TEMPORARY_dt";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                
                textBoxSerialNumber.Items.Add(Convert.ToString(reader[0]));
            }

            sqlConnection1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text == "" || comboBoxCustomer.Text == "")
            {
                MessageBox.Show("Serial number & Customer name must be writed ! ! ! ");
            }
            else
            {
                try
                {
                Delete_Previous_Data_From_DataBase();
                insetToTemporaryDataBase();
                MessageBox.Show("Saved successfully");
                clearFieldsAfterDone();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Restore_data_from_temporary_data_base();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text == "" || comboBoxCustomer.Text == "")
            {
                MessageBox.Show("Serial number & Customer name must be writed ! ! ! ");
            }
            else
            {
                try
                {
                    Delete_Previous_Data_From_DataBase();
                    insetToDataBase();
                    MessageBox.Show("Done successfully");
                    clearFieldsAfterDone();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text == "" || comboBoxCustomer.Text == "")
            {
                MessageBox.Show("Serial number & Customer name must be writed ! ! ! ");
            }
            else
            {
                try
                {
                    Delete_Previous_Data_From_DataBase();
                    insetToTemporaryDataBase();
                    MessageBox.Show("Saved successfully");
                    clearFieldsAfterDone();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text == "" || comboBoxCustomer.Text == "")
            {
                MessageBox.Show("Serial number & Customer name must be writed ! ! ! ");
            }
            else
            {
                try
                {
                    Delete_Previous_Data_From_DataBase();
                    insetToDataBase();
                    MessageBox.Show("Done successfully");
                    clearFieldsAfterDone();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void watchStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Stock_DataGrig().Show();
            this.Hide();
        }
    }
}
