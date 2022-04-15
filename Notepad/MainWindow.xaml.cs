using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public static OnLoad OnLoaded;

        public MainWindow()
        {
            InitializeComponent();
            JustTools.Alternatives.ConsoleAllocator.ShowConsoleWindow();


            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (OnLoaded != null)
            {
                OnLoaded.Invoke();
            }

            Transition.Visibility = Visibility.Visible;

        }


        private void OpenHelpCenter(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://BetterPadHelpCenter.remzistudios.repl.co") { UseShellExecute = true });
        }

        public void setBlurState(string state)
        {
            if (state == "active")
            {
                SetAccentState(BlurredLoginUIWindow.Class.AccentState.ACCENT_ENABLE_BLURBEHIND);
            }
            else
            {
                SetAccentState(BlurredLoginUIWindow.Class.AccentState.ACCENT_DISABLED);
            }
        }

        private void SwitchSettingsMenu(object sender, MouseButtonEventArgs _event)
        {
            SettingsMenu.Visibility = SettingsMenu.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            editorMenu.Visibility = SettingsMenu.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void SwitchPluginsMenu(object sender, MouseButtonEventArgs _event)
        {
            SettingsMenu.Visibility = SettingsMenu.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            PluginsMenu.Visibility = SettingsMenu.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            PluginsDisplay.ShowLoadingObject();

            if (PluginsMenu.Visibility == Visibility.Visible)
            {
                PluginsDisplay.ReloadDisplay(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        PluginsDisplay.HideLoadingObject();
                    });
                });
            }
        }

        private void InputTextChanged(object sender, TextChangedEventArgs _event)
        {
            SaveHandler.MarkUnsaved();
        }

        public delegate void OnLoad();
    }
}