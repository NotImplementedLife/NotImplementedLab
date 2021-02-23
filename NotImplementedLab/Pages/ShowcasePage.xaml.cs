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
            List.ItemsSource = Items;
        }

        public List<ShowcaseListItem> Items = new List<ShowcaseListItem>();

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Owner.MainFrame.Navigate(Owner.FieldSelectorPage);
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(List.SelectedItems.Count==1)
            {               
                int index = List.Items.IndexOf(List.SelectedItems[List.SelectedItems.Count - 1]);
                var uc = Items[index].UserControl;
                if (uc == null)
                {
                    Owner.RaiseError("Could not load activity.");
                }
                else
                {
                    (Owner.ActivityPage as ActivityPage).UIObject = uc;
                    Owner.MainFrame.Navigate(Owner.ActivityPage);
                }                
            }
        }
    }
}
