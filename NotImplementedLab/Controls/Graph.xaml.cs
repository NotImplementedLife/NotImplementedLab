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
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : UserControl
    {
        double f(double x)
        {
            return x * x * x - 10 * x;
        }
        public Graph()
        {
            InitializeComponent();
            var geometry = new PathGeometry();
            var figure = new PathFigure();
            figure.StartPoint = new Point(-225, -f(-5));
            figure.IsClosed = false;
                        
            for (double i = -4.5; i <= 5; i+=0.5)
            {
                figure.Segments.Add(new LineSegment(new Point(45 * i, -f(i)), true));
            }
            geometry.Figures.Add(figure);
            Path.Data = geometry;

        }

        public static DependencyProperty OriginXProperty = DependencyProperty.Register("OriginX", typeof(double), typeof(Graph),
            new PropertyMetadata(50.0, OriginXPropertyChanged));

        public double OriginX
        {
            get => (double)GetValue(OriginXProperty);
            set => SetValue(OriginXProperty, value);
        }        

        private static void OriginXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Graph).RefreshYAxis();
        }

        public static DependencyProperty OriginYProperty = DependencyProperty.Register("OriginY", typeof(double), typeof(Graph),
            new PropertyMetadata(50.0, OriginYPropertyChanged));       

        public double OriginY
        {
            get => (double)GetValue(OriginYProperty);
            set => SetValue(OriginYProperty, value);
        }

        private static void OriginYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Graph).RefreshXAxis();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshXAxis();
            RefreshYAxis();
            Canvas.SetLeft(Path, Canvas.GetLeft(VertAxis)+5);
            Canvas.SetTop(Path, Canvas.GetTop(HorizAxis)+5);
            //Path.RenderTransformOrigin = new Point(Canvas.GetLeft(VertAxis), Canvas.GetTop(HorizAxis));
        }

        private void RefreshXAxis()
        {
            Canvas.SetTop(HorizAxis, OriginY * 0.01 * Container.ActualHeight);            
        }

        private void RefreshYAxis()
        {
            Canvas.SetLeft(VertAxis, OriginX * 0.01 * Container.ActualWidth);
        }
    }
}
