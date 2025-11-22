namespace CidCodeComparer.Forms
{
    partial class ClassSelectionDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblFile1 = new System.Windows.Forms.Label();
            this.cmbFile1Classes = new System.Windows.Forms.ComboBox();
            this.lblFile2 = new System.Windows.Forms.Label();
            this.cmbFile2Classes = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblFile1
            //
            this.lblFile1.AutoSize = true;
            this.lblFile1.Location = new System.Drawing.Point(12, 15);
            this.lblFile1.Name = "lblFile1";
            this.lblFile1.Size = new System.Drawing.Size(120, 13);
            this.lblFile1.TabIndex = 0;
            this.lblFile1.Text = "Select class from File 1:";
            //
            // cmbFile1Classes
            //
            this.cmbFile1Classes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFile1Classes.FormattingEnabled = true;
            this.cmbFile1Classes.Location = new System.Drawing.Point(15, 35);
            this.cmbFile1Classes.Name = "cmbFile1Classes";
            this.cmbFile1Classes.Size = new System.Drawing.Size(370, 21);
            this.cmbFile1Classes.TabIndex = 1;
            //
            // lblFile2
            //
            this.lblFile2.AutoSize = true;
            this.lblFile2.Location = new System.Drawing.Point(12, 70);
            this.lblFile2.Name = "lblFile2";
            this.lblFile2.Size = new System.Drawing.Size(120, 13);
            this.lblFile2.TabIndex = 2;
            this.lblFile2.Text = "Select class from File 2:";
            //
            // cmbFile2Classes
            //
            this.cmbFile2Classes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFile2Classes.FormattingEnabled = true;
            this.cmbFile2Classes.Location = new System.Drawing.Point(15, 90);
            this.cmbFile2Classes.Name = "cmbFile2Classes";
            this.cmbFile2Classes.Size = new System.Drawing.Size(370, 21);
            this.cmbFile2Classes.TabIndex = 3;
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(229, 130);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(310, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // ClassSelectionDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 165);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbFile2Classes);
            this.Controls.Add(this.lblFile2);
            this.Controls.Add(this.cmbFile1Classes);
            this.Controls.Add(this.lblFile1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClassSelectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Classes to Merge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFile1;
        private System.Windows.Forms.ComboBox cmbFile1Classes;
        private System.Windows.Forms.Label lblFile2;
        private System.Windows.Forms.ComboBox cmbFile2Classes;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
