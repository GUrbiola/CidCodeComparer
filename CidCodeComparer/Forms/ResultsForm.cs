using CidCodeComparer.Engine;
using CidCodeComparer.Extensions;
using CidCodeComparer.Models;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Windows.Forms;

namespace CidCodeComparer
{
    public partial class ResultsForm : Form
    {
        private readonly string _file1Path;
        private readonly string _file2Path;
        private readonly string _fileType;
        private ComparisonResult _comparisonResult;
        private ImageList _imageList;
        private Dictionary<string, CodeNode> _file1Nodes;
        private Dictionary<string, CodeNode> _file2Nodes;
        private Dictionary<TreeNode, string> _treeNodeKeys;
        private bool _suppressSelectionEvents = false;
        private Dictionary<TreeNode, List<TreeNode>> _originalChildren = new Dictionary<TreeNode, List<TreeNode>>();
        private TreeNode _contextMenuNode = null;

        public ResultsForm(string file1Path, string file2Path, string fileType)
        {
            InitializeComponent();
            _file1Path = file1Path;
            _file2Path = file2Path;
            _fileType = fileType;

            InitializeImageList();
            this.Load += ResultsForm_Load;
        }

        private void InitializeImageList()
        {
            _imageList = new ImageList();
            _imageList.ImageSize = new Size(16, 16);
            _imageList.ColorDepth = ColorDepth.Depth32Bit;

            // Create icons for different node types
            _imageList.Images.Add("File", CreateIcon(Color.LightGray, "F"));
            _imageList.Images.Add("Namespace", CreateIcon(Color.LightBlue, "N"));
            _imageList.Images.Add("Class", CreateIcon(Color.FromArgb(255, 200, 100), "C"));
            _imageList.Images.Add("Interface", CreateIcon(Color.FromArgb(180, 150, 255), "I"));
            _imageList.Images.Add("Struct", CreateIcon(Color.FromArgb(150, 200, 150), "S"));
            _imageList.Images.Add("Enum", CreateIcon(Color.FromArgb(200, 200, 150), "E"));
            _imageList.Images.Add("Record", CreateIcon(Color.FromArgb(200, 180, 200), "R"));
            _imageList.Images.Add("Method", CreateIcon(Color.FromArgb(255, 180, 200), "M"));
            _imageList.Images.Add("Property", CreateIcon(Color.FromArgb(180, 220, 255), "P"));
            _imageList.Images.Add("Field", CreateIcon(Color.FromArgb(220, 220, 220), "f"));
            _imageList.Images.Add("Constructor", CreateIcon(Color.FromArgb(255, 200, 150), "c"));
            _imageList.Images.Add("Event", CreateIcon(Color.FromArgb(255, 255, 150), "e"));

            treeView1.ImageList = _imageList;
            treeView2.ImageList = _imageList;
        }

        private Bitmap CreateIcon(Color backgroundColor, string letter)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Draw rounded rectangle background
                using (Brush brush = new SolidBrush(backgroundColor))
                {
                    g.FillRectangle(brush, 0, 0, 16, 16);
                }

                // Draw border
                using (Pen pen = new Pen(Color.FromArgb(100, 100, 100), 1))
                {
                    g.DrawRectangle(pen, 0, 0, 15, 15);
                }

