using System;


namespace Project_Product_List
{
    public partial class frmOK : MaterialSkin.Controls.MaterialForm
    {
        public frmOK()
        {
            InitializeComponent();
        }

        public string Message
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }

        private void FrmOK_Load(object sender, EventArgs e)
        {

        }
    }
}
