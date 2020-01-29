using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Product_List
{
    public partial class AdminOptionsEntry : Form
    {
        public AdminOptionsEntry()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
            

        }

        private void button4_Click(object sender, EventArgs e)
        {


            const string main_password = "UTC2019";

            string password_entered_by_user = textBoxPassword.Text.ToUpper();


            if (main_password == password_entered_by_user)
            {

                new AdminOptions().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong password, please try again");
            }
        }
    }
}
