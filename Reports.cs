using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Project_Product_List
{
    public partial class Reports : Form
    {
        private RMA_FORM rma_form = new RMA_FORM();


        
        public Reports()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Customers_List(rma_form).Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new RMA_history().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new SERVICE_FORM_HISTORY().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Invoice_History().Show();
            this.Hide();
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
            pi.FileName = MyDirectory() + @"\HELP UTC TESTS\Reports.docx";

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

        private void Button8_Click(object sender, EventArgs e)
        {
            new ProductionReports().Show();
            this.Hide();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            new Stock_DataGrig().Show();
            this.Hide();
        }
    }
}
