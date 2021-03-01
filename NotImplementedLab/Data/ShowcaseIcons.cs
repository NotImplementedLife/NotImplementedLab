using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NotImplementedLab.Controls;

namespace NotImplementedLab.Data
{
    internal static class ShowcaseIcons
    {
        public static List<ShowcaseListItem> MathsItems = new List<ShowcaseListItem>
        {
            new ShowcaseListItem("Maths1", "", new Graph(),new Button() { Content="123" }),
            new ShowcaseListItem("Maths2", "", new Graph()),
            new ShowcaseListItem("Maths3", "", null),
        };

        public static List<ShowcaseListItem> PhysicsItems = new List<ShowcaseListItem>
        {
            new ShowcaseListItem("Physics1", "", null),
            new ShowcaseListItem("Physics2", "", null),
            new ShowcaseListItem("Physics3", "", null),
        };

        public static List<ShowcaseListItem> CSItems = new List<ShowcaseListItem>
        {
            new ShowcaseListItem("CS1", "", null),
            new ShowcaseListItem("CS2", "", null),
            new ShowcaseListItem("CS3", "", null),
        };
    }
}
