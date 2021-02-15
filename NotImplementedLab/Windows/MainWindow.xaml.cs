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

namespace NotImplementedLab.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //List.ItemsSource=new List<Show>
            Items.Add(new ShowcaseListItem("Maths", ""));
            Items.Add(new ShowcaseListItem("Physics", ""));
            Items.Add(new ShowcaseListItem("Astrology", ""));
            Items.Add(new ShowcaseListItem("Computer Science", ""));
            List.ItemsSource = Items;
        }

        public List<ShowcaseListItem> Items = new List<ShowcaseListItem>();
    }
}
