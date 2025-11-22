namespace CidCodeComparer
{
    partial class FileSelectionDialog
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
            this.lblFile1 = new System.Windows.Forms.Label();
            this.txtFile1 = new System.Windows.Forms.TextBox();
            this.btnBrowseFile1 = new System.Windows.Forms.Button();
            this.lblFile2 = new System.Windows.Forms.Label();
            this.txtFile2 = new System.Windows.Forms.TextBox();
            this.btnBrowseFile2 = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblFile1
            //
            this.lblFile1.AutoSize = true;
            this.lblFile1.Location = new System.Drawing.Point(12, 15);
            this.lblFile1.Name = "lblFile1";
            this.lblFile1.Size = new System.Drawing.Size(50, 13);
            this.lblFile1.TabIndex = 0;
            this.lblFile1.Text = "First File:";
            //
            // txtFile1
            //
            this.txtFile1.Location = new System.Drawing.Point(15, 31);
            this.txtFile1.Name = "txtFile1";
            this.txtFile1.Size = new System.Drawing.Size(450, 20);
            this.txtFile1.TabIndex = 1;
            //
            // btnBrowseFile1
            //
            this.btnBrowseFile1.Location = new System.Drawing.Point(471, 29);
            this.btnBrowseFile1.Name = "btnBrowseFile1";
            this.btnBrowseFile1.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFile1.TabIndex = 2;
            this.btnBrowseFile1.Text = "Browse...";
            this.btnBrowseFile1.UseVisualStyleBackColor = true;
            this.btnBrowseFile1.Click += new System.EventHandler(this.btnBrowseFile1_Click);
            //
            // lblFile2
            //
            this.lblFile2.AutoSize = true;
            this.lblFile2.Location = new System.Drawing.Point(12, 65);
            this.lblFile2.Name = "lblFile2";
            this.lblFile2.Size = new System.Drawing.Size(68, 13);
            this.lblFile2.TabIndex = 3;
            this.lblFile2.Text = "Second File:";
            //
            // txtFile2
            //
            this.txtFile2.Location = new System.Drawing.Point(15, 81);
            this.txtFile2.Name = "txtFile2";
            this.txtFile2.Size = new System.Drawing.Size(450, 20);
            this.txtFile2.TabIndex = 4;
            //
            // btnBrowseFile2
            //
            this.btnBrowseFile2.Location = new System.Drawing.Point(471, 79);
            this.btnBrowseFile2.Name = "btnBrowseFile2";
            this.btnBrowseFile2.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFile2.TabIndex = 5;
            this.btnBrowseFile2.Text = "Browse...";
            this.btnBrowseFile2.UseVisualStyleBackColor = true;
            this.btnBrowseFile2.Click += new System.EventHandler(this.btnBrowseFile2_Click);
            //
            // btnCompare
            //
            this.btnCompare.Location = new System.Drawing.Point(390, 120);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 6;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(471, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // FileSelectionDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 155);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.btnBrowseFile2);
            this.Controls.Add(this.txtFile2);
            this.Controls.Add(this.lblFile2);
            this.Controls.Add(this.btnBrowseFile1);
            this.Controls.Add(this.txtFile1);
            this.Controls.Add(this.lblFile1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileSelectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Files to Compare";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblFile1;
        private System.Windows.Forms.TextBox txtFile1;
        private System.Windows.Forms.Button btnBrowseFile1;
        private System.Windows.Forms.Label lblFile2;
        private System.Windows.Forms.TextBox txtFile2;
        private System.Windows.Forms.Button btnBrowseFile2;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnCancel;
    }
}
