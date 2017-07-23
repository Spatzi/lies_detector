using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using LieDetectorApplication.Models;

namespace LieDetectorApplication.GUI
{
    public partial class ExtractForm : Form
    {
        private List<string> _recordFiles = new List<string>();

        public ExtractForm()
        {
            InitializeComponent();
        }

        private void extract_btn_Click(object sender, EventArgs e)
        {
            if (extractFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(
                        extractFileDialog.FileName,
                        _recordFiles.Select(
                            path =>
                                RecordPropertyExtractor.PropertyValues(Record.Open(path)).Select(prop => prop.ToString()).Aggregate((prop1, prop2) => prop1 + "," + prop2)
                        ).Aggregate((obj1, obj2) => obj1 + "\n" + obj2)
                    );
                }
                catch (SerializationException)
                {
                    MessageBox.Show("Can't open files, one of them is not a record", "Can't open files",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MessageBox.Show("Property extraction complete", "Done",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void records_btn_Click(object sender, EventArgs e)
        {
            _recordFiles.Clear();
            if (selectRecordsBrowserDialog.ShowDialog() == DialogResult.OK)
                _recordFiles.AddRange(Directory.GetFiles(selectRecordsBrowserDialog.SelectedPath));
            if (_recordFiles.Count != 0) extract_btn.Enabled = true;
            else extract_btn.Enabled = false;
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}