using NotImplementedLab.Windows;
using System.Windows.Controls;

namespace NotImplementedLab.Pages
{
    public class _Page : Page
    {
        public _Page() { }

        public MainWindow Owner;

        public object LastContent;

        public bool CanGoBack { get => LastContent != null; }

        public void GoBack()
        {
            if (CanGoBack)
            {
                Owner.MainFrame.Navigate(LastContent);
            }
        }
    }
}
