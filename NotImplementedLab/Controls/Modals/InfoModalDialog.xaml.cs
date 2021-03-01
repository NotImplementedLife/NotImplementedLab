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

namespace NotImplementedLab.Controls.Modals
{
    /// <summary>
    /// Interaction logic for InfoModalDialog.xaml
    /// </summary>
    public partial class InfoModalDialog : ModalDialogBase
    {
        public InfoModalDialog()
        {
            InitializeComponent();
        }

        public static DependencyProperty InfoProperty = DependencyProperty.Register("Info", typeof(FrameworkElement), typeof(InfoModalDialog),
            new PropertyMetadata(null, InfoPropertyChanged));

        public FrameworkElement Info
        {
            get => GetValue(InfoProperty) as FrameworkElement;
            set => SetValue(InfoProperty, value);

        }

        private static void InfoPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as InfoModalDialog).ScrollContainer.Content = e.NewValue;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }        
    }
}
