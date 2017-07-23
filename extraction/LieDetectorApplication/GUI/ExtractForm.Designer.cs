namespace LieDetectorApplication.GUI
{
    partial class ExtractForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.records_btn = new System.Windows.Forms.Button();
            this.extract_btn = new System.Windows.Forms.Button();
            this.selectRecordsBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.extractFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // records_btn
            // 
            this.records_btn.Location = new System.Drawing.Point(13, 12);
            this.records_btn.Name = "records_btn";
            this.records_btn.Size = new System.Drawing.Size(259, 23);
            this.records_btn.TabIndex = 1;
            this.records_btn.Text = "Select Records Folder";
            this.records_btn.UseVisualStyleBackColor = true;
            this.records_btn.Click += new System.EventHandler(this.records_btn_Click);
            // 
            // extract_btn
            // 
            this.extract_btn.Enabled = false;
            this.extract_btn.Location = new System.Drawing.Point(13, 41);
            this.extract_btn.Name = "extract_btn";
            this.extract_btn.Size = new System.Drawing.Size(259, 23);
            this.extract_btn.TabIndex = 3;
            this.extract_btn.Text = "Extract";
            this.extract_btn.UseVisualStyleBackColor = true;
            this.extract_btn.Click += new System.EventHandler(this.extract_btn_Click);
            // 
            // extractFileDialog
            // 
            this.extractFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // ExtractForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 75);
            this.Controls.Add(this.extract_btn);
            this.Controls.Add(this.records_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ExtractForm";
            this.Text = "Extract";
            this.ResumeLayout(false);

        }

        private void selectTruthsBrowserDialog_HelpRequest(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button records_btn;
        private System.Windows.Forms.Button extract_btn;
        private System.Windows.Forms.FolderBrowserDialog selectRecordsBrowserDialog;
        private System.Windows.Forms.SaveFileDialog extractFileDialog;

    }
}