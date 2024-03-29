﻿using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Project_Product_List
{
    public partial class ChooseTestForm : Form
    {
        public ChooseTestForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Udi14InspectionForm().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            new UdiBoatUnit14Form().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new UdiBoatUnit28Form().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Udi18InspectionForm().Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new ADCS_BoatForm().Show();
            this.Hide();
        }

        
        private void ChooseTestForm_Load(object sender, EventArgs e)
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
            pi.FileName = Paths.Paths.CHOOSE_FORM_HISTORY_HELP_FILE;
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
