using System;
using System.IO;
using System.Windows.Forms;

namespace CidCodeComparer
{
    public partial class FileSelectionDialog : Form
    {
        public string File1Path { get; private set; }
        public string File2Path { get; private set; }
        private readonly string _fileType;

        public FileSelectionDialog(string fileType)
        {
            InitializeComponent();
            _fileType = fileType;
            this.Text = $"Select {fileType} Files to Compare";
        }

        private void btnBrowseFile1_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = GetFileFilter();
                openFileDialog.Title = $"Select First {_fileType} File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFile1.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnBrowseFile2_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = GetFileFilter();
                openFileDialog.Title = $"Select Second {_fileType} File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFile2.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFile1.Text) || string.IsNullOrWhiteSpace(txtFile2.Text))
            {
                MessageBox.Show("Please select both files before comparing.", "Missing Files",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(txtFile1.Text))
            {
                MessageBox.Show("First file does not exist.", "File Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(txtFile2.Text))
            {
                MessageBox.Show("Second file does not exist.", "File Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            File1Path = txtFile1.Text;
            File2Path = txtFile2.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private string GetFileFilter()
        {
            switch (_fileType)
            {
                case "C#":
                    return "C# Files (*.cs)|*.cs|All Files (*.*)|*.*";
                case "JavaScript":
                    return "JavaScript Files (*.js)|*.js|All Files (*.*)|*.*";
                case "HTML":
                    return "HTML Files (*.html;*.htm)|*.html;*.htm|All Files (*.*)|*.*";
                case "XML":
                    return "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                case "JSON":
                    return "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                case "Text":
                    return "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                default:
                    return "All Files (*.*)|*.*";
            }
        }
    }
}
