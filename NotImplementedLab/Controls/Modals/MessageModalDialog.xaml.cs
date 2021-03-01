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
    public partial class MessageModalDialog : ModalDialogBase
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

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
