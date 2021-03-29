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
            PointA = new NotImplementedLab.Data.GeometryEntities.Point() { X = 400, Y = 30, Label = "A" };
            PointB = new NotImplementedLab.Data.GeometryEntities.Point() { X = 150, Y = 400, Label = "B" };
            PointC = new NotImplementedLab.Data.GeometryEntities.Point() { X = 650, Y = 400, Label = "C" };
            Canvas.Entities.Add(PointA);
            Canvas.Entities.Add(PointB);
            Canvas.Entities.Add(PointC);
            Canvas.Entities.Add(new NotImplementedLab.Data.GeometryEntities.Segment(PointA, PointB));
            Canvas.Entities.Add(new NotImplementedLab.Data.GeometryEntities.Segment(PointB, PointC));
            Canvas.Entities.Add(new NotImplementedLab.Data.GeometryEntities.Segment(PointC, PointA));
        }

        NotImplementedLab.Data.GeometryEntities.Point PointA, PointB, PointC;

        private void Compute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InputA.Text = ""; InputA.Foreground = Brushes.Black;
            InputB.Text = ""; InputB.Foreground = Brushes.Black;
            InputC.Text = ""; InputC.Foreground = Brushes.Black;
            Inputa.Text = ""; Inputa.Foreground = Brushes.Black;
            Inputb.Text = ""; Inputb.Foreground = Brushes.Black;
            Inputc.Text = ""; Inputc.Foreground = Brushes.Black;
        }
    }


}
