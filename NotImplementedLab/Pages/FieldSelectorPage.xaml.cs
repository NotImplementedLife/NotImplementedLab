using NotImplementedLab.Data;

namespace NotImplementedLab.Pages
{
    /// <summary>
    /// Interaction logic for FieldSelectorPage.xaml
    /// </summary>
    public partial class FieldSelectorPage : _Page
    {
        public FieldSelectorPage()
        {
            InitializeComponent();
        }

        private void MtFieldPresenterButton_Click(object s)
        {
            var sp = Owner.ShowcasePage as ShowcasePage;
            sp.Items.Clear();
            sp.Items.AddRange(ShowcaseIcons.MathsItems);
            sp.List.Items.Refresh();
            Owner.MainFrame.Navigate(Owner.ShowcasePage);
        }

        private void PhFieldPresenterButton_Click(object s)
        {
            var sp = Owner.ShowcasePage as ShowcasePage;
            sp.Items.Clear();
            sp.Items.AddRange(ShowcaseIcons.PhysicsItems);
            sp.List.Items.Refresh();
            Owner.MainFrame.Navigate(Owner.ShowcasePage);
        }

        private void CsFieldPresenterButton_Click(object s)
        {
            var sp = Owner.ShowcasePage as ShowcasePage;
            sp.Items.Clear();
            sp.Items.AddRange(ShowcaseIcons.CSItems);
            sp.List.Items.Refresh();
            Owner.MainFrame.Navigate(Owner.ShowcasePage);
        }

        private void FieldPresenterButton_Click(object s)
        {
            Owner.MessageModal("Work in progress..");
        }


        bool msDown = false;
        private void GithubLink_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                msDown = true;
        }

        private void GithubLink_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(msDown)
            {
                System.Diagnostics.Process.Start("https://github.com/NotImplementedLife/NotImplementedLab");
            }
        }

        private void GithubLink_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            msDown = false;
        }
    }
}
