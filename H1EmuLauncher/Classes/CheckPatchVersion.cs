﻿using System;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Net;
using System.Windows;
using System.Diagnostics;
using System.Windows.Threading;
using System.Reflection;

namespace H1EmuLauncher.Classes
{
    class CheckPatchVersion
    {
        public static ManualResetEvent mainMa = new ManualResetEvent(false);

        public static string gameVersion;
        public static string latestPatchVersion;

        public static void DownloadLatestVersionNumber()
        {
            latestPatchVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString().TrimEnd('0').TrimEnd('.');

            Settings.installPatchResetEvent.Set();
            mainMa.Set();
        }

        public static void CheckPatch()
        {
            mainMa.Reset();

            DownloadLatestVersionNumber();
            mainMa.WaitOne();

            gameVersion = Settings.gameVersion;

            switch (gameVersion)
            {
                case "15jan2015":
                case "22dec2016":
                    if (Properties.Settings.Default.currentPatchVersion != latestPatchVersion || !File.Exists($"{Properties.Settings.Default.activeDirectory}\\dinput8.dll") ||
                    !File.Exists($"{Properties.Settings.Default.activeDirectory}\\msvcp140d.dll") || !File.Exists($"{Properties.Settings.Default.activeDirectory}\\ucrtbased.dll") ||
                    !File.Exists($"{Properties.Settings.Default.activeDirectory}\\vcruntime140d.dll") || !File.Exists($"{Properties.Settings.Default.activeDirectory}\\vcruntime140_1d.dll"))
                    {
                        Application.Current.Dispatcher.Invoke(new Action(delegate
                        {
                            Launcher.launcherInstance.playButton.IsEnabled = false;
                            Launcher.launcherInstance.playButton.Content = Application.Current.FindResource("item150").ToString();
                        }));

                        ApplyPatch();
                    }
                    break;
                case "processBeingUsed":
                    Application.Current.Dispatcher.Invoke(new Action(delegate
                    {
                        CustomMessageBox.Show(Application.Current.FindResource("item121").ToString().Replace("\\n\\n", Environment.NewLine + Environment.NewLine), Launcher.launcherInstance);
                    }));
                    break;
                default:
                    Application.Current.Dispatcher.Invoke(new Action(delegate
                    {
                        CustomMessageBox.Show(Application.Current.FindResource("item58").ToString().Replace("\\n\\n", Environment.NewLine + Environment.NewLine), Launcher.launcherInstance);
                    }));
                    break;
            }

            Launcher.ma.Set();

            Application.Current.Dispatcher.Invoke(new Action(delegate
            {
                Launcher.launcherInstance.playButton.IsEnabled = true;
                Launcher.launcherInstance.playButton.Content = Application.Current.FindResource("item8").ToString();
            }));
        }

        public static void ApplyPatch()
        {
            // Deletes old patch files if any of them are already in the directory, including the .zip in the case of corruption.
            if (File.Exists($"{Properties.Settings.Default.activeDirectory}\\dinput8.dll") || File.Exists($"{Properties.Settings.Default.activeDirectory}\\msvcp140d.dll") ||
                File.Exists($"{Properties.Settings.Default.activeDirectory}\\ucrtbased.dll") || File.Exists($"{Properties.Settings.Default.activeDirectory}\\vcruntime140d.dll") ||
                File.Exists($"{Properties.Settings.Default.activeDirectory}\\vcruntime140_1d.dll"))
            {
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\dinput8.dll");
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\msvcp140d.dll");
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\ucrtbased.dll");
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\vcruntime140d.dll");
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\vcruntime140_1d.dll");
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\Patch2015.zip");
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\Patch2016.zip");
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\AssetPatch2015.zip");
            }

            // Unzip all of the files to directory.
            try
            {
                if (gameVersion == "15jan2015")
                {
                    File.WriteAllBytes($"{Properties.Settings.Default.activeDirectory}\\Patch2015.zip", Properties.Resources.Game_Patch_2015);
                    ZipFile.ExtractToDirectory($"{Properties.Settings.Default.activeDirectory}\\Patch2015.zip", $"{Properties.Settings.Default.activeDirectory}");
                }
                else
                {
                    File.WriteAllBytes($"{Properties.Settings.Default.activeDirectory}\\Patch2016.zip", Properties.Resources.Game_Patch_2016);
                    ZipFile.ExtractToDirectory($"{Properties.Settings.Default.activeDirectory}\\Patch2016.zip", $"{Properties.Settings.Default.activeDirectory}");
                }
            }
            catch { }

            // Delete the .zip file, not needed anymore.
            if (gameVersion == "15jan2015")
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\Patch2015.zip");
            else
                File.Delete($"{Properties.Settings.Default.activeDirectory}\\Patch2016.zip");

            // Extract Asset_256.pack to fix blueberries.
            if (gameVersion == "22dec2016")
                File.WriteAllBytes($"{Properties.Settings.Default.activeDirectory}\\Resources\\Assets\\Assets_256.pack", Properties.Resources.Assets_256);

            // Replace users ClientConfig.ini with modified version
            File.WriteAllBytes($"{Properties.Settings.Default.activeDirectory}\\ClientConfig.ini", Properties.Resources.CustomClientConfig);

            // Finish.
            Properties.Settings.Default.currentPatchVersion = latestPatchVersion;
            Properties.Settings.Default.Save();
        }
    }
}