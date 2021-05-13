using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Domino.Tiling
{
    public class Model
    {
        public Tile[,] Board;
        public int Dim;
        public Model(int n)
        {
            Dim = n;
            Board = new Tile[Dim, Dim];
            for (int r = 0; r < Dim; r++) 
                for (int c = 0; c < Dim; c++)
                {
                    Board[r, c] = new Tile(r, c, r * Dim + c);
                }
            RefreshEdges();

        }

        public void RefreshEdges()
        {
            for (int r = 0; r < Dim; r++)
            {
                for (int c = 0; c < Dim; c++)
                {
                    Board[r, c].Neighbors.Clear();
                    if (Board[r, c].IsHole) continue;
                    if (r > 0 && !Board[r - 1, c].IsHole) Board[r, c].Neighbors.Add(Board[r - 1, c]);
                    if (c > 0 && !Board[r, c - 1].IsHole) Board[r, c].Neighbors.Add(Board[r, c - 1]);
                    if (r < Dim - 1 && !Board[r + 1, c].IsHole) Board[r, c].Neighbors.Add(Board[r + 1, c]);
                    if (c < Dim - 1 && !Board[r, c + 1].IsHole) Board[r, c].Neighbors.Add(Board[r, c + 1]);
                }
            }
        }

        public List<Domi> CoverWithDominos()
        {
            return new List<Domi>
            {
                new Domi(0,0,Orientation.Horizontal),
                new Domi(1,0,Orientation.Vertical),
                new Domi(1,1,Orientation.Vertical),
            };
        }

        bool CheckNumberOfTiles()
        {
            int w = 0, b = 0;
            for (int r = 0; r < Dim; r++)
                for (int c = 0; c < Dim; c++)
                    if (!Board[r, c].IsHole)
                    {
                        if (Board[r, c].IsWhite)
                            w++;
                        else
                            b++;
                    }
            return w == b;
        }

        public void Lee(int r, int c, ref int b, ref int w)
        {
            if (r < 0 || c < 0 || r >= Dim || c >= Dim) return;
            if (Board[r, c].IsHole) 
                return;
            if (Board[r, c].Tag != 0)
                return;
            Board[r, c].Tag = 1;
            if (Board[r, c].IsWhite)
                w++;
            else b++;
            Lee(r - 1, c, ref b, ref w);
            Lee(r + 1, c, ref b, ref w);
            Lee(r, c - 1, ref b, ref w);
            Lee(r, c + 1, ref b, ref w);
        }

        public bool CheckRegions()
        {
            for (int r = 0; r < Dim; r++)
            {
                for (int c = 0; c < Dim; c++)
                    Board[r, c].Tag = 0;
            }

            int b = 0, w = 0;
            for (int r = 0; r < Dim; r++)
            {
                for (int c = 0; c < Dim; c++)
                {
                    if(Board[r,c].Tag==0)
                    {
                        b = w = 0;
                        Lee(r, c, ref b, ref w);
                        if (b != w)
                            return false;
                    }
                }
            }
            return true;
        }


        public bool IsFeasible()
        {
            return CheckNumberOfTiles() && CheckRegions();
        }
        
    }

    public class Tile
    {
        public Tile(int row, int col, int id, bool isHole = false)
        {
            Row = row;
            Col = col;
            Id = id;
            IsWhite = (row + col) % 2 == 0;
            IsHole = isHole;
        }

        public int Id { get; }
        public bool IsHole { get;  set; }
        public int Row { get; }
        public int Col { get; }
        public bool IsWhite { get; }

        public int Tag { get; set; } = 0;  // may be used as "Visited?"

        public List<Tile> Neighbors = new List<Tile>(4); // tile acts like a node

        public new string ToString() => $"({Row},{Col},{(IsHole ? "Hole" : IsWhite ? "White" : "Black")})";
    }    
}
