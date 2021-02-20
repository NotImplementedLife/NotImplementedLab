using NotImplementedLab.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotImplementedLab.Pages
{
    /// <summary>
    /// Interaction logic for ShowcasePage.xaml
    /// </summary>
    public partial class ShowcasePage : _Page
    {
        public ShowcasePage()
        {
            InitializeComponent();
            Items.Add(new ShowcaseListItem("Maths", ""));
            Items.Add(new ShowcaseListItem("Physics", ""));
            Items.Add(new ShowcaseListItem("Astrology", ""));
            Items.Add(new ShowcaseListItem("Computer Science", ""));
            Items.Add(new ShowcaseListItem("Computer Science", ""));
            Items.Add(new ShowcaseListItem("Computer Science", ""));
            Items.Add(new ShowcaseListItem("Computer Science", ""));
            Items.Add(new ShowcaseListItem("Computer Science", ""));
            Items.Add(new ShowcaseListItem("Computer Science", ""));
            List.ItemsSource = Items;
        }

        public List<ShowcaseListItem> Items = new List<ShowcaseListItem>();      
    }
}
