namespace MovieEditor.VideoViewer
{
    partial class Viewer
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FrameSelector = new MovieEditor.MyControls.ColorSlider();
            this.bFrameDown = new System.Windows.Forms.Button();
            this.bFrameUp = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.BPlay = new System.Windows.Forms.Button();
            this.PlayTimer = new System.Windows.Forms.Timer(this.components);
            this.PlayAudioTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(686, 337);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.FrameSelector);
            this.panel1.Controls.Add(this.bFrameDown);
            this.panel1.Controls.Add(this.bFrameUp);
            this.panel1.Controls.Add(this.bStop);
            this.panel1.Controls.Add(this.BPlay);
            this.panel1.Location = new System.Drawing.Point(-1, 341);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(686, 46);
            this.panel1.TabIndex = 2;
            // 
            // FrameSelector
            // 
            this.FrameSelector.BackColor = System.Drawing.Color.Transparent;
            this.FrameSelector.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.FrameSelector.LargeChange = ((uint)(5u));
            this.FrameSelector.Location = new System.Drawing.Point(251, 13);
            this.FrameSelector.Maximum = 10000;
            this.FrameSelector.Name = "FrameSelector";
            this.FrameSelector.Size = new System.Drawing.Size(429, 18);
            this.FrameSelector.SmallChange = ((uint)(1u));
            this.FrameSelector.TabIndex = 4;
            this.FrameSelector.Text = "colorSlider1";
            this.FrameSelector.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.FrameSelector.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FrameSelector_Scroll);
            // 
            // bFrameDown
            // 
            this.bFrameDown.Location = new System.Drawing.Point(144, 4);
            this.bFrameDown.Name = "bFrameDown";
            this.bFrameDown.Size = new System.Drawing.Size(52, 39);
            this.bFrameDown.TabIndex = 3;
            this.bFrameDown.Text = "Frame Down";
            this.bFrameDown.UseVisualStyleBackColor = true;
            this.bFrameDown.Click += new System.EventHandler(this.bFrameDown_Click);
            // 
            // bFrameUp
            // 
            this.bFrameUp.Location = new System.Drawing.Point(27, 4);
            this.bFrameUp.Name = "bFrameUp";
            this.bFrameUp.Size = new System.Drawing.Size(59, 39);
            this.bFrameUp.TabIndex = 2;
            this.bFrameUp.Text = "Frame Up";
            this.bFrameUp.UseVisualStyleBackColor = true;
            this.bFrameUp.Click += new System.EventHandler(this.bFrameUp_Click);
            // 
            // bStop
            // 
            this.bStop.Location = new System.Drawing.Point(202, 4);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(43, 39);
            this.bStop.TabIndex = 1;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // BPlay
            // 
            this.BPlay.Location = new System.Drawing.Point(92, 4);
            this.BPlay.Name = "BPlay";
            this.BPlay.Size = new System.Drawing.Size(46, 39);
            this.BPlay.TabIndex = 0;
            this.BPlay.Text = "Play";
            this.BPlay.UseVisualStyleBackColor = true;
            this.BPlay.Click += new System.EventHandler(this.BPlay_Click);
            // 
            // PlayTimer
            // 
            this.PlayTimer.Tick += new System.EventHandler(this.PlayTimer_Tick);
            // 
            // PlayAudioTimer
            // 
            this.PlayAudioTimer.Tick += new System.EventHandler(this.PlayAudioTimer_Tick);
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 393);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Viewer";
            this.TabText = "Movie Preview";
            this.Text = "Movie Preview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Viewer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bFrameDown;
        private System.Windows.Forms.Button bFrameUp;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button BPlay;
        private MovieEditor.MyControls.ColorSlider FrameSelector;
        private System.Windows.Forms.Timer PlayTimer;
        private System.Windows.Forms.Timer PlayAudioTimer;
    }
}