using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using NotImplementedLab.Windows;

namespace NotImplementedLab.Controls.Modals
{
    public class ModalDialogBase : UserControl
    {
        private bool _hideRequest = false;
        private bool _result = false;
        private UIElement _parent;

        public void SetParent(UIElement parent)
        {
            _parent = parent;
        }

        public bool ShowHandlerDialog()
        {
            //Message = message;
            Visibility = Visibility.Visible;

            ModalShow?.Invoke(this, null);
            _parent.IsEnabled = false;

            _hideRequest = false;
            while (!_hideRequest)
            {
                // HACK: Stop the thread if the application is about to close
                if (Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished)
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                this.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return _result;
        }

        private void HideHandlerDialog()
        {
            _hideRequest = true;
            ModalClose?.Invoke(this, null);
            Visibility = Visibility.Collapsed;
            _parent.IsEnabled = true;
        }

        public delegate void OnModalShow(object o, EventArgs e);
        public event OnModalShow ModalShow;

        public delegate void OnModalClose(object o, EventArgs e);
        public event OnModalClose ModalClose;

        public MainWindow Owner;
        
        public void Close()
        {
            _result = true;
            HideHandlerDialog();
        }
    }
}
