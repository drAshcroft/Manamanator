namespace MovieEditor.TimeLine
{
    partial class Track
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Vis = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Vis)).BeginInit();
            this.SuspendLayout();
            // 
            // Vis
            // 
            this.Vis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Vis.Location = new System.Drawing.Point(0, 0);
            this.Vis.Name = "Vis";
            this.Vis.Size = new System.Drawing.Size(667, 41);
            this.Vis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Vis.TabIndex = 1;
            this.Vis.TabStop = false;
            this.Vis.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Vis_MouseMove);
            this.Vis.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Vis_MouseDown);
            this.Vis.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Vis_MouseUp);
            // 
            // Track
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Vis);
            this.Name = "Track";
            this.Size = new System.Drawing.Size(667, 41);
            ((System.ComponentModel.ISupportInitialize)(this.Vis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.PictureBox Vis;

    }
}
