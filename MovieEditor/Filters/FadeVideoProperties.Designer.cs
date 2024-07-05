namespace MovieEditor.Filters
{
    partial class FadeVideoProperties
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
            this.gbFadeColor = new System.Windows.Forms.GroupBox();
            this.rBFadeToBlack = new System.Windows.Forms.RadioButton();
            this.rBFadeToWhite = new System.Windows.Forms.RadioButton();
            this.gbFadeColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFadeColor
            // 
            this.gbFadeColor.Controls.Add(this.rBFadeToBlack);
            this.gbFadeColor.Controls.Add(this.rBFadeToWhite);
            this.gbFadeColor.Location = new System.Drawing.Point(3, 3);
            this.gbFadeColor.Name = "gbFadeColor";
            this.gbFadeColor.Size = new System.Drawing.Size(221, 68);
            this.gbFadeColor.TabIndex = 0;
            this.gbFadeColor.TabStop = false;
            this.gbFadeColor.Text = "Fade Color";
            // 
            // rBFadeToBlack
            // 
            this.rBFadeToBlack.AutoSize = true;
            this.rBFadeToBlack.Location = new System.Drawing.Point(17, 42);
            this.rBFadeToBlack.Name = "rBFadeToBlack";
            this.rBFadeToBlack.Size = new System.Drawing.Size(91, 17);
            this.rBFadeToBlack.TabIndex = 1;
            this.rBFadeToBlack.Text = "Fade to Black";
            this.rBFadeToBlack.UseVisualStyleBackColor = true;
            this.rBFadeToBlack.CheckedChanged += new System.EventHandler(this.rBFadeToBlack_CheckedChanged);
            // 
            // rBFadeToWhite
            // 
            this.rBFadeToWhite.AutoSize = true;
            this.rBFadeToWhite.Location = new System.Drawing.Point(17, 19);
            this.rBFadeToWhite.Name = "rBFadeToWhite";
            this.rBFadeToWhite.Size = new System.Drawing.Size(92, 17);
            this.rBFadeToWhite.TabIndex = 0;
            this.rBFadeToWhite.Text = "Fade to White";
            this.rBFadeToWhite.UseVisualStyleBackColor = true;
            this.rBFadeToWhite.CheckedChanged += new System.EventHandler(this.rBFadeToWhite_CheckedChanged);
            // 
            // FadeVideoProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFadeColor);
            this.Name = "FadeVideoProperties";
            this.Size = new System.Drawing.Size(227, 92);
            this.gbFadeColor.ResumeLayout(false);
            this.gbFadeColor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFadeColor;
        private System.Windows.Forms.RadioButton rBFadeToBlack;
        private System.Windows.Forms.RadioButton rBFadeToWhite;
    }
}
