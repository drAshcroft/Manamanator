namespace MovieEditor.TimeLine
{
    partial class TimelineMaster
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
            this.TrackPanel = new System.Windows.Forms.Panel();
            this.SelectorLocator = new System.Windows.Forms.PictureBox();
            this.TimeLocation = new System.Windows.Forms.PictureBox();
            this.CutButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FilterButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.frameSelector1 = new MovieEditor.MyControls.FrameSelector();
            this.TrackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectorLocator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeLocation)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrackPanel
            // 
            this.TrackPanel.AllowDrop = true;
            this.TrackPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TrackPanel.Controls.Add(this.SelectorLocator);
            this.TrackPanel.Controls.Add(this.TimeLocation);
            this.TrackPanel.Controls.Add(this.frameSelector1);
            this.TrackPanel.Location = new System.Drawing.Point(0, 0);
            this.TrackPanel.MinimumSize = new System.Drawing.Size(1200, 2);
            this.TrackPanel.Name = "TrackPanel";
            this.TrackPanel.Size = new System.Drawing.Size(1200, 468);
            this.TrackPanel.TabIndex = 5;
            this.TrackPanel.DragOver += new System.Windows.Forms.DragEventHandler(this.TrackPanel_DragOver);
            this.TrackPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TrackPanel_MouseMove);
            this.TrackPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.TrackPanel_DragDrop);
            this.TrackPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrackPanel_MouseDown);
            this.TrackPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrackPanel_MouseUp);
            this.TrackPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.TrackPanel_DragEnter);
            // 
            // SelectorLocator
            // 
            this.SelectorLocator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.SelectorLocator.BackColor = System.Drawing.Color.Red;
            this.SelectorLocator.Location = new System.Drawing.Point(598, -1);
            this.SelectorLocator.Name = "SelectorLocator";
            this.SelectorLocator.Size = new System.Drawing.Size(3, 468);
            this.SelectorLocator.TabIndex = 8;
            this.SelectorLocator.TabStop = false;
            this.SelectorLocator.Visible = false;
            // 
            // TimeLocation
            // 
            this.TimeLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.TimeLocation.BackColor = System.Drawing.Color.Black;
            this.TimeLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TimeLocation.Location = new System.Drawing.Point(70, 0);
            this.TimeLocation.Name = "TimeLocation";
            this.TimeLocation.Size = new System.Drawing.Size(3, 468);
            this.TimeLocation.TabIndex = 7;
            this.TimeLocation.TabStop = false;
            // 
            // CutButton
            // 
            this.CutButton.Location = new System.Drawing.Point(2, 96);
            this.CutButton.Name = "CutButton";
            this.CutButton.Size = new System.Drawing.Size(52, 25);
            this.CutButton.TabIndex = 9;
            this.CutButton.Text = "Cut Film";
            this.CutButton.UseVisualStyleBackColor = true;
            this.CutButton.Click += new System.EventHandler(this.CutButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(2, 66);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(52, 24);
            this.SelectButton.TabIndex = 10;
            this.SelectButton.Text = "Select";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FilterButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.SelectButton);
            this.panel1.Controls.Add(this.CutButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(57, 374);
            this.panel1.TabIndex = 12;
            // 
            // FilterButton
            // 
            this.FilterButton.Location = new System.Drawing.Point(3, 127);
            this.FilterButton.Name = "FilterButton";
            this.FilterButton.Size = new System.Drawing.Size(52, 38);
            this.FilterButton.TabIndex = 9;
            this.FilterButton.Text = "Add Filter";
            this.FilterButton.UseVisualStyleBackColor = true;
            this.FilterButton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Seconds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.TrackPanel);
            this.panel2.Location = new System.Drawing.Point(58, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(807, 351);
            this.panel2.TabIndex = 13;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Location = new System.Drawing.Point(868, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(19, 351);
            this.vScrollBar1.TabIndex = 9;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Location = new System.Drawing.Point(60, 355);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(808, 19);
            this.hScrollBar1.TabIndex = 14;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // frameSelector1
            // 
            this.frameSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.frameSelector1.Dpu = 15;
            this.frameSelector1.HighlightEnabled = false;
            this.frameSelector1.HighlightLength = 0F;
            this.frameSelector1.HighlightStart = 0F;
            this.frameSelector1.Location = new System.Drawing.Point(0, 0);
            this.frameSelector1.MeasurementUnit = MovieEditor.MyControls.FMeasurementUnit.Second;
            this.frameSelector1.Name = "frameSelector1";
            this.frameSelector1.Offset = 0F;
            this.frameSelector1.Size = new System.Drawing.Size(1198, 19);
            this.frameSelector1.TabIndex = 6;
            this.frameSelector1.Value = 0F;
            this.frameSelector1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frameSelector1_MouseUp);
            // 
            // TimelineMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(887, 374);
            this.CloseButton = false;
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TimelineMaster";
            this.TabText = "TimelineMaster";
            this.Text = "TimelineMaster";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TimelineMaster_KeyUp);
            this.Resize += new System.EventHandler(this.TimelineMaster_Resize);
            this.TrackPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SelectorLocator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeLocation)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

       
        private System.Windows.Forms.Panel TrackPanel;
        private MovieEditor.MyControls.FrameSelector frameSelector1;
        private System.Windows.Forms.PictureBox TimeLocation;
        private System.Windows.Forms.Button CutButton;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox SelectorLocator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FilterButton;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        
    }
}