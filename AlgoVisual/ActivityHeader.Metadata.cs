using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityHeader
{
    public static class Metadata
    {
        public static string PluginName = "AlgoVisual";
        public static List<Tuple<string, string>> Exports = new List<Tuple<string, string>>
        {
            new Tuple<string,string> ("CS", "AlgoVisual.FloatingPoint.ShowcaseListItem"),            
            new Tuple<string,string> ("CS", "AlgoVisual.LeesAlgorithm.ShowcaseListItem"),
            new Tuple<string,string> ("CS", "AlgoVisual.PrimsAlgorithm.ShowcaseListItem"),
        };
    }
}
