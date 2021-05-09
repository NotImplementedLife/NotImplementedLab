using System;
using System.Windows.Controls;

namespace Domino.Tiling
{
    public class Domi
    {
        public Domi(int r, int c, Orientation o)
        {
            Row = r;
            Col = c;
            Orientation = o;
        }

        public Domi(Tile t1, Tile t2)
        {
            if (Math.Abs(t1.Row - t2.Row) + Math.Abs(t1.Col - t2.Col) != 1)
                throw new ArgumentException("No domino can cover these two tiles.");
            if (t1.Row == t2.Row)
            {
                Orientation = Orientation.Horizontal;
                Row = t1.Row;
                Col = Math.Min(t1.Col, t2.Col);
            }
            else if (t1.Col == t2.Col)
            {
                Orientation = Orientation.Vertical;
                Col = t1.Col;
                Row = Math.Min(t1.Row, t2.Row);
            }
        }

        public int Row { get; private set; }
        public int Col { get; private set; }
        public Orientation Orientation { get; private set; }
    }
}
