using System;
using System.Windows.Forms;

namespace LieDetectorApplication.GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void extract_form_btn_Click(object sender, EventArgs e)
        {
            (new ExtractForm()).ShowDialog();
        }

        private void record_form_btn_Click(object sender, EventArgs e)
        {
            (new RecordForm()).ShowDialog();
        }
    }
}