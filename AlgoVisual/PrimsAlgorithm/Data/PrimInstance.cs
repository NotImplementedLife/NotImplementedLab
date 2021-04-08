using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoVisual.PrimsAlgorithm.Data
{
    public class PrimInstance
    {
        static Random rand = new Random();
        Graph Data;
        int[,] C = new int[18, 30];
        Edge[,] E = new Edge[18, 30];
        
        List<Cell> Q = new List<Cell>();

        public PrimInstance(Graph g)
        {
            Data = g;
            Init();            
        }

        public void Init()
        {
            Q.Clear();
            for (int r = 0; r < 18; r++)
            {
                for (int c = 0; c < 30; c++)
                {
                    C[r, c] = int.MaxValue;
                    E[r, c] = null;
                    Q.Add(Data[r, c]);
                }
            }

            C[rand.Next() % 18, rand.Next() % 30] = 0;
            for (int i = 0; i < Data.Edges.Count; i++) 
                Data.Edges[i].Highlighted = false;
        }

        public void ExecuteStep()
        {
            int minInd = 0, minC = int.MaxValue;
            
            for(int i=0;i<Q.Count;i++)
            {
                int c = C[Q[i].Row, Q[i].Col];
                if (c < minC)
                {
                    minC = c;
                    minInd = i;
                }
            }           

            Cell v = Q[minInd];
            Q.RemoveAt(minInd);

            if(E[v.Row,v.Col]!=null)
            {
                E[v.Row, v.Col].Highlighted = true;
            }

            foreach (var e in Data.Edges.Where(e => e.C1 == v || e.C2 == v)) 
            {
                var w = (e.C1 == v) ? e.C2 : e.C1;
                C[w.Row, w.Col] = e.Cost;
                E[w.Row, w.Col] = e;
            }
        }

        public bool IsReady() => Q.Count == 0;
    }
}
