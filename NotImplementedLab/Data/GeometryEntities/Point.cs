using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using NotImplementedLab.Controls;

namespace NotImplementedLab.Data.GeometryEntities
{
    public class Point : GeometryEntity
    {
        private double _X, _Y;
        public Point()
        {
            _fwe = new Ellipse()
            {
                Stroke = Brushes.Black,
                Fill = Brushes.DarkBlue,
                Width = 10,
                Height = 10,
                RenderTransform = new TranslateTransform(-5, -5)
            };
            _lbl = new TextBlock();

            X = 100;
            Y = 100;
            Label = "B";
        }

        public override void Initialize(GeometryCanvas canvas)
        {
            canvas.Children.Add(_fwe);
            canvas.Children.Add(_lbl);            
        }

        public double X
        {
            get => _X;
            set
            {
                _X = value;
                Canvas.SetLeft(_fwe, _X);
                Canvas.SetLeft(_lbl, _X + 8);
                for (int i = 0, cnt = Dependants.Count; i < cnt; i++)
                    Dependants[i].Update();
            }
        }

        public double Y
        {
            get => _Y;
            set
            {
                _Y = value;
                Canvas.SetTop(_fwe, _Y);
                Canvas.SetTop(_lbl, _Y + 5);
                for (int i = 0, cnt = Dependants.Count; i < cnt; i++)
                    Dependants[i].Update();
            }
        }

        public string Label
        {
            get => _lbl.Text;
            set => _lbl.Text = value;
        }
    }
}
