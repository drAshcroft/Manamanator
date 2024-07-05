using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace MovieEditor.CoreAV
{

    public class FilterManager
    {
        private Dictionary <string,Type> FilterList =null;
        public class FilterDescriptionHolder
        {
            public FilterDescriptionHolder(string FilterName, string FilterDescription, string FilterVersion,string FilterType)
            {
                this.FilterDescription = FilterDescription;
                this.FilterName = FilterName;
                this.FilterVersion = FilterVersion;
                this.FilterType = FilterType;
            }
            public string FilterName;
            public string FilterDescription;
            public string FilterVersion;
            public string FilterType;
        }
        public List<FilterDescriptionHolder  > PossibleFilters()
        {
            List<FilterDescriptionHolder > filters = new List<FilterDescriptionHolder >();
            foreach ( Type  t in FilterList.Values   )
            {
                try
                {
                    object [] Attributes =  t.GetCustomAttributes(typeof(Filters.FilterPropsAttribute), false);
                    Filters.FilterPropsAttribute fpa = (Filters.FilterPropsAttribute)Attributes[0];
                    filters.Add(new FilterDescriptionHolder (fpa.FilterName,fpa.FilterDescription,fpa.FilterVersion,t.ToString()));
                }
                catch { }
            }
            return filters;
        }

        public Filters.IFilter GetFilter( string FilterType)
        {
             Type type = FilterList [FilterType.ToLower()];
             ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
             Filters.IFilter  filter = (Filters.IFilter )ci.Invoke(null);

             return (filter );
           
        }

        public  void  GetPluginFilters()
        {

            List<Assembly> assemblies = new List<Assembly>();

            string homeDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            bool dirExists;

            try
            {
                dirExists = Directory.Exists(homeDir);
            }
            catch
            {
                dirExists = false;
            }

            if (dirExists)
            {
                string fileSpec = "filter*.dll";
                string[] filePaths = Directory.GetFiles(homeDir, fileSpec);

                foreach (string filePath in filePaths)
                {
                    Assembly pluginAssembly = null;

                    try
                    {
                        pluginAssembly = Assembly.LoadFrom(filePath);
                        assemblies.Add(pluginAssembly);
                    }

                    catch 
                    {
                        //Tracing.Ping("Exception while loading " + filePath + ": " + ex.ToString());
                    }
                }
                assemblies.Add( Assembly.GetExecutingAssembly());

            }
            List<Type> Filters = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    try
                    {
                        if (typeof(Filters.IFilter ).IsAssignableFrom(type) && type!=typeof(Filters.IFilter ) )
                            Filters.Add(type);
                    }
                    catch 
                    {

                    }
                }
            }
            FilterList = new Dictionary<string, Type>();
            foreach (Type gdc in Filters)
                try
                {
                    FilterList.Add(gdc.ToString().ToLower(), gdc);

                }
                catch
                { }
            
           // FilterList.Add(typeof(MovieEditor.Filters.FadeVideo).ToString().ToLower(), typeof(MovieEditor.Filters.FadeVideo));
        }
    }
}
