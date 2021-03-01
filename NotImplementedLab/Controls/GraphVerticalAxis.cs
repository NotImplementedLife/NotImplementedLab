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
    public class GraphVerticalAxis : Canvas
    {
        public GraphVerticalAxis()
        {
            Width = MinWidth = MaxWidth = 10;
            MainLine.X1 = MainLine.X2 = 5;
            MainLine.Y1 = 0;            
            Binding HeightBinding = new Binding("ActualHeight");
            HeightBinding.Source = this;
            BindingOperations.SetBinding(MainLine, Line.Y2Property, HeightBinding);
            MainLine.Stroke = Brushes.Black;
            MainLine.StrokeThickness = 1;
            SetTop(MainLine, 0);
            SetLeft(MainLine, 0);
            Children.Add(MainLine);

            TriangleCap.Fill = Brushes.Black;
            SetTop(TriangleCap, 0);
            SetLeft(TriangleCap, 0);
            Children.Add(TriangleCap);
        }

        public Line MainLine = new Line();
        public Path TriangleCap = new Path() { Data = Geometry.Parse("M5,0L0,5L10,5") };
    }
}
