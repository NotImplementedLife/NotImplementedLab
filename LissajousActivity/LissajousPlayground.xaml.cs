using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using NotImplementedLab.Extensions;

namespace LissajousActivity
{
    /// <summary>
    /// Interaction logic for LissajousPlayground.xaml
    /// </summary>
    public partial class LissajousPlayground : UserControl
    {
        private double _A = 100, _a = 2, _B = 100, _b = 3, _d = 1;

        private void InputA_ValueChanged(object sender)
        {                        
            _A = InputA.Value;
            Update();           
        }

        private void InputB_ValueChanged(object sender)
        {
            _B = InputB.Value;
            Update();
        }

        private void Inputa_ValueChanged_1(object sender)
        {
            _a = Inputa.Value;
            Update();
        }

        private void Inputb_ValueChanged_1(object sender)
        {
            _b = Inputb.Value;
            Update();
        }

        private void Inputd_ValueChanged(object sender)
        {
            _d = Inputd.Value;
            Update();
        }

        private double fX(double t)
        {
            return _A * Math.Cos(_a * t + _d);
        }

        private double fY(double t)
        {
            return _B * Math.Cos(_b * t);
        }
        
        public LissajousPlayground()
        {
            InitializeComponent();            
            Update();
        }

        void Update()
        {
            new List<Point>().CreateFromCurve(fX, fY, -10, 10, 0.01).PlotOnGraph(Graph);
        }
    }
}
