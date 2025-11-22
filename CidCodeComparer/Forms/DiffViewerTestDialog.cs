using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CidCodeComparer.Controls;
using CidCodeComparer.Models;

namespace CidCodeComparer.Forms
{
    public partial class DiffViewerTestDialog : Form
    {
        //private DiffViewerControl diffViewerControl;
        private Button btnLoadSample;
        private Button btnClear;

        public DiffViewerTestDialog()
        {
            InitializeComponent();
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            // Sample text 1
            string text1 = File.ReadAllText(@"C:\Users\Gonzalo\Desktop\Extensions\Extensions00.cs");


            // Sample text 2 (with some differences)
            string text2 = File.ReadAllText(@"C:\Users\Gonzalo\Desktop\Extensions\Extensions05.cs");

            // Load the texts into the diff viewer
            diffViewerControl.SetSyntaxHighlighting(".cs");
            diffViewerControl.LoadTexts(text1, text2);
        }

        private void btnLoadSample_Click(object sender, EventArgs e)
        {
            // Sample text 1
            string text1 = File.ReadAllText(@"C:\Users\Gonzalo\Desktop\Extensions\Sample1");


            // Sample text 2 (with some differences)
            string text2 = File.ReadAllText(@"C:\Users\Gonzalo\Desktop\Extensions\Sample2");

            // Load the texts into the diff viewer
            diffViewerControl.SetSyntaxHighlighting(".sql");
            diffViewerControl.LoadTexts(text1, text2);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            diffViewerControl.ClearHighlights();
            diffViewerControl.LoadTexts(string.Empty, string.Empty, null);
        }

        private void toggleNavigationPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diffViewerControl.ShowJumpButtons = !diffViewerControl.ShowJumpButtons;
        }

        private void toggleSummaryPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diffViewerControl.ShowSummaryPanel = !diffViewerControl.ShowSummaryPanel;
        }
    }
}
