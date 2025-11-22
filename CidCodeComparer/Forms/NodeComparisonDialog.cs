using CidCodeComparer.Controls;
using CidCodeComparer.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CidCodeComparer.Forms
{
    public partial class NodeComparisonDialog : Form
    {
        public NodeComparisonDialog(CodeNode leftNode, CodeNode rightNode)
        {
            InitializeComponent();
            LoadComparison(leftNode, rightNode);
        }

        private void LoadComparison(CodeNode leftNode, CodeNode rightNode)
        {
            if (leftNode == null || rightNode == null)
                return;

            // Set dialog title
            this.Text = $"Node Comparison - {leftNode.Name}";

            // Load the source code into the diff viewer
            string leftSource = leftNode.SourceCode ?? string.Empty;
            string rightSource = rightNode.SourceCode ?? string.Empty;

            // Use DiffViewerControl to show the differences
            diffViewerControl.LoadTexts(leftSource, rightSource);
            diffViewerControl.SetSyntaxHighlighting(".cs");
        }
    }
}
