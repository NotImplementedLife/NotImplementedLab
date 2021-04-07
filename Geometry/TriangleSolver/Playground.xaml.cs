using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Geometry.TriangleSolver
{
    /// <summary>
    /// Interaction logic for Playground.xaml
    /// </summary>
    public partial class Playground : UserControl
    {
        public Playground()
        {
            InitializeComponent();
            PointA = new NotImplementedLab.Data.GeometryEntities.Point() { X = 400, Y = 30, Label = "A" };
            PointB = new NotImplementedLab.Data.GeometryEntities.Point() { X = 150, Y = 400, Label = "B" };
            PointC = new NotImplementedLab.Data.GeometryEntities.Point() { X = 650, Y = 400, Label = "C" };
            Canvas.Entities.Add(PointA);
            Canvas.Entities.Add(PointB);
            Canvas.Entities.Add(PointC);
            Canvas.Entities.Add(new NotImplementedLab.Data.GeometryEntities.Segment(PointA, PointB));
            Canvas.Entities.Add(new NotImplementedLab.Data.GeometryEntities.Segment(PointB, PointC));
            Canvas.Entities.Add(new NotImplementedLab.Data.GeometryEntities.Segment(PointC, PointA));
        }

        NotImplementedLab.Data.GeometryEntities.Point PointA, PointB, PointC;

        private void Compute_Click(object sender, RoutedEventArgs e)
        {
            int angles = (InputA.Text != "" ? 1 : 0) + (InputB.Text != "" ? 1 : 0) + (InputC.Text != "" ? 1 : 0);
            int sides = (Inputa.Text != "" ? 1 : 0) + (Inputb.Text != "" ? 1 : 0) + (Inputc.Text != "" ? 1 : 0);            
            if(angles+sides!=3)
            {
                (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Three values must be provided in order to solve the triangle");
                return; 
            }
            if(angles==3)
            {
                (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("At least one side must be provided in order to solve the triangle");
                return;
            }
            //MessageBox.Show("" + sides + " " + angles);
            double a = 0, b = 0, c = 0, A = 0, B = 0, C = 0;
            if (sides == 3)
            {
                if (!(double.TryParse(Inputa.Text, out a) && double.TryParse(Inputb.Text, out b) && double.TryParse(Inputc.Text, out c)))
                {
                    (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Incorrect side values");
                    return;
                }
                if (a < 0 || b < 0 || c < 0)
                {
                    (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Side lengths cannot be negative");
                    return;
                }
                if (a == 0 || b == 0 || c == 0)
                {
                    (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Side lengths cannot be 0");
                    return;
                }
                if (a + b <= c || b + c <= a || c + a <= b)
                {
                    (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Given side lengths could not form a triangle");
                    return;
                }
                A = Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * 180 / Math.PI;
                B = Math.Acos((a * a + c * c - b * b) / (2 * a * c)) * 180 / Math.PI;
                C = Math.Acos((a * a + b * b - c * c) / (2 * a * b)) * 180 / Math.PI;
            }
            else if (sides == 2)
            {
                if (Inputa.Text == "")
                {
                    if (InputA.Text != "")
                    {
                        if (!(double.TryParse(Inputb.Text, out b) && double.TryParse(Inputc.Text, out c) && double.TryParse(InputA.Text, out A)))
                        {
                            (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                            return;
                        }
                        if (!Check2Sides(b, c, A)) return;
                        Solve2Sides(b, c, out a, out B, out C, A);
                    }
                    else if (InputB.Text != "")
                    {
                        if (!(double.TryParse(Inputb.Text, out b) && double.TryParse(Inputc.Text, out c) && double.TryParse(InputB.Text, out B)))
                        {
                            (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                            return;
                        }
                        if (!Check2Sides(b, c, B)) return;
                        Solve2Sides(b, c, out a, B, out C, out A);
                    }
                    else // if(InputC.Text!="")
                    {
                        if (!(double.TryParse(Inputb.Text, out b) && double.TryParse(Inputc.Text, out c) && double.TryParse(InputC.Text, out C)))
                        {
                            (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                            return;
                        }
                        if (!Check2Sides(b, c, C)) return;
                        Solve2Sides(c, b, out a, C, out B, out A);
                    }
                }
                else if (Inputb.Text == "")
                {
                    if (InputA.Text != "")
                    {
                        if (!(double.TryParse(Inputa.Text, out a) && double.TryParse(Inputc.Text, out c) && double.TryParse(InputA.Text, out A)))
                        {
                            (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                            return;
                        }
                        if (!Check2Sides(a, c, A)) return;
                        Solve2Sides(a, c, out b, A, out C, out B);
                    }
                    else if (InputB.Text != "")
                    {
                        if (!(double.TryParse(Inputa.Text, out a) && double.TryParse(Inputc.Text, out c) && double.TryParse(InputB.Text, out B)))
                        {
                            (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                            return;
                        }
                        if (!Check2Sides(a, c, B)) return;
                        Solve2Sides(a, c, out b, out A, out C, B);
                    }
                    else // if(InputC.Text!="")
                    {
                        if (!(double.TryParse(Inputa.Text, out a) && double.TryParse(Inputc.Text, out c) && double.TryParse(InputC.Text, out C)))
                        {
                            (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                            return;
                        }
                        if (!Check2Sides(a, c, C)) return;
                        Solve2Sides(c, a, out b, C, out A, out B);
                    }
                }
                else if (Inputc.Text == "")
                {
                    if (InputA.Text != "")
                    {
                        if (!(double.TryParse(Inputa.Text, out a) && double.TryParse(Inputb.Text, out b) && double.TryParse(InputA.Text, out A)))
                        {
                            (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                            return;
                        }
                        if (!Check2Sides(a, b, A)) return;
                        Solve2Sides(a, b, out c, A, out B, out C);
                    }
                    else if (InputB.Text != "")
                    {
                        if (!(double.TryParse(Inputa.Text, out a) && double.TryParse(Inputb.Text, out b) && double.TryParse(InputB.Text, out B)))
                        {
                            (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                            return;
                        }
                        if (!Check2Sides(a, b, B)) return;
                        Solve2Sides(b, a, out c, B, out A, out C);
                    }
                    else // if(InputC.Text!="")
                    {
                        if (!(double.TryParse(Inputa.Text, out a) && double.TryParse(Inputb.Text, out b) && double.TryParse(InputC.Text, out C)))
                        {
                            (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                            return;
                        }
                        MessageBox.Show("Here");
                        if (!Check2Sides(a, b, C)) return;
                        Solve2Sides(a, b, out c, out A, out B, C);
                    }
                }
            }
            else if (sides == 1)
            {
                bool haveA = double.TryParse(InputA.Text, out A);
                bool haveB = double.TryParse(InputB.Text, out B);
                bool haveC = double.TryParse(InputC.Text, out C);
                if((haveA?1:0)+(haveB?1:0)+(haveC?1:0)!=2)
                {
                    (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                    return;
                }
                if (!haveA) A = 180 - B - C;
                else if (!haveB) B = 180 - A - C;
                else if (!haveC) C = 180 - A - B;

                if(A<=0 || B<=0 || C<=0)
                {
                    (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Sum of angle measures exceeds 180 degrees.");
                    return;
                }

                bool havea = double.TryParse(Inputa.Text, out a);
                bool haveb = double.TryParse(Inputb.Text, out b);
                bool havec = double.TryParse(Inputc.Text, out c);
                if ((havea ? 1 : 0) + (haveb ? 1 : 0) + (havec ? 1 : 0) != 1) 
                {
                    (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Parsing errors. Please check the input again");
                    return;
                }

                // apply sine theorem
                if(havea)
                {
                    double _2R = a / Math.Sin(rad(A));
                    b = _2R * Math.Sin(rad(B));
                    c = _2R * Math.Sin(rad(C));
                }
                else if(haveb)
                {
                    double _2R = b / Math.Sin(rad(B));
                    a = _2R * Math.Sin(rad(A));
                    c = _2R * Math.Sin(rad(C));
                }
                else if (havec)
                {
                    double _2R = c / Math.Sin(rad(C));
                    a = _2R * Math.Sin(rad(A));
                    b = _2R * Math.Sin(rad(B));
                }
            }


            Inputa.Text = a.ToString();
            Inputb.Text = b.ToString();
            Inputc.Text = c.ToString();
            InputA.Text = A.ToString();
            InputB.Text = B.ToString();
            InputC.Text = C.ToString();
            RenderTriangle(B, C);
        }

        private bool Check2Sides(double a,double b, double A)
        {
            if(a<0 || b<0)
            {
                (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Side lengths cannot be negative");
                return false;
            }

            if (a == 0 || b == 0)
            {
                (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Side lengths cannot be 0");
                return false;
            }
            if(A<=0 || A>=180)
            {
                (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Inocrrect angle measure. Must be between 0 and 180");
                return false;
            }
            return true;
        }

        // know 2 sides and the angle between
        private void Solve2Sides(double a, double b, out double c, out double A, out double B, double C)
        {
            c = Math.Sqrt(a * a + b * b - 2 * a * b * Math.Cos(rad(C)));
            A = Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * 180 / Math.PI;
            B = 180 - A - C;            
        }

        // know 2 sides and the angle opposite to A
        private void Solve2Sides(double a, double b, out double c, double A, out double B, out double C)
        {
            c = Math.Sqrt(a * a - b * b + 2 * a * b * Math.Cos(rad(A)));
            double sin = Math.Sin(rad(A));
            double d = Math.Sqrt(a * a - b * b * sin * sin);
            c = b * Math.Cos(rad(A)); 
            if(d<c)
            {
                (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseWarning("There are two triangles with these measures. One of them will be shown");
            }
            c += d;
            B = Math.Acos((a * a + c * c - b * b) / (2 * a * c)) * 180 / Math.PI;
            C = 180 - A - B;
        }

        private double rad(double a) => a * Math.PI / 180;

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InputA.Text = ""; InputA.Foreground = Brushes.Black;
            InputB.Text = ""; InputB.Foreground = Brushes.Black;
            InputC.Text = ""; InputC.Foreground = Brushes.Black;
            Inputa.Text = ""; Inputa.Foreground = Brushes.Black;
            Inputb.Text = ""; Inputb.Foreground = Brushes.Black;
            Inputc.Text = ""; Inputc.Foreground = Brushes.Black;
        }

        private void RenderTriangle(double B, double C)
        {
            PointB.X = 150;
            PointC.X = 650;
            PointB.Y = PointC.Y = 400;            
            double tgB = Math.Tan(rad(B));
            double tgC = Math.Tan(rad(C));
            double xa = (150 * tgB + 650 * tgC) / (tgB + tgC);            
            double ya = 400 - (xa - 150) * tgB;          

            PointA.X = xa;
            PointA.Y = ya;
            
            if(ya<0 || xa>0 || xa>800)
            {
                FitTriangle();
            }
        }

        private void FitTriangle()
        {
            double Gx = (PointA.X + PointB.X + PointC.X) / 3;
            double Gy = (PointA.Y + PointB.Y + PointC.Y) / 3;
            double fx = 400 / (PointA.X - PointB.X);
            double fy = 400 / (PointB.Y - PointA.Y);
            double f = Math.Min(fy, fx);
            PointA.X = f * PointA.X + (1 - f) * Gx;
            PointA.Y = f * PointA.Y + (1 - f) * Gy;

            PointB.X = f * PointB.X + (1 - f) * Gx;
            PointB.Y = f * PointB.Y + (1 - f) * Gy;

            PointC.X = f * PointC.X + (1 - f) * Gx;
            PointC.Y = f * PointC.Y + (1 - f) * Gy;

            if (f == fx)
            {
                double tX = Math.Min(PointA.X, PointB.X) - 50;
                PointA.X -= tX;
                PointB.X -= tX;
                PointC.X -= tX;
            }
            else if(f==fy)
            {
                double tY = PointA.Y - 50;
                PointA.Y -= tY;
                PointB.Y -= tY;
                PointC.Y -= tY;
            }

            //MessageBox.Show(tX.ToString());

        }        

    }


}
