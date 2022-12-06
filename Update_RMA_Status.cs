using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Configuration;

namespace Project_Product_List
{
    public partial class Update_RMA_Status : Form
    {


        public Update_RMA_Status()
        {
            InitializeComponent();
        }

        void updateStatusOf_RMA_BySerialNumber()
        {

            SQLiteCommand cmd;
            SQLiteDataReader reader;
            string SerialNumber = textBoxUpdateStatus.Text.Trim();



            using (SQLiteConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "UPDATE RMA SET isArrived = '" + 1 + "'  WHERE SerialNumber1 = '" + SerialNumber + "'; ";
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

            SQLiteCommand cmd;
            SQLiteDataReader reader;
            string RMANumber = textBoxUpdateStatus.Text.Trim();



            using (SQLiteConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "UPDATE RMA SET isArrived = '" + 1 + "'  WHERE RMA_Number = '" + RMANumber + "'; ";
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


        private void buttonUpdateStatus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm package is arrived", "Update status", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                updateStatus();
                updateStatusOf_RMA_BySerialNumber();
                updateStatusOf_RMA_By_RMANumber();
            }
        }

        private void textBoxUpdateStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        void updateStatus()
        {

            SQLiteCommand cmd;
            SQLiteDataReader reader;
            string RMANumber = textBoxUpdateStatus.Text.Trim();



            using (SQLiteConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {

                    cmd = new SQLiteCommand();
                    cmd.CommandText = "UPDATE RMA SET isArrived = '" + 1 + "'  WHERE SerialNumber1 = '" + RMANumber + "'; ";
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

            MessageBox.Show("Done !");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            this.Hide();
        }

        private void Update_RMA_Status_Load(object sender, EventArgs e)
        {

        }
    }
}
