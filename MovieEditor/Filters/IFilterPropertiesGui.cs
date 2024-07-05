using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms  ;

namespace MovieEditor.Filters
{
    public interface  IFilterPropertiesGui  
    {
         void SetControlFilter(IFilter myFilter);
         UserControl GetGui();
    }
}