                // Draw letter
                using (Font font = new Font("Arial", 9, FontStyle.Bold))
                using (Brush textBrush = new SolidBrush(Color.Black))
                {
                    SizeF textSize = g.MeasureString(letter, font);
                    float x = (16 - textSize.Width) / 2;
                    float y = (16 - textSize.Height) / 2;
                    g.DrawString(letter, font, textBrush, x, y);
                }
            }
            return bmp;
        }

        private void ResultsForm_Load(object sender, EventArgs e)
        {
            // Update labels
            lblFile1.Text = Path.GetFileName(_file1Path);
            lblFile2.Text = Path.GetFileName(_file2Path);

            // Start comparison on background thread
            lblStatus.Text = "Comparing files...";
            this.Cursor = Cursors.WaitCursor;

            var thread = new Thread(PerformComparisonBackground);
            thread.IsBackground = true;
            thread.Start();
        }

        private void PerformComparisonBackground()
        {
            try
            {
                var engine = new ComparisonEngine();
                var result = engine.CompareFiles(_file1Path, _file2Path, _fileType);

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => OnComparisonComplete(result)));
                }
                else
                {
                    OnComparisonComplete(result);
                }
            }
            catch (Exception ex)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => OnComparisonError(ex)));
                }
                else
                {
                    OnComparisonError(ex);
                }
            }
        }

        private void OnComparisonComplete(ComparisonResult result)
        {
            _comparisonResult = result;
            this.Cursor = Cursors.Default;

            // Build node dictionaries for comparison
            _file1Nodes = new Dictionary<string, CodeNode>();
            _file2Nodes = new Dictionary<string, CodeNode>();
            _treeNodeKeys = new Dictionary<TreeNode, string>();

            if (result.File1Structure != null)
            {
                BuildNodeDictionary(result.File1Structure, "", _file1Nodes);
            }

            if (result.File2Structure != null)
            {
                BuildNodeDictionary(result.File2Structure, "", _file2Nodes);
            }

            // Populate tree views with color coding
            if (result.File1Structure != null)
            {
                treeView1.Nodes.Clear();
                var rootNode1 = CreateTreeNode(result.File1Structure, "", true);
                treeView1.Nodes.Add(rootNode1);
                rootNode1.Expand();
            }

            if (result.File2Structure != null)
            {
                treeView2.Nodes.Clear();
                var rootNode2 = CreateTreeNode(result.File2Structure, "", false);
                treeView2.Nodes.Add(rootNode2);
                rootNode2.Expand();
            }

            // Load files into TextEditorControls
            string file1Content = File.ReadAllText(_file1Path);
            string file2Content = File.ReadAllText(_file2Path);

            txtEditor1.Text = file1Content;
            txtEditor1.SetHighlighting("C#");
            txtEditor1.IsReadOnly = true;

            txtEditor2.Text = file2Content;
            txtEditor2.SetHighlighting("C#");
            txtEditor2.IsReadOnly = true;

            lblEditor1.Text = Path.GetFileName(_file1Path);
            lblEditor2.Text = Path.GetFileName(_file2Path);

            // Update status
            int added = 0, removed = 0, modified = 0;
            foreach (var diff in result.Differences)
            {
                switch (diff.Type)
                {
                    case DifferenceType.Added:
                        added++;
                        break;
                    case DifferenceType.Removed:
                        removed++;
                        break;
                    case DifferenceType.Modified:
                        modified++;
                        break;
                }
            }

            lblStatus.Text = $"Added: {added}, Removed: {removed}, Modified: {modified}";
        }

        private void OnComparisonError(Exception ex)
        {
            this.Cursor = Cursors.Default;
            MessageBox.Show($"Error comparing files: {ex.Message}",
                "Comparison Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }

        private void BuildNodeDictionary(CodeNode node, string parentPath, Dictionary<string, CodeNode> dictionary)
        {
            string nodePath;

            // Ignore namespaces in the path
            if (node.Type == "Namespace" || node.Type == "File")
            {
                nodePath = ""; // Don't include namespace/file in path
            }
            else if (node.Type == "Class" || node.Type == "Interface" || node.Type == "Struct" ||
                     node.Type == "Enum" || node.Type == "Record")
            {
                // For classes and similar types, use just the class name
                nodePath = node.Name;
            }
            else
            {
                // For class members (methods, properties, fields, etc.)
                // Use ClassName.MemberName
                nodePath = string.IsNullOrEmpty(parentPath)
                    ? node.Name
                    : $"{parentPath}.{node.Name}";
            }

            // Create key with type information
            string key = $"{nodePath}|{node.Type}";

            // For methods, include parameters in the key for overload differentiation
            if (node.Type == "Method" && node.Parameters != null && node.Parameters.Count > 0)
            {
                var paramTypes = string.Join(",", node.Parameters.Select(p => p.Type));
                key = $"{nodePath}({paramTypes})|{node.Type}";
            }

            // Only add non-namespace/file nodes to dictionary
            if (node.Type != "Namespace" && node.Type != "File")
            {
                dictionary[key] = node;
            }

            foreach (var child in node.Children)
            {
                BuildNodeDictionary(child, nodePath, dictionary);
            }
        }

        private TreeNode CreateTreeNode(CodeNode codeNode, string parentPath, bool isLeftTree)
        {
            string displayText = codeNode.Name;

            // Add more detailed information to the display
            if (!string.IsNullOrEmpty(codeNode.ReturnType))
            {
                displayText = $"{codeNode.Name}: {codeNode.ReturnType}";
            }
            else if (!string.IsNullOrEmpty(codeNode.DataType))
            {
                displayText = $"{codeNode.Name}: {codeNode.DataType}";
            }

            // Add parameters for methods
            if (codeNode.Type == "Method" && codeNode.Parameters != null && codeNode.Parameters.Count > 0)
            {
                var paramList = string.Join(", ", codeNode.Parameters.Select(p => $"{p.Type} {p.Name}"));
                displayText = $"{codeNode.Name}({paramList})";
                if (!string.IsNullOrEmpty(codeNode.ReturnType))
                {
                    displayText += $": {codeNode.ReturnType}";
                }
            }

            var treeNode = new TreeNode(displayText)
            {
                Tag = codeNode
            };

            // Set icon based on node type
            string iconKey = codeNode.Type;
            if (_imageList.Images.ContainsKey(iconKey))
            {
                treeNode.ImageKey = iconKey;
                treeNode.SelectedImageKey = iconKey;
            }

            // Build node path for comparison (ignoring namespaces)
            string nodePath;

            if (codeNode.Type == "Namespace" || codeNode.Type == "File")
            {
                nodePath = ""; // Don't include namespace/file in path
            }
            else if (codeNode.Type == "Class" || codeNode.Type == "Interface" || codeNode.Type == "Struct" ||
                     codeNode.Type == "Enum" || codeNode.Type == "Record")
            {
                // For classes and similar types, use just the class name
                nodePath = codeNode.Name;
            }
            else
            {
                // For class members (methods, properties, fields, etc.)
                // Use ClassName.MemberName
                nodePath = string.IsNullOrEmpty(parentPath)
                    ? codeNode.Name
                    : $"{parentPath}.{codeNode.Name}";
            }

            string nodeKey = $"{nodePath}|{codeNode.Type}";
            if (codeNode.Type == "Method" && codeNode.Parameters != null && codeNode.Parameters.Count > 0)
            {
                var paramTypes = string.Join(",", codeNode.Parameters.Select(p => p.Type));
                nodeKey = $"{nodePath}({paramTypes})|{codeNode.Type}";
            }

            // Store the node key for later retrieval
            _treeNodeKeys[treeNode] = nodeKey;

            // Determine color based on comparison (skip namespaces and files)
            if (codeNode.Type != "Namespace" && codeNode.Type != "File")
            {
                Color? backgroundColor = GetNodeBackgroundColor(nodeKey, codeNode, isLeftTree);
                if (backgroundColor.HasValue)
                {
                    treeNode.BackColor = backgroundColor.Value;
                }
            }

            // Add children (sorted alphabetically)
            var sortedChildren = codeNode.Children.OrderBy(c => c.Name).ToList();
            foreach (var child in sortedChildren)
            {
                treeNode.Nodes.Add(CreateTreeNode(child, nodePath, isLeftTree));
            }

            return treeNode;
        }

        private Color? GetNodeBackgroundColor(string nodeKey, CodeNode codeNode, bool isLeftTree)
        {
            if (isLeftTree)
            {
                // Check if node exists in right tree
                if (!_file2Nodes.ContainsKey(nodeKey))
                {
                    // Only in left tree - GREEN
                    return Color.LightGreen;
                }
                else
                {
                    // Exists in both trees - check if source code is the same
                    var rightNode = _file2Nodes[nodeKey];
                    if (!IsSourceCodeEqual(codeNode.SourceCode, rightNode.SourceCode))
                    {
                        // Different source code - YELLOW
                        return Color.LightYellow;
                    }
                }
            }
            else
            {
                // Check if node exists in left tree
                if (!_file1Nodes.ContainsKey(nodeKey))
                {
                    // Only in right tree - RED
                    return Color.LightCoral;
                }
                else
                {
                    // Exists in both trees - check if source code is the same
                    var leftNode = _file1Nodes[nodeKey];
                    if (!IsSourceCodeEqual(leftNode.SourceCode, codeNode.SourceCode))
                    {
                        // Different source code - YELLOW
                        return Color.LightYellow;
                    }
                }
            }

            // Same in both or no color needed
            return null;
        }

        private bool IsSourceCodeEqual(string source1, string source2)
        {
            // Normalize whitespace and compare
            string normalized1 = NormalizeSourceCode(source1);
            string normalized2 = NormalizeSourceCode(source2);
            return normalized1 == normalized2;
        }

        private string NormalizeSourceCode(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
                return string.Empty;

            // Remove leading/trailing whitespace and normalize line endings
            return sourceCode.Trim().Replace("\r\n", "\n").Replace("\r", "\n");
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node?.Tag is CodeNode codeNode)
            {
                ShowNodeDetails(e.Node, codeNode, true);
            }
        }

        private void treeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node?.Tag is CodeNode codeNode)
            {
                ShowNodeDetails(e.Node, codeNode, false);
            }
        }

        private void ShowNodeDetails(TreeNode treeNode, CodeNode codeNode, bool isLeftTree)
        {
            // Check if the node has a yellow background (modified)
            if (treeNode.BackColor == Color.LightYellow)
            {
                // Get the node key from the stored dictionary
                string nodeKey = _treeNodeKeys.ContainsKey(treeNode) ? _treeNodeKeys[treeNode] : null;

                if (!string.IsNullOrEmpty(nodeKey))
                {
                    CodeNode otherNode = isLeftTree
                        ? (_file2Nodes.ContainsKey(nodeKey) ? _file2Nodes[nodeKey] : null)
                        : (_file1Nodes.ContainsKey(nodeKey) ? _file1Nodes[nodeKey] : null);

                    if (otherNode != null)
                    {
                        // Show comparison dialog with left node first, right node second
                        using (var dialog = new Forms.NodeComparisonDialog(
                            isLeftTree ? codeNode : otherNode,
                            isLeftTree ? otherNode : codeNode))
                        {
                            dialog.ShowDialog(this);
                        }
                        return;
                    }
                }

                // Fallback to regular details if other node not found
                using (var dialog = new Forms.NodeDetailsDialog(codeNode))
                {
                    dialog.ShowDialog(this);
                }
            }
            else
            {
                // Show regular details dialog for non-modified nodes
                using (var dialog = new Forms.NodeDetailsDialog(codeNode))
                {
                    dialog.ShowDialog(this);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_suppressSelectionEvents || e.Node == null)
                return;

            if (e.Node.Tag is CodeNode codeNode)
            {
                // Move cursor to the node's position in the left editor
                MoveCursorToNode(txtEditor1, codeNode);

                // If it's a yellow node (modified), select the matching node in the other tree
                if (e.Node.BackColor == Color.LightYellow)
                {
                    SelectMatchingNode(treeView2, e.Node, codeNode, txtEditor2);
                }
            }
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_suppressSelectionEvents || e.Node == null)
                return;

            if (e.Node.Tag is CodeNode codeNode)
            {
                // Move cursor to the node's position in the right editor
                MoveCursorToNode(txtEditor2, codeNode);

                // If it's a yellow node (modified), select the matching node in the other tree
                if (e.Node.BackColor == Color.LightYellow)
                {
                    SelectMatchingNode(treeView1, e.Node, codeNode, txtEditor1);
                }
            }
        }

        private void MoveCursorToNode(ICSharpCode.TextEditor.TextEditorControl editor, CodeNode node)
        {
            if (node.StartLine < 0 || node.StartLine >= editor.Document.TotalNumberOfLines)
                return;

            try
            {
                // Clear any existing selection first
                editor.ActiveTextAreaControl.SelectionManager.ClearSelection();

                // Calculate the end line, ensuring it's within bounds
                int endLine = Math.Min(node.EndLine, editor.Document.TotalNumberOfLines - 1);

                // Get the line segments for start and end
                LineSegment startLineSegment = editor.Document.GetLineSegment(node.StartLine);
                LineSegment endLineSegment = editor.Document.GetLineSegment(endLine);

                // Create selection from the start of StartLine to the end of EndLine
                TextLocation start = new TextLocation(0, node.StartLine);
                TextLocation end = new TextLocation(endLineSegment.Length, endLine);

                // Set the selection to highlight the entire node's code
                editor.ActiveTextAreaControl.SelectionManager.SetSelection(start, end);

                // Use the extension method to scroll to the line and center it
                editor.ScrollToLine(node.StartLine, centerInView: true);
            }
            catch
            {
                // If positioning fails, just ignore
            }
        }

        private void SelectMatchingNode(TreeView otherTreeView, TreeNode currentNode, CodeNode currentCodeNode, ICSharpCode.TextEditor.TextEditorControl otherEditor)
        {
            // Get the node key
            string nodeKey = _treeNodeKeys.ContainsKey(currentNode) ? _treeNodeKeys[currentNode] : null;

            if (string.IsNullOrEmpty(nodeKey))
                return;

            // Find the matching CodeNode in the other dictionary
            bool isLeftTree = otherTreeView == treeView2;
            CodeNode matchingCodeNode = isLeftTree
                ? (_file2Nodes.ContainsKey(nodeKey) ? _file2Nodes[nodeKey] : null)
                : (_file1Nodes.ContainsKey(nodeKey) ? _file1Nodes[nodeKey] : null);

            if (matchingCodeNode == null)
                return;

            // Find the matching TreeNode
            TreeNode matchingTreeNode = FindTreeNodeByCodeNode(otherTreeView.Nodes, matchingCodeNode);

            if (matchingTreeNode != null)
            {
                // Use BeginInvoke to ensure UI updates happen on the message queue
                // This prevents timing issues with the selection events
                this.BeginInvoke(new Action(() =>
                {
                    // Suppress selection events to avoid infinite recursion
                    _suppressSelectionEvents = true;
                    try
                    {
                        // Select the matching node in the other tree
                        otherTreeView.SelectedNode = matchingTreeNode;
                        matchingTreeNode.EnsureVisible();

                        // Force the tree view to refresh
                        otherTreeView.Refresh();

                        // Move cursor in the other editor
                        MoveCursorToNode(otherEditor, matchingCodeNode);
                    }
                    finally
                    {
                        _suppressSelectionEvents = false;
                    }
                }));
            }
        }

        private TreeNode FindTreeNodeByCodeNode(TreeNodeCollection nodes, CodeNode targetCodeNode)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag == targetCodeNode)
                    return node;

                TreeNode found = FindTreeNodeByCodeNode(node.Nodes, targetCodeNode);
                if (found != null)
                    return found;
            }

            return null;
        }

        private void treeContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Get the TreeView that opened the context menu
            TreeView treeView = treeContextMenu.SourceControl as TreeView;
            if (treeView == null)
            {
                e.Cancel = true;
                return;
            }

            // Get the node at the mouse position
            Point mousePos = treeView.PointToClient(Cursor.Position);
            _contextMenuNode = treeView.GetNodeAt(mousePos);

            // Show menu if node has children OR if it has a filter applied
            bool hasFilter = _contextMenuNode != null && _originalChildren.ContainsKey(_contextMenuNode);
            bool hasChildren = _contextMenuNode != null && (_contextMenuNode.Nodes.Count > 0 || hasFilter);

            if (!hasChildren)
            {
                // No node, no children, and no filter - cancel the menu
                e.Cancel = true;
                return;
            }

            // Always show all menu items - Clear Filter is always available
        }

        private void filterByModifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextMenuNode != null)
            {
                ApplyFilter(_contextMenuNode, Color.LightYellow);
            }
        }

        private void filterByAddedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextMenuNode != null)
            {
                TreeView treeView = _contextMenuNode.TreeView;
                bool isLeftTree = (treeView == treeView1);

                // Added (Green) only shows on left tree, so filter by Green
                ApplyFilter(_contextMenuNode, Color.LightGreen);
            }
        }

        private void filterByRemovedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextMenuNode != null)
            {
                TreeView treeView = _contextMenuNode.TreeView;
                bool isLeftTree = (treeView == treeView1);

                // Removed (Red) only shows on right tree, so filter by Red
                ApplyFilter(_contextMenuNode, Color.LightCoral);
            }
        }

        private void filterByEqualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextMenuNode != null)
            {
                ApplyFilterByEqual(_contextMenuNode);
            }
        }

        private void filterByDifferentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextMenuNode != null)
            {
                ApplyFilterByDifferent(_contextMenuNode);
            }
        }

        private void clearFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextMenuNode != null)
            {
                ClearFilter(_contextMenuNode);
            }
        }

        private void ApplyFilter(TreeNode node, Color filterColor)
        {
            TreeView treeView = node.TreeView;
            bool isLeftTree = (treeView == treeView1);

            // Store original children if not already stored
            if (!_originalChildren.ContainsKey(node))
            {
                List<TreeNode> children = new List<TreeNode>();
                foreach (TreeNode child in node.Nodes)
                {
                    children.Add(child);
                }
                _originalChildren[node] = children;
            }

            // Clear current nodes
            node.Nodes.Clear();

            // Add back only nodes that match the filter color
            foreach (TreeNode child in _originalChildren[node])
            {
                if (child.BackColor == filterColor)
                {
                    node.Nodes.Add(child);
                }
            }

            // Expand the node to show filtered results
            node.Expand();

            // If the current node has a match in the other tree, apply the same filter there
            string nodeKey = _treeNodeKeys.ContainsKey(node) ? _treeNodeKeys[node] : null;
            if (!string.IsNullOrEmpty(nodeKey))
            {
                TreeView otherTreeView = isLeftTree ? treeView2 : treeView1;
                CodeNode matchingCodeNode = isLeftTree
                    ? (_file2Nodes.ContainsKey(nodeKey) ? _file2Nodes[nodeKey] : null)
                    : (_file1Nodes.ContainsKey(nodeKey) ? _file1Nodes[nodeKey] : null);

                if (matchingCodeNode != null)
                {
                    TreeNode matchingTreeNode = FindTreeNodeByCodeNode(otherTreeView.Nodes, matchingCodeNode);
                    if (matchingTreeNode != null && matchingTreeNode.Nodes.Count > 0)
                    {
                        // Apply the same filter to the matching node
                        ApplyFilterToNode(matchingTreeNode, filterColor);
                    }
                }
            }
        }

        private void ApplyFilterToNode(TreeNode node, Color filterColor)
        {
            // Store original children if not already stored
            if (!_originalChildren.ContainsKey(node))
            {
                List<TreeNode> children = new List<TreeNode>();
                foreach (TreeNode child in node.Nodes)
                {
                    children.Add(child);
                }
                _originalChildren[node] = children;
            }

            // Clear current nodes
            node.Nodes.Clear();

            // Add back only nodes that match the filter color
            foreach (TreeNode child in _originalChildren[node])
            {
                if (child.BackColor == filterColor)
                {
                    node.Nodes.Add(child);
                }
            }

            // Expand the node to show filtered results
            node.Expand();
        }

        private void ClearFilter(TreeNode node)
        {
            TreeView treeView = node.TreeView;
            bool isLeftTree = (treeView == treeView1);

            // Restore original children if they exist
            if (_originalChildren.ContainsKey(node))
            {
                node.Nodes.Clear();

                foreach (TreeNode child in _originalChildren[node])
                {
                    node.Nodes.Add(child);
                }

                _originalChildren.Remove(node);
                node.Expand();
            }

            // If the current node has a match in the other tree, clear filter there too
            string nodeKey = _treeNodeKeys.ContainsKey(node) ? _treeNodeKeys[node] : null;
            if (!string.IsNullOrEmpty(nodeKey))
            {
                TreeView otherTreeView = isLeftTree ? treeView2 : treeView1;
                CodeNode matchingCodeNode = isLeftTree
                    ? (_file2Nodes.ContainsKey(nodeKey) ? _file2Nodes[nodeKey] : null)
                    : (_file1Nodes.ContainsKey(nodeKey) ? _file1Nodes[nodeKey] : null);

                if (matchingCodeNode != null)
                {
                    TreeNode matchingTreeNode = FindTreeNodeByCodeNode(otherTreeView.Nodes, matchingCodeNode);
                    if (matchingTreeNode != null && _originalChildren.ContainsKey(matchingTreeNode))
                    {
                        // Clear the filter on the matching node
                        matchingTreeNode.Nodes.Clear();

                        foreach (TreeNode child in _originalChildren[matchingTreeNode])
                        {
                            matchingTreeNode.Nodes.Add(child);
                        }

                        _originalChildren.Remove(matchingTreeNode);
                        matchingTreeNode.Expand();
                    }
                }
            }
        }

        private void ApplyFilterByEqual(TreeNode node)
        {
            TreeView treeView = node.TreeView;
            bool isLeftTree = (treeView == treeView1);

            // Store original children if not already stored
            if (!_originalChildren.ContainsKey(node))
            {
                List<TreeNode> children = new List<TreeNode>();
                foreach (TreeNode child in node.Nodes)
                {
                    children.Add(child);
                }
                _originalChildren[node] = children;
            }

            // Clear current nodes
            node.Nodes.Clear();

            // Add back only nodes with default/white background (unchanged nodes)
            foreach (TreeNode child in _originalChildren[node])
            {
                // Check if the node has the default background color (no color set or White)
                if (child.BackColor == Color.Empty || child.BackColor == Color.White ||
                    child.BackColor == SystemColors.Window)
                {
                    node.Nodes.Add(child);
                }
            }

            // Expand the node to show filtered results
            node.Expand();

            // Apply same filter to matching node in other tree
            string nodeKey = _treeNodeKeys.ContainsKey(node) ? _treeNodeKeys[node] : null;
            if (!string.IsNullOrEmpty(nodeKey))
            {
                TreeView otherTreeView = isLeftTree ? treeView2 : treeView1;
                CodeNode matchingCodeNode = isLeftTree
                    ? (_file2Nodes.ContainsKey(nodeKey) ? _file2Nodes[nodeKey] : null)
                    : (_file1Nodes.ContainsKey(nodeKey) ? _file1Nodes[nodeKey] : null);

                if (matchingCodeNode != null)
                {
                    TreeNode matchingTreeNode = FindTreeNodeByCodeNode(otherTreeView.Nodes, matchingCodeNode);
                    if (matchingTreeNode != null && matchingTreeNode.Nodes.Count > 0)
                    {
                        // Apply the same filter to the matching node
                        ApplyFilterByEqualToNode(matchingTreeNode);
                    }
                }
            }
        }

        private void ApplyFilterByEqualToNode(TreeNode node)
        {
            // Store original children if not already stored
            if (!_originalChildren.ContainsKey(node))
            {
                List<TreeNode> children = new List<TreeNode>();
                foreach (TreeNode child in node.Nodes)
                {
                    children.Add(child);
                }
                _originalChildren[node] = children;
            }

            // Clear current nodes
            node.Nodes.Clear();

            // Add back only nodes with default/white background
            foreach (TreeNode child in _originalChildren[node])
            {
                if (child.BackColor == Color.Empty || child.BackColor == Color.White ||
                    child.BackColor == SystemColors.Window)
                {
                    node.Nodes.Add(child);
                }
            }

            // Expand the node to show filtered results
            node.Expand();
        }

        private void ApplyFilterByDifferent(TreeNode node)
        {
            TreeView treeView = node.TreeView;
            bool isLeftTree = (treeView == treeView1);

            // Store original children if not already stored
            if (!_originalChildren.ContainsKey(node))
            {
                List<TreeNode> children = new List<TreeNode>();
                foreach (TreeNode child in node.Nodes)
                {
                    children.Add(child);
                }
                _originalChildren[node] = children;
            }

            // Clear current nodes
            node.Nodes.Clear();

            // Add back only nodes with colored backgrounds (yellow, green, or red)
            foreach (TreeNode child in _originalChildren[node])
            {
                // Check if the node has a colored background (not default/white)
                if (child.BackColor != Color.Empty && child.BackColor != Color.White &&
                    child.BackColor != SystemColors.Window)
                {
                    node.Nodes.Add(child);
                }
            }

            // Expand the node to show filtered results
            node.Expand();

            // Apply same filter to matching node in other tree
            string nodeKey = _treeNodeKeys.ContainsKey(node) ? _treeNodeKeys[node] : null;
            if (!string.IsNullOrEmpty(nodeKey))
            {
                TreeView otherTreeView = isLeftTree ? treeView2 : treeView1;
                CodeNode matchingCodeNode = isLeftTree
                    ? (_file2Nodes.ContainsKey(nodeKey) ? _file2Nodes[nodeKey] : null)
                    : (_file1Nodes.ContainsKey(nodeKey) ? _file1Nodes[nodeKey] : null);

                if (matchingCodeNode != null)
                {
                    TreeNode matchingTreeNode = FindTreeNodeByCodeNode(otherTreeView.Nodes, matchingCodeNode);
                    if (matchingTreeNode != null && matchingTreeNode.Nodes.Count > 0)
                    {
                        // Apply the same filter to the matching node
                        ApplyFilterByDifferentToNode(matchingTreeNode);
                    }
                }
            }
        }

        private void ApplyFilterByDifferentToNode(TreeNode node)
        {
            // Store original children if not already stored
            if (!_originalChildren.ContainsKey(node))
            {
                List<TreeNode> children = new List<TreeNode>();
                foreach (TreeNode child in node.Nodes)
                {
                    children.Add(child);
                }
                _originalChildren[node] = children;
            }

            // Clear current nodes
            node.Nodes.Clear();

            // Add back only nodes with colored backgrounds
            foreach (TreeNode child in _originalChildren[node])
            {
                if (child.BackColor != Color.Empty && child.BackColor != Color.White &&
                    child.BackColor != SystemColors.Window)
                {
                    node.Nodes.Add(child);
                }
            }

            // Expand the node to show filtered results
            node.Expand();
        }

        private void splitContainerTrees_Panel1_SizeChanged(object sender, EventArgs e)
        {
            splitContainerEditors.SplitterDistance = splitContainerTrees.Panel1.Width;
        }

        private void splitContainerEditors_Panel1_SizeChanged(object sender, EventArgs e)
        {
            splitContainerTrees.SplitterDistance = splitContainerEditors.Panel1.Width;
        }

        private void btnMergeClasses_Click(object sender, EventArgs e)
        {
            // Get all classes from both files
            var file1Classes = GetClassNodes(_comparisonResult.File1Structure);
            var file2Classes = GetClassNodes(_comparisonResult.File2Structure);

            if (file1Classes.Count == 0 || file2Classes.Count == 0)
            {
                MessageBox.Show("Both files must contain at least one class to merge.",
                    "Merge Classes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CodeNode selectedClass1, selectedClass2;

            // If more than one class in either file, ask user to select
            if (file1Classes.Count > 1 || file2Classes.Count > 1)
            {
                using (var dialog = new Forms.ClassSelectionDialog(file1Classes, file2Classes,
                    Path.GetFileName(_file1Path), Path.GetFileName(_file2Path)))
                {
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    selectedClass1 = dialog.SelectedClass1;
                    selectedClass2 = dialog.SelectedClass2;
                }
            }
            else
            {
                selectedClass1 = file1Classes[0];
                selectedClass2 = file2Classes[0];
            }

            // Launch the merge wizard
            using (var wizard = new Forms.MergeWizardDialog(selectedClass1, selectedClass2,
                _file1Nodes, _file2Nodes))
            {
                wizard.WindowState = FormWindowState.Maximized;
                if (wizard.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Class merge completed successfully!",
                        "Merge Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private List<CodeNode> GetClassNodes(CodeNode root)
        {
            var classes = new List<CodeNode>();
            CollectClassNodes(root, classes);
            return classes;
        }

        private void CollectClassNodes(CodeNode node, List<CodeNode> classes)
        {
            if (node.Type == "Class")
            {
                classes.Add(node);
            }

            foreach (var child in node.Children)
            {
                CollectClassNodes(child, classes);
            }
        }

        private void ResultsForm_Shown(object sender, EventArgs e)
        {
            splitContainerTrees.SplitterDistance = splitContainerTrees.Width / 2;
            splitContainerEditors.SplitterDistance = splitContainerEditors.Width / 2;
        }
    }
}
