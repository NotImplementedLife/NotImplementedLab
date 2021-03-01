﻿using NotImplementedLab.Data;

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
            Owner.MessageModal("Coming soon");
            return;
            var sp = Owner.ShowcasePage as ShowcasePage;
            sp.Items.Clear();
            sp.Items.AddRange(ShowcaseIcons.CSItems);
            sp.List.Items.Refresh();
            Owner.MainFrame.Navigate(Owner.ShowcasePage);
        }
    }
}
