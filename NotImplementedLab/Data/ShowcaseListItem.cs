using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NotImplementedLab.Data
{
    public class ShowcaseListItem
    {
        public ShowcaseListItem(string caption,string drawingPath,UserControl uc=null)
        {
            Caption = caption;
            DrawingPath = drawingPath;
            UserControl = uc;
        }

        public string Caption { get; set; } = "Caption";
        public string DrawingPath { get; set; } = "";
        public UserControl UserControl;
    }
}
