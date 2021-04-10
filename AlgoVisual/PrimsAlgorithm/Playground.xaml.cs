using AlgoVisual.PrimsAlgorithm.Data;
using System;
using System.Collections.Generic;
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

namespace AlgoVisual.PrimsAlgorithm
{
    /// <summary>
    /// Interaction logic for Playground.xaml
    /// </summary>
    public partial class Playground : UserControl
    {
        Graph Data = new Graph();
        PrimInstance PrimInstance;

        public Playground()
        {
            InitializeComponent();
            PrimInstance = new PrimInstance(Data);
            Image.Source = graphBmp;
            RenderData();            
        }

        RenderTargetBitmap graphBmp = new RenderTargetBitmap(800, 480, 96, 96, PixelFormats.Pbgra32);

        void ClearBitmap()
        {
            var rectangle = new Rectangle { Fill = Brushes.White, Width = 800, Height = 480 };
            Size size = new Size(800, 480);          
            DrawingVisual drawingVisual = new DrawingVisual();                        

            rectangle.Measure(size);
            rectangle.Arrange(new Rect(size));

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(rectangle);
                drawingContext.DrawRectangle(visualBrush, null,
                  new Rect(new Point(), new Size(size.Width, size.Height)));
            }
            graphBmp.Render(drawingVisual);
        }        

        void RenderData()
        {
            ClearBitmap();
            Data.DrawOnRTB(graphBmp, PrimInstance.IsReady());
        }        

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (!PrimInstance.IsReady())
            {
                PrimInstance.ExecuteStep();
                RenderData();
            }
        }

        private void Auto_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAutoRunning)
            {
                Task.Run(new Action(AutoRender));
                Auto.BorderBrush = Brushes.ForestGreen;
            }
            else
            {
                AutoCancelOrder = true;
                Auto.BorderBrush = Brushes.DodgerBlue;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            PrimInstance.Init();
            RenderData();
        }

        bool IsAutoRunning = false;
        bool AutoCancelOrder = false;
        void AutoRender()
        {
            IsAutoRunning = true;
            while (!PrimInstance.IsReady() && !AutoCancelOrder) 
            {
                Thread.Sleep(30);
                PrimInstance.ExecuteStep();
                Dispatcher.Invoke(RenderData);
            }
            AutoCancelOrder = false;
            Dispatcher.Invoke(() => Auto.BorderBrush = Brushes.DodgerBlue);
            IsAutoRunning = false;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            AutoCancelOrder = true;
            Data = new Graph();
            PrimInstance = new PrimInstance(Data);
            RenderData();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoCancelOrder = true;
        }
    }
}
