﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NotImplementedLab.Pages
{
    /// <summary>
    /// Interaction logic for ActivityPage.xaml
    /// </summary>
    public partial class ActivityPage : _Page
    {
        public ActivityPage()
        {
            InitializeComponent();
        }

        public static DependencyProperty UIObjectProperty =
            DependencyProperty.Register("UIObject", typeof(UserControl), typeof(ActivityPage),
                new PropertyMetadata(UIPropertyChanged));

        public UserControl UIObject
        {
            get => GetValue(UIObjectProperty) as UserControl;
            set => SetValue(UIObjectProperty, value);
        }


        private static void UIPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ActivityPage).UIObjectControl.Child = e.NewValue as UserControl;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            Owner.MainFrame.Navigate(Owner.ShowcasePage);
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Owner.wInfoModal.ScrollContainer.Content == null) 
            {
                Owner.RaiseError("This activity does not contain a help page.");
            }
            else
            {
                Owner.InfoModal();
            }            
        }
    }
}
