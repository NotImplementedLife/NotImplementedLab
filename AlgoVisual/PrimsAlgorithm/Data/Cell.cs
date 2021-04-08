using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AlgoVisual.PrimsAlgorithm.Data
{
    public class Cell
    {
        public Cell(int r, int c)
        {
            Row = r;
            Col = c;
        }

        public int Row;
        public int Col;

        public Point Position = new Point(0, 0);        

        public void DrawOnRTB(RenderTargetBitmap rtb, bool display = false)
        {
            var node = new Rectangle { Fill = display ? Brushes.Green : Brushes.White, Width = 6, Height = 6 };
            Size size = new Size(6, 6);

            node.Measure(size);
            node.Arrange(new Rect(Position, size));

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen()) 
            {
                VisualBrush visualBrush = new VisualBrush(node);
                drawingContext.DrawRectangle(visualBrush, null,
                  new Rect(Position, new Size(size.Width, size.Height)));
            }
            rtb.Render(drawingVisual);
        }
    }
}
