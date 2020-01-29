using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;


namespace Project_Product_List
{
    public partial class frmYesNo : MaterialSkin.Controls.MaterialForm
    {
        public frmYesNo()
        {
            InitializeComponent();
        }

        public string Message
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }
    }
}
