using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AlgoVisual.LeesAlgorithm
{
    /// <summary>
    /// Interaction logic for Playground.xaml
    /// </summary>
    public partial class Playground : UserControl
    {
        public Playground()
        {
            InitializeComponent();
            Image.Source = rtb;
            ResetData();
            Q.Clear();
            Q.Enqueue(new Tuple<int, int>(StartR, StartC));
        }

        RenderTargetBitmap rtb = new RenderTargetBitmap(800, 480, 96, 96, PixelFormats.Pbgra32);
        DrawingVisual visual = new DrawingVisual();        

        Rectangle WhiteSquare = new Rectangle { Width = 25, Height = 25, Fill = Brushes.White };
        Rectangle GraySquare = new Rectangle { Width = 25, Height = 25, Fill = Brushes.LightGray };
        Rectangle BlackSquare = new Rectangle { Width = 25, Height = 25, Fill = Brushes.Black };
        Rectangle YellowSquare = new Rectangle { Width = 25, Height = 25, Fill = Brushes.Yellow };
        Rectangle BlueSquare = new Rectangle { Width = 25, Height = 25, Fill = Brushes.DodgerBlue };

        Ellipse StartPoint = new Ellipse { Width = 25, Height = 25, Fill = Brushes.Green };
        Ellipse EndPoint = new Ellipse { Width = 25, Height = 25, Fill = Brushes.Red };

        int[,] Data = new int[18, 30];

        void Render()
        {
            VisualBrush visualBrush;
            Size size = new Size(25, 25);
            using (DrawingContext ctx = visual.RenderOpen())
            {
                for (int r = 0; r < 18; r++)
                    for (int c = 0; c < 30; c++)
                    {
                        Rectangle square = null;
                        switch (Data[r, c])
                        {
                            case 0: square = (r + c) % 2 == 0 ? WhiteSquare : GraySquare; break;
                            case -1: square = BlackSquare; break;
                            case 2021: square = BlueSquare; break;
                            default: square = YellowSquare; break;
                        }                        

                        square.Measure(size);
                        square.Arrange(new Rect(size));

                        visualBrush = new VisualBrush(square);
                        ctx.DrawRectangle(visualBrush, null,
                          new Rect(new Point(25 + 25 * c, 15 + 25 * r), size));
                    }
                StartPoint.Measure(size);
                StartPoint.Arrange(new Rect(size));
                visualBrush = new VisualBrush(StartPoint);
                ctx.DrawRectangle(visualBrush, null,
                  new Rect(new Point(25 + 25 * StartC, 15 + 25 * StartR), size));

                EndPoint.Measure(size);
                EndPoint.Arrange(new Rect(size));
                visualBrush = new VisualBrush(EndPoint);
                ctx.DrawRectangle(visualBrush, null,
                  new Rect(new Point(25 + 25 * EndC, 15 + 25 * EndR), size));
            }
            rtb.Render(visual);
        }


        void ResetData()
        {
            Random rand = new Random();
            for (int r = 0; r < 18; r++)
                for (int c = 0; c < 30; c++)
                    Data[r, c] = 0;
            Render();
        }

        int action = 1;

        bool msDown = false;
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LeeRunning)
                return;
            msDown = true;
            int c = (int)((e.GetPosition(Image).X - 25) / 25);
            int r = (int)((e.GetPosition(Image).Y - 15) / 25);
            if (r < 0 || r > 17 || c < 0 || c > 29) return;

            if (r == StartR && c == StartC)
            {
                action = 2;
                ActionButton.Content = "Start";
            }
            else if (r == EndR && c == EndC)
            {
                action = 3;
                ActionButton.Content = "End";
            }


            if (action == 1) 
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    Data[r, c] = -1;
                else if (e.RightButton == MouseButtonState.Pressed)
                    Data[r, c] = 0;
            }            
            else if(action==2)
            {
                StartR = r;
                StartC = c;
                Q.Clear();
                Q.Enqueue(new Tuple<int, int>(StartR, StartC));
            }
            else if(action==3)
            {
                EndR = r;
                EndC = c;
            }
            Render();
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (!msDown) return;
            int c = (int)((e.GetPosition(Image).X - 25) / 25);
            int r = (int)((e.GetPosition(Image).Y - 15) / 25);
            if (r < 0 || r > 17 || c < 0 || c > 29) return;
            
            if (action == 1) 
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    Data[r, c] = -1;
                else if (e.RightButton == MouseButtonState.Pressed)
                    Data[r, c] = 0;
            }
            else if (action == 2)
            {
                StartR = r;
                StartC = c;
                Q.Clear();
                Q.Enqueue(new Tuple<int, int>(StartR, StartC));
            }
            else if (action == 3)
            {
                EndR = r;
                EndC = c;
            }
            Render();
        }

        int StartR = 0, StartC = 0;
        int EndR = 17, EndC = 29;

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            msDown = false;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            msDown = false;
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if(action==1)
            {
                action = 2;
                ActionButton.Content = "Start";
            }
            else if(action==2)
            {
                action = 3;
                ActionButton.Content = "End";

            }
            else if(action==3)
            {
                action = 1;
                ActionButton.Content = "Barrier";
            }
        }

        Queue<Tuple<int, int>> Q = new Queue<Tuple<int, int>>();

        static int[] dr = { -1, 0, 1, 0 };

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            NextStep();
        }

        static int[] dc = { 0, -1, 0, 1 };

        bool Solved = false;
        bool IsReady = false;

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            if (!LeeRunning)
            {                
                if(StartR==EndR && StartC==EndC)
                {
                    (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Start and end position cannot be the same");
                    return;
                }
                if (Data[StartR, StartC] == -1 || Data[EndR, EndC] == -1)
                {
                    (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Start and end positions must be free");
                    return;
                }
                Q.Clear();
                Q.Enqueue(new Tuple<int, int>(StartR, StartC));                
                Task.Run(new Action(RunLee));
            }
        }

        bool Error = false;        

        void NextStep()
        {
            if(IsReady)
            {
                return;
            }
            if (!Solved)
            {
                if(Q.Count==0)
                {
                    Error = true;
                    IsReady = true;
                    return;
                }
                var pt = Q.Dequeue();
                int cR = pt.Item1;
                int cC = pt.Item2;
                if (cR == EndR && cC == EndC)
                {
                    Solved = true;
                    /*string str = "";
                    for(int r=0;r<18;r++)
                    {
                        for(int c=0;c<30;c++)
                        {
                            str += Data[r, c].ToString().PadLeft(3, ' ') + " ";
                        }
                        str += "\n"; 
                    }
                    File.WriteAllText("debug.log", str);*/
                    return;
                }

                Data[cR, cC]++;

                for (int i = 0; i < 4; i++)
                {
                    var r = cR + dr[i];
                    var c = cC + dc[i];
                    if (r < 0 || r > 17 || c < 0 || c > 29) continue;
                    if (Data[r, c] != 0) continue;
                    Data[r, c] = Data[cR, cC];
                    Q.Enqueue(new Tuple<int, int>(r, c));
                }                
            }
            else
            {
                int r = EndR;
                int c = EndC;

                for (int i = 0; i < 4; i++)
                {
                    var _r = r + dr[i];
                    var _c = c + dc[i];
                    if (_r < 0 || _r > 17 || _c < 0 || _c > 29) continue;
                    if (Data[_r, _c] == Data[r, c]) 
                    {
                        Data[r, c]++;                        
                        break;
                    }
                }

                while (Data[r, c] > 1) 
                {                    
                    for (int i = 0; i < 4; i++)
                    {
                        var _r = r + dr[i];
                        var _c = c + dc[i];
                        if (_r < 0 || _r > 17 || _c < 0 || _c > 29) continue;
                        if (Data[_r, _c] < 0) continue;
                        if (Data[_r, _c] < Data[r, c]) 
                        {
                            Data[r, c] = 2021;
                            r = _r;
                            c = _c;
                            break;
                        }                        
                    }
                }
                Data[StartR, StartC] = 2021;
                IsReady = true;                
            }
        }

        bool LeeRunning = false;

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            IsReady = true;            
        }

        void RunLee()
        {
            LeeRunning = true;
            for (int r = 0; r < 18; r++)
                for (int c = 0; c < 30; c++)
                    if (Data[r, c] != -1)
                        Data[r, c] = 0;
            Dispatcher.Invoke(Render);
            Solved = IsReady = Error = false;

            while (!IsReady) 
            {                
                NextStep();
                Dispatcher.Invoke(Render);
                Thread.Sleep(20);
            }

            if(Error)
            {
                Dispatcher.Invoke(() => (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Could not reach end position"));
            }
            Q.Clear();
            LeeRunning = false;
        }        
    }
}
