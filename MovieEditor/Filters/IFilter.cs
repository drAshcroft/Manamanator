using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MovieEditor.Filters
{
    public interface IFilter
    {
        IFilter Clone();

        string FilterName
        {
            get;
        }
        string FilterDescription
        {
            get;
        }
        double ClipStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// Used only for filters that move from one track to another
        /// this shows where the transition is from one to another.
        /// </summary>
        double ClipMidTime
        {
            get;
            set;
        }
        double ClipEndTime
        {
            get;
            set;
        }
        Control GUI
        {
            get;
            set;
        }
        eFilterType FilterType
        {
            get;
        }
        Bitmap TransformFrame(Bitmap InputFrame, double GlobalTime,CoreAV.vidFrameIndex[] FrameList);
        Dictionary<string, string> GetFilterProperties();
        void SetFilterProperties(Dictionary<string, string> Properties);
        void SetFilterProp(string Propname, string PropValue);
        UserControl   FilterPropertiesGui
        {
            get;
        }
    }
    public enum eFilterType
    {
        Internal,
        Start,
        End,
        Transition
    }

    // create custom attribute to be assigned to class members
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Field |
        AttributeTargets.Method |
        AttributeTargets.Property,
        AllowMultiple = true)]
    public class FilterPropsAttribute : System.Attribute
    {
        // attribute constructor for 
        // positional parameters
        public FilterPropsAttribute(string FilterName, string FilterDescription,string FilterVersion)
        {
            this.filtername  =FilterName ;
            this.filterDescription  =FilterDescription ;
            this.filterVersion  =FilterDescription ;
        }

        public string FilterName
        {
            get { return filtername; }
        }
        public string FilterDescription
        {
            get { return filterDescription; }
        }
        public string FilterVersion
        {
            get { return filterVersion; }
        }

        private string filtername;
        private string filterDescription;
        private string filterVersion;
    }


}
