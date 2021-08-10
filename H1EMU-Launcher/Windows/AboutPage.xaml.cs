﻿using H1EMU_Launcher.Resources;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace H1EMU_Launcher
{
    /// <summary>
    /// Interaction logic for AboutPage.xaml
    /// </summary>

    public partial class AboutPage : Window
    {
        public static AboutPage abtpage;

        public AboutPage()
        {
            InitializeComponent();
            abtpage = this;

            //Adds the correct language file to the resource dictionary and then load it.
            Resources.MergedDictionaries.Add(SetLanguageFile.LoadFile());
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            this.Close();
        }

        private void MainAboutLoaded(object sender, RoutedEventArgs e)
        {
            Launcher.lncher.launcherBlur.Radius = 15;
            Launcher.lncher.launcherFade.Visibility = Visibility.Visible;
        }

        private void MainAboutClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Launcher.lncher.launcherBlur.Radius = 0;
            Launcher.lncher.launcherFade.Visibility = Visibility.Hidden;
        }

        private void MainAboutActivated(object sender, EventArgs e)
        {
            aboutPageBlur.Radius = 0;
            aboutPageFade.Visibility = Visibility.Hidden;
        }
    }
}
