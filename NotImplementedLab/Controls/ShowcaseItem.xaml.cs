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

namespace NotImplementedLab.Controls
{
    /// <summary>
    /// Interaction logic for ShowcaseItem.xaml
    /// </summary>
    public partial class ShowcaseItem : UserControl
    {
        public ShowcaseItem()
        {
            InitializeComponent();
        }

        public static DependencyProperty DisplayPathDataProperty =
            DependencyProperty.Register("DisplayPathData", typeof(string), typeof(ShowcaseItem),
            new PropertyMetadata("",DisplayPathDataPropertyChanged));

        
        public string DisplayPathData
        {
            get => GetValue(DisplayPathDataProperty) as string;
            set => SetValue(DisplayPathDataProperty, value);
        }

        public static DependencyProperty GrayscaleProperty =
            DependencyProperty.Register("Grayscale", typeof(bool), typeof(ShowcaseItem),
                new PropertyMetadata(GrayscalePropertyChanged));       

        public bool Grayscale
        {
            get => (bool)GetValue(GrayscaleProperty);
            set => SetValue(GrayscaleProperty, value);
        }

        public static DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption",typeof(string),typeof(ShowcaseItem),
            new PropertyMetadata("Caption",CaptionPropertyChanged));
       
        public string Caption
        {
            get => GetValue(CaptionProperty) as string;
            set => SetValue(CaptionProperty, value);
        }

        private static void DisplayPathDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowcaseItem).ShowcaseDisplay.DisplayPathData = e.NewValue as string;
        }

        private static void GrayscalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowcaseItem).ShowcaseDisplay.Grayscale = (bool)e.NewValue;
        }

        private static void CaptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowcaseItem).CaptionControl.Text = (string)e.NewValue;
        }
    }
}
