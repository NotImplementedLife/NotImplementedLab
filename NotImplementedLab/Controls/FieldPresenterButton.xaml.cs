using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace NotImplementedLab.Controls
{
    /// <summary>
    /// Interaction logic for FieldPresenterButton.xaml
    /// </summary>
    [DefaultEvent("Click")]
    public partial class FieldPresenterButton : UserControl
    {
        public FieldPresenterButton()
        {
            InitializeComponent();
        }

        public static DependencyProperty FieldNameProperty = DependencyProperty.Register("FieldName", typeof(string), typeof(FieldPresenterButton),
            new PropertyMetadata("Default", FieldNamePropertyChanged));

        public string FieldName
        {
            get => GetValue(FieldNameProperty) as string;
            set => SetValue(FieldNameProperty, value);
        }

        private static void FieldNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as FieldPresenterButton).FieldNameControl.Text = e.NewValue as string;
        }

        bool __msdown = false;

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        { 
            if(e.ChangedButton==MouseButton.Left)
            {
                __msdown = true;
            }            
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            __msdown = false;
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if(__msdown) // click!
            {
                __msdown = false;
                Click?.Invoke(this);
            }
        }

        public delegate void OnClick(object s);
        public event OnClick Click;
    }
}
