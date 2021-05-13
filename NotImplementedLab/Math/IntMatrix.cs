using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace NotImplementedLab.Math
{
    using System.Diagnostics;
    using static Arithmetics;
    public class IntMatrix
    {
        private int[,] Data { get; }
        
        public IntMatrix(int n, int m)
        {
            Data = new int[n, m];
        }

        public int RowsCount { get => Data == null ? 0 : Data.GetLength(0); }
        public int ColsCount { get => Data == null ? 0 : Data.GetLength(1); }

        public int this[int i,int j]
        {
            get => Data[i, j];
            set => Data[i, j] = value;
        }

        // R0*=f
        private void MulRow(int r0, int f)
        {
            for (int j = 0; j < ColsCount; j++)            
                Data[r0, j] *= f;           
        }

        // R0 += f*R
        private void AddRowTo(int r0, int r, int f = 1)
        {
            for (int j = 0; j < ColsCount; j++)
                Data[r0, j] += f * Data[r, j];            
        }

        private void SwapRows(int r1,int r2)
        {
            for (int j = 0; j < ColsCount; j++)
            {
                int aux = Data[r1, j];
                Data[r1, j] = Data[r2, j];
                Data[r2, j] = aux;
            }
        }

        private void SwapCols(int c1,int c2)
        {
            for(int i=0;i<RowsCount;i++)
            {
                int aux = Data[i, c1];
                Data[i, c1] = Data[i, c2];
                Data[i, c2] = aux;
            }
        }

        public void PerformGaussianElimination()
        {
            int p = 0;
            for (int i = 0; i < RowsCount; i++)
            {
                if (Data[i, p] == 0) 
                {
                    //bool ok = false;
                    int r = i, n0 = ColsCount;

                    for (int t = i; t < RowsCount; t++) 
                    {
                        int j = p;
                        for (; j < ColsCount && Data[t, j] == 0; j++);
                        if (j < n0)
                        {
                            r = t;
                            n0 = j;
                        }
                    }
                    SwapRows(i, r);

                    while (p < ColsCount && Data[i, p] == 0) p++;
                    if (p == ColsCount) return;

                }
                if (Data[i, p] < 0)
                    MulRow(i, -1);
                for (int k = 0; k < RowsCount; k++) 
                {
                    if (k != i)
                    {
                        var d = Abs(GCD(Data[i, p], Data[k, p]));                        
                        MulRow(k, Abs(Data[i, p] / d));
                        var f = Data[k, p] / Data[i, p];
                        for (int j = p; j < ColsCount; j++)
                            Data[k, j] -= f * Data[i, j];
                    }
                }
                p++;
                if (p == ColsCount) return;                
            }
        }

        public new string ToString()
        {
            var s = "";
            for (int i = 0; i < RowsCount; i++) 
            {
                for (int j = 0; j < ColsCount; j++) 
                {
                    if (j > 0) s += " ";
                    s += Data[i, j].ToString().PadLeft(6);
                }
                s += "\n";
            }
            return s;
        }
    }
}
