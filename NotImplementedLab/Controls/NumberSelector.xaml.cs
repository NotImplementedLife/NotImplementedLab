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
    /// Interaction logic for NumericRangeSelector.xaml
    /// </summary>
    public partial class NumberSelector : UserControl
    {
        private TextBox Editor;
        public NumberSelector()
        {
            InitializeComponent();
            Editor = new TextBox
            {
                Text = "0.0",
                TextAlignment = TextAlignment.Right,
                VerticalContentAlignment=VerticalAlignment.Center,
                VerticalAlignment=VerticalAlignment.Center,
                BorderThickness= new Thickness(0),
                Height=20,
                FontSize=14
            };
            Editor.TextChanged += Editor_TextChanged;
            Editor.LostFocus += Editor_LostFocus;
            DataContext = Editor;
        }

        bool inapp = false;
        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inapp) return;
            if(!double.TryParse(Editor.Text,out double res))
            {
                Editor.Foreground = Brushes.Red;
                return;
            }
            if (res < Slider.Minimum || res > Slider.Maximum) 
            {
                Editor.Foreground = Brushes.Red;
                return;
            }
            inapp = true;
            Value = Slider.Value = res;            
            inapp = false;
            Editor.Foreground = Brushes.Black;

        }

        private void Editor_LostFocus(object sender, RoutedEventArgs e)
        {
            inapp = true;
            if(Editor.Foreground==Brushes.Red)
            {
                Editor.Text = Slider.Value.ToString();
            }
            Editor.Foreground = Brushes.Black;
            inapp = false;
        }
        
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (inapp) return;
            inapp = true;
            Editor.Text = Slider.Value.ToString();
            inapp = false;
            Value = Slider.Value;
            Editor.Foreground = Brushes.Black;            
            ValueChanged?.Invoke(this);            
        }

        public static DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(NumberSelector),
            new PropertyMetadata(0.0, MinimumPropertyChanged));

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        private static void MinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumberSelector).Slider.Minimum = (double)e.NewValue;
        }

        public static DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(NumberSelector),
            new PropertyMetadata(0.0, MaximumPropertyChanged));

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        private static void MaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumberSelector).Slider.Maximum = (double)e.NewValue;
        }

        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(NumberSelector),
            new PropertyMetadata(0.0, ValuePropertyChanged));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumberSelector).Editor.Text = ((double)e.NewValue).ToString();
            (d as NumberSelector).ValueChanged?.Invoke((d as NumberSelector));
        }

        public delegate void OnValueChanged(object sender);
        public event OnValueChanged ValueChanged;
    }
}
