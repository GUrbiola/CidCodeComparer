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
            this.lblGreenTitle = new System.Windows.Forms.Label();
            this.splitContainerGreen = new System.Windows.Forms.SplitContainer();
            this.checkedListBoxGreen = new System.Windows.Forms.CheckedListBox();
            this.panelGreenButtons = new System.Windows.Forms.Panel();
            this.btnGreenSelectAll = new System.Windows.Forms.Button();
            this.btnGreenSelectNone = new System.Windows.Forms.Button();
            this.txtGreenPreview = new ICSharpCode.TextEditor.TextEditorControl();
            this.panelRedNodes = new System.Windows.Forms.Panel();
            this.lblRedTitle = new System.Windows.Forms.Label();
            this.splitContainerRed = new System.Windows.Forms.SplitContainer();
            this.checkedListBoxRed = new System.Windows.Forms.CheckedListBox();
            this.panelRedButtons = new System.Windows.Forms.Panel();
            this.btnRedSelectAll = new System.Windows.Forms.Button();
            this.btnRedSelectNone = new System.Windows.Forms.Button();
            this.txtRedPreview = new ICSharpCode.TextEditor.TextEditorControl();
            this.panelYellowNodes = new System.Windows.Forms.Panel();
            this.lblYellowQuestion = new System.Windows.Forms.Label();
            this.diffViewer = new CidCodeComparer.Controls.DiffViewerControl();
            this.btnYellowKeepLeft = new System.Windows.Forms.Button();
            this.btnYellowKeepRight = new System.Windows.Forms.Button();
            this.panelSaveLocation = new System.Windows.Forms.Panel();
            this.lblSaveLocation = new System.Windows.Forms.Label();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelGreenNodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGreen)).BeginInit();
            this.splitContainerGreen.Panel1.SuspendLayout();
            this.splitContainerGreen.Panel2.SuspendLayout();
            this.splitContainerGreen.SuspendLayout();
            this.panelGreenButtons.SuspendLayout();
            this.panelRedNodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRed)).BeginInit();
            this.splitContainerRed.Panel1.SuspendLayout();
            this.splitContainerRed.Panel2.SuspendLayout();
            this.splitContainerRed.SuspendLayout();
            this.panelRedButtons.SuspendLayout();
            this.panelYellowNodes.SuspendLayout();
            this.panelSaveLocation.SuspendLayout();
            this.panelNavigation.SuspendLayout();
            this.SuspendLayout();
            //
            // panelGreenNodes
            //
            this.panelGreenNodes.Controls.Add(this.splitContainerGreen);
            this.panelGreenNodes.Controls.Add(this.lblGreenTitle);
            this.panelGreenNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGreenNodes.Location = new System.Drawing.Point(0, 0);
            this.panelGreenNodes.Name = "panelGreenNodes";
            this.panelGreenNodes.Size = new System.Drawing.Size(800, 500);
            this.panelGreenNodes.TabIndex = 0;
            this.panelGreenNodes.Visible = false;
            //
            // lblGreenTitle
            //
            this.lblGreenTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGreenTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblGreenTitle.Location = new System.Drawing.Point(0, 0);
            this.lblGreenTitle.Name = "lblGreenTitle";
            this.lblGreenTitle.Padding = new System.Windows.Forms.Padding(10);
            this.lblGreenTitle.Size = new System.Drawing.Size(800, 40);
            this.lblGreenTitle.TabIndex = 0;
            this.lblGreenTitle.Text = "Select members from LEFT class to add:";
            //
            // splitContainerGreen
            //
            this.splitContainerGreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerGreen.Location = new System.Drawing.Point(0, 40);
            this.splitContainerGreen.Name = "splitContainerGreen";
            this.splitContainerGreen.Size = new System.Drawing.Size(800, 460);
            this.splitContainerGreen.SplitterDistance = 350;
            this.splitContainerGreen.TabIndex = 1;
            //
            // splitContainerGreen.Panel1
            //
            this.splitContainerGreen.Panel1.Controls.Add(this.checkedListBoxGreen);
            this.splitContainerGreen.Panel1.Controls.Add(this.panelGreenButtons);
            //
            // splitContainerGreen.Panel2
            //
            this.splitContainerGreen.Panel2.Controls.Add(this.txtGreenPreview);
            //
            // checkedListBoxGreen
            //
            this.checkedListBoxGreen.BackColor = System.Drawing.Color.LightGreen;
            this.checkedListBoxGreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxGreen.FormattingEnabled = true;
            this.checkedListBoxGreen.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxGreen.Name = "checkedListBoxGreen";
            this.checkedListBoxGreen.Size = new System.Drawing.Size(350, 420);
            this.checkedListBoxGreen.TabIndex = 0;
            this.checkedListBoxGreen.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxGreen_SelectedIndexChanged);
            //
            // panelGreenButtons
            //
            this.panelGreenButtons.Controls.Add(this.btnGreenSelectAll);
            this.panelGreenButtons.Controls.Add(this.btnGreenSelectNone);
            this.panelGreenButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGreenButtons.Location = new System.Drawing.Point(0, 420);
            this.panelGreenButtons.Name = "panelGreenButtons";
            this.panelGreenButtons.Size = new System.Drawing.Size(350, 40);
            this.panelGreenButtons.TabIndex = 1;
            //
            // btnGreenSelectAll
            //
            this.btnGreenSelectAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGreenSelectAll.Location = new System.Drawing.Point(75, 8);
            this.btnGreenSelectAll.Name = "btnGreenSelectAll";
            this.btnGreenSelectAll.Size = new System.Drawing.Size(90, 25);
            this.btnGreenSelectAll.TabIndex = 0;
            this.btnGreenSelectAll.Text = "Select All";
            this.btnGreenSelectAll.UseVisualStyleBackColor = true;
            this.btnGreenSelectAll.Click += new System.EventHandler(this.btnGreenSelectAll_Click);
            //
            // btnGreenSelectNone
            //
            this.btnGreenSelectNone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGreenSelectNone.Location = new System.Drawing.Point(185, 8);
            this.btnGreenSelectNone.Name = "btnGreenSelectNone";
            this.btnGreenSelectNone.Size = new System.Drawing.Size(90, 25);
            this.btnGreenSelectNone.TabIndex = 1;
            this.btnGreenSelectNone.Text = "Select None";
            this.btnGreenSelectNone.UseVisualStyleBackColor = true;
            this.btnGreenSelectNone.Click += new System.EventHandler(this.btnGreenSelectNone_Click);
            //
            // txtGreenPreview
            //
            this.txtGreenPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGreenPreview.IsReadOnly = true;
            this.txtGreenPreview.Location = new System.Drawing.Point(0, 0);
            this.txtGreenPreview.Name = "txtGreenPreview";
            this.txtGreenPreview.ShowLineNumbers = true;
            this.txtGreenPreview.Size = new System.Drawing.Size(446, 460);
            this.txtGreenPreview.TabIndex = 0;
            //
            // panelRedNodes
            //
            this.panelRedNodes.Controls.Add(this.splitContainerRed);
            this.panelRedNodes.Controls.Add(this.lblRedTitle);
            this.panelRedNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRedNodes.Location = new System.Drawing.Point(0, 0);
            this.panelRedNodes.Name = "panelRedNodes";
            this.panelRedNodes.Size = new System.Drawing.Size(800, 500);
            this.panelRedNodes.TabIndex = 1;
            this.panelRedNodes.Visible = false;
            //
            // lblRedTitle
            //
            this.lblRedTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRedTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblRedTitle.Location = new System.Drawing.Point(0, 0);
            this.lblRedTitle.Name = "lblRedTitle";
            this.lblRedTitle.Padding = new System.Windows.Forms.Padding(10);
            this.lblRedTitle.Size = new System.Drawing.Size(800, 40);
            this.lblRedTitle.TabIndex = 0;
            this.lblRedTitle.Text = "Select members from RIGHT class to add:";
            //
            // splitContainerRed
            //
            this.splitContainerRed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRed.Location = new System.Drawing.Point(0, 40);
            this.splitContainerRed.Name = "splitContainerRed";
            this.splitContainerRed.Size = new System.Drawing.Size(800, 460);
            this.splitContainerRed.SplitterDistance = 350;
            this.splitContainerRed.TabIndex = 1;
            //
            // splitContainerRed.Panel1
            //
            this.splitContainerRed.Panel1.Controls.Add(this.checkedListBoxRed);
            this.splitContainerRed.Panel1.Controls.Add(this.panelRedButtons);
            //
            // splitContainerRed.Panel2
            //
            this.splitContainerRed.Panel2.Controls.Add(this.txtRedPreview);
            //
            // checkedListBoxRed
            //
            this.checkedListBoxRed.BackColor = System.Drawing.Color.LightCoral;
            this.checkedListBoxRed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxRed.FormattingEnabled = true;
            this.checkedListBoxRed.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxRed.Name = "checkedListBoxRed";
            this.checkedListBoxRed.Size = new System.Drawing.Size(350, 420);
            this.checkedListBoxRed.TabIndex = 0;
            this.checkedListBoxRed.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxRed_SelectedIndexChanged);
            //
            // panelRedButtons
            //
            this.panelRedButtons.Controls.Add(this.btnRedSelectAll);
            this.panelRedButtons.Controls.Add(this.btnRedSelectNone);
            this.panelRedButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRedButtons.Location = new System.Drawing.Point(0, 420);
            this.panelRedButtons.Name = "panelRedButtons";
            this.panelRedButtons.Size = new System.Drawing.Size(350, 40);
            this.panelRedButtons.TabIndex = 1;
            //
            // btnRedSelectAll
            //
            this.btnRedSelectAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRedSelectAll.Location = new System.Drawing.Point(75, 8);
            this.btnRedSelectAll.Name = "btnRedSelectAll";
            this.btnRedSelectAll.Size = new System.Drawing.Size(90, 25);
            this.btnRedSelectAll.TabIndex = 0;
            this.btnRedSelectAll.Text = "Select All";
            this.btnRedSelectAll.UseVisualStyleBackColor = true;
            this.btnRedSelectAll.Click += new System.EventHandler(this.btnRedSelectAll_Click);
            //
            // btnRedSelectNone
            //
            this.btnRedSelectNone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRedSelectNone.Location = new System.Drawing.Point(185, 8);
            this.btnRedSelectNone.Name = "btnRedSelectNone";
            this.btnRedSelectNone.Size = new System.Drawing.Size(90, 25);
            this.btnRedSelectNone.TabIndex = 1;
            this.btnRedSelectNone.Text = "Select None";
            this.btnRedSelectNone.UseVisualStyleBackColor = true;
            this.btnRedSelectNone.Click += new System.EventHandler(this.btnRedSelectNone_Click);
            //
            // txtRedPreview
            //
            this.txtRedPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRedPreview.IsReadOnly = true;
            this.txtRedPreview.Location = new System.Drawing.Point(0, 0);
            this.txtRedPreview.Name = "txtRedPreview";
            this.txtRedPreview.ShowLineNumbers = true;
            this.txtRedPreview.Size = new System.Drawing.Size(446, 460);
            this.txtRedPreview.TabIndex = 0;
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
            this.panelSaveLocation.Controls.Add(this.lblNamespace);
            this.panelSaveLocation.Controls.Add(this.txtNamespace);
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
            this.lblSaveLocation.Location = new System.Drawing.Point(150, 150);
            this.lblSaveLocation.Name = "lblSaveLocation";
            this.lblSaveLocation.Size = new System.Drawing.Size(500, 30);
            this.lblSaveLocation.TabIndex = 0;
            this.lblSaveLocation.Text = "Choose where to save the merged class:";
            this.lblSaveLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // txtSavePath
            //
            this.txtSavePath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSavePath.Location = new System.Drawing.Point(150, 190);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(400, 20);
            this.txtSavePath.TabIndex = 1;
            //
            // btnBrowse
            //
            this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBrowse.Location = new System.Drawing.Point(560, 188);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(90, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            // lblNamespace
            //
            this.lblNamespace.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblNamespace.Location = new System.Drawing.Point(150, 240);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(500, 30);
            this.lblNamespace.TabIndex = 3;
            this.lblNamespace.Text = "Namespace for the merged class:";
            this.lblNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // txtNamespace
            //
            this.txtNamespace.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNamespace.Location = new System.Drawing.Point(150, 280);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(500, 20);
            this.txtNamespace.TabIndex = 4;
            this.txtNamespace.Text = "Default";
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
            this.splitContainerGreen.Panel1.ResumeLayout(false);
            this.splitContainerGreen.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGreen)).EndInit();
            this.splitContainerGreen.ResumeLayout(false);
            this.panelGreenButtons.ResumeLayout(false);
            this.panelRedNodes.ResumeLayout(false);
            this.splitContainerRed.Panel1.ResumeLayout(false);
            this.splitContainerRed.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRed)).EndInit();
            this.splitContainerRed.ResumeLayout(false);
            this.panelRedButtons.ResumeLayout(false);
            this.panelYellowNodes.ResumeLayout(false);
            this.panelSaveLocation.ResumeLayout(false);
            this.panelSaveLocation.PerformLayout();
            this.panelNavigation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGreenNodes;
        private System.Windows.Forms.Label lblGreenTitle;
        private System.Windows.Forms.SplitContainer splitContainerGreen;
        private System.Windows.Forms.CheckedListBox checkedListBoxGreen;
        private System.Windows.Forms.Panel panelGreenButtons;
        private System.Windows.Forms.Button btnGreenSelectAll;
        private System.Windows.Forms.Button btnGreenSelectNone;
        private ICSharpCode.TextEditor.TextEditorControl txtGreenPreview;
        private System.Windows.Forms.Panel panelRedNodes;
        private System.Windows.Forms.Label lblRedTitle;
        private System.Windows.Forms.SplitContainer splitContainerRed;
        private System.Windows.Forms.CheckedListBox checkedListBoxRed;
        private System.Windows.Forms.Panel panelRedButtons;
        private System.Windows.Forms.Button btnRedSelectAll;
        private System.Windows.Forms.Button btnRedSelectNone;
        private ICSharpCode.TextEditor.TextEditorControl txtRedPreview;
        private System.Windows.Forms.Panel panelYellowNodes;
        private System.Windows.Forms.Label lblYellowQuestion;
        private Controls.DiffViewerControl diffViewer;
        private System.Windows.Forms.Button btnYellowKeepLeft;
        private System.Windows.Forms.Button btnYellowKeepRight;
        private System.Windows.Forms.Panel panelSaveLocation;
        private System.Windows.Forms.Label lblSaveLocation;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnCancel;
    }
}
