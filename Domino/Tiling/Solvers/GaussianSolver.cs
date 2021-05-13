﻿using NotImplementedLab.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino.Tiling.Solvers
{       
    public class GausssianSolver : Solver
    {
        public override string Name { get; } = "Gaussian";

        public GausssianSolver(Model model) : base(model, 20) { }

        Dictionary<Tuple<int, int>, Variable> Vars = new Dictionary<Tuple<int, int>, Variable>();
        List<Constraint> Constraints = new List<Constraint>();

        List<Variable> FreeVars = new List<Variable>();
        List<Variable> ConstrianedVars = new List<Variable>();

        Tuple<int, int> GetTuple(int i, int j)
        {
            return i < j ? new Tuple<int, int>(i, j) : new Tuple<int, int>(j, i);
        }

        IntMatrix Matrix;       

        public override void Init()
        {
            IsReady = false;
            Vars.Clear();
            Constraints.Clear();
            int nTiles = 0;

            // Generate tile constraints
            for (int r = 0; r < Model.Dim; r++)
            {
                for (int c = 0; c < Model.Dim; c++)
                {
                    var tile = Model.Board[r, c];
                    if (!tile.IsHole) 
                    {
                        nTiles++;
                        var constraint = new Constraint { Target = 1 };
                        foreach (var n in tile.Neighbors)
                        {
                            var tuple = GetTuple(tile.Id, n.Id);
                            if (Vars.ContainsKey(tuple)) 
                            {
                                constraint.VarIds.Add(Vars[tuple].Id);
                            }
                            else
                            {
                                var id = Vars.Count;
                                Vars[tuple] = new Variable(id);
                                Vars[tuple].Domino = new Domi(tile, n);
                                constraint.VarIds.Add(id);                                
                            }
                        }
                        constraint.Shrink();
                        Constraints.Add(constraint);
                    }
                }
            }

            // Generate global constraint
            var globalConstraint = new Constraint { Target = nTiles / 2 };
            foreach(var v in Vars)
            {
                globalConstraint.VarIds.Add(v.Value.Id);
            }
            globalConstraint.Shrink();
            Constraints.Add(globalConstraint);

            Debug.WriteLine($"# Constraints: {Constraints.Count}");

            // create matrix

            Matrix = new IntMatrix(Constraints.Count, Vars.Count + 1);

            for (int i = 0; i < Matrix.RowsCount; i++) 
            {
                for (int j = 0, cnt = Constraints[i].VarIds.Count; j < cnt; j++)
                {
                    Matrix[i, Constraints[i].VarIds[j]] = 1;
                }
                Matrix[i, Vars.Count] = Constraints[i].Target;
            }

            try
            {
                Matrix.PerformGaussianElimination();
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }            

            /*bool ok = false;
            for (int i = 0; i < Matrix.RowsCount && !ok; i++) 
            {
                for (int j = 0; j < Matrix.ColsCount && !ok; j++) 
                {
                    if (Math.Abs(Matrix[i, j]) >= 2)
                    {
                        //System.Windows.MessageBox.Show($"{i} {j} {Matrix[i, j]}");
                        ok = true;
                    }                            
                }
            }*/

            //System.IO.File.WriteAllText("matrix.log", Matrix.ToString());

            FreeVars.Clear();
            // express variables            

            var vList = Vars.Select(e => e.Value).ToList();            
            int pivot = 0;
            for (int i = 0; i < Matrix.RowsCount; i++, pivot++)
            {
                while (pivot < Matrix.ColsCount - 1 && Matrix[i, pivot] == 0)
                {
                    vList[pivot].IsFree = true;
                    FreeVars.Add(vList[pivot]);
                    pivot++;
                }
                if (pivot >= Matrix.ColsCount - 1) break;
            }


            ConstrianedVars.Clear();            
            for (int i = 0; i < Matrix.RowsCount; i++)
            {
                int j = 0;
                while (j<Matrix.ColsCount-1 && Matrix[i, j] == 0) j++;
                if (j == Matrix.ColsCount - 1)
                    continue;                               
                //Debug.WriteLine($"{i} {j}");
                ConstrianedVars.Add(vList[j]);
                vList[j].Expr = new List<SumTerm>();
                vList[j].Amplifier = Matrix[i, j];                

                vList[j].Expr.Add(new SumTerm(Matrix[i, Matrix.ColsCount - 1], null));

                for (int k = j + 1; k < Matrix.ColsCount - 1; k++)  
                {
                    if (Matrix[i, k] != 0)
                    {
                        vList[j].Expr.Add(new SumTerm(-Matrix[i, k], vList[k]));
                    }
                }                
            }            

            foreach(var fv in FreeVars)
            {
                fv.Value = 0;
            }

            for (int i = 0, cnt = vList.Count; i < cnt; i++) 
            {
                if (!vList[i].IsFree) 
                {
                    var e = vList[i].Expr;                    
                    if(e[0].Value==0) // free term is 0
                    {
                        //Debug.WriteLine($"$$$$$$$ {vList[i].ExprString()}");
                        bool ok = true;
                        for (int j = 1; j < e.Count && ok; j++)
                            if (e[j].Coefficient >= 0) 
                            {
                                //Debug.WriteLine($"Here {e[j].Value}");
                                ok = false;
                            }
                        if (ok)  // if sum(-xi)=0
                        {
                            for (int j = 1; j < e.Count; j++)
                            {
                                e[j].Var.IsFixed = true;  // variables are fixed to 0
                                //Debug.WriteLine($"Fixed {e[j].Var.Id}");
                            }
                        }
                    }                  
                }
            }

            var L = new List<Variable>();
            L.AddRange(FreeVars);           
            L = L.Where((t) => !t.IsFixed).ToList();
            FreeVars.Clear();
            FreeVars.AddRange(L);

            //Debug.WriteLine(Matrix.ToString());           

            Debug.WriteLine($"_______{FreeVars.Count}");
            Debug.WriteLine("Free Vars:");
            Debug.WriteLine(string.Join(" ", FreeVars.Select(t => t.Id + 1)));
            foreach (var v in ConstrianedVars) 
            {
                //System.Windows.MessageBox.Show(v.ExprString());
                Debug.WriteLine(v.ExprString());
            }
        }

        Random rnd = new Random();

        public void BuildSolution()
        {
            bool ok = true;
            PartialSol.Clear();
            var countDomis = 0;

            var vList = Vars.Select(e => e.Value).ToList();
            for (int i = 0, cnt = vList.Count; i < cnt; i++) 
            {
                if (!vList[i].IsFree)
                {
                    vList[i].Value = vList[i].GetExprValue() / vList[i].Amplifier;
                }
                if (vList[i].Value == 1)
                {
                    PartialSol.Add(vList[i].Domino);
                    countDomis++;
                }
                else if (vList[i].Value != 0 && vList[i].Value != 1) 
                {
                    ok = false;
                    break;
                }                               
            }
            if (countDomis != (Constraints.Count - 1) / 2) 
            {
                ok = false;
            }
            if(ok)
            {
                IsReady = true;
            }
        }

        private bool vListNext(List<Variable> vars)
        {
            int i = vars.Count - 1;
            while (i >= 0 && vars[i].Value == 1) 
            {
                vars[i].Value = 0;
                i--;
            }
            if (i >= 0)
            {
                vars[i].Value = 1;
                return true;
            }
            else return false;
        }

        public override void NextStep()
        {
            if (IsReady) return;
            BuildSolution();
            if (!vListNext(FreeVars)) 
            {                
                IsReady = true; 
                return;
            }
            if (IsReady)
            {
                return;
            }
            //IsReady = true;
        }

        public class Variable
        {
            public Variable(int id)
            {
                Id = id;
            }

            public Domi Domino;

            public int Id;
            public int Value = 0;

            // fields set after gaussian elimination
            public bool IsFree = false;
            public bool IsFixed = false;
            public List<SumTerm> Expr = null;
            public int Amplifier = 1;
            public int GetExprValue() => Expr.Sum(t => t.Value);            

            public string ExprString()
            {
                return new SumTerm(Amplifier, this).ToString() + " = " + string.Join(" ", Expr.Select(t => t.ToString()));
            }           
        }

        /// <summary>
        /// Constraint : 
        /// vars[i0]+vars[i1]+...+vars[in]=Target
        /// </summary>
        public class Constraint
        {
            public List<int> VarIds = new List<int>();
            public int Target;

            public void Shrink()
            {
                VarIds.Sort();
            }            
        }        


        public class SumTerm
        {
            public SumTerm(int coeff, Variable var)
            {
                Coefficient = coeff;
                Var = var;
            }

            public Variable Var = null; // Var=null = free term
            public int Coefficient = 0;

            public int Value { get => Var == null ? Coefficient : Coefficient * Var.Value; }

            public override string ToString()
            {
                if(Var==null)
                {
                    return $"{(Math.Sign(Coefficient) >= 0 ? "+" : "-")}{ Math.Abs(Coefficient)}";
                }
                var acf = Math.Abs(Coefficient);
                return $"{(Math.Sign(Coefficient) >= 0 ? "+" : "-")}{(acf == 1 ? "" : acf.ToString())}{"x" + (Var.Id + 1).ToString()}";
            }
        }
    }
}
