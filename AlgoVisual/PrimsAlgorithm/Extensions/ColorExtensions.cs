using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AlgoVisual.PrimsAlgorithm.Extensions
{
    public static class ColorExtensions
    {
        public static Color Blend(this Color c1, Color c2, int p /* % */)
        {
            double a = 0.01 * p;
            byte r = (byte)(a * c2.R + (1 - a) * c1.R);
            byte g = (byte)(a * c2.G + (1 - a) * c1.G);
            byte b = (byte)(a * c2.B + (1 - a) * c1.B);
            return Color.FromRgb(r, g, b);
        }

        public static Pen ToPen(this Color c, double thickness = 1)
            => new Pen(new SolidColorBrush(c), thickness);
    }
}
