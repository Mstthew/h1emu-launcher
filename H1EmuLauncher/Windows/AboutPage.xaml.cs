﻿using System;
using System.Windows;
using System.Windows.Input;
using H1EmuLauncher.Classes;

namespace H1EmuLauncher
{
    public partial class AboutPage : Window
    {
        public static AboutPage aboutPageInstance;

        public AboutPage()
        {
            InitializeComponent();
            aboutPageInstance = this;
            Owner = Launcher.launcherInstance;

            // Adds the correct language file to the resource dictionary and then loads it.
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
            Launcher.launcherInstance.launcherBlur.Radius = 15;
            Launcher.launcherInstance.launcherFade.Visibility = Visibility.Visible;
        }

        private void MainAboutClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Launcher.launcherInstance.launcherBlur.Radius = 0;
            Launcher.launcherInstance.launcherFade.Visibility = Visibility.Hidden;
        }

        private void MainAboutActivated(object sender, EventArgs e)
        {
            aboutPageBlur.Radius = 0;
            aboutPageFade.Visibility = Visibility.Hidden;
        }
    }
}