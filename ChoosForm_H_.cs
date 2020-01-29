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
using System.IO;
using System.Reflection;

namespace Project_Product_List
{
    public partial class ChoosForm_H_ : Form
    {
        public ChoosForm_H_()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new UDI14_H_Form().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new UDI28_H_Form().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new BoatUnit14_H_Form().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new BoatUnit28_H_Form().Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new ADCS_H_Form().Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new TestPressureOpacity_H_Form().Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new TestPressureReport_H_Form().Show();
            this.Hide();
        }

        private void ChoosForm_H__Load(object sender, EventArgs e)
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
    }
}
