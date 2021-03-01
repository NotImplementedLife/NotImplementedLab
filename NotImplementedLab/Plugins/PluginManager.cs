using NotImplementedLab.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotImplementedLab.Plugins
{
    internal static class PluginManager
    {
        public static void ImportPlugins(string dllpath)
        {            
            try
            {
                Assembly assembly = Assembly.LoadFrom(dllpath);
                Type type = assembly.GetType("ActivityHeader.Metadata");
                var asm_name = type.GetField("PluginName").GetValue(null) as string;                
                var exports = type.GetField("Exports").GetValue(null) as List<Tuple<string, string>>;
                for (int i = 0, cnt = exports.Count; i < cnt; i++)
                {
                    try
                    {                        
                        var _class = assembly.GetType(exports[i].Item2);                        
                        if (_class.IsSubclassOf(typeof(ShowcaseListItem)))
                        {                            
                            Plugins.Add(new PluginData(asm_name, exports[i].Item2, exports[i].Item1, _class));
                        }
                    }
                    catch (TypeLoadException)
                    {
                        
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public static List<PluginData> Plugins = new List<PluginData>();

        public static void Populate()
        {
            ShowcaseIcons.MathsItems.Clear();
            ShowcaseIcons.PhysicsItems.Clear();
            ShowcaseIcons.CSItems.Clear();
            for (int i = 0, cnt = Plugins.Count; i < cnt; i++)
            {
                //MessageBox.Show(Plugins[i].ClassName);
                switch(Plugins[i].Field)
                {
                    case "Maths" :
                        ShowcaseIcons.MathsItems.Add(Plugins[i].Instantiate());
                        break;
                    case "Physics" :
                        ShowcaseIcons.PhysicsItems.Add(Plugins[i].Instantiate());
                        break;
                    case "CS" :
                        ShowcaseIcons.PhysicsItems.Add(Plugins[i].Instantiate());
                        break;
                }              
            }
        }
    }
}
