﻿using System;
using System.Windows;
using System.Windows.Controls;
using H1EmuLauncher.Classes;

namespace H1EmuLauncher.SteamFrame
{
    public partial class DownloadComplete : UserControl
    {
        public DownloadComplete()
        {
            InitializeComponent();

            // Adds the correct language file to the resource dictionary and then loads it.
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(SetLanguageFile.LoadFile());
        }

        private void BackToLoginPage(object sender, RoutedEventArgs e)
        {
            LauncherWindow.launcherInstance.steamFramePanel.Navigate(new Uri("..\\SteamFrame\\Login.xaml", UriKind.Relative));
        }
    }
}