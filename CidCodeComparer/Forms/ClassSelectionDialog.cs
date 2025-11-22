using CidCodeComparer.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CidCodeComparer.Forms
{
    public partial class ClassSelectionDialog : Form
    {
        public CodeNode SelectedClass1 { get; private set; }
        public CodeNode SelectedClass2 { get; private set; }

        public ClassSelectionDialog(List<CodeNode> file1Classes, List<CodeNode> file2Classes,
            string file1Name, string file2Name)
        {
            InitializeComponent();

            lblFile1.Text = $"Select class from {file1Name}:";
            lblFile2.Text = $"Select class from {file2Name}:";

            foreach (var cls in file1Classes)
            {
                cmbFile1Classes.Items.Add(cls.Name);
            }

            foreach (var cls in file2Classes)
            {
                cmbFile2Classes.Items.Add(cls.Name);
            }

            if (file1Classes.Count > 0)
                cmbFile1Classes.SelectedIndex = 0;

            if (file2Classes.Count > 0)
                cmbFile2Classes.SelectedIndex = 0;

            cmbFile1Classes.Tag = file1Classes;
            cmbFile2Classes.Tag = file2Classes;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbFile1Classes.SelectedIndex < 0 || cmbFile2Classes.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a class from both files.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var file1Classes = (List<CodeNode>)cmbFile1Classes.Tag;
            var file2Classes = (List<CodeNode>)cmbFile2Classes.Tag;

            SelectedClass1 = file1Classes[cmbFile1Classes.SelectedIndex];
            SelectedClass2 = file2Classes[cmbFile2Classes.SelectedIndex];

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
