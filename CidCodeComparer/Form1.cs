using CidCodeComparer.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CidCodeComparer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void CompareFiles(string fileType)
        {
            using (var dialog = new FileSelectionDialog(fileType))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var resultsForm = new ResultsForm(dialog.File1Path, dialog.File2Path, fileType);
                    resultsForm.Show();
                }
            }
        }

        private void menuItemCSharp_Click(object sender, EventArgs e)
        {
            CompareFiles("C#");
        }

        private void menuItemJavaScript_Click(object sender, EventArgs e)
        {
            CompareFiles("JavaScript");
        }

        private void menuItemHtml_Click(object sender, EventArgs e)
        {
            CompareFiles("HTML");
        }

        private void menuItemXml_Click(object sender, EventArgs e)
        {
            CompareFiles("XML");
        }

        private void menuItemJson_Click(object sender, EventArgs e)
        {
            CompareFiles("JSON");
        }

        private void menuItemText_Click(object sender, EventArgs e)
        {
            CompareFiles("Text");
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void testDiffViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var testDialog = new DiffViewerTestDialog();
            testDialog.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TextEditorControl textEditor = textEditorControl1;
            textEditor.Text = "This is a line with a bookmark.";

            // Define the position and length of the text to mark (e.g., the word "bookmark")
            int offset = textEditor.Text.IndexOf("bookmark");
            int length = "bookmark".Length;

            // Create a new TextMarker
            // You can use different TextMarkerType values like TextMarkerType.WaveLine, TextMarkerType.SolidBlock, etc.
            TextMarker marker = new TextMarker(offset, length, TextMarkerType.SolidBlock, Color.Yellow);

            // Add the marker to the document's MarkerStrategy
            textEditor.Document.MarkerStrategy.AddMarker(marker);

            // Optional: Refresh the editor to ensure the marker is displayed immediately
            textEditor.ActiveTextAreaControl.Refresh();




        }
    }
}
