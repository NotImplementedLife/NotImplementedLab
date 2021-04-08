using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AlgoVisual.PrimsAlgorithm.Extensions
{
    public static class PointExtensions
    {
        public static Point Add(this Point p, double v)
            => new Point(p.X + v, p.Y + v);
    }
}
