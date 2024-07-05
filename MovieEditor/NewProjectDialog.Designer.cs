namespace MovieEditor
{
    partial class NewProjectDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbAudioSampleType = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbAudioSampleRate = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rBAutoDetect = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.udNumChannels = new System.Windows.Forms.NumericUpDown();
            this.rBCustom = new System.Windows.Forms.RadioButton();
            this.rB51Audio = new System.Windows.Forms.RadioButton();
            this.rBStereo = new System.Windows.Forms.RadioButton();
            this.rBMono = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tBHeight = new System.Windows.Forms.TextBox();
            this.tBWidth = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tBFrameRate = new System.Windows.Forms.TextBox();
            this.cBAutodetectFrameRate = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbVideoResolution = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udNumChannels)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(352, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Default";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(375, 375);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Create Project";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(477, 375);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbAudioSampleType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbAudioSampleRate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(412, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 308);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio Properties";
            // 
            // lbAudioSampleType
            // 
            this.lbAudioSampleType.FormattingEnabled = true;
            this.lbAudioSampleType.Items.AddRange(new object[] {
            "Auto Detect",
            "8 bit",
            "16 bit",
            "24 bit"});
            this.lbAudioSampleType.Location = new System.Drawing.Point(128, 193);
            this.lbAudioSampleType.Name = "lbAudioSampleType";
            this.lbAudioSampleType.Size = new System.Drawing.Size(101, 95);
            this.lbAudioSampleType.TabIndex = 4;
            this.lbAudioSampleType.SelectedIndexChanged += new System.EventHandler(this.lbAudioSampleType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Sound Resolution";
            // 
            // lbAudioSampleRate
            // 
            this.lbAudioSampleRate.FormattingEnabled = true;
            this.lbAudioSampleRate.Items.AddRange(new object[] {
            "32 ",
            "44.1",
            "48"});
            this.lbAudioSampleRate.Location = new System.Drawing.Point(14, 193);
            this.lbAudioSampleRate.Name = "lbAudioSampleRate";
            this.lbAudioSampleRate.Size = new System.Drawing.Size(66, 95);
            this.lbAudioSampleRate.TabIndex = 2;
            this.lbAudioSampleRate.SelectedIndexChanged += new System.EventHandler(this.lbAudioSampleRate_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Output Rate (kHz)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rBAutoDetect);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.udNumChannels);
            this.groupBox3.Controls.Add(this.rBCustom);
            this.groupBox3.Controls.Add(this.rB51Audio);
            this.groupBox3.Controls.Add(this.rBStereo);
            this.groupBox3.Controls.Add(this.rBMono);
            this.groupBox3.Location = new System.Drawing.Point(14, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 141);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Channels";
            // 
            // rBAutoDetect
            // 
            this.rBAutoDetect.AutoSize = true;
            this.rBAutoDetect.Checked = true;
            this.rBAutoDetect.Location = new System.Drawing.Point(6, 19);
            this.rBAutoDetect.Name = "rBAutoDetect";
            this.rBAutoDetect.Size = new System.Drawing.Size(82, 17);
            this.rBAutoDetect.TabIndex = 6;
            this.rBAutoDetect.TabStop = true;
            this.rBAutoDetect.Text = "Auto Detect";
            this.rBAutoDetect.UseVisualStyleBackColor = true;
            this.rBAutoDetect.CheckedChanged += new System.EventHandler(this.rBAutoDetect_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Number Channels";
            // 
            // udNumChannels
            // 
            this.udNumChannels.Enabled = false;
            this.udNumChannels.Location = new System.Drawing.Point(103, 104);
            this.udNumChannels.Name = "udNumChannels";
            this.udNumChannels.Size = new System.Drawing.Size(126, 20);
            this.udNumChannels.TabIndex = 4;
            this.udNumChannels.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // rBCustom
            // 
            this.rBCustom.AutoSize = true;
            this.rBCustom.Location = new System.Drawing.Point(6, 88);
            this.rBCustom.Name = "rBCustom";
            this.rBCustom.Size = new System.Drawing.Size(60, 17);
            this.rBCustom.TabIndex = 3;
            this.rBCustom.Text = "Custom";
            this.rBCustom.UseVisualStyleBackColor = true;
            this.rBCustom.CheckedChanged += new System.EventHandler(this.rBCustom_CheckedChanged);
            // 
            // rB51Audio
            // 
            this.rB51Audio.AutoSize = true;
            this.rB51Audio.Location = new System.Drawing.Point(132, 42);
            this.rB51Audio.Name = "rB51Audio";
            this.rB51Audio.Size = new System.Drawing.Size(70, 17);
            this.rB51Audio.TabIndex = 2;
            this.rB51Audio.Text = "5.1 Audio";
            this.rB51Audio.UseVisualStyleBackColor = true;
            this.rB51Audio.CheckedChanged += new System.EventHandler(this.rB51Audio_CheckedChanged);
            // 
            // rBStereo
            // 
            this.rBStereo.AutoSize = true;
            this.rBStereo.Location = new System.Drawing.Point(6, 65);
            this.rBStereo.Name = "rBStereo";
            this.rBStereo.Size = new System.Drawing.Size(56, 17);
            this.rBStereo.TabIndex = 1;
            this.rBStereo.Text = "Stereo";
            this.rBStereo.UseVisualStyleBackColor = true;
            this.rBStereo.CheckedChanged += new System.EventHandler(this.rBStereo_CheckedChanged);
            // 
            // rBMono
            // 
            this.rBMono.AutoSize = true;
            this.rBMono.Location = new System.Drawing.Point(6, 42);
            this.rBMono.Name = "rBMono";
            this.rBMono.Size = new System.Drawing.Size(52, 17);
            this.rBMono.TabIndex = 0;
            this.rBMono.Text = "Mono";
            this.rBMono.UseVisualStyleBackColor = true;
            this.rBMono.CheckedChanged += new System.EventHandler(this.rBMono_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tBHeight);
            this.groupBox2.Controls.Add(this.tBWidth);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tBFrameRate);
            this.groupBox2.Controls.Add(this.cBAutodetectFrameRate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lbVideoResolution);
            this.groupBox2.Location = new System.Drawing.Point(15, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 308);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Video Properties";
            // 
            // tBHeight
            // 
            this.tBHeight.Enabled = false;
            this.tBHeight.Location = new System.Drawing.Point(120, 174);
            this.tBHeight.Name = "tBHeight";
            this.tBHeight.Size = new System.Drawing.Size(77, 20);
            this.tBHeight.TabIndex = 8;
            this.tBHeight.TextChanged += new System.EventHandler(this.tBHeight_TextChanged);
            // 
            // tBWidth
            // 
            this.tBWidth.Enabled = false;
            this.tBWidth.Location = new System.Drawing.Point(9, 174);
            this.tBWidth.Name = "tBWidth";
            this.tBWidth.Size = new System.Drawing.Size(76, 20);
            this.tBWidth.TabIndex = 7;
            this.tBWidth.TextChanged += new System.EventHandler(this.tBWidth_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(117, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Height";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Width";
            // 
            // tBFrameRate
            // 
            this.tBFrameRate.Location = new System.Drawing.Point(12, 255);
            this.tBFrameRate.Name = "tBFrameRate";
            this.tBFrameRate.Size = new System.Drawing.Size(121, 20);
            this.tBFrameRate.TabIndex = 4;
            this.tBFrameRate.Text = "24";
            this.tBFrameRate.TextChanged += new System.EventHandler(this.tBFrameRate_TextChanged);
            // 
            // cBAutodetectFrameRate
            // 
            this.cBAutodetectFrameRate.AutoSize = true;
            this.cBAutodetectFrameRate.Checked = true;
            this.cBAutodetectFrameRate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBAutodetectFrameRate.Location = new System.Drawing.Point(12, 232);
            this.cBAutodetectFrameRate.Name = "cBAutodetectFrameRate";
            this.cBAutodetectFrameRate.Size = new System.Drawing.Size(80, 17);
            this.cBAutodetectFrameRate.TabIndex = 3;
            this.cBAutodetectFrameRate.Text = "AutoDetect";
            this.cBAutodetectFrameRate.UseVisualStyleBackColor = true;
            this.cBAutodetectFrameRate.CheckedChanged += new System.EventHandler(this.cBAutodetectFrameRate_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Frame Rate (Frames/sec)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Video Resolution";
            // 
            // lbVideoResolution
            // 
            this.lbVideoResolution.FormattingEnabled = true;
            this.lbVideoResolution.Items.AddRange(new object[] {
            "AutoDetermine",
            "Custom\t",
            "PAL, SECAM      520 × 576 lines\t4 : 3",
            "PALplus\t          520 × 576 lines\t4 : 3",
            "NTSC\t          440 × 486 lines\t16 : 9",
            "VHS \t          320 X 480 (NTSC)\t4 : 3",
            "S - VHS \t          530 X 480    \t4 : 3",
            "UMD\t          480 × 272    \t16:9",
            "Video disc         352 x 480     \t4 : 3",
            "SVCD\t         480 X 480     \t1 : 1",
            "SDTV               640 X 480     \t4 : 3",
            "DVD   \t         720 X 480     \t4 : 3",
            "HDTV\t         1280 X 720   \t16 : 9",
            "Blu - Ray\t         1920 X 1080  \t16 : 9",
            "\t"});
            this.lbVideoResolution.Location = new System.Drawing.Point(6, 40);
            this.lbVideoResolution.Name = "lbVideoResolution";
            this.lbVideoResolution.Size = new System.Drawing.Size(295, 108);
            this.lbVideoResolution.TabIndex = 0;
            this.lbVideoResolution.SelectedIndexChanged += new System.EventHandler(this.lbVideoResolution_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // NewProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 410);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "NewProjectDialog";
            this.Text = "Project Properties";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udNumChannels)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbVideoResolution;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rBStereo;
        private System.Windows.Forms.RadioButton rBMono;
        private System.Windows.Forms.RadioButton rB51Audio;
        private System.Windows.Forms.RadioButton rBCustom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udNumChannels;
        private System.Windows.Forms.ListBox lbAudioSampleRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbAudioSampleType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rBAutoDetect;
        private System.Windows.Forms.TextBox tBFrameRate;
        private System.Windows.Forms.CheckBox cBAutodetectFrameRate;
        private System.Windows.Forms.TextBox tBHeight;
        private System.Windows.Forms.TextBox tBWidth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}