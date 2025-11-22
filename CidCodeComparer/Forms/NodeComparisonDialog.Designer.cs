namespace CidCodeComparer.Forms
{
    partial class NodeComparisonDialog
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

        private void InitializeComponent()
        {
            this.diffViewerControl = new CidCodeComparer.Controls.DiffViewerControl();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // diffViewerControl
            // 
            this.diffViewerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diffViewerControl.IgnoreCase = false;
            this.diffViewerControl.IgnoreWhitespace = false;
            this.diffViewerControl.Location = new System.Drawing.Point(12, 12);
            this.diffViewerControl.Name = "diffViewerControl";
            this.diffViewerControl.ShowJumpButtons = true;
            this.diffViewerControl.ShowSummaryPanel = true;
            this.diffViewerControl.SimilarityThreshold = 1D;
            this.diffViewerControl.Size = new System.Drawing.Size(960, 537);
            this.diffViewerControl.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(897, 555);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // NodeComparisonDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 590);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.diffViewerControl);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "NodeComparisonDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Node Comparison";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        private CidCodeComparer.Controls.DiffViewerControl diffViewerControl;
        private System.Windows.Forms.Button btnClose;
    }
}
