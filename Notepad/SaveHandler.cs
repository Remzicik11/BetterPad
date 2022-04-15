using System;
using System.IO;
using JustTools;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Notepad
{
    public static class SaveHandler
    {
        private static MainWindow window = Application.Current.MainWindow as MainWindow;

        private static string SavePath;

        public static void MarkUnsaved()
        {
            if (window.FileTitle == null) { return; }
            window.Dispatcher.Invoke(() =>
            {
                if (!window.FileTitle.Text.EndsWith("*"))
                {
                    window.FileTitle.Text = string.IsNullOrEmpty(window.FileTitle.Text) ? "Adsız - Better Pad*" : window.FileTitle.Text + "*";
                }
            });
        }

        public static void Load(string Path)
        {
            window.Dispatcher.Invoke(() =>
            {
                SavePath = Path;
                window.InputText.Text = File.ReadAllText(Path);
                window.FileTitle.Text = System.IO.Path.GetFileName(Path) + "- Better Pad";
            });
        }

        public static void Save(bool asNew)
        {
            if(!SystemBehaviours.ApplicationIsActivated()) { return; }
            
            bool refresh = !File.Exists(SavePath);

            if (SavePath == null || asNew)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.DefaultExt = ".txt";
                dialog.Filter = "Text documents (.txt)|*.txt";
                dialog.Title = asNew ? "Farklı Kaydet" : "Kaydet";

                if (dialog.ShowDialog() == true)
                {
                    SavePath = dialog.FileName;
                    window.Dispatcher.Invoke(() =>
                    {
                        File.WriteAllText(dialog.FileName, window.InputText.Text);

                        if (dialog.FileName.Contains("/"))
                        {
                            var pathList = dialog.FileName.Split("/");
                            window.FileTitle.Text = pathList[pathList.Length - 1] + "- Better Pad";
                        }
                        else if (dialog.FileName.Contains("\\"))
                        {
                            var pathList = dialog.FileName.Split("\\");
                            window.FileTitle.Text = pathList[pathList.Length - 1] + "- Better Pad";
                        }
                        else
                        {
                            window.FileTitle.Text = dialog.FileName + "- Better Pad";
                        }
                    });
                }
            }
            else
            {
                window.Dispatcher.Invoke(() =>
                {
                    File.WriteAllText(SavePath, window.InputText.Text);

                    if (SavePath.Contains("/"))
                    {
                        var pathList = SavePath.Split("/");
                        window.FileTitle.Text = pathList[pathList.Length - 1] + "- Better Pad";
                    }
                    else if (SavePath.Contains("\\"))
                    {
                        var pathList = SavePath.Split("\\");
                        window.FileTitle.Text = pathList[pathList.Length - 1] + "- Better Pad";
                    }
                    else
                    {
                        window.FileTitle.Text = SavePath + "- Better Pad";
                    }
                });
            }

            if (refresh)
            {
                SystemBehaviours.RefreshWindowsExplorer();
            }
        }

    }

}
