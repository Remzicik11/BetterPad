using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad
{
    public class AdvancedTools
    {
        private static GridLengthConverter gridLengthConverter = new GridLengthConverter();

        private static MainWindow window = Application.Current.MainWindow as MainWindow;

        public static void showAdvancedTools()
        {
            window.editorMenu.RowDefinitions[0].Height = (GridLength)gridLengthConverter.ConvertFrom("100");
            window.Tabs.Visibility = Visibility.Visible;

        }

        public static void hideAdvancedTools()
        {
            window.editorMenu.RowDefinitions[0].Height = (GridLength)gridLengthConverter.ConvertFrom("59");
            window.Tabs.Visibility = Visibility.Hidden;
        }
    }


    public partial class MainWindow : Window
    {
        public void ToggleAdvancedTools(string status)
        {
            if (status == "open")
            {
                AdvancedTools.showAdvancedTools();
            }
            else
            {
                AdvancedTools.hideAdvancedTools();
            }
        }
    }

}
