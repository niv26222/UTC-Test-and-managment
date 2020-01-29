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
using System.Data.SqlServerCe;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Diagnostics;


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

            SqlConnection sqlConnection1 = new SqlConnection(Constants.Constants.UTC_SQL_CONNECTION_NEW);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string RMANumber = textBoxUpdateStatus.Text.Trim();

            cmd.CommandText = "UPDATE RMA_dt SET isArrived = '" + 1 + "'  WHERE SerialNumber1 = '" + RMANumber + "'; ";
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


        void updateStatusOf_RMA_By_RMANumber()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Constants.Constants.UTC_SQL_CONNECTION_NEW);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string RMANumber = textBoxUpdateStatus.Text.Trim();

            cmd.CommandText = "UPDATE RMA_dt SET isArrived = '" + 1 + "'  WHERE RMA_Number = '" + RMANumber + "'; ";
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

        void updateStatus()
        {

            SqlConnection sqlConnection1 = new SqlConnection(Constants.Constants.UTC_SQL_CONNECTION_NEW);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string RMANumber = textBoxUpdateStatus.Text.Trim();

            cmd.CommandText = "UPDATE RMA_dt SET isArrived = '" + 1 + "'  WHERE SerialNumber1 = '" + RMANumber + "'; ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;


            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Read();
            }

            sqlConnection1.Close();
            MessageBox.Show("Done !");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            this.Hide();
        }
    }
}
