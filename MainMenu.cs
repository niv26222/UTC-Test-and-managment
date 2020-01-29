using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;




namespace Project_Product_List  
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();

            labelTime.Text = DateTime.Now.ToLongTimeString();
            labelDate.Text = DateTime.Now.ToLongDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            new ChooseTestForm().Show();
            this.Hide();
        }


        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            new ChoosForm_H_().Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            labelTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void labelTime_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new RMA_FORM().Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new ServiceForm().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            this.Hide();
        }

        private void labelDate_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            new ServiceForm().Show();
            this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            new TestPressureOpacity_Form().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            new TestPressureReport_Form().Show();
            this.Hide();
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
            new Production_testingFORM().Show();
            this.Hide();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version 7.1.6\n\nDeveloped in UTC for Maintenance and Management.\n\nProgramming language : C# - { " + typeof(string).Assembly.ImageRuntimeVersion +" }\n\nSoftware Developer : Niv Ben Abat.");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit ?", "Exit ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new Invoices_FORM().Show();
            this.Hide();
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("http://utc.co.il/"); // should open our web site
        }

        private void learnMoreAboutOurProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.utc.co.il/support/");
        }

        public void Load_SQL_file_and_run_script_for_Backup_Data()
        {
            try
            {                
                string sqlConnectionString = @"Data Source=DC\PRI;Initial Catalog=UTCgeneral;Integrated Security=True;";

                string script = File.ReadAllText(@"P:\Niv\backupScript.sql");
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                Server server = new Server(new ServerConnection(conn)); // <------ Problem is here :(  Why ??
                server.ConnectionContext.ExecuteNonQuery(script);
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void buttonAdminOptions_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AdminOptionsEntry().Show();
            
        }
    }
}
    