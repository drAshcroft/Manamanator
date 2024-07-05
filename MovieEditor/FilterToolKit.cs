using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace MovieEditor
{
    public partial class FilterToolKit : DockContent   
    {
        private CoreAV.MainAVHandler MainAV;
        private List<CoreAV.FilterManager.FilterDescriptionHolder  > FilterList=null;
        public FilterToolKit()
        {
            InitializeComponent();
        }

        public void SetMainAV(CoreAV.MainAVHandler MainAV)
        {
            this.MainAV = MainAV;
            List<CoreAV.FilterManager.FilterDescriptionHolder > lFilters = MainAV.GetPossibleFilters();
            FilterList=lFilters ;
            AddAlienMenu (lFilters);

            alienMenuControl5.MenuStartColor = Color.FromArgb(239, 239, 239);
            alienMenuControl5.MenuEndColor = Color.FromArgb(202, 202, 202);
            alienMenuControl5.MenuInnerBorderColor = Color.FromArgb(254, 254, 254);
            alienMenuControl5.MenuOuterBorderColor = Color.FromArgb(192, 192, 192);
            alienMenuControl5.SideBar = false ;
            alienMenuControl5.SideBarCaption = "Mac style menu";
            alienMenuControl5.SideBarEndGradient = Color.FromArgb(202, 202, 202);
            alienMenuControl5.SideBarStartGradient = Color.FromArgb(202, 202, 202);
            alienMenuControl5.SideBarFontColor = Color.Black;
            //alienMenuControl5.SideBarBitmap = ((Bitmap)MenuControlTest.Properties.Resources.favorites.GetThumbnailImage(22, 22, null, IntPtr.Zero));

            foreach (VistaMenuControl.VistaMenuItem item in alienMenuControl5.Items)
            {
                item.SelectionStartColor = Color.FromArgb(152, 193, 233);
                item.SelectionEndColor = Color.FromArgb(134, 186, 237);
                item.SelectionStartColorStart = Color.FromArgb(104, 169, 234);
                item.SelectionEndColorEnd = Color.FromArgb(169, 232, 255);
                item.InnerBorder = Color.FromArgb(254, 254, 254);
                item.OuterBorder = Color.FromArgb(231, 231, 231);
                item.CaptionFont = new Font("Tahoma", 10, FontStyle.Bold);
                item.ContentFont = new Font("Tahoma", 7);
                item.CaptionColor = Color.Black;
                item.ContentColor = Color.Black;



            }
            alienMenuControl5.Width = this.ClientSize.Width;
            alienMenuControl5.MinimumSize = this.ClientSize;
            alienMenuControl5.Refresh();
        }
        private void AddAlienMenu(List<CoreAV.FilterManager.FilterDescriptionHolder> lFilters)
        {
            foreach (CoreAV.FilterManager.FilterDescriptionHolder  f in lFilters)
            {
                alienMenuControl5.Items.Add(f.FilterName, f.FilterDescription);
            }
        }
        private void AddLinkedList(List<Filters.IFilter> lFilters)
        {
            int LabelTop = 10;
            LinkLabel linkLabel1;
            foreach (Filters.IFilter f in lFilters)
            {
                linkLabel1 = new System.Windows.Forms.LinkLabel();
                linkLabel1.AutoSize = true;
                linkLabel1.Location = new System.Drawing.Point(LabelTop , 21);
                linkLabel1.Name = f.GetType().ToString();
                linkLabel1.Size = new System.Drawing.Size(55, 13);
                linkLabel1.TabIndex = 0;
                linkLabel1.TabStop = true;
                linkLabel1.Text = f.FilterName ;
                linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
                this.Controls.Add(linkLabel1);
                LabelTop +=(int)(linkLabel1.Height * 1.2);
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void FilterToolKit_ResizeEnd(object sender, EventArgs e)
        {
            alienMenuControl5.Width = this.ClientSize.Width;
            alienMenuControl5.MinimumSize = this.ClientSize;
            alienMenuControl5.Refresh();
        }

        private void FilterToolKit_Resize(object sender, EventArgs e)
        {
            alienMenuControl5.Width = this.ClientSize.Width;
            alienMenuControl5.MinimumSize = this.ClientSize;
            alienMenuControl5.Refresh();
        }

        private void FilterToolKit_SizeChanged(object sender, EventArgs e)
        {
            alienMenuControl5.Width = this.ClientSize.Width;
            alienMenuControl5.MinimumSize = this.ClientSize;
            alienMenuControl5.Refresh();
        }

        private void alienMenuControl5_VistaMenuItemClick(VistaMenuControl.VistaMenuControl.VistaMenuItemClickArgs e)
        {
            foreach (CoreAV.FilterManager.FilterDescriptionHolder  f in FilterList )
            {
                if (f.FilterName == e.Item.Text && f.FilterDescription ==e.Item.Description )
                {
                    MainAV.CurrentActiveFilter = MainAV.LoadFilter(f.FilterType );
                    MainAV.SetTimelineToFilterMode();
                }

            }
            
            
        }
    }
}
