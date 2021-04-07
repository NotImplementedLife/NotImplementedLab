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

namespace Oscillators.PendulumSimulator
{
    /// <summary>
    /// Interaction logic for Playground.xaml
    /// </summary>
    public partial class Playground : UserControl
    {
        public Playground()
        {
            InitializeComponent();
            m = Inputm.Value;
            L = InputL.Value;
            damp = InputDamp.Value;
            tetha = Math.PI / 3;
            DataContext = this;
            isPaused = true;
            Task.Run(new Action(Rendering));
            //CompositionTarget.Rendering += Rendering;            
        }        
        double t = 0, dt = 0;
        public double L { get; set; }
        public double m { get; set; }
        public double damp { get; set; }
        public double tetha { get; set; }

        double cX = 400;
        double cY = 0;

        double d_tetha = 0;
        double d2_tetha = 0;

        static readonly double g = 9.80665;

        private void SwitchControlPanelVisibilityButton_Click(object sender, RoutedEventArgs e)
        {
            switch (ControlPanel.Visibility)
            {
                case Visibility.Visible:
                    {
                        ControlPanel.Visibility = Visibility.Collapsed;
                        SwitchControlPanelVisibilityButton.Content = "\u00AB";
                        break;
                    }
                case Visibility.Collapsed:
                    {
                        ControlPanel.Visibility = Visibility.Visible;
                        SwitchControlPanelVisibilityButton.Content = "\u00BB";
                        break;
                    }
                default: { break; }
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
                Keyboard.ClearFocus();
            }
        }

        bool isPaused = false;
        bool forceRendering = false;
        BitmapImage icPlay = new BitmapImage(new Uri("pack://application:,,,/Oscillators;component/PendulumSimulator/Drawable/ic_play.png"));
        BitmapImage icPause = new BitmapImage(new Uri("pack://application:,,,/Oscillators;component/PendulumSimulator/Drawable/ic_pause.png"));

        private void PlayPauseControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPaused = !isPaused;
            PlayPauseControl.Source = isPaused ? icPlay : icPause;
        }


        bool updateValues = true;
        private void Inputm_ValueChanged(object sender)
        {
            if (!updateValues) return;
            m = Inputm.Value;
        }

        private void InputL_ValueChanged(object sender)
        {
            if (!updateValues) return;
            L = InputL.Value;
            forceRendering = true;
        }

        private void InputDamp_ValueChanged(object sender)
        {
            if (!updateValues) return;
            damp = InputDamp.Value;
        }        

        double ddt = 0;
        double delayer = 0;

        List<double> ValTetha = new List<double>();
        List<double> ValDTetha = new List<double>();
        List<double> ValD2Tetha = new List<double>();

        List<Point> PTetha = new List<Point>();
        List<Point> PDTetha = new List<Point>();
        List<Point> PD2Tetha = new List<Point>();

        private void Rendering()
        {
            var lastRender = DateTime.Now;            
            while (true) 
            {
                var rtime = DateTime.Now;
                ddt = (rtime - lastRender).TotalSeconds;
                lastRender = rtime;
                
                if (isPaused || isCanvasPaused) 
                {
                    if (forceRendering)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Body.Center = new Point(
                                cX + L * 100 * Math.Cos(Math.PI / 2 - tetha),
                                cY + L * 100 * Math.Sin(Math.PI / 2 - tetha)
                                );
                            forceRendering = false;
                        });
                    }
                    continue;
                }
                dt += ddt;
                if (dt < 0.06) continue;

                d2_tetha = -g / L * Math.Sin(tetha) - damp / m * d_tetha;
                d_tetha += d2_tetha * dt;
                tetha += d_tetha * dt;
                Dispatcher.Invoke(() =>
                {
                    Body.Center = new Point(
                        cX + L * 100 * Math.Cos(Math.PI / 2 - tetha),
                        cY + L * 100 * Math.Sin(Math.PI / 2 - tetha)
                        );
                });

                ValTetha.Add(100 * tetha);
                ValDTetha.Add(100 * d_tetha);
                ValD2Tetha.Add(100 * d2_tetha);

                if (ValTetha.Count > 300) ValTetha.RemoveAt(0);
                if (ValDTetha.Count > 300) ValDTetha.RemoveAt(0);
                if (ValD2Tetha.Count > 300) ValD2Tetha.RemoveAt(0);

                Dispatcher.Invoke(() =>
                {
                    GraphT.PlotStep(ValTetha, 0, 2);
                    GraphDT.PlotStep(ValDTetha, 0, 2);
                    GraphD2T.PlotStep(ValD2Tetha, 0, 2);
                });

                t += dt;
                dt = 0;
            }
        }        

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isCanvasPaused) return;
            var p = e.GetPosition(sender as Canvas);
            if (p.Y == 0)
            {
                tetha = Math.PI / 2 * Math.Sign(p.X - 400);
                L = Math.Abs(p.X - 400);
            }
            else
            {
                tetha = Math.Atan((p.X - 400) / p.Y);
                L = Math.Min(3.7, p.Y / Math.Cos(tetha) * 0.01);
            }
            updateValues = false;
            InputL.Value = L;
            updateValues = true;
            forceRendering = true;
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isCanvasPaused = false;
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            isCanvasPaused = false;            
        }

        bool isCanvasPaused = false;
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isCanvasPaused = true;
            var p = e.GetPosition(sender as Canvas);
            if (p.Y == 0) 
            {
                tetha = Math.PI / 2 * Math.Sign(p.X - 400);
                L = Math.Abs(p.X - 400);
            }
            else
            {
                tetha = Math.Atan((p.X - 400) / p.Y);
                L = Math.Min(3.7, p.Y / Math.Cos(tetha) * 0.01);
            }
            updateValues = false;
            InputL.Value = L;
            updateValues = true;
            forceRendering = true;
        }
    }
}
