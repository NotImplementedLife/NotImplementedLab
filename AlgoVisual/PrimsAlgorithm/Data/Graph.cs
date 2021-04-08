using AlgoVisual.PrimsAlgorithm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AlgoVisual.PrimsAlgorithm.Data
{
    public class Graph
    {
        Random rand = new Random();
        public Cell[,] Cells = new Cell[18, 30];
        public List<Edge> Edges = new List<Edge>();
        public Graph()
        {
            for (int r = 0; r < 18; r++)
            {
                for (int c = 0; c < 30; c++)
                {
                    Cells[r, c] = new Cell(r, c);
                }
            }
            for (int r = 0; r < 18; r++)
                for (int c = 0; c < 30; c++)
                {
                    if (r < 17) Edges.Add(new Edge(Cells[r, c], Cells[r + 1, c], rand.Next() % 100 + 1));                    
                    if (c < 29) Edges.Add(new Edge(Cells[r, c], Cells[r, c + 1], rand.Next() % 100 + 1));                    
                    Cells[r, c].Position.X = 25 + 25 * c;
                    Cells[r, c].Position.Y = 15 + 25 * r;
                }
        }        

        public Cell this[int r,int c]
        {
            get => Cells[r, c];
        }

        public void DrawOnRTB(RenderTargetBitmap rtb, bool display = false)
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext ctx = visual.RenderOpen())
            {
                for (int i = 0; i < Edges.Count; i++)
                {
                    ctx.DrawLine(Edges[i].Color.ToPen(6), Edges[i].C1.Position.Add(3), Edges[i].C2.Position.Add(3));
                }                
            }
            rtb.Render(visual);

            for (int r = 0; r < 18; r++)
                for (int c = 0; c < 30; c++)
                {
                    Cells[r, c].DrawOnRTB(rtb, display);
                }
        }
    }
}
