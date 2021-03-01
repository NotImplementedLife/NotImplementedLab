using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityHeader
{
    public static class Metadata
    {
        public static string PluginName = "Lissajous";
        public static List<Tuple<string, string>> Exports = new List<Tuple<string, string>>
        {
            new Tuple<string,string> ("Physics", "LissajousActivity.LissajousShowcaseListItem")            
        };
    }
}
