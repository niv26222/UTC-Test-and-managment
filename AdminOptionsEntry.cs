using System;
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


            const string main_password = "UTC2022";

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

        private void AdminOptionsEntry_Load(object sender, EventArgs e)
        {

        }
    }
}
