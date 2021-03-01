using NotImplementedLab.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotImplementedLab.Plugins
{
    internal class PluginData
    {
        public PluginData(string asmname, string classname, string field, Type _class)
        {
            AssemblyName = asmname;
            ClassName = classname;
            Field = field;
            Class = _class;
        }

        public string AssemblyName { get; }
        public string ClassName { get; }
        public string PluginName { get; }
        public string Field { get; }
        public Type Class { get; }

        public ShowcaseListItem Instantiate() => (ShowcaseListItem)Activator.CreateInstance(Class);
    }
}
