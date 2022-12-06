using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Data.SQLite;
using System.Configuration;

namespace Project_Product_List
{
    public partial class RemoveProductFromStock_Form : Form
    {

        public RemoveProductFromStock_Form()
        {
            InitializeComponent();
        }


        public void LOAD_SerialNumber_TO_ComboBox()
        {

            /////load the names to combobox
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SerialNumber FROM STOCK";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        textBoxSerialNumber.Items.Add(dr["SerialNumber"]);
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


        public void restorecomboBoxProduct()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Product FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxProduct.Text = Convert.ToString(dr[0]);
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

        public void restoreAssemblyPersonName()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT AssemblyPersonName FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxAssemblyPersonName.Text = Convert.ToString(dr[0]);
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

        public void restoreCommunicationWithThePC()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CommunicationWithThePC FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxCommunicationWithThePC.Text = Convert.ToString(dr[0]);
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

        public void restoreCircleVersion()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CircleVersion FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxCircleVersion.Text = Convert.ToString(dr[0]);
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

        public void restoreSLAVEVersion()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SLAVEVersion FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxSLAVEVersion.Text = Convert.ToString(dr[0]);
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

        public void restoreBOOTVersion()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT BOOTVersion FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxBOOTVersion.Text = Convert.ToString(dr[0]);
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

        public void restoreSoftwareVersion()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SoftwareVersion FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        textBoxSoftwareVersion.Text = Convert.ToString(dr[0]);
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

        public void restoreResetMemory()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT ResetMemory FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxResetMemory.Text = Convert.ToString(dr[0]);
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

        public void restoreDepthGaugeMbar()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT DepthGaugeMbar FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxDepthGauge_mbar.Text = Convert.ToString(dr[0]);
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


        public void restoreSubmergingCalibration()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SubmergingCalibration FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxSubmergingCalibration.Text = Convert.ToString(dr[0]);
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

        public void restoreSystemCalibration()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SystemCalibration FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxSystemCalibration.Text = Convert.ToString(dr[0]);
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

        public void restoreCustomer()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Customer FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxCustomer.Text = Convert.ToString(dr[0]);
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

        public void restoreTest1()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Test1 FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxTest_1.Text = Convert.ToString(dr[0]);
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

        public void restoreFirstTremor()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT FirstTremor FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxFirstTremor.Text = Convert.ToString(dr[0]);
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

        public void restoreFirstSubmerging()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT FirstSubmerging FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxFirstSubmerging.Text = Convert.ToString(dr[0]);
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

        public void restoreTest2()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Test2 FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxTest_2.Text = Convert.ToString(dr[0]);
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

        public void restoreSecondTremor()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SecondTremor FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxsecondTremor.Text = Convert.ToString(dr[0]);
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

        public void restoreSecondSubmerging()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT SecondSubmerging FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxsecondSubmerging.Text = Convert.ToString(dr[0]);
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

        public void restoreTest3()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Test3 FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxTest_3.Text = Convert.ToString(dr[0]);
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

        public void restoreThirdTremor()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT ThirdTremor FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxThirdTremor.Text = Convert.ToString(dr[0]);
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

        public void restoreThirdSubmerging()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT ThirdSubmerging FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxThirdSubmerging.Text = Convert.ToString(dr[0]);
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

        public void restoreTest4()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Test4 FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
                    cmd.Connection = conn;
                    conn.Open();
                    dr = cmd.ExecuteReader();


                    string customer_address = cmd.CommandText.ToString();

                    while (dr.Read())
                    {
                        comboBoxTest4.Text = Convert.ToString(dr[0]);
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

        public void restoreCompletionDateOfTests()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT CompletionDateOfTests FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
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

        public void restoreComment()
        {
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            string SerialNumber = textBoxSerialNumber.Text.Trim();


            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {
                    cmd = new SQLiteCommand();
                    cmd.CommandText = "SELECT Comment FROM STOCK WHERE Serial_Number = '" + SerialNumber + "';";
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


        private void RemoveProductFromStock_Form_Load(object sender, EventArgs e)
        {
            // ON LOAD ! ! !
            LOAD_SerialNumber_TO_ComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Production_testingFORM().Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Restore_data_from_temporary_data_base();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            new Production_testingFORM().Show();
            this.Hide();
        }

        public void Delete_Product_From_DataBase()
        {
            SQLiteCommand cmd;
            SQLiteDataReader reader;
            string StockSerialNumber = textBoxSerialNumber.Text.Trim();

            using (SQLiteConnection conn = new SQLiteConnection(General.LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "DELETE FROM STOCK WHERE Serial_Number = '" + StockSerialNumber + "';";
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Product from stock ?", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Delete_Product_From_DataBase();
                MessageBox.Show("Successfully Deleted");
                clearFieldsAfterDone();
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
    }
}
