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
    /// Interaction logic for ShowcaseDisplay.xaml
    /// </summary>
    public partial class ShowcaseDisplay : UserControl
    {
        public ShowcaseDisplay()
        {
            InitializeComponent();
        }


        public static DependencyProperty DisplayPathDataProperty =
           DependencyProperty.Register("DisplayPathData", typeof(string), typeof(ShowcaseDisplay),
           new PropertyMetadata(DisplayPathDataPropertyChanged));

        public string DisplayPathData
        {
            get => GetValue(DisplayPathDataProperty) as string;
            set => SetValue(DisplayPathDataProperty, value);
        }

        public static DependencyProperty GrayscaleProperty =
            DependencyProperty.Register("Grayscale", typeof(bool), typeof(ShowcaseDisplay),
                new PropertyMetadata(GrayscalePropertyChanged));

        public bool Grayscale
        {
            get => (bool)GetValue(GrayscaleProperty);
            set => SetValue(GrayscaleProperty, value);
        }

        private static void GrayscalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowcaseDisplay).SetGrayscale((bool)e.NewValue);
        }

        private static void DisplayPathDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = d as ShowcaseDisplay;
            c.SetData(c.DisplayPathData);
        }

        private List<ShowcaseDisplayPathData> _Data = new List<ShowcaseDisplayPathData>();      
        public List<ShowcaseDisplayPathData> Data
        {
            get => _Data;
            set
            {
                _Data = value;
                RefreshData();
            }
        }

        void RefreshData()
        {
            Canvas.Children.Clear();
            for (int i = 0; i < Data.Count; i++)
                Canvas.Children.Add(Data[i].Path);
        }

        Color BackgroundColor;
        Color GrayscaleBackgroundColor;

        public void SetData(string str)
        {
            if (str == "" || str==null)
                str = "Turquoise!Yellow | M 100 100 m - 75 0 a 75 75 0 1 0 150 0 a 75 75 0 1 0 - 150 0;";
            Data.Clear();
            var parts = str.Split('!');
            try
            {
                BackgroundColor = (Color)ColorConverter.ConvertFromString(parts[0]);                
                byte grayScale = (byte)((BackgroundColor.R * 0.3) + (BackgroundColor.G * 0.59) + (BackgroundColor.B * 0.11));
                GrayscaleBackgroundColor = Color.FromArgb(BackgroundColor.A, grayScale, grayScale, grayScale);
                Background = new SolidColorBrush(BackgroundColor);
            }
            catch (Exception) { }
            var paths = parts[1].Split(';');
            for(int i=0;i<paths.Length;i++)
            {                
                var args = paths[i].Split('|');
                if (args.Length == 2) 
                {
                    try
                    {
                        //MessageBox.Show(args[0] + "\n" + args[1]);
                        Data.Add(new ShowcaseDisplayPathData(args[1], (Color)ColorConverter.ConvertFromString(args[0])));
                    }
                    catch(Exception) { }
                }
            }
            RefreshData();
            if (IsGrayscale)
            {
                SetGrayscale(true);
            }
        }

        public bool IsGrayscale = false;

        public void SetGrayscale(bool opt=true)
        {
            Background = new SolidColorBrush(opt ? GrayscaleBackgroundColor : BackgroundColor);
            for (int i = 0, cnt = Data.Count; i < cnt; i++)
                Data[i].SetPathGrayscale(opt);
            IsGrayscale = opt;
        }
    }
}
