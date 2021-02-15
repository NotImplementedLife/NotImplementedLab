using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotImplementedLab.Data
{
    public class ShowcaseListItem
    {
        public ShowcaseListItem(string caption,string drawingPath)
        {
            Caption = caption;
            DrawingPath = drawingPath;
        }

        public string Caption { get; set; } = "Caption";
        public string DrawingPath { get; set; } = "";        
    }
}
