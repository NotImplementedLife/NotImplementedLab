using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NotImplementedLab.Data
{
    public class ShowcaseDisplayPathData
    {
        public ShowcaseDisplayPathData(string pathData,Color fillColor)
        {
            Path = new Path
            {
                Data = Geometry.Parse(pathData),
                Fill = new SolidColorBrush(fillColor)
            };
            FillColor = fillColor;
            byte grayScale = (byte)((fillColor.R * 0.3) + (fillColor.G * 0.59) + (fillColor.B * 0.11));
            GrayScaleFillColor = Color.FromArgb(fillColor.A, grayScale, grayScale, grayScale);
        }
        public Path Path;        
        public readonly Color FillColor;
        public readonly Color GrayScaleFillColor;

        public void SetPathGrayscale(bool opt = true)
            => Path.Fill = new SolidColorBrush(opt ? GrayScaleFillColor : FillColor);
    }
}
