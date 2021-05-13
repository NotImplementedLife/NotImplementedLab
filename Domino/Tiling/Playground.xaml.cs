using Microsoft.Win32;
using NotImplementedLab.Windows;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static NotImplementedLab.Windows.MainWindow;

namespace Domino.Tiling
{
    /// <summary>
    /// Interaction logic for Playground.xaml
    /// </summary>
    public partial class Playground : UserControl
    {
        public Playground()
        {
            InitializeComponent();            
        }
        
        Model Model;
        Solver Solver;

        int tileSize;
        void GenerateBoard(int n)
        {
            Model = new Model(n);
            Board.Children.Clear(); 
            int sz = 400 / n;
            tileSize = sz;
            Board.Width = Board.Height = DominoPlace.Width = DominoPlace.Height = sz * n;
            for (int x = 0; x < n; x++) 
            {
                for (int y = 0; y < n; y++) 
                {
                    var r = new Rectangle
                    {
                        Fill = new SolidColorBrush(((x + y) % 2 == 1) ? Color.FromRgb(138, 120, 93) : Color.FromRgb(220, 211, 202)),
                        Stroke = Brushes.Black,
                        StrokeThickness = 1.5,
                        Width = sz,
                        Height = sz,
                    };                    
                    Canvas.SetLeft(r, sz * x);
                    Canvas.SetTop(r, sz * y);
                    Board.Children.Add(r);
                }
            }
        }

        void PlaceDominos(List<Domi> Domis)
        {
            DominoPlace.Children.Clear();            
            for (var i = 0; i < Domis.Count; i++) 
            {
                var r = new Rectangle
                {
                    Fill = Brushes.DodgerBlue,
                    Width = (Domis[i].Orientation == Orientation.Horizontal ? 2 * tileSize : tileSize) - 2,
                    Height = (Domis[i].Orientation == Orientation.Vertical ? 2 * tileSize : tileSize) - 2,         
                };
                RenderOptions.SetBitmapScalingMode(r, BitmapScalingMode.NearestNeighbor);
                RenderOptions.SetEdgeMode(r, EdgeMode.Aliased);
                Canvas.SetLeft(r, Domis[i].Col * tileSize + 1);
                Canvas.SetTop(r, Domis[i].Row * tileSize + 1);
                DominoPlace.Children.Add(r);
            }            
        }


        bool isRunning = false;
        private void Pave_Click(object sender, RoutedEventArgs e)
        {
            if(Model.Dim>Solver.MaxDim)
            {
                (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError(
                    $"{Solver.Name} solver doesn't support the tables larger than {Solver.MaxDim}x{Solver.MaxDim}. " +
                    $"Change the table size or try another solver.");
                return;
            }
            Task.Run(() =>
            {
                if (isRunning)
                    return;
                isRunning = true;

                //Dispatcher.Invoke(() => MessageBox.Show(Model.Lee(0, 0).ToString()));

                if(!Model.IsFeasible())
                {
                    Dispatcher.Invoke(() => RaiseError("The current configuration cannot be completely covered"));
                    isRunning = false;
                    return;
                }

                Dispatcher.Invoke(() => GridSizeInput.IsEnabled = false);               
                Solver.Init();                
                int nn = Model.Dim * Model.Dim / 2;
                while (isRunning && !Solver.IsReady) //cp.PartialSol.Count < nn) 
                {
                    for (int i = 0; i < 10; i++) 
                    {
                        Solver.NextStep();
                    }
                    if (isRunning)
                    {
                        try
                        {
                            Dispatcher.Invoke(() => PlaceDominos(Solver.PartialSol));
                        }
                        catch { } // a task was canceled
                    }
                    Thread.Sleep(2);
                }
                isRunning = false;
                Dispatcher.Invoke(() => GridSizeInput.IsEnabled = true);
            });
        }

        public void RaiseWarning(string msg)
            => Dispatcher.Invoke(() => (Window.GetWindow(this) as MainWindow).RaiseWarning(msg));

        public void RaiseError(string msg)
            => Dispatcher.Invoke(() => (Window.GetWindow(this) as MainWindow).RaiseError(msg));

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            isRunning = false;
            DominoPlace.Children.Clear();
            //Solver.Init();

        }

        private void Board_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isRunning) return;
            if (DominoPlace.Children.Count > 0)
                return;
            var p = e.GetPosition(Board);
            int r = (int)(p.Y / tileSize);
            int c = (int)(p.X / tileSize);
            Model.Board[r, c].IsHole = !Model.Board[r, c].IsHole;
            Model.RefreshEdges();

            (Board.Children[c * Model.Dim + r] as Rectangle).Fill =
                Model.Board[r, c].IsHole ? Brushes.Black :
                new SolidColorBrush(((c + r) % 2 == 1) ? Color.FromRgb(138, 120, 93) : Color.FromRgb(220, 211, 202));            
        }

        private void SolverProps_Click(object sender, RoutedEventArgs e)
        {
            var modal = Solver.PropertiesModal;
            if (modal == null)
                RaiseError("No properties available for this solver.");
            else
                ShowModal(modal);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning) return;
            if (DominoPlace.Children.Count > 0)
                return;
            for (int r = 0; r < Model.Dim; r++)
            {
                for (int c = 0; c < Model.Dim; c++)
                {
                    Model.Board[r, c].IsHole = false;
                    (Board.Children[c * Model.Dim + r] as Rectangle).Fill =
                        new SolidColorBrush(((c + r) % 2 == 1) ? Color.FromRgb(138, 120, 93) : Color.FromRgb(220, 211, 202));
                }
            }
        }

        bool loaded = false;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateBoard(int.Parse((GridSizeInput.SelectedItem as ComboBoxItem).Content as string));
            Solver = new Solvers.GausssianSolver(Model);
            loaded = true;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            isRunning = false;
        }

        private void GridSizeInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!loaded) return;
            DominoPlace.Children.Clear();
            GenerateBoard(int.Parse((GridSizeInput.SelectedItem as ComboBoxItem).Content as string));
            Solver = new Solvers.GausssianSolver(Model);            
        }

        SaveFileDialog sfd = new SaveFileDialog
        {
            Filter = "Portable Network Graphics (*.png)|*.png"
        };


        private void SaveBmp_Click(object sender, RoutedEventArgs e)
        {
            if (sfd.ShowDialog() == true)
            {
                DisplayGrid.Measure(new Size(400, 400));
                DisplayGrid.Arrange(new Rect(new Size(400, 400)));

                RenderTargetBitmap bmp = new RenderTargetBitmap(400, 400, 96, 96, PixelFormats.Pbgra32);

                bmp.Render(DisplayGrid);

                var encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(bmp));
                try
                {
                    using (Stream stream = File.Create(sfd.FileName))
                        encoder.Save(stream);
                }
                catch
                {
                    RaiseError("File could not be saved");
                }
            }
        }
    }
}
