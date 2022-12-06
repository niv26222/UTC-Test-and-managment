using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Project_Product_List
{
    public partial class Production_testingFORM : Form
    {
        public Production_testingFORM()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Add_new_product_Form().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new RemoveProductFromStock_Form().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Stock_DataGrig().Show();
            this.Hide();
        }

        private void Production_testingFORM_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new Tracking_by_waybill_number().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Waybill().Show();
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

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
