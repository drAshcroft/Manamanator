using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieEditor.Filters
{
    public partial class FadeVideoProperties :  UserControl ,IFilterPropertiesGui   
    {
        private IFilter ParentFilter = null;
        public FadeVideoProperties()
        {
            InitializeComponent();
        }
        public UserControl GetGui()
        {
            return this;
        }
        public void SetControlFilter(IFilter ParentFilter)
        {
            this.ParentFilter = ParentFilter;
            try
            {
                if (ParentFilter.GetFilterProperties()["FadeColor"] == "Black")
                {
                    rBFadeToBlack.Checked = true;
                }
                else
                    rBFadeToWhite.Checked = true;
            }
            catch
            {
                rBFadeToBlack.Checked = true;
            }
        }

        private void rBFadeToWhite_CheckedChanged(object sender, EventArgs e)
        {
            ParentFilter.SetFilterProp("FadeColor", "White");
        }

        private void rBFadeToBlack_CheckedChanged(object sender, EventArgs e)
        {
            ParentFilter.SetFilterProp("FadeColor", "Black");
        }
    }
}
