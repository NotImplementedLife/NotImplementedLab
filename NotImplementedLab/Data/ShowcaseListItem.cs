using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NotImplementedLab.Data
{
    public class ShowcaseListItem
    {
        public ShowcaseListItem(string caption, string drawingPath, Type uc = null, FrameworkElement content = null)
        {
            Caption = caption;
            DrawingPath = drawingPath;
            //UserControl = uc;
            Content = content;
            UserControlType = uc;            
        }

        public string Caption { get; set; } = "Caption";
        public string DrawingPath { get; set; } = "";
        //public UserControl UserControl;
        public FrameworkElement Content;

        internal Type UserControlType;

        public UserControl InstantiateUserControl()
        {
            return (UserControl)Activator.CreateInstance(UserControlType);
        }

    }
}
