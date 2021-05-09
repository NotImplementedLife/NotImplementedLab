using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Domino.Tiling.Solvers
{
    public class CPSolver : Solver
    {
        public override string Name { get; } = "CP";
        public CPSolver(Model model) : base(model, 8) { }

        public List<Tile> WhiteOnes = new List<Tile>();        

        public override void Init()
        {
            WhiteOnes.Clear();
            for (int r = 0; r < Model.Dim; r++)
                for (int c = 0; c < Model.Dim; c++)
                    if (!Model.Board[r, c].IsHole && Model.Board[r, c].IsWhite) 
                    {
                        WhiteOnes.Add(Model.Board[r, c]); 
                    }
            WhiteOnes.Sort((a, b) => a.Neighbors.Count - b.Neighbors.Count);
            Root = new StateTreeNode(this);
            Node = Root;
            IsReady = false;
        }
        public override void NextStep()
        {
            if (Node == null || Node.Depth == WhiteOnes.Count - 1) 
            {
                IsReady = true;
                return;
            }
            Node.Expand();
            if (Node.Children.Count > 0 && !Node.WhiteKill()) 
            {
                Node = Node.Children[0];
            }
            else
            {
                while (Node != null && Node.NextSibling() == null) 
                {
                    Node = Node.Parent;
                }
                if (Node != null)
                    Node = Node.NextSibling();
            }
            Node.BuildSolution();
        }

        private StateTreeNode Root;
        StateTreeNode Node;
    }       
}
