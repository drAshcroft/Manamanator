namespace MovieEditor
{
    partial class ExportOptions
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
            this.ComboBoxDefinedFormat = new System.Windows.Forms.ComboBox();
            this.PanelOutput = new System.Windows.Forms.Panel();
            this.CheckBoxOpenOutputFolderAfterConversion = new System.Windows.Forms.CheckBox();
            this.LabelOutputFileName = new System.Windows.Forms.Label();
            this.TextBoxOutputFileName = new System.Windows.Forms.TextBox();
            this.ButtonBrowseOutput = new System.Windows.Forms.Button();
            this.TextBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.LabelOutput = new System.Windows.Forms.Label();
            this.LabelOutputFolder = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.PanelOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboBoxDefinedFormat
            // 
            this.ComboBoxDefinedFormat.FormattingEnabled = true;
            this.ComboBoxDefinedFormat.Location = new System.Drawing.Point(10, 181);
            this.ComboBoxDefinedFormat.Name = "ComboBoxDefinedFormat";
            this.ComboBoxDefinedFormat.Size = new System.Drawing.Size(305, 21);
            this.ComboBoxDefinedFormat.TabIndex = 0;
            // 
            // PanelOutput
            // 
            this.PanelOutput.BackColor = System.Drawing.Color.White;
            this.PanelOutput.Controls.Add(this.CheckBoxOpenOutputFolderAfterConversion);
            this.PanelOutput.Controls.Add(this.LabelOutputFileName);
            this.PanelOutput.Controls.Add(this.TextBoxOutputFileName);
            this.PanelOutput.Controls.Add(this.ButtonBrowseOutput);
            this.PanelOutput.Controls.Add(this.TextBoxOutputFolder);
            this.PanelOutput.Controls.Add(this.LabelOutput);
            this.PanelOutput.Controls.Add(this.LabelOutputFolder);
            this.PanelOutput.Cursor = System.Windows.Forms.Cursors.Default;
            this.PanelOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelOutput.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PanelOutput.Location = new System.Drawing.Point(10, 10);
            this.PanelOutput.Name = "PanelOutput";
            this.PanelOutput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PanelOutput.Size = new System.Drawing.Size(621, 137);
            this.PanelOutput.TabIndex = 3;
            // 
            // CheckBoxOpenOutputFolderAfterConversion
            // 
            this.CheckBoxOpenOutputFolderAfterConversion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxOpenOutputFolderAfterConversion.AutoSize = true;
            this.CheckBoxOpenOutputFolderAfterConversion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CheckBoxOpenOutputFolderAfterConversion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxOpenOutputFolderAfterConversion.Location = new System.Drawing.Point(256, 20);
            this.CheckBoxOpenOutputFolderAfterConversion.Name = "CheckBoxOpenOutputFolderAfterConversion";
            this.CheckBoxOpenOutputFolderAfterConversion.Size = new System.Drawing.Size(195, 19);
            this.CheckBoxOpenOutputFolderAfterConversion.TabIndex = 3;
            this.CheckBoxOpenOutputFolderAfterConversion.Text = "Open Outp&ut Folder when Done";
            this.CheckBoxOpenOutputFolderAfterConversion.UseVisualStyleBackColor = true;
            this.CheckBoxOpenOutputFolderAfterConversion.CheckedChanged += new System.EventHandler(this.CheckBoxOpenOutputFolderAfterConversion_CheckedChanged);
            // 
            // LabelOutputFileName
            // 
            this.LabelOutputFileName.AutoSize = true;
            this.LabelOutputFileName.BackColor = System.Drawing.Color.Transparent;
            this.LabelOutputFileName.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelOutputFileName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelOutputFileName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelOutputFileName.Location = new System.Drawing.Point(17, 83);
            this.LabelOutputFileName.Name = "LabelOutputFileName";
            this.LabelOutputFileName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LabelOutputFileName.Size = new System.Drawing.Size(60, 15);
            this.LabelOutputFileName.TabIndex = 6;
            this.LabelOutputFileName.Text = "File &Name";
            // 
            // TextBoxOutputFileName
            // 
            this.TextBoxOutputFileName.AcceptsReturn = true;
            this.TextBoxOutputFileName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TextBoxOutputFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxOutputFileName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TextBoxOutputFileName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxOutputFileName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TextBoxOutputFileName.Location = new System.Drawing.Point(19, 101);
            this.TextBoxOutputFileName.MaxLength = 0;
            this.TextBoxOutputFileName.Name = "TextBoxOutputFileName";
            this.TextBoxOutputFileName.ReadOnly = true;
            this.TextBoxOutputFileName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TextBoxOutputFileName.Size = new System.Drawing.Size(537, 23);
            this.TextBoxOutputFileName.TabIndex = 7;
            // 
            // ButtonBrowseOutput
            // 
            this.ButtonBrowseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonBrowseOutput.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonBrowseOutput.Cursor = System.Windows.Forms.Cursors.Default;
            this.ButtonBrowseOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonBrowseOutput.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonBrowseOutput.Location = new System.Drawing.Point(562, 53);
            this.ButtonBrowseOutput.Name = "ButtonBrowseOutput";
            this.ButtonBrowseOutput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButtonBrowseOutput.Size = new System.Drawing.Size(38, 23);
            this.ButtonBrowseOutput.TabIndex = 1;
            this.ButtonBrowseOutput.Text = "...";
            this.ButtonBrowseOutput.UseVisualStyleBackColor = true;
            this.ButtonBrowseOutput.Click += new System.EventHandler(this.ButtonBrowseOutput_Click);
            // 
            // TextBoxOutputFolder
            // 
            this.TextBoxOutputFolder.AcceptsReturn = true;
            this.TextBoxOutputFolder.BackColor = System.Drawing.SystemColors.Window;
            this.TextBoxOutputFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxOutputFolder.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TextBoxOutputFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxOutputFolder.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TextBoxOutputFolder.Location = new System.Drawing.Point(19, 53);
            this.TextBoxOutputFolder.MaxLength = 0;
            this.TextBoxOutputFolder.Name = "TextBoxOutputFolder";
            this.TextBoxOutputFolder.ReadOnly = true;
            this.TextBoxOutputFolder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TextBoxOutputFolder.Size = new System.Drawing.Size(537, 23);
            this.TextBoxOutputFolder.TabIndex = 5;
            // 
            // LabelOutput
            // 
            this.LabelOutput.AutoSize = true;
            this.LabelOutput.BackColor = System.Drawing.Color.Transparent;
            this.LabelOutput.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LabelOutput.Location = new System.Drawing.Point(6, 6);
            this.LabelOutput.Name = "LabelOutput";
            this.LabelOutput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LabelOutput.Size = new System.Drawing.Size(47, 15);
            this.LabelOutput.TabIndex = 0;
            this.LabelOutput.Text = "Output";
            // 
            // LabelOutputFolder
            // 
            this.LabelOutputFolder.AutoSize = true;
            this.LabelOutputFolder.BackColor = System.Drawing.Color.Transparent;
            this.LabelOutputFolder.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelOutputFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelOutputFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelOutputFolder.Location = new System.Drawing.Point(17, 35);
            this.LabelOutputFolder.Name = "LabelOutputFolder";
            this.LabelOutputFolder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LabelOutputFolder.Size = new System.Drawing.Size(81, 15);
            this.LabelOutputFolder.TabIndex = 4;
            this.LabelOutputFolder.Text = "&Output Folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Final Video Format";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(387, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(491, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 24);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ExportOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 210);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PanelOutput);
            this.Controls.Add(this.ComboBoxDefinedFormat);
            this.Name = "ExportOptions";
            this.Text = "ExportOptions";
            this.Load += new System.EventHandler(this.ExportOptions_Load);
            this.PanelOutput.ResumeLayout(false);
            this.PanelOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBoxDefinedFormat;
        public System.Windows.Forms.Panel PanelOutput;
        internal System.Windows.Forms.CheckBox CheckBoxOpenOutputFolderAfterConversion;
        public System.Windows.Forms.Label LabelOutputFileName;
        public System.Windows.Forms.TextBox TextBoxOutputFileName;
        public System.Windows.Forms.Button ButtonBrowseOutput;
        public System.Windows.Forms.TextBox TextBoxOutputFolder;
        public System.Windows.Forms.Label LabelOutput;
        public System.Windows.Forms.Label LabelOutputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}