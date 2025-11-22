namespace CidCodeComparer
{
    partial class ResultsForm
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
            this.components = new System.ComponentModel.Container();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerTrees = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.filterByModifiedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterByAddedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterByRemovedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterByEqualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterByDifferentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFile1 = new System.Windows.Forms.Label();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.lblFile2 = new System.Windows.Forms.Label();
            this.splitContainerEditors = new System.Windows.Forms.SplitContainer();
            this.txtEditor1 = new ICSharpCode.TextEditor.TextEditorControl();
            this.lblEditor1 = new System.Windows.Forms.Label();
            this.txtEditor2 = new ICSharpCode.TextEditor.TextEditorControl();
            this.lblEditor2 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnMergeClasses = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTrees)).BeginInit();
            this.splitContainerTrees.Panel1.SuspendLayout();
            this.splitContainerTrees.Panel2.SuspendLayout();
            this.splitContainerTrees.SuspendLayout();
            this.treeContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEditors)).BeginInit();
            this.splitContainerEditors.Panel1.SuspendLayout();
            this.splitContainerEditors.Panel2.SuspendLayout();
            this.splitContainerEditors.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerTrees);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerEditors);
            this.splitContainerMain.Size = new System.Drawing.Size(1200, 678);
            this.splitContainerMain.SplitterDistance = 273;
            this.splitContainerMain.TabIndex = 0;
            // 
            // splitContainerTrees
            // 
            this.splitContainerTrees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTrees.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTrees.Name = "splitContainerTrees";
            // 
            // splitContainerTrees.Panel1
            // 
            this.splitContainerTrees.Panel1.Controls.Add(this.treeView1);
            this.splitContainerTrees.Panel1.Controls.Add(this.lblFile1);
            this.splitContainerTrees.Panel1.SizeChanged += new System.EventHandler(this.splitContainerTrees_Panel1_SizeChanged);
            // 
            // splitContainerTrees.Panel2
            // 
            this.splitContainerTrees.Panel2.Controls.Add(this.treeView2);
            this.splitContainerTrees.Panel2.Controls.Add(this.lblFile2);
            this.splitContainerTrees.Size = new System.Drawing.Size(1200, 273);
            this.splitContainerTrees.SplitterDistance = 595;
            this.splitContainerTrees.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.treeContextMenu;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 23);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(595, 250);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // treeContextMenu
            // 
            this.treeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterByModifiedToolStripMenuItem,
            this.filterByAddedToolStripMenuItem,
            this.filterByRemovedToolStripMenuItem,
            this.filterByEqualToolStripMenuItem,
            this.filterByDifferentToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearFilterToolStripMenuItem});
            this.treeContextMenu.Name = "treeContextMenu";
            this.treeContextMenu.Size = new System.Drawing.Size(236, 142);
            this.treeContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.treeContextMenu_Opening);
            // 
            // filterByModifiedToolStripMenuItem
            // 
            this.filterByModifiedToolStripMenuItem.Name = "filterByModifiedToolStripMenuItem";
            this.filterByModifiedToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.filterByModifiedToolStripMenuItem.Text = "Filter by Modified (Yellow)";
            this.filterByModifiedToolStripMenuItem.Click += new System.EventHandler(this.filterByModifiedToolStripMenuItem_Click);
            // 
            // filterByAddedToolStripMenuItem
            // 
            this.filterByAddedToolStripMenuItem.Name = "filterByAddedToolStripMenuItem";
            this.filterByAddedToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.filterByAddedToolStripMenuItem.Text = "Filter by Added (Green)";
            this.filterByAddedToolStripMenuItem.Click += new System.EventHandler(this.filterByAddedToolStripMenuItem_Click);
            // 
            // filterByRemovedToolStripMenuItem
            // 
            this.filterByRemovedToolStripMenuItem.Name = "filterByRemovedToolStripMenuItem";
            this.filterByRemovedToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.filterByRemovedToolStripMenuItem.Text = "Filter by Removed (Red)";
            this.filterByRemovedToolStripMenuItem.Click += new System.EventHandler(this.filterByRemovedToolStripMenuItem_Click);
            // 
            // filterByEqualToolStripMenuItem
            // 
            this.filterByEqualToolStripMenuItem.Name = "filterByEqualToolStripMenuItem";
            this.filterByEqualToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.filterByEqualToolStripMenuItem.Text = "Filter by Equal (White)";
            this.filterByEqualToolStripMenuItem.Click += new System.EventHandler(this.filterByEqualToolStripMenuItem_Click);
            // 
            // filterByDifferentToolStripMenuItem
            // 
            this.filterByDifferentToolStripMenuItem.Name = "filterByDifferentToolStripMenuItem";
            this.filterByDifferentToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.filterByDifferentToolStripMenuItem.Text = "Filter by Different (Non-White)";
            this.filterByDifferentToolStripMenuItem.Click += new System.EventHandler(this.filterByDifferentToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(232, 6);
            // 
            // clearFilterToolStripMenuItem
            // 
            this.clearFilterToolStripMenuItem.Name = "clearFilterToolStripMenuItem";
            this.clearFilterToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.clearFilterToolStripMenuItem.Text = "Clear Filter";
            this.clearFilterToolStripMenuItem.Click += new System.EventHandler(this.clearFilterToolStripMenuItem_Click);
            // 
            // lblFile1
            // 
            this.lblFile1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblFile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFile1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFile1.ForeColor = System.Drawing.Color.White;
            this.lblFile1.Location = new System.Drawing.Point(0, 0);
            this.lblFile1.Name = "lblFile1";
            this.lblFile1.Padding = new System.Windows.Forms.Padding(5);
            this.lblFile1.Size = new System.Drawing.Size(595, 23);
            this.lblFile1.TabIndex = 1;
            this.lblFile1.Text = "File 1";
            this.lblFile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeView2
            // 
            this.treeView2.ContextMenuStrip = this.treeContextMenu;
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Location = new System.Drawing.Point(0, 23);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(601, 250);
            this.treeView2.TabIndex = 0;
            this.treeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterSelect);
            this.treeView2.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView2_NodeMouseDoubleClick);
            // 
            // lblFile2
            // 
            this.lblFile2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblFile2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFile2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFile2.ForeColor = System.Drawing.Color.White;
            this.lblFile2.Location = new System.Drawing.Point(0, 0);
            this.lblFile2.Name = "lblFile2";
            this.lblFile2.Padding = new System.Windows.Forms.Padding(5);
            this.lblFile2.Size = new System.Drawing.Size(601, 23);
            this.lblFile2.TabIndex = 1;
            this.lblFile2.Text = "File 2";
            this.lblFile2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainerEditors
            // 
            this.splitContainerEditors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEditors.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEditors.Name = "splitContainerEditors";
            // 
            // splitContainerEditors.Panel1
            // 
            this.splitContainerEditors.Panel1.Controls.Add(this.txtEditor1);
            this.splitContainerEditors.Panel1.Controls.Add(this.lblEditor1);
            this.splitContainerEditors.Panel1.SizeChanged += new System.EventHandler(this.splitContainerEditors_Panel1_SizeChanged);
            // 
            // splitContainerEditors.Panel2
            // 
            this.splitContainerEditors.Panel2.Controls.Add(this.txtEditor2);
            this.splitContainerEditors.Panel2.Controls.Add(this.lblEditor2);
            this.splitContainerEditors.Size = new System.Drawing.Size(1200, 401);
            this.splitContainerEditors.SplitterDistance = 595;
            this.splitContainerEditors.TabIndex = 0;
            // 
            // txtEditor1
            // 
            this.txtEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditor1.IsReadOnly = true;
            this.txtEditor1.Location = new System.Drawing.Point(0, 23);
            this.txtEditor1.Name = "txtEditor1";
            this.txtEditor1.Size = new System.Drawing.Size(595, 378);
            this.txtEditor1.TabIndex = 0;
            // 
            // lblEditor1
            // 
            this.lblEditor1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblEditor1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEditor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditor1.ForeColor = System.Drawing.Color.White;
            this.lblEditor1.Location = new System.Drawing.Point(0, 0);
            this.lblEditor1.Name = "lblEditor1";
            this.lblEditor1.Padding = new System.Windows.Forms.Padding(5);
            this.lblEditor1.Size = new System.Drawing.Size(595, 23);
            this.lblEditor1.TabIndex = 1;
            this.lblEditor1.Text = "File 1";
            this.lblEditor1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEditor2
            // 
            this.txtEditor2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditor2.IsReadOnly = true;
            this.txtEditor2.Location = new System.Drawing.Point(0, 23);
            this.txtEditor2.Name = "txtEditor2";
            this.txtEditor2.Size = new System.Drawing.Size(601, 378);
            this.txtEditor2.TabIndex = 0;
            // 
            // lblEditor2
            // 
            this.lblEditor2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblEditor2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEditor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditor2.ForeColor = System.Drawing.Color.White;
            this.lblEditor2.Location = new System.Drawing.Point(0, 0);
            this.lblEditor2.Name = "lblEditor2";
            this.lblEditor2.Padding = new System.Windows.Forms.Padding(5);
            this.lblEditor2.Size = new System.Drawing.Size(601, 23);
            this.lblEditor2.TabIndex = 1;
            this.lblEditor2.Text = "File 2";
            this.lblEditor2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 723);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnMergeClasses);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 678);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1200, 45);
            this.panelBottom.TabIndex = 2;
            // 
            // btnMergeClasses
            // 
            this.btnMergeClasses.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMergeClasses.Location = new System.Drawing.Point(525, 11);
            this.btnMergeClasses.Name = "btnMergeClasses";
            this.btnMergeClasses.Size = new System.Drawing.Size(150, 23);
            this.btnMergeClasses.TabIndex = 0;
            this.btnMergeClasses.Text = "Merge 2 Classes";
            this.btnMergeClasses.UseVisualStyleBackColor = true;
            this.btnMergeClasses.Click += new System.EventHandler(this.btnMergeClasses_Click);
            // 
            // ResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 745);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.statusStrip);
            this.Name = "ResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comparison Results";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.ResultsForm_Shown);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerTrees.Panel1.ResumeLayout(false);
            this.splitContainerTrees.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTrees)).EndInit();
            this.splitContainerTrees.ResumeLayout(false);
            this.treeContextMenu.ResumeLayout(false);
            this.splitContainerEditors.Panel1.ResumeLayout(false);
            this.splitContainerEditors.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEditors)).EndInit();
            this.splitContainerEditors.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnMergeClasses;
        private System.Windows.Forms.ContextMenuStrip treeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem filterByModifiedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterByAddedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterByRemovedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterByEqualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterByDifferentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearFilterToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerTrees;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lblFile1;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.Label lblFile2;
        private System.Windows.Forms.SplitContainer splitContainerEditors;
        private ICSharpCode.TextEditor.TextEditorControl txtEditor1;
        private System.Windows.Forms.Label lblEditor1;
        private ICSharpCode.TextEditor.TextEditorControl txtEditor2;
        private System.Windows.Forms.Label lblEditor2;
    }
}
