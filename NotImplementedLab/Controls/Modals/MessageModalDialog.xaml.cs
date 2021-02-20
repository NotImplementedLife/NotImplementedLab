using NotImplementedLab.Windows;
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
using System.Windows.Threading;

namespace NotImplementedLab.Controls.Modals
{
    /// <summary>
    /// Interaction logic for MessageModalDialog.xaml
    /// </summary>
    public partial class MessageModalDialog : UserControl
    {
        public MessageModalDialog()
        {
            InitializeComponent();
        }

        #region Message

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(MessageModalDialog),
            new UIPropertyMetadata("Nevermind.", MessagePropertyChanged));

        private static void MessagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MessageModalDialog).TextBlock.Text = e.NewValue as string;
        }

        public string Message
         {
             get { return (string)GetValue(MessageProperty); }
             set { SetValue(MessageProperty, value); }
         }         

         #endregion

        private bool _hideRequest = false;
        private bool _result = false;
        private UIElement _parent;

        public void SetParent(UIElement parent)
        {
            _parent = parent;
        }

        public bool ShowHandlerDialog()
        {
            //Message = message;
            Visibility = Visibility.Visible;

            ModalShow?.Invoke(this, null);            
            _parent.IsEnabled = false;

            _hideRequest = false;
            while (!_hideRequest)
            {
                // HACK: Stop the thread if the application is about to close
                if (this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished)
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                this.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return _result;
        }

        private void HideHandlerDialog()
        {
            _hideRequest = true;
            ModalClose?.Invoke(this, null);
            Visibility = Visibility.Collapsed; 
            _parent.IsEnabled = true;
        }

        public delegate void OnModalShow(object o, EventArgs e);
        public event OnModalShow ModalShow;

        public delegate void OnModalClose(object o, EventArgs e);
        public event OnModalClose ModalClose;       

        public MainWindow Owner;                

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _result = true;
            HideHandlerDialog();
        }
    }
}
