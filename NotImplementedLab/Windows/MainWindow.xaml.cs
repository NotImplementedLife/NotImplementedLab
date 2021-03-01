using NotImplementedLab.Controls;
using NotImplementedLab.Data;
using NotImplementedLab.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            FieldSelectorPage = new FieldSelectorPage { Owner = this };
            ShowcasePage = new ShowcasePage { Owner = this };
            ActivityPage = new ActivityPage { Owner = this };
            MainFrame.NavigationService.Navigate(FieldSelectorPage);

            wMessageModal.Owner = this;
            wMessageModal.SetParent(MainContent);
            wMessageModal.ModalShow += OnModalShow;
            wMessageModal.ModalClose += OnModalClose;

            wInfoModal.Owner = this;
            wInfoModal.SetParent(MainContent);
            wInfoModal.ModalShow += OnModalShow;
            wInfoModal.ModalClose += OnModalClose;
            Plugins.PluginManager.ImportPlugins(@"Plugins\LissajousActivity.dll");
            Plugins.PluginManager.Populate();
        }

        private void OnModalShow(object o, EventArgs e) => GlobalBlurMask.Visibility = Visibility.Visible;                 
        private void OnModalClose(object o, EventArgs e) => GlobalBlurMask.Visibility = Visibility.Hidden;


        public _Page FieldSelectorPage;
        public _Page ShowcasePage;
        public _Page ActivityPage;

        public void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (!MainFrame.CanGoBack) return;
            MainFrame.RemoveBackEntry();
        }


        public void InfoModal()
        {
            wInfoModal.ShowHandlerDialog();
        }
        public void MessageModal(string message)
        {
            wMessageModal.Message = message;
            wMessageModal.ShowHandlerDialog();
        }

        public void RaiseWarning(string message)
        {
            var warning = new PopupNotification()
            {
                PopupType = "Warning",
                PopupMessage = message,
                IsEnabled = false,
                Opacity = 0.9
            };
            if (PopupPanel.Children.Count >= 3) PopupPanel.Children.RemoveRange(0, PopupPanel.Children.Count - 2);
            PopupPanel.Children.Add(warning);
            Task.Run(new Action(() =>
            {
                Thread.Sleep(5000);
                double op = 0;
                Dispatcher.Invoke(new Action(() => { op = warning.Opacity; }));
                while (op > 0)
                {
                    op -= 0.1;
                    if (op < 0) op = 0;
                    Dispatcher.Invoke(new Action(() => { try { warning.Opacity = op; } catch (Exception) { } }));
                    Thread.Sleep(50);
                }
                Dispatcher.Invoke(new Action(() => { PopupPanel.Children.Remove(warning); }));
            }));
        }

        public void RaiseError(string message)
        {
            var error = new PopupNotification()
            {
                PopupType = "Error",
                PopupMessage = message,
                IsEnabled = false,
                IsHitTestVisible = false,
                Opacity = 0.9
            };
            if (PopupPanel.Children.Count >= 3) PopupPanel.Children.RemoveRange(0, PopupPanel.Children.Count - 2);
            PopupPanel.Children.Add(error);
            Task.Run(new Action(() =>
            {
                Thread.Sleep(5000);
                double op = 0;
                Dispatcher.Invoke(new Action(() => { try { op = error.Opacity; } catch (Exception) { } }));
                while (op > 0)
                {
                    op -= 0.1;
                    if (op < 0) op = 0;
                    Dispatcher.Invoke(new Action(() =>
                    {
                        error.Opacity = op;
                    }));
                    Thread.Sleep(50);
                }
                Dispatcher.Invoke(new Action(() => { PopupPanel.Children.Remove(error); }));
            }));
        }
    }
}
