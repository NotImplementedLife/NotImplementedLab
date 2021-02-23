using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NotImplementedLab.Controls;

namespace NotImplementedLab.Data
{
    public static class ShowcaseIcons
    {
        public static List<ShowcaseListItem> MathsItems = new List<ShowcaseListItem>
        {
            new ShowcaseListItem("Maths1", "", new FieldPresenterButton { FieldName="Test" }),
            new ShowcaseListItem("Maths2", ""),
            new ShowcaseListItem("Maths3", ""),
        };

        public static List<ShowcaseListItem> PhysicsItems = new List<ShowcaseListItem>
        {
            new ShowcaseListItem("Physics1", ""),
            new ShowcaseListItem("Physics2", ""),
            new ShowcaseListItem("Physics3", ""),
        };

        public static List<ShowcaseListItem> CSItems = new List<ShowcaseListItem>
        {
            new ShowcaseListItem("CS1", ""),
            new ShowcaseListItem("CS2", ""),
            new ShowcaseListItem("CS3", ""),
        };
    }
}
