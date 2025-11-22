namespace CidCodeComparer.Controls
{
    partial class DiffViewerControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.leftTextEditor = new ICSharpCode.TextEditor.TextEditorControl();
            this.rightTextEditor = new ICSharpCode.TextEditor.TextEditorControl();
            this.locationPanel = new System.Windows.Forms.Panel();
            this.navigationPanel = new System.Windows.Forms.Panel();
            this.lblDiffCounter = new System.Windows.Forms.Label();
            this.btnPrevDiff = new System.Windows.Forms.Button();
            this.btnNextDiff = new System.Windows.Forms.Button();
            this.navigationTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.navigationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.leftTextEditor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rightTextEditor);
            this.splitContainer1.Size = new System.Drawing.Size(695, 565);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.TabIndex = 0;
            // 
            // leftTextEditor
            // 
            this.leftTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftTextEditor.IsReadOnly = false;
            this.leftTextEditor.Location = new System.Drawing.Point(0, 0);
            this.leftTextEditor.Name = "leftTextEditor";
            this.leftTextEditor.ShowVRuler = false;
            this.leftTextEditor.Size = new System.Drawing.Size(343, 565);
            this.leftTextEditor.TabIndex = 0;
            // 
            // rightTextEditor
            // 
            this.rightTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightTextEditor.IsReadOnly = false;
            this.rightTextEditor.Location = new System.Drawing.Point(0, 0);
            this.rightTextEditor.Name = "rightTextEditor";
            this.rightTextEditor.Size = new System.Drawing.Size(348, 565);
            this.rightTextEditor.TabIndex = 1;
            // 
            // locationPanel
            // 
            this.locationPanel.BackColor = System.Drawing.Color.White;
            this.locationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.locationPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.locationPanel.Location = new System.Drawing.Point(695, 35);
            this.locationPanel.Name = "locationPanel";
            this.locationPanel.Size = new System.Drawing.Size(60, 565);
            this.locationPanel.TabIndex = 1;
            // 
            // navigationPanel
            // 
            this.navigationPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.navigationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.navigationPanel.Controls.Add(this.lblDiffCounter);
            this.navigationPanel.Controls.Add(this.btnPrevDiff);
            this.navigationPanel.Controls.Add(this.btnNextDiff);
            this.navigationPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigationPanel.Location = new System.Drawing.Point(0, 0);
            this.navigationPanel.Name = "navigationPanel";
            this.navigationPanel.Size = new System.Drawing.Size(800, 35);
            this.navigationPanel.TabIndex = 2;
            // 
            // lblDiffCounter
            // 
            this.lblDiffCounter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDiffCounter.AutoSize = true;
            this.lblDiffCounter.Location = new System.Drawing.Point(710, 11);
            this.lblDiffCounter.Name = "lblDiffCounter";
            this.lblDiffCounter.Size = new System.Drawing.Size(30, 13);
            this.lblDiffCounter.TabIndex = 2;
            this.lblDiffCounter.Text = "0 / 0";
            this.lblDiffCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPrevDiff
            // 
            this.btnPrevDiff.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPrevDiff.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrevDiff.Location = new System.Drawing.Point(640, 6);
            this.btnPrevDiff.Name = "btnPrevDiff";
            this.btnPrevDiff.Size = new System.Drawing.Size(30, 23);
            this.btnPrevDiff.TabIndex = 0;
            this.btnPrevDiff.Text = "▲";
            this.btnPrevDiff.UseVisualStyleBackColor = true;
            // 
            // btnNextDiff
            // 
            this.btnNextDiff.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNextDiff.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNextDiff.Location = new System.Drawing.Point(675, 6);
            this.btnNextDiff.Name = "btnNextDiff";
            this.btnNextDiff.Size = new System.Drawing.Size(30, 23);
            this.btnNextDiff.TabIndex = 1;
            this.btnNextDiff.Text = "▼";
            this.btnNextDiff.UseVisualStyleBackColor = true;
            // 
            // navigationTrackBar
            // 
            this.navigationTrackBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.navigationTrackBar.LargeChange = 10;
            this.navigationTrackBar.Location = new System.Drawing.Point(755, 35);
            this.navigationTrackBar.Maximum = 100;
            this.navigationTrackBar.Name = "navigationTrackBar";
            this.navigationTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.navigationTrackBar.Size = new System.Drawing.Size(45, 565);
            this.navigationTrackBar.TabIndex = 3;
            this.navigationTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // DiffViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.locationPanel);
            this.Controls.Add(this.navigationTrackBar);
            this.Controls.Add(this.navigationPanel);
            this.Name = "DiffViewerControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.navigationPanel.ResumeLayout(false);
            this.navigationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ICSharpCode.TextEditor.TextEditorControl leftTextEditor;
        private ICSharpCode.TextEditor.TextEditorControl rightTextEditor;
        private System.Windows.Forms.Panel locationPanel;
        private System.Windows.Forms.Panel navigationPanel;
        private System.Windows.Forms.Button btnPrevDiff;
        private System.Windows.Forms.Button btnNextDiff;
        private System.Windows.Forms.Label lblDiffCounter;
        private System.Windows.Forms.TrackBar navigationTrackBar;
    }
}
