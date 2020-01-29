using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Product_List
{
    public static class MaterialMessageBox
    {
        public static DialogResult Show(string message, string caption, MessageBoxButtons button)
        {
            DialogResult result = DialogResult.None;
            switch (button)
            {
                case MessageBoxButtons.YesNo:
                    using (frmYesNo yesNo = new frmYesNo())
                    {
                        yesNo.Text = caption;
                        yesNo.Message = message;
                        result = yesNo.DialogResult;
                    }
                    break;
                case MessageBoxButtons.OK:
                    using (frmOK ok = new frmOK())
                    {
                        ok.Text = caption;
                        ok.Message = message;
                        result = ok.DialogResult;
                    }
                    break;
            }
            return 0;
        }
        internal static DialogResult Show(string v1, string v2, MessageBoxButtons yesNo, MessageBoxIcon question)
        {
            throw new NotImplementedException();
        }
    }
}
