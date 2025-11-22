using CidCodeComparer.Controls;
using CidCodeComparer.Engine;
using CidCodeComparer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CidCodeComparer.Forms
{
    public partial class MergeWizardDialog : Form
    {
        private readonly CodeNode _class1;
        private readonly CodeNode _class2;
        private readonly Dictionary<string, CodeNode> _file1Nodes;
        private readonly Dictionary<string, CodeNode> _file2Nodes;

        private List<CodeNode> _greenNodes;
        private List<CodeNode> _redNodes;
        private List<CodeNode> _yellowNodes;
        private List<CodeNode> _whiteNodes;

        private Dictionary<CodeNode, CodeNode> _yellowDecisions; // Maps to chosen node (left or right)
        private int _currentYellowIndex;

        private int _currentStep;

        public MergeWizardDialog(CodeNode class1, CodeNode class2,
            Dictionary<string, CodeNode> file1Nodes, Dictionary<string, CodeNode> file2Nodes)
        {
            InitializeComponent();

            _class1 = class1;
            _class2 = class2;
            _file1Nodes = file1Nodes;
            _file2Nodes = file2Nodes;

            _yellowDecisions = new Dictionary<CodeNode, CodeNode>();
            _currentYellowIndex = 0;
            _currentStep = 0;

            AnalyzeClassDifferences();
            LoadGreenNodes();
            LoadRedNodes();
            ShowCurrentStep();
        }

        private void AnalyzeClassDifferences()
        {
            _greenNodes = new List<CodeNode>();
            _redNodes = new List<CodeNode>();
            _yellowNodes = new List<CodeNode>();
            _whiteNodes = new List<CodeNode>();

            // Build dictionaries for comparison
            var class1Members = new Dictionary<string, CodeNode>();
            var class2Members = new Dictionary<string, CodeNode>();

            BuildMemberDictionary(_class1, "", class1Members);
            BuildMemberDictionary(_class2, "", class2Members);

            // Find green nodes (only in class1)
            foreach (var kvp in class1Members)
            {
                if (!class2Members.ContainsKey(kvp.Key))
                {
                    _greenNodes.Add(kvp.Value);
                }
            }

            // Find red nodes (only in class2)
            foreach (var kvp in class2Members)
            {
                if (!class1Members.ContainsKey(kvp.Key))
                {
                    _redNodes.Add(kvp.Value);
                }
            }

            // Find yellow and white nodes (in both)
            foreach (var kvp in class1Members)
            {
                if (class2Members.ContainsKey(kvp.Key))
                {
                    var node1 = kvp.Value;
                    var node2 = class2Members[kvp.Key];

                    if (IsSourceCodeEqual(node1.SourceCode, node2.SourceCode))
                    {
                        _whiteNodes.Add(node1);
                    }
                    else
                    {
                        _yellowNodes.Add(node1);
                    }
                }
            }
        }

        private void BuildMemberDictionary(CodeNode node, string parentPath, Dictionary<string, CodeNode> dictionary)
        {
            foreach (var child in node.Children)
            {
                if (child.Type == "Namespace" || child.Type == "File")
                    continue;

                string nodePath = child.Name;
                string key = $"{nodePath}|{child.Type}";

                if (child.Type == "Method" && child.Parameters != null && child.Parameters.Count > 0)
                {
                    var paramTypes = string.Join(",", child.Parameters.Select(p => p.Type));
                    key = $"{nodePath}({paramTypes})|{child.Type}";
                }

                dictionary[key] = child;
            }
        }

        private bool IsSourceCodeEqual(string source1, string source2)
        {
            return NormalizeSourceCode(source1) == NormalizeSourceCode(source2);
        }

        private string NormalizeSourceCode(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
                return string.Empty;
            return sourceCode.Trim().Replace("\r\n", "\n").Replace("\r", "\n");
        }

        private void LoadGreenNodes()
        {
            checkedListBoxGreen.Items.Clear();
            foreach (var node in _greenNodes)
            {
                string displayText = GetNodeDisplayText(node);
                checkedListBoxGreen.Items.Add(displayText, false);
            }
        }

        private void LoadRedNodes()
        {
            checkedListBoxRed.Items.Clear();
            foreach (var node in _redNodes)
            {
                string displayText = GetNodeDisplayText(node);
                checkedListBoxRed.Items.Add(displayText, false);
            }
        }

        private string GetNodeDisplayText(CodeNode node)
        {
            if (node.Type == "Method" && node.Parameters != null && node.Parameters.Count > 0)
            {
                var paramTypes = string.Join(", ", node.Parameters.Select(p => $"{p.Type} {p.Name}"));
                return $"{node.Type}: {node.Name}({paramTypes})";
            }
            return $"{node.Type}: {node.Name}";
        }

        private void ShowCurrentStep()
        {
            // Clear all panels
            panelGreenNodes.Visible = false;
            panelRedNodes.Visible = false;
            panelYellowNodes.Visible = false;
            panelSaveLocation.Visible = false;

            // Step 0: Green nodes
            if (_currentStep == 0 && _greenNodes.Count > 0)
            {
                panelGreenNodes.Visible = true;
                UpdateNavigationButtons();
            }
            // Step 1: Red nodes
            else if ((_currentStep == 0 && _greenNodes.Count == 0) || _currentStep == 1)
            {
                _currentStep = 1;
                if (_redNodes.Count > 0)
                {
                    panelRedNodes.Visible = true;
                    UpdateNavigationButtons();
                }
                else
                {
                    _currentStep++;
                    ShowCurrentStep();
                }
            }
            // Step 2: Yellow nodes
            else if (_currentStep == 2)
            {
                if (_yellowNodes.Count > 0 && _currentYellowIndex < _yellowNodes.Count)
                {
                    ShowYellowIndividualStep();
                }
                else
                {
                    _currentStep++;
                    ShowCurrentStep();
                }
            }
            // Step 3: Save location
            else if (_currentStep == 3)
            {
                ShowSaveLocationStep();
            }
            else
            {
                // All done, generate merged class
                GenerateMergedClass();
            }
        }

        private void ShowYellowIndividualStep()
        {
            var node = _yellowNodes[_currentYellowIndex];

            // Find matching node in class2
            var key = GetNodeKey(node);
            CodeNode matchingNode = null;

            foreach (var child in _class2.Children)
            {
                if (GetNodeKey(child) == key)
                {
                    matchingNode = child;
                    break;
                }
            }

            panelYellowNodes.Visible = true;
            lblYellowQuestion.Text = $"Choose which version to keep ({_currentYellowIndex + 1}/{_yellowNodes.Count}): {node.Name}";
            diffViewer.LoadTexts(node.SourceCode, matchingNode?.SourceCode ?? "");
            diffViewer.SetSyntaxHighlighting(".cs");

            UpdateNavigationButtons();
        }

        private void ShowSaveLocationStep()
        {
            panelSaveLocation.Visible = true;

            btnBack.Enabled = true;
            btnNext.Enabled = false;
            btnFinish.Visible = true;
        }

        private string GetNodeKey(CodeNode node)
        {
            string key = $"{node.Name}|{node.Type}";
            if (node.Type == "Method" && node.Parameters != null && node.Parameters.Count > 0)
            {
                var paramTypes = string.Join(",", node.Parameters.Select(p => p.Type));
                key = $"{node.Name}({paramTypes})|{node.Type}";
            }
            return key;
        }

        private void UpdateNavigationButtons()
        {
            btnBack.Enabled = _currentStep > 0 || _currentYellowIndex > 0;
            btnNext.Enabled = true;
            btnFinish.Visible = false;
        }

        private void checkedListBoxGreen_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = checkedListBoxGreen.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < _greenNodes.Count)
            {
                var node = _greenNodes[selectedIndex];
                txtGreenPreview.Text = node.SourceCode ?? "";
                txtGreenPreview.SetHighlighting("C#");
                txtGreenPreview.Refresh();
            }
        }

        private void checkedListBoxRed_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = checkedListBoxRed.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < _redNodes.Count)
            {
                var node = _redNodes[selectedIndex];
                txtRedPreview.Text = node.SourceCode ?? "";
                txtRedPreview.SetHighlighting("C#");
                txtRedPreview.Refresh();
            }
        }

        private void btnGreenSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxGreen.Items.Count; i++)
            {
                checkedListBoxGreen.SetItemChecked(i, true);
            }
        }

        private void btnGreenSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxGreen.Items.Count; i++)
            {
                checkedListBoxGreen.SetItemChecked(i, false);
            }
        }

        private void btnRedSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxRed.Items.Count; i++)
            {
                checkedListBoxRed.SetItemChecked(i, true);
            }
        }

        private void btnRedSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxRed.Items.Count; i++)
            {
                checkedListBoxRed.SetItemChecked(i, false);
            }
        }

        private void btnYellowKeepLeft_Click(object sender, EventArgs e)
        {
            _yellowDecisions[_yellowNodes[_currentYellowIndex]] = _yellowNodes[_currentYellowIndex];
            _currentYellowIndex++;
            ShowCurrentStep();
        }

        private void btnYellowKeepRight_Click(object sender, EventArgs e)
        {
            var key = GetNodeKey(_yellowNodes[_currentYellowIndex]);
            CodeNode matchingNode = null;

            foreach (var child in _class2.Children)
            {
                if (GetNodeKey(child) == key)
                {
                    matchingNode = child;
                    break;
                }
            }

            _yellowDecisions[_yellowNodes[_currentYellowIndex]] = matchingNode;
            _currentYellowIndex++;
            ShowCurrentStep();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "C# Files (*.cs)|*.cs|All Files (*.*)|*.*";
                dialog.DefaultExt = "cs";
                dialog.FileName = $"{_class1.Name}_Merged.cs";

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtSavePath.Text = dialog.FileName;
                }
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSavePath.Text))
            {
                MessageBox.Show("Please select a location to save the merged class.",
                    "Save Location Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GenerateMergedClass();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // Move to next step
            if (_currentStep == 0)
            {
                _currentStep = 1;
            }
            else if (_currentStep == 1)
            {
                _currentStep = 2;
                _currentYellowIndex = 0;
            }
            else if (_currentStep == 2 && _currentYellowIndex < _yellowNodes.Count)
            {
                _currentYellowIndex = _yellowNodes.Count;
            }
            else
            {
                _currentStep++;
            }

            ShowCurrentStep();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_currentStep == 2 && _currentYellowIndex > 0)
            {
                _currentYellowIndex--;
                ShowCurrentStep();
            }
            else if (_currentStep > 0)
            {
                if (_currentStep == 2)
                {
                    _currentYellowIndex = 0;
                }
                _currentStep--;
                ShowCurrentStep();
            }
        }

        private void GenerateMergedClass()
        {
            try
            {
                var sb = new StringBuilder();

                // Get namespace from the textbox
                string namespaceValue = txtNamespace.Text?.Trim();
                if (string.IsNullOrEmpty(namespaceValue))
                {
                    namespaceValue = "Default";
                }

                // Add usings (combine from both classes)
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Linq;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine();

                // Add namespace
                sb.AppendLine($"namespace {namespaceValue}");
                sb.AppendLine("{");

                // Add class declaration
                sb.AppendLine($"    public class {_class1.Name}");
                sb.AppendLine("    {");

                // Add white nodes (no differences)
                foreach (var node in _whiteNodes)
                {
                    sb.AppendLine(IndentCode(node.SourceCode, 2));
                    sb.AppendLine();
                }

                // Add selected green nodes
                for (int i = 0; i < checkedListBoxGreen.Items.Count; i++)
                {
                    if (checkedListBoxGreen.GetItemChecked(i))
                    {
                        var node = _greenNodes[i];
                        sb.AppendLine(IndentCode(node.SourceCode, 2));
                        sb.AppendLine();
                    }
                }

                // Add selected red nodes
                for (int i = 0; i < checkedListBoxRed.Items.Count; i++)
                {
                    if (checkedListBoxRed.GetItemChecked(i))
                    {
                        var node = _redNodes[i];
                        sb.AppendLine(IndentCode(node.SourceCode, 2));
                        sb.AppendLine();
                    }
                }

                // Add selected yellow nodes
                foreach (var kvp in _yellowDecisions)
                {
                    sb.AppendLine(IndentCode(kvp.Value.SourceCode, 2));
                    sb.AppendLine();
                }

                sb.AppendLine("    }");
                sb.AppendLine("}");

                // Use CodeFormatter to write the file with proper indentation
                var formatter = new CodeFormatter(sb.ToString());
                formatter.Namespace = namespaceValue;
                formatter.WriteToFile(txtSavePath.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating merged class: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string IndentCode(string code, int indentLevel)
        {
            if (string.IsNullOrEmpty(code))
                return string.Empty;

            var lines = code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var indentedLines = new List<string>();
            string indent = new string(' ', indentLevel * 4);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    indentedLines.Add(string.Empty);
                }
                else
                {
                    indentedLines.Add(indent + line.TrimStart());
                }
            }

            return string.Join(Environment.NewLine, indentedLines);
        }
    }
}
