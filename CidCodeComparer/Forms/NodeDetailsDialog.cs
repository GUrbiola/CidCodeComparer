using CidCodeComparer.Models;
using ICSharpCode.TextEditor;
using System;
using System.Text;
using System.Windows.Forms;

namespace CidCodeComparer.Forms
{
    public partial class NodeDetailsDialog : Form
    {
        public NodeDetailsDialog(CodeNode node)
        {
            InitializeComponent();
            PopulateDetails(node);
        }

        private void PopulateDetails(CodeNode node)
        {
            if (node == null)
                return;

            var details = new StringBuilder();

            details.AppendLine($"Name: {node.Name}");
            details.AppendLine($"Type: {node.Type}");
            details.AppendLine($"Lines: {node.StartLine} - {node.EndLine}");
            details.AppendLine();

            if (!string.IsNullOrEmpty(node.Description))
            {
                details.AppendLine($"Description: {node.Description}");
                details.AppendLine();
            }

            if (!string.IsNullOrEmpty(node.AccessModifier))
            {
                details.AppendLine($"Access Modifier: {node.AccessModifier}");
            }

            if (node.Modifiers != null && node.Modifiers.Count > 0)
            {
                details.AppendLine($"Modifiers: {string.Join(", ", node.Modifiers)}");
            }

            if (!string.IsNullOrEmpty(node.ReturnType))
            {
                details.AppendLine($"Return Type: {node.ReturnType}");
            }

            if (!string.IsNullOrEmpty(node.DataType))
            {
                details.AppendLine($"Data Type: {node.DataType}");
            }

            if (node.Parameters != null && node.Parameters.Count > 0)
            {
                details.AppendLine();
                details.AppendLine("Parameters:");
                foreach (var param in node.Parameters)
                {
                    details.AppendLine($"  - {param}");
                }
            }

            if (node.BaseTypes != null && node.BaseTypes.Count > 0)
            {
                details.AppendLine();
                details.AppendLine($"Base Types: {string.Join(", ", node.BaseTypes)}");
            }

            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                details.AppendLine();
                details.AppendLine("Attributes:");
                foreach (var attr in node.Attributes)
                {
                    details.AppendLine($"  - [{attr}]");
                }
            }

            txtDetails.Text = details.ToString();

            // Load source code into TextEditorControl with syntax highlighting
            if (!string.IsNullOrEmpty(node.SourceCode))
            {
                txtSourceCode.Text = node.SourceCode;
                txtSourceCode.SetHighlighting("C#");
                txtSourceCode.IsReadOnly = true;
            }
        }
    }
}
