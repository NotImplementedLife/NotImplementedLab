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

namespace Geometry.TriangleSolver
{
    /// <summary>
    /// Interaction logic for Playground.xaml
    /// </summary>
    public partial class Playground : UserControl
    {
        public Playground()
        {
            InitializeComponent();
            PointA = new NotImplementedLab.Data.GeometryEntities.Point() { X = 10, Y = 10, Label = "A" };
            PointB = new NotImplementedLab.Data.GeometryEntities.Point() { X = 170, Y = 400, Label = "B" };
            PointC = new NotImplementedLab.Data.GeometryEntities.Point() { X = 300, Y = 270, Label = "C" };
            Canvas.Entities.Add(PointA);
            Canvas.Entities.Add(PointB);
            Canvas.Entities.Add(PointC);
            Canvas.Entities.Add(new NotImplementedLab.Data.GeometryEntities.Segment(PointA, PointB));
            Canvas.Entities.Add(new NotImplementedLab.Data.GeometryEntities.Segment(PointB, PointC));
            Canvas.Entities.Add(new NotImplementedLab.Data.GeometryEntities.Segment(PointC, PointA));
        }

        NotImplementedLab.Data.GeometryEntities.Point PointA, PointB, PointC;
    }
}
