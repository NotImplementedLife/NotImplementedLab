using NotImplementedLab.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotImplementedLab.Plugins
{
    internal class PluginData : INotifyPropertyChanged
    {
        public PluginData(string asmname, string classname, string field, Type _class)
        {
            AssemblyName = asmname;
            ClassName = classname;
            Field = field;
            Class = _class;
            Active = true;
        }

        private bool _Active;
        public bool Active
        {
            get => _Active;
            set
            {
                if(value!=_Active)
                {
                    _Active = value;
                    NotifyPropertyChanged("Active");
                }
            }
        }
        public string AssemblyName { get; }
        public string ClassName { get; }
        public string PluginName { get; }
        public string Field { get; }
        public Type Class { get; }

        public ShowcaseListItem Instantiate() => (ShowcaseListItem)Activator.CreateInstance(Class);

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
