namespace CidCodeComparer.Forms
{
    partial class MergeWizardDialog
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
            this.panelGreenNodes = new System.Windows.Forms.Panel();
            this.lblGreenQuestion = new System.Windows.Forms.Label();
            this.txtGreenNodeInfo = new System.Windows.Forms.TextBox();
            this.btnGreenAddThis = new System.Windows.Forms.Button();
            this.btnGreenSkipThis = new System.Windows.Forms.Button();
            this.btnGreenAddAll = new System.Windows.Forms.Button();
            this.btnGreenSkipAll = new System.Windows.Forms.Button();
            this.panelRedNodes = new System.Windows.Forms.Panel();
            this.lblRedQuestion = new System.Windows.Forms.Label();
            this.txtRedNodeInfo = new System.Windows.Forms.TextBox();
            this.btnRedAddThis = new System.Windows.Forms.Button();
            this.btnRedSkipThis = new System.Windows.Forms.Button();
            this.btnRedAddAll = new System.Windows.Forms.Button();
            this.btnRedSkipAll = new System.Windows.Forms.Button();
            this.panelYellowNodes = new System.Windows.Forms.Panel();
            this.lblYellowQuestion = new System.Windows.Forms.Label();
            this.diffViewer = new CidCodeComparer.Controls.DiffViewerControl();
            this.btnYellowKeepLeft = new System.Windows.Forms.Button();
            this.btnYellowKeepRight = new System.Windows.Forms.Button();
            this.panelSaveLocation = new System.Windows.Forms.Panel();
            this.lblSaveLocation = new System.Windows.Forms.Label();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelGreenNodes.SuspendLayout();
            this.panelRedNodes.SuspendLayout();
            this.panelYellowNodes.SuspendLayout();
            this.panelSaveLocation.SuspendLayout();
            this.panelNavigation.SuspendLayout();
            this.SuspendLayout();
            //
            // panelGreenNodes
            //
            this.panelGreenNodes.Controls.Add(this.lblGreenQuestion);
            this.panelGreenNodes.Controls.Add(this.txtGreenNodeInfo);
            this.panelGreenNodes.Controls.Add(this.btnGreenAddThis);
            this.panelGreenNodes.Controls.Add(this.btnGreenSkipThis);
            this.panelGreenNodes.Controls.Add(this.btnGreenAddAll);
            this.panelGreenNodes.Controls.Add(this.btnGreenSkipAll);
            this.panelGreenNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGreenNodes.Location = new System.Drawing.Point(0, 0);
            this.panelGreenNodes.Name = "panelGreenNodes";
            this.panelGreenNodes.Size = new System.Drawing.Size(800, 500);
            this.panelGreenNodes.TabIndex = 0;
            this.panelGreenNodes.Visible = false;
            //
            // lblGreenQuestion
            //
            this.lblGreenQuestion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGreenQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblGreenQuestion.Location = new System.Drawing.Point(0, 0);
            this.lblGreenQuestion.Name = "lblGreenQuestion";
            this.lblGreenQuestion.Padding = new System.Windows.Forms.Padding(10);
            this.lblGreenQuestion.Size = new System.Drawing.Size(800, 40);
            this.lblGreenQuestion.TabIndex = 0;
            this.lblGreenQuestion.Text = "Green Nodes Question";
            //
            // txtGreenNodeInfo
            //
            this.txtGreenNodeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGreenNodeInfo.BackColor = System.Drawing.Color.LightGreen;
            this.txtGreenNodeInfo.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtGreenNodeInfo.Location = new System.Drawing.Point(10, 50);
            this.txtGreenNodeInfo.Multiline = true;
            this.txtGreenNodeInfo.Name = "txtGreenNodeInfo";
            this.txtGreenNodeInfo.ReadOnly = true;
            this.txtGreenNodeInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGreenNodeInfo.Size = new System.Drawing.Size(780, 390);
            this.txtGreenNodeInfo.TabIndex = 1;
            this.txtGreenNodeInfo.WordWrap = false;
            //
            // btnGreenAddThis
            //
            this.btnGreenAddThis.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGreenAddThis.Location = new System.Drawing.Point(250, 450);
            this.btnGreenAddThis.Name = "btnGreenAddThis";
            this.btnGreenAddThis.Size = new System.Drawing.Size(100, 30);
            this.btnGreenAddThis.TabIndex = 2;
            this.btnGreenAddThis.Text = "Add This";
            this.btnGreenAddThis.UseVisualStyleBackColor = true;
            this.btnGreenAddThis.Click += new System.EventHandler(this.btnGreenAddThis_Click);
            //
            // btnGreenSkipThis
            //
            this.btnGreenSkipThis.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGreenSkipThis.Location = new System.Drawing.Point(450, 450);
            this.btnGreenSkipThis.Name = "btnGreenSkipThis";
            this.btnGreenSkipThis.Size = new System.Drawing.Size(100, 30);
            this.btnGreenSkipThis.TabIndex = 3;
            this.btnGreenSkipThis.Text = "Skip This";
            this.btnGreenSkipThis.UseVisualStyleBackColor = true;
            this.btnGreenSkipThis.Click += new System.EventHandler(this.btnGreenSkipThis_Click);
            //
            // btnGreenAddAll
            //
            this.btnGreenAddAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGreenAddAll.Location = new System.Drawing.Point(250, 450);
            this.btnGreenAddAll.Name = "btnGreenAddAll";
            this.btnGreenAddAll.Size = new System.Drawing.Size(100, 30);
            this.btnGreenAddAll.TabIndex = 4;
            this.btnGreenAddAll.Text = "Add All";
            this.btnGreenAddAll.UseVisualStyleBackColor = true;
            this.btnGreenAddAll.Click += new System.EventHandler(this.btnGreenAddAll_Click);
            //
            // btnGreenSkipAll
            //
            this.btnGreenSkipAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGreenSkipAll.Location = new System.Drawing.Point(450, 450);
            this.btnGreenSkipAll.Name = "btnGreenSkipAll";
            this.btnGreenSkipAll.Size = new System.Drawing.Size(100, 30);
            this.btnGreenSkipAll.TabIndex = 5;
            this.btnGreenSkipAll.Text = "Skip All";
            this.btnGreenSkipAll.UseVisualStyleBackColor = true;
            this.btnGreenSkipAll.Click += new System.EventHandler(this.btnGreenSkipAll_Click);
            //
            // panelRedNodes
            //
            this.panelRedNodes.Controls.Add(this.lblRedQuestion);
            this.panelRedNodes.Controls.Add(this.txtRedNodeInfo);
            this.panelRedNodes.Controls.Add(this.btnRedAddThis);
            this.panelRedNodes.Controls.Add(this.btnRedSkipThis);
            this.panelRedNodes.Controls.Add(this.btnRedAddAll);
            this.panelRedNodes.Controls.Add(this.btnRedSkipAll);
            this.panelRedNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRedNodes.Location = new System.Drawing.Point(0, 0);
            this.panelRedNodes.Name = "panelRedNodes";
            this.panelRedNodes.Size = new System.Drawing.Size(800, 500);
            this.panelRedNodes.TabIndex = 1;
            this.panelRedNodes.Visible = false;
            //
            // lblRedQuestion
            //
            this.lblRedQuestion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRedQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblRedQuestion.Location = new System.Drawing.Point(0, 0);
            this.lblRedQuestion.Name = "lblRedQuestion";
            this.lblRedQuestion.Padding = new System.Windows.Forms.Padding(10);
            this.lblRedQuestion.Size = new System.Drawing.Size(800, 40);
            this.lblRedQuestion.TabIndex = 0;
            this.lblRedQuestion.Text = "Red Nodes Question";
            //
            // txtRedNodeInfo
            //
            this.txtRedNodeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRedNodeInfo.BackColor = System.Drawing.Color.LightCoral;
            this.txtRedNodeInfo.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtRedNodeInfo.Location = new System.Drawing.Point(10, 50);
            this.txtRedNodeInfo.Multiline = true;
            this.txtRedNodeInfo.Name = "txtRedNodeInfo";
            this.txtRedNodeInfo.ReadOnly = true;
            this.txtRedNodeInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRedNodeInfo.Size = new System.Drawing.Size(780, 390);
            this.txtRedNodeInfo.TabIndex = 1;
            this.txtRedNodeInfo.WordWrap = false;
            //
            // btnRedAddThis
            //
            this.btnRedAddThis.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRedAddThis.Location = new System.Drawing.Point(250, 450);
            this.btnRedAddThis.Name = "btnRedAddThis";
            this.btnRedAddThis.Size = new System.Drawing.Size(100, 30);
            this.btnRedAddThis.TabIndex = 2;
            this.btnRedAddThis.Text = "Add This";
            this.btnRedAddThis.UseVisualStyleBackColor = true;
            this.btnRedAddThis.Click += new System.EventHandler(this.btnRedAddThis_Click);
            //
            // btnRedSkipThis
            //
            this.btnRedSkipThis.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRedSkipThis.Location = new System.Drawing.Point(450, 450);
            this.btnRedSkipThis.Name = "btnRedSkipThis";
            this.btnRedSkipThis.Size = new System.Drawing.Size(100, 30);
            this.btnRedSkipThis.TabIndex = 3;
            this.btnRedSkipThis.Text = "Skip This";
            this.btnRedSkipThis.UseVisualStyleBackColor = true;
            this.btnRedSkipThis.Click += new System.EventHandler(this.btnRedSkipThis_Click);
            //
            // btnRedAddAll
            //
            this.btnRedAddAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRedAddAll.Location = new System.Drawing.Point(250, 450);
            this.btnRedAddAll.Name = "btnRedAddAll";
            this.btnRedAddAll.Size = new System.Drawing.Size(100, 30);
            this.btnRedAddAll.TabIndex = 4;
            this.btnRedAddAll.Text = "Add All";
            this.btnRedAddAll.UseVisualStyleBackColor = true;
            this.btnRedAddAll.Click += new System.EventHandler(this.btnRedAddAll_Click);
            //
            // btnRedSkipAll
            //
            this.btnRedSkipAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRedSkipAll.Location = new System.Drawing.Point(450, 450);
            this.btnRedSkipAll.Name = "btnRedSkipAll";
            this.btnRedSkipAll.Size = new System.Drawing.Size(100, 30);
            this.btnRedSkipAll.TabIndex = 5;
            this.btnRedSkipAll.Text = "Skip All";
            this.btnRedSkipAll.UseVisualStyleBackColor = true;
            this.btnRedSkipAll.Click += new System.EventHandler(this.btnRedSkipAll_Click);
            //
            // panelYellowNodes
            //
            this.panelYellowNodes.Controls.Add(this.lblYellowQuestion);
            this.panelYellowNodes.Controls.Add(this.diffViewer);
            this.panelYellowNodes.Controls.Add(this.btnYellowKeepLeft);
            this.panelYellowNodes.Controls.Add(this.btnYellowKeepRight);
            this.panelYellowNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelYellowNodes.Location = new System.Drawing.Point(0, 0);
            this.panelYellowNodes.Name = "panelYellowNodes";
            this.panelYellowNodes.Size = new System.Drawing.Size(800, 500);
            this.panelYellowNodes.TabIndex = 2;
            this.panelYellowNodes.Visible = false;
            //
            // lblYellowQuestion
            //
            this.lblYellowQuestion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblYellowQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblYellowQuestion.Location = new System.Drawing.Point(0, 0);
            this.lblYellowQuestion.Name = "lblYellowQuestion";
            this.lblYellowQuestion.Padding = new System.Windows.Forms.Padding(10);
            this.lblYellowQuestion.Size = new System.Drawing.Size(800, 40);
            this.lblYellowQuestion.TabIndex = 0;
            this.lblYellowQuestion.Text = "Yellow Nodes Question";
            //
            // diffViewer
            //
            this.diffViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diffViewer.Location = new System.Drawing.Point(10, 50);
            this.diffViewer.Name = "diffViewer";
            this.diffViewer.Size = new System.Drawing.Size(780, 390);
            this.diffViewer.TabIndex = 1;
            //
            // btnYellowKeepLeft
            //
            this.btnYellowKeepLeft.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnYellowKeepLeft.Location = new System.Drawing.Point(250, 450);
            this.btnYellowKeepLeft.Name = "btnYellowKeepLeft";
            this.btnYellowKeepLeft.Size = new System.Drawing.Size(100, 30);
            this.btnYellowKeepLeft.TabIndex = 2;
            this.btnYellowKeepLeft.Text = "Keep Left";
            this.btnYellowKeepLeft.UseVisualStyleBackColor = true;
            this.btnYellowKeepLeft.Click += new System.EventHandler(this.btnYellowKeepLeft_Click);
            //
            // btnYellowKeepRight
            //
            this.btnYellowKeepRight.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnYellowKeepRight.Location = new System.Drawing.Point(450, 450);
            this.btnYellowKeepRight.Name = "btnYellowKeepRight";
            this.btnYellowKeepRight.Size = new System.Drawing.Size(100, 30);
            this.btnYellowKeepRight.TabIndex = 3;
            this.btnYellowKeepRight.Text = "Keep Right";
            this.btnYellowKeepRight.UseVisualStyleBackColor = true;
            this.btnYellowKeepRight.Click += new System.EventHandler(this.btnYellowKeepRight_Click);
            //
            // panelSaveLocation
            //
            this.panelSaveLocation.Controls.Add(this.lblSaveLocation);
            this.panelSaveLocation.Controls.Add(this.txtSavePath);
            this.panelSaveLocation.Controls.Add(this.btnBrowse);
            this.panelSaveLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSaveLocation.Location = new System.Drawing.Point(0, 0);
            this.panelSaveLocation.Name = "panelSaveLocation";
            this.panelSaveLocation.Size = new System.Drawing.Size(800, 500);
            this.panelSaveLocation.TabIndex = 3;
            this.panelSaveLocation.Visible = false;
            //
            // lblSaveLocation
            //
            this.lblSaveLocation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSaveLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblSaveLocation.Location = new System.Drawing.Point(150, 200);
            this.lblSaveLocation.Name = "lblSaveLocation";
            this.lblSaveLocation.Size = new System.Drawing.Size(500, 30);
            this.lblSaveLocation.TabIndex = 0;
            this.lblSaveLocation.Text = "Choose where to save the merged class:";
            this.lblSaveLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // txtSavePath
            //
            this.txtSavePath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSavePath.Location = new System.Drawing.Point(150, 240);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(400, 20);
            this.txtSavePath.TabIndex = 1;
            //
            // btnBrowse
            //
            this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBrowse.Location = new System.Drawing.Point(560, 238);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(90, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            // panelNavigation
            //
            this.panelNavigation.Controls.Add(this.btnBack);
            this.panelNavigation.Controls.Add(this.btnNext);
            this.panelNavigation.Controls.Add(this.btnFinish);
            this.panelNavigation.Controls.Add(this.btnCancel);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelNavigation.Location = new System.Drawing.Point(0, 500);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(800, 50);
            this.panelNavigation.TabIndex = 4;
            //
            // btnBack
            //
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBack.Location = new System.Drawing.Point(458, 13);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            //
            // btnNext
            //
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNext.Location = new System.Drawing.Point(539, 13);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            //
            // btnFinish
            //
            this.btnFinish.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFinish.Location = new System.Drawing.Point(620, 13);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 2;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Visible = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(701, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // MergeWizardDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.panelGreenNodes);
            this.Controls.Add(this.panelRedNodes);
            this.Controls.Add(this.panelYellowNodes);
            this.Controls.Add(this.panelSaveLocation);
            this.Controls.Add(this.panelNavigation);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(816, 589);
            this.Name = "MergeWizardDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Merge Classes Wizard";
            this.panelGreenNodes.ResumeLayout(false);
            this.panelGreenNodes.PerformLayout();
            this.panelRedNodes.ResumeLayout(false);
            this.panelRedNodes.PerformLayout();
            this.panelYellowNodes.ResumeLayout(false);
            this.panelSaveLocation.ResumeLayout(false);
            this.panelSaveLocation.PerformLayout();
            this.panelNavigation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGreenNodes;
        private System.Windows.Forms.Label lblGreenQuestion;
        private System.Windows.Forms.TextBox txtGreenNodeInfo;
        private System.Windows.Forms.Button btnGreenAddThis;
        private System.Windows.Forms.Button btnGreenSkipThis;
        private System.Windows.Forms.Button btnGreenAddAll;
        private System.Windows.Forms.Button btnGreenSkipAll;
        private System.Windows.Forms.Panel panelRedNodes;
        private System.Windows.Forms.Label lblRedQuestion;
        private System.Windows.Forms.TextBox txtRedNodeInfo;
        private System.Windows.Forms.Button btnRedAddThis;
        private System.Windows.Forms.Button btnRedSkipThis;
        private System.Windows.Forms.Button btnRedAddAll;
        private System.Windows.Forms.Button btnRedSkipAll;
        private System.Windows.Forms.Panel panelYellowNodes;
        private System.Windows.Forms.Label lblYellowQuestion;
        private Controls.DiffViewerControl diffViewer;
        private System.Windows.Forms.Button btnYellowKeepLeft;
        private System.Windows.Forms.Button btnYellowKeepRight;
        private System.Windows.Forms.Panel panelSaveLocation;
        private System.Windows.Forms.Label lblSaveLocation;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnCancel;
    }
}
