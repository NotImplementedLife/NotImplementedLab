using AlgoVisual.PrimsAlgorithm.Extensions;
using System.Windows.Media;

namespace AlgoVisual.PrimsAlgorithm.Data
{
    public class Edge
    {
        public Edge(Cell c1, Cell c2, int cost)
        {
            C1 = c1;
            C2 = c2;
            Cost = cost;
        }
        public Cell C1, C2;
        public int Cost;
        public bool Highlighted = false;

        public Color Color
        {
            get => Highlighted ? Colors.Green : Colors.LightGray.Blend(Colors.Gold, Cost);
        }
    }
}
