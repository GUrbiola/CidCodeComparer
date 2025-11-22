namespace CidCodeComparer.Forms
{
    partial class DiffViewerTestDialog
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
            this.btnLoadSample = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.diffViewerControl = new CidCodeComparer.Controls.DiffViewerControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.diffControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleNavigationPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleSummaryPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadSample
            // 
            this.btnLoadSample.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLoadSample.Location = new System.Drawing.Point(0, 638);
            this.btnLoadSample.Name = "btnLoadSample";
            this.btnLoadSample.Size = new System.Drawing.Size(884, 23);
            this.btnLoadSample.TabIndex = 0;
            this.btnLoadSample.Text = "Load Sample";
            this.btnLoadSample.UseVisualStyleBackColor = true;
            this.btnLoadSample.Click += new System.EventHandler(this.btnLoadSample_Click);
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClear.Location = new System.Drawing.Point(0, 615);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(884, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // diffViewerControl
            // 
            this.diffViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diffViewerControl.IgnoreCase = false;
            this.diffViewerControl.IgnoreWhitespace = false;
            this.diffViewerControl.Location = new System.Drawing.Point(0, 24);
            this.diffViewerControl.Name = "diffViewerControl";
            this.diffViewerControl.ShowJumpButtons = false;
            this.diffViewerControl.ShowSummaryPanel = false;
            this.diffViewerControl.SimilarityThreshold = 1D;
            this.diffViewerControl.Size = new System.Drawing.Size(884, 591);
            this.diffViewerControl.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diffControlToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // diffControlToolStripMenuItem
            // 
            this.diffControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleNavigationPanelToolStripMenuItem,
            this.toggleSummaryPanelToolStripMenuItem});
            this.diffControlToolStripMenuItem.Name = "diffControlToolStripMenuItem";
            this.diffControlToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.diffControlToolStripMenuItem.Text = "DiffControl";
            // 
            // toggleNavigationPanelToolStripMenuItem
            // 
            this.toggleNavigationPanelToolStripMenuItem.Name = "toggleNavigationPanelToolStripMenuItem";
            this.toggleNavigationPanelToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.toggleNavigationPanelToolStripMenuItem.Text = "Toggle Navigation Panel";
            this.toggleNavigationPanelToolStripMenuItem.Click += new System.EventHandler(this.toggleNavigationPanelToolStripMenuItem_Click);
            // 
            // toggleSummaryPanelToolStripMenuItem
            // 
            this.toggleSummaryPanelToolStripMenuItem.Name = "toggleSummaryPanelToolStripMenuItem";
            this.toggleSummaryPanelToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.toggleSummaryPanelToolStripMenuItem.Text = "Toggle Summary Panel";
            this.toggleSummaryPanelToolStripMenuItem.Click += new System.EventHandler(this.toggleSummaryPanelToolStripMenuItem_Click);
            // 
            // DiffViewerTestDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.diffViewerControl);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnLoadSample);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "DiffViewerTestDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diff Viewer Test Dialog";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.DiffViewerControl diffViewerControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem diffControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleNavigationPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleSummaryPanelToolStripMenuItem;
    }
}
