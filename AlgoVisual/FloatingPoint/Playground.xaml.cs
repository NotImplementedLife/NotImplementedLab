using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace AlgoVisual.FloatingPoint
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

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Input.Text == "")
            {                
                // show placeholder
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(new Uri("pack://application:,,,/AlgoVisual;component/FloatingPoint/Drawable/placeholder.png"));
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.None;                
                Input.Background = textImageBrush;
            }
            else
            {
                Input.Background = null;
            }
        }

        private void ExecButton_Click(object sender, RoutedEventArgs e)
        {
            if (Input.Text == "" || !Regex.IsMatch(Input.Text, @"^[-+]?[0-9]*\.?[0-9]*$")) 
            {
                (Window.GetWindow(this) as NotImplementedLab.Windows.MainWindow).RaiseError("Please enter a valid number");
                return;                                
            }
            Result.Visibility = Visibility.Visible;
            float f = float.Parse(Input.Text);
            var bytes = BitConverter.GetBytes(f);
            OHex.Text = "0x" + string.Join("", bytes.Reverse().Select(b => b.ToString("X2"))); 
            string bits = string.Join("", bytes.Reverse().Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));

            OSign.Text = string.Join(" ", bits.Substring(0, 1).ToList());
            OExp.Text = string.Join(" ", bits.Substring(1, 8).ToList());
            OMant.Text = string.Join(" ", bits.Substring(9, 23).ToList());

            OPlusMinus.Text = FSgn.Text = bits[0] == '0' ? "+" : "-";
            int exp = Convert.ToInt32(bits.Substring(1, 8), 2);
            OExp1.Text = exp.ToString();
            exp -= 127;
            OExp2.Text = FExp.Text = exp.ToString();

            string mnt = bits.Substring(9, 23);
            OMant2.Text = "1." + mnt;
            decimal m = 1;
            decimal x = (decimal)0.5;
            for(int i=0;i<23;i++)
            {
                if(mnt[i]=='1')
                {
                    m += x;
                }
                x *= (decimal)0.5;
            }
            OMant10.Text = FMnt.Text = m.ToString();
            FResult.Text = f.ToString();
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Return)
            {
                ExecButton_Click(null, null);
            }
        }
    }
}
