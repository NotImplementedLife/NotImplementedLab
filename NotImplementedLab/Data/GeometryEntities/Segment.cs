using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using NotImplementedLab.Controls;

namespace NotImplementedLab.Data.GeometryEntities
{
    public class Segment : GeometryEntity
    {
        public Point P1, P2;
        public Segment(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
            _fwe = new Line()
            {
                Stroke=Brushes.Black,
                X1 = P1.X,
                Y1 = P1.Y,
                X2 = P2.X,
                Y2 = P2.Y
            };
            P1.Dependants.Add(this);
            P2.Dependants.Add(this);           
        }      

        public override void Initialize(GeometryCanvas canvas)
        {
            canvas.Children.Add(_fwe);
        }

        public override void Update()
        {
            (_fwe as Line).X1 = P1.X;            
            (_fwe as Line).Y1 = P1.Y;            
            (_fwe as Line).X2 = P2.X;            
            (_fwe as Line).Y2 = P2.Y;            
        }
    }
}
