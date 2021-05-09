using NotImplementedLab.Plugins;
using NotImplementedLab.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NotImplementedLab.Controls.Modals
{
    /// <summary>
    /// Interaction logic for PluginsModalDialog.xaml
    /// </summary>
    public partial class PluginsModalDialog : ModalDialogBase
    {
        public PluginsModalDialog()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            PluginManager.SaveDisabledPluginsList();
            PluginManager.Populate();
            Close();            
        }

        private void ModalDialogBase_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox.ItemsSource = PluginManager.Plugins;
        }

        private void SAB_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < PluginManager.Plugins.Count; i++)
                PluginManager.Plugins[i].Active = true;
        }

        private void DAB_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < PluginManager.Plugins.Count; i++)
                PluginManager.Plugins[i].Active = false;
        }
    }
}
