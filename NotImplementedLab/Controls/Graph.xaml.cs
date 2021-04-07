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
using NotImplementedLab.Extensions;

namespace NotImplementedLab.Controls
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : UserControl
    {     
        public Graph()
        {
            InitializeComponent();
            Foreground = Brushes.Red;
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

        public void Plot(List<Point> points)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();
            figure.StartPoint = points[0];
            figure.IsClosed = false;

            for (int i = 1; i < points.Count; i += 1) 
            {
                figure.Segments.Add(new LineSegment(points[i], true));
            }
            geometry.Figures.Add(figure);
            Path.Data = geometry;            
        }

        public void Plot(List<double> values, double start, double end)
        {
            if (values.Count == 0) return;
            double step = (end - start) / values.Count;
            var geometry = new PathGeometry();
            var figure = new PathFigure();
            var index = start;
            figure.StartPoint = new Point(index, values[0]);
            index += step;
            figure.IsClosed = false;

            for (int i = 1; i < values.Count; i += 1) 
            {
                figure.Segments.Add(new LineSegment(new Point(index,values[i]), true));
                index += step;
            }
            geometry.Figures.Add(figure);
            Path.Data = geometry;
        }

        public void PlotStep(List<double> values, double start, double step)
        {
            if (values.Count == 0) return;
            var geometry = new PathGeometry();
            var figure = new PathFigure();
            var index = start;
            figure.StartPoint = new Point(index, values[0]);
            index += step;
            figure.IsClosed = false;

            for (int i = 1; i < values.Count; i += 1)
            {
                figure.Segments.Add(new LineSegment(new Point(index, values[i]), true));
                index += step;
            }
            geometry.Figures.Add(figure);
            Path.Data = geometry;
        }       
    }
}
