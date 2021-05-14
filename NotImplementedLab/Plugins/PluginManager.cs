using NotImplementedLab.Data;
using System;
using System.Collections.Generic;
using System.IO;
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
                            //MessageBox.Show(exports[i].Item1);
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

        public static void LoadDisabledPluginsList()
        {
            for (int i = 0, cnt = Plugins.Count; i < cnt; i++)
                Plugins[i].Active = true;
            try
            {
                using (var r = new StreamReader(File.OpenRead("disabled_plugins.dat")))
                {
                    while (!r.EndOfStream) 
                    {
                        try
                        {
                            var line = r.ReadLine().Split(null);
                            //MessageBox.Show(string.Join("\n", line));
                            var field = line[0];
                            var pname = line[1];
                            var cname = line[2];
                            var p = Plugins.Find((t) => t.Field == field && t.AssemblyName == pname && t.ClassName == cname);
                            if(p!=null)
                            {
                                p.Active = false;
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }            
        }

        public static void SaveDisabledPluginsList()
        {
            using (var w = new StreamWriter(File.Create("disabled_plugins.dat"))) 
            {
                for (int i = 0, cnt = Plugins.Count; i < cnt; i++)
                {
                    if (!Plugins[i].Active) 
                    {
                        w.WriteLine($"{Plugins[i].Field} {Plugins[i].AssemblyName} {Plugins[i].ClassName}");
                    }
                }
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
                if (Plugins[i].Active)
                {
                    switch (Plugins[i].Field)
                    {
                        case "Maths":
                            ShowcaseIcons.MathsItems.Add(Plugins[i].Instantiate());
                            break;
                        case "Physics":
                            ShowcaseIcons.PhysicsItems.Add(Plugins[i].Instantiate());
                            break;
                        case "CS":
                            ShowcaseIcons.CSItems.Add(Plugins[i].Instantiate());
                            break;
                    }
                }
            }
        }

        public static void SealedPopulate()
        {
            ShowcaseIcons.SealedItems.Clear();
            for (int i = 0, cnt = Plugins.Count; i < cnt; i++)
            {
                if (Plugins[i].Active)
                {
                    ShowcaseIcons.SealedItems.Add(Plugins[i].Instantiate());
                }
            }
        }
    }
}
