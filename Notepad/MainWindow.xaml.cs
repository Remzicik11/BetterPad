using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static OnLoad OnLoaded;

        public MainWindow()
        {
            InitializeComponent();

            //JustTools.Alternatives.ConsoleAllocator.ShowConsoleWindow();

            #region

            #endregion
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (OnLoaded != null)
            {
                OnLoaded.Invoke();
            }
            //ThemeHandler.SetTheme("Dark");
            Transition.Visibility = Visibility.Visible;
            //SetAccentState(BlurredLoginUIWindow.Class.AccentState.ACCENT_ENABLE_BLURBEHIND);
        }

        private void OpenHelpCenter(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://BetterPadHelpCenter.remzistudios.repl.co") { UseShellExecute = true });
        }


        private void SwitchSettingsMenu(object sender, MouseButtonEventArgs _event)
        {
            SettingsMenu.Visibility = SettingsMenu.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            editorMenu.Visibility = SettingsMenu.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void InputTextChanged(object sender, TextChangedEventArgs _event)
        {
            SaveHandler.MarkUnsaved();
        }

        public delegate void OnLoad();
    }
}