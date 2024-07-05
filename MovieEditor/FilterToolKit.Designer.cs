namespace MovieEditor
{
    partial class FilterToolKit
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
            this.alienMenuControl5 = new VistaMenuControl.VistaMenuControl();
            this.SuspendLayout();
            // 
            // alienMenuControl5
            // 
            this.alienMenuControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.alienMenuControl5.BackImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.alienMenuControl5.BackMenuImage = null;
            this.alienMenuControl5.CheckOnClick = false;
            this.alienMenuControl5.FlatSeparators = false;
            this.alienMenuControl5.FlatSeparatorsColor = System.Drawing.Color.Silver;
            this.alienMenuControl5.ItemHeight = 48;
            this.alienMenuControl5.ItemWidth = 75;
            this.alienMenuControl5.Location = new System.Drawing.Point(0, 0);
            this.alienMenuControl5.MaximumSize = new System.Drawing.Size(300, 400);
            this.alienMenuControl5.MenuEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.alienMenuControl5.MenuInnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.alienMenuControl5.MenuOrientation = VistaMenuControl.VistaMenuControl.VistaMenuOrientation.Vertical;
            this.alienMenuControl5.MenuOuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.alienMenuControl5.MenuStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.alienMenuControl5.MinimumSize = new System.Drawing.Size(100, 46);
            this.alienMenuControl5.Name = "alienMenuControl5";
            this.alienMenuControl5.RenderSeparators = true;
            this.alienMenuControl5.SelectedItem = -1;
            this.alienMenuControl5.SideBar = false;
            this.alienMenuControl5.SideBarBitmap = null;
            this.alienMenuControl5.SideBarCaption = "Alien Cool Menu";
            this.alienMenuControl5.SideBarEndGradient = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.alienMenuControl5.SideBarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.alienMenuControl5.SideBarFontColor = System.Drawing.Color.White;
            this.alienMenuControl5.SideBarStartGradient = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.alienMenuControl5.Size = new System.Drawing.Size(300, 151);
            this.alienMenuControl5.TabIndex = 2;
            this.alienMenuControl5.VistaMenuItemClick += new VistaMenuControl.VistaMenuControl.VistaMenuItemClickHandler(this.alienMenuControl5_VistaMenuItemClick);
            // 
            // FilterToolKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(312, 316);
            this.Controls.Add(this.alienMenuControl5);
            this.Name = "FilterToolKit";
            this.TabText = "FilterToolKit";
            this.Text = "FilterToolKit";
            this.SizeChanged += new System.EventHandler(this.FilterToolKit_SizeChanged);
            this.ResizeEnd += new System.EventHandler(this.FilterToolKit_ResizeEnd);
            this.Resize += new System.EventHandler(this.FilterToolKit_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private VistaMenuControl.VistaMenuControl alienMenuControl5;

    }
}