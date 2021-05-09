using Domino.Tiling.Solvers;
using System.Collections.Generic;

namespace Domino.Tiling
{
    class StateTreeNode
    {
        public StateTreeNode(CPSolver solver)
        {
            Solver = solver;
        }

        CPSolver Solver;
        public int Depth = -1;
        public StateTreeNode Parent = null;
        public Tile Value = null;
        public List<StateTreeNode> Children { get; private set; } = new List<StateTreeNode>();
        public bool IsExpanded { get; private set; } = false;

        List<Tile> UsedBlackTiles = new List<Tile>();

        public StateTreeNode NextSibling()
        {
            if (Parent == null)
                return null;
            int index = Parent.Children.IndexOf(this) + 1;
            return index == Parent.Children.Count ? null : Parent.Children[index];
        }

        public void Expand()
        {
            if (IsExpanded) return;
            IsExpanded = true;
            int wi = Depth + 1;
            if (wi >= Solver.WhiteOnes.Count)
                return;

            for (int i = 0; i < Solver.WhiteOnes[wi].Neighbors.Count; i++)
                if (!UsedBlackTiles.Contains(Solver.WhiteOnes[wi].Neighbors[i]))
                {
                    var node = new StateTreeNode(Solver)
                    {
                        Depth = wi,
                        Parent = this,
                        Value = Solver.WhiteOnes[wi].Neighbors[i]
                    };
                    node.UsedBlackTiles.Add(node.Value);
                    node.UsedBlackTiles.AddRange(UsedBlackTiles);
                    Children.Add(node);
                }
        }

        // returns true if finds white isolated tiles
        public bool WhiteKill()
        {
            if (Depth == -1)
                return false;
            for (int i = Depth + 1, cnt = Solver.WhiteOnes.Count; i < cnt; i++)
            {
                var wt = Solver.WhiteOnes[i];
                int q = 0;
                for (int j = 0; j < wt.Neighbors.Count; j++)
                    if (UsedBlackTiles.Contains(wt.Neighbors[j]))
                        q++;
                //Debug.WriteLine($"{q} {wt.Neighbors.Count} {wt.ToString()}");
                if (q > 0 && q == wt.Neighbors.Count)
                {
                    //Application.Current.Dispatcher.Invoke(() => MessageBox.Show(wt.ToString()));
                    return true;
                }
            }
            return false;
        }

        public void BuildSolution()
        {
            Solver.PartialSol.Clear();
            var node = this;
            while (node.Depth != -1)
            {
                Solver.PartialSol.Add(new Domi(Solver.WhiteOnes[node.Depth], node.Value));
                node = node.Parent;
            }
        }
    }
 }
