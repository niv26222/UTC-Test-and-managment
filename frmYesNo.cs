
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
