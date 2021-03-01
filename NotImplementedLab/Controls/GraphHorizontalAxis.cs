using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NotImplementedLab.Controls
{
    public class GraphHorizontalAxis : Canvas
    {
        public GraphHorizontalAxis()
        {
            Height = MinHeight = MaxHeight = 10;
            MainLine.X1 = 0;
            MainLine.Y1 = MainLine.Y2 = 5;
            Binding WidthBinding = new Binding("ActualWidth");
            WidthBinding.Source = this;           
            BindingOperations.SetBinding(MainLine, Line.X2Property, WidthBinding);            
            MainLine.Stroke = Brushes.Black;
            MainLine.StrokeThickness = 1;
            SetTop(MainLine, 0);
            SetLeft(MainLine, 0);
            Children.Add(MainLine);

            TriangleCap.Fill = Brushes.Black;
            SetTop(TriangleCap, 0);
            SetRight(TriangleCap, 0);
            Children.Add(TriangleCap);
        }


        public Line MainLine = new Line();
        public Path TriangleCap = new Path() { Data = Geometry.Parse("M0,0L0,10L5,5") };
    }
}
