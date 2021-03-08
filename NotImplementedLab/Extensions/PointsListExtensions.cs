using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotImplementedLab.Extensions
{
    public static class PointsListExtensions
    {
        public static void PlotOnGraph(this List<Point> lst, Controls.Graph graph)
            => graph.Plot(lst);
        
        public static List<Point> CreateFromCurve(this List<Point> lst,Func<double,double> fX,Func<double,double> fY,double t0,double t1,double tstep)
        {
            lst.Clear();
            for (double t = t0; t < t1; t += tstep)
                lst.Add(new Point(fX(t), -fY(t)));
            lst.Add(new Point(fX(t1), -fY(t1)));
            return lst;
        }
    }
}
