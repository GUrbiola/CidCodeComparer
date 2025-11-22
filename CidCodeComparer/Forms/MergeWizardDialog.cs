using CidCodeComparer.Controls;
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

        private Dictionary<CodeNode, bool> _greenDecisions;
        private Dictionary<CodeNode, bool> _redDecisions;
        private Dictionary<CodeNode, CodeNode> _yellowDecisions; // Maps to chosen node (left or right)

        private int _currentStep;
        private int _currentGreenIndex;
        private int _currentRedIndex;
        private int _currentYellowIndex;

        public MergeWizardDialog(CodeNode class1, CodeNode class2,
            Dictionary<string, CodeNode> file1Nodes, Dictionary<string, CodeNode> file2Nodes)
        {
            InitializeComponent();

            _class1 = class1;
            _class2 = class2;
            _file1Nodes = file1Nodes;
            _file2Nodes = file2Nodes;

            _greenDecisions = new Dictionary<CodeNode, bool>();
            _redDecisions = new Dictionary<CodeNode, bool>();
            _yellowDecisions = new Dictionary<CodeNode, CodeNode>();

            _currentStep = 0;
            _currentGreenIndex = 0;
            _currentRedIndex = 0;
            _currentYellowIndex = 0;

            AnalyzeClassDifferences();
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
                if (_currentGreenIndex == 0)
                {
                    // Show "Add all green" option
                    ShowGreenAddAllStep();
                }
                else if (_currentGreenIndex <= _greenNodes.Count)
                {
                    ShowGreenIndividualStep();
                }
                else
                {
                    // Move to next step
                    _currentStep++;
                    ShowCurrentStep();
                }
            }
            // Step 1: Red nodes
            else if ((_currentStep == 0 && _greenNodes.Count == 0) || _currentStep == 1)
            {
                _currentStep = 1;

                if (_redNodes.Count > 0)
                {
                    if (_currentRedIndex == 0)
                    {
                        ShowRedAddAllStep();
                    }
                    else if (_currentRedIndex <= _redNodes.Count)
                    {
                        ShowRedIndividualStep();
                    }
                    else
                    {
                        _currentStep++;
                        ShowCurrentStep();
                    }
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

        private void ShowGreenAddAllStep()
        {
            panelGreenNodes.Visible = true;
            lblGreenQuestion.Text = $"Found {_greenNodes.Count} member(s) only in left class. Add all?";
            txtGreenNodeInfo.Text = GetNodesSummary(_greenNodes);
            btnGreenAddThis.Visible = false;
            btnGreenSkipThis.Visible = false;
            btnGreenAddAll.Visible = true;
            btnGreenSkipAll.Visible = true;

            UpdateNavigationButtons();
        }

        private void ShowGreenIndividualStep()
        {
            int index = _currentGreenIndex - 1;
            var node = _greenNodes[index];

            panelGreenNodes.Visible = true;
            lblGreenQuestion.Text = $"Add this member? ({index + 1}/{_greenNodes.Count})";
            txtGreenNodeInfo.Text = GetNodeInfo(node);
            btnGreenAddThis.Visible = true;
            btnGreenSkipThis.Visible = true;
            btnGreenAddAll.Visible = false;
            btnGreenSkipAll.Visible = false;

            UpdateNavigationButtons();
        }

        private void ShowRedAddAllStep()
        {
            panelRedNodes.Visible = true;
            lblRedQuestion.Text = $"Found {_redNodes.Count} member(s) only in right class. Add all?";
            txtRedNodeInfo.Text = GetNodesSummary(_redNodes);
            btnRedAddThis.Visible = false;
            btnRedSkipThis.Visible = false;
            btnRedAddAll.Visible = true;
            btnRedSkipAll.Visible = true;

            UpdateNavigationButtons();
        }

        private void ShowRedIndividualStep()
        {
            int index = _currentRedIndex - 1;
            var node = _redNodes[index];

            panelRedNodes.Visible = true;
            lblRedQuestion.Text = $"Add this member? ({index + 1}/{_redNodes.Count})";
            txtRedNodeInfo.Text = GetNodeInfo(node);
            btnRedAddThis.Visible = true;
            btnRedSkipThis.Visible = true;
            btnRedAddAll.Visible = false;
            btnRedSkipAll.Visible = false;

            UpdateNavigationButtons();
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
            lblYellowQuestion.Text = $"Choose which version to keep ({_currentYellowIndex + 1}/{_yellowNodes.Count}):";
            diffViewer.LoadTexts(node.SourceCode, matchingNode?.SourceCode ?? "");

            UpdateNavigationButtons();
        }

        private void ShowSaveLocationStep()
        {
            panelSaveLocation.Visible = true;
            lblSaveLocation.Text = "Choose where to save the merged class:";
            txtSavePath.Text = "";

            btnBack.Enabled = false;
            btnNext.Enabled = false;
            btnFinish.Visible = true;
        }

        private string GetNodesSummary(List<CodeNode> nodes)
        {
            var sb = new StringBuilder();
            foreach (var node in nodes)
            {
                sb.AppendLine($"- {node.Type}: {node.Name}");
            }
            return sb.ToString();
        }

        private string GetNodeInfo(CodeNode node)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Type: {node.Type}");
            sb.AppendLine($"Name: {node.Name}");
            if (!string.IsNullOrEmpty(node.ReturnType))
                sb.AppendLine($"Return Type: {node.ReturnType}");
            if (node.Parameters != null && node.Parameters.Count > 0)
            {
                sb.AppendLine($"Parameters: {string.Join(", ", node.Parameters.Select(p => $"{p.Type} {p.Name}"))}");
            }
            sb.AppendLine();
            sb.AppendLine("Source Code:");
            sb.AppendLine(node.SourceCode);

            return sb.ToString();
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
            btnBack.Enabled = _currentStep > 0 || _currentGreenIndex > 0 || _currentRedIndex > 0 || _currentYellowIndex > 0;
            btnNext.Enabled = true;
            btnFinish.Visible = false;
        }

        private void btnGreenAddAll_Click(object sender, EventArgs e)
        {
            foreach (var node in _greenNodes)
            {
                _greenDecisions[node] = true;
            }
            _currentGreenIndex = _greenNodes.Count + 1;
            ShowCurrentStep();
        }

        private void btnGreenSkipAll_Click(object sender, EventArgs e)
        {
            foreach (var node in _greenNodes)
            {
                _greenDecisions[node] = false;
            }
            _currentGreenIndex = _greenNodes.Count + 1;
            ShowCurrentStep();
        }

        private void btnGreenAddThis_Click(object sender, EventArgs e)
        {
            int index = _currentGreenIndex - 1;
            _greenDecisions[_greenNodes[index]] = true;
            _currentGreenIndex++;
            ShowCurrentStep();
        }

        private void btnGreenSkipThis_Click(object sender, EventArgs e)
        {
            int index = _currentGreenIndex - 1;
            _greenDecisions[_greenNodes[index]] = false;
            _currentGreenIndex++;
            ShowCurrentStep();
        }

        private void btnRedAddAll_Click(object sender, EventArgs e)
        {
            foreach (var node in _redNodes)
            {
                _redDecisions[node] = true;
            }
            _currentRedIndex = _redNodes.Count + 1;
            ShowCurrentStep();
        }

        private void btnRedSkipAll_Click(object sender, EventArgs e)
        {
            foreach (var node in _redNodes)
            {
                _redDecisions[node] = false;
            }
            _currentRedIndex = _redNodes.Count + 1;
            ShowCurrentStep();
        }

        private void btnRedAddThis_Click(object sender, EventArgs e)
        {
            int index = _currentRedIndex - 1;
            _redDecisions[_redNodes[index]] = true;
            _currentRedIndex++;
            ShowCurrentStep();
        }

        private void btnRedSkipThis_Click(object sender, EventArgs e)
        {
            int index = _currentRedIndex - 1;
            _redDecisions[_redNodes[index]] = false;
            _currentRedIndex++;
            ShowCurrentStep();
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
            // Handle specific panel actions
            if (panelGreenNodes.Visible && _currentGreenIndex == 0)
            {
                // Skip green add all
                _currentGreenIndex = 1;
                ShowCurrentStep();
            }
            else if (panelRedNodes.Visible && _currentRedIndex == 0)
            {
                // Skip red add all
                _currentRedIndex = 1;
                ShowCurrentStep();
            }
            else
            {
                // Move to next step
                if (_currentStep == 0 && _currentGreenIndex <= _greenNodes.Count)
                {
                    _currentGreenIndex = _greenNodes.Count + 1;
                }
                else if (_currentStep == 1 && _currentRedIndex <= _redNodes.Count)
                {
                    _currentRedIndex = _redNodes.Count + 1;
                }
                else if (_currentStep == 2 && _currentYellowIndex < _yellowNodes.Count)
                {
                    _currentYellowIndex = _yellowNodes.Count;
                }

                ShowCurrentStep();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_currentYellowIndex > 0)
            {
                _currentYellowIndex--;
                ShowCurrentStep();
            }
            else if (_currentRedIndex > 0)
            {
                _currentRedIndex--;
                ShowCurrentStep();
            }
            else if (_currentGreenIndex > 0)
            {
                _currentGreenIndex--;
                ShowCurrentStep();
            }
            else if (_currentStep > 0)
            {
                _currentStep--;
                ShowCurrentStep();
            }
        }

        private void GenerateMergedClass()
        {
            try
            {
                var sb = new StringBuilder();

                // Add usings (combine from both classes)
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Linq;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine();

                // Add class declaration
                sb.AppendLine($"public class {_class1.Name}");
                sb.AppendLine("{");

                // Add white nodes (no differences)
                foreach (var node in _whiteNodes)
                {
                    sb.AppendLine(IndentCode(node.SourceCode, 1));
                    sb.AppendLine();
                }

                // Add selected green nodes
                foreach (var kvp in _greenDecisions)
                {
                    if (kvp.Value)
                    {
                        sb.AppendLine(IndentCode(kvp.Key.SourceCode, 1));
                        sb.AppendLine();
                    }
                }

                // Add selected red nodes
                foreach (var kvp in _redDecisions)
                {
                    if (kvp.Value)
                    {
                        sb.AppendLine(IndentCode(kvp.Key.SourceCode, 1));
                        sb.AppendLine();
                    }
                }

                // Add selected yellow nodes
                foreach (var kvp in _yellowDecisions)
                {
                    sb.AppendLine(IndentCode(kvp.Value.SourceCode, 1));
                    sb.AppendLine();
                }

                sb.AppendLine("}");

                // Write to file
                File.WriteAllText(txtSavePath.Text, sb.ToString());

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating merged class: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string IndentCode(string code, int level)
        {
            if (string.IsNullOrEmpty(code))
                return code;

            var indent = new string(' ', level * 4);
            var lines = code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return string.Join(Environment.NewLine, lines.Select(line => indent + line));
        }
    }
}
