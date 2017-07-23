namespace LieDetectorApplication.GUI
{
    partial class MainForm
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
            this.extract_form_btn = new System.Windows.Forms.Button();
            this.record_form_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // extract_form_btn
            // 
            this.extract_form_btn.Location = new System.Drawing.Point(13, 41);
            this.extract_form_btn.Name = "extract_form_btn";
            this.extract_form_btn.Size = new System.Drawing.Size(259, 23);
            this.extract_form_btn.TabIndex = 5;
            this.extract_form_btn.Text = "Extract";
            this.extract_form_btn.UseVisualStyleBackColor = true;
            this.extract_form_btn.Click += new System.EventHandler(this.extract_form_btn_Click);
            // 
            // record_form_btn
            // 
            this.record_form_btn.Location = new System.Drawing.Point(13, 12);
            this.record_form_btn.Name = "record_form_btn";
            this.record_form_btn.Size = new System.Drawing.Size(259, 23);
            this.record_form_btn.TabIndex = 4;
            this.record_form_btn.Text = "Record";
            this.record_form_btn.UseVisualStyleBackColor = true;
            this.record_form_btn.Click += new System.EventHandler(this.record_form_btn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 75);
            this.Controls.Add(this.extract_form_btn);
            this.Controls.Add(this.record_form_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Lie Detector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button extract_form_btn;
        private System.Windows.Forms.Button record_form_btn;
    }
}