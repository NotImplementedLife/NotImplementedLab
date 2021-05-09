using System;
using System.Collections.Generic;

namespace ActivityHeader
{
    public static class Metadata
    {
        public static string PluginName = "Domino";
        public static List<Tuple<string, string>> Exports = new List<Tuple<string, string>>
        {
            new Tuple<string,string>
            (
                "Maths",
                "Domino.Tiling.ShowcaseListItem" 
            )
        };
    }
}