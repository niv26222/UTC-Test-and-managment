namespace Project_Product_List
{
    partial class frmOK
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OK = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lblMessage = new MaterialSkin.Controls.MaterialLabel();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Depth = 0;
            this.OK.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.OK.Location = new System.Drawing.Point(184, 190);
            this.OK.MouseState = MaterialSkin.MouseState.HOVER;
            this.OK.Name = "OK";
            this.OK.Primary = true;
            this.OK.Size = new System.Drawing.Size(103, 34);
            this.OK.TabIndex = 3;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.Depth = 0;
            this.lblMessage.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMessage.Location = new System.Drawing.Point(12, 79);
            this.lblMessage.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(465, 81);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "lblMessage";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // frmOK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 266);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.lblMessage);
            this.Name = "frmOK";
            this.Text = "frmYesNo";
            this.Load += new System.EventHandler(this.FrmOK_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton OK;
        private MaterialSkin.Controls.MaterialLabel lblMessage;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}