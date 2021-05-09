using NotImplementedLab.Math;
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

            /*Debug.WriteLine("Free Vars:");
            Debug.WriteLine(string.Join(" ", FreeVars.Select(t => t.Id + 1)));
            foreach (var v in ConstrianedVars)
            {
                //System.Windows.MessageBox.Show(v.ExprString());
                Debug.WriteLine(v.ExprString());
            }
            foreach(var fv in FreeVars)
            {
                fv.Value = 0;
            }*/           
        }

        Random rnd = new Random();

        public void BuildSolution()
        {
            bool ok = true;
            PartialSol.Clear();           

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
                }
                else if (vList[i].Value != 0 && vList[i].Value != 1) 
                {
                    ok = false;
                    break;
                }                               
            }           
            if(ok)
            {
                IsReady = true;
            }

        }

        public override void NextStep()
        {
            BuildSolution();
            //Debug.WriteLine(string.Join(" ", FreeVars.Select(v => v.Value)));
            int i = FreeVars.Count - 1;
            while (i >= 0 && FreeVars[i].Value == 1) 
            {
                FreeVars[i].Value = 0;                
                i--;
            }
            if(IsReady)
            {
                return;
            }
            if (i >= 0) 
            {
                FreeVars[i].Value = 1;
            }
            else
            {
                IsReady = true;                
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
