using NotImplementedLab.Controls.Modals;
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

namespace Domino.Tiling.Modals
{
    /// <summary>
    /// Interaction logic for GaussianSolverPropertiesModal.xaml
    /// </summary>
    public partial class GaussianSolverPropertiesModal : ModalDialogBase
    {
        public GaussianSolverPropertiesModal()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
