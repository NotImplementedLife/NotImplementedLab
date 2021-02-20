using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace NotImplementedLab.Controls
{
    /// <summary>
    /// Interaction logic for PopupNotification.xaml
    /// </summary>
    public partial class PopupNotification : UserControl, INotifyPropertyChanged
    {
        public PopupNotification()
        {
            InitializeComponent();
        }        

        private string _PopupType = "Warning";
        public string PopupType
        {
            get => _PopupType;
            set
            {
                _PopupType = value;
                OnPropertyChanged();
            }
        }

        private string _PopupMessage = "long long long is too long";
        public string PopupMessage
        {
            get => _PopupMessage;
            set
            {
                _PopupMessage = value;
                OnPropertyChanged();
            }
        }

        public Thickness InnerPadding
        {
            get => _Border.Padding;
            set
            {
                _Border.Padding = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
