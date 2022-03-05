using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Notepad
{
    public partial class App : Application
    {
        public static ToolBarMenuManager toolBarMenuManager = new ToolBarMenuManager();
        private void ApplicationStartup(object sender, StartupEventArgs _event)
        {
            Notepad.MainWindow.OnLoaded += new MainWindow.OnLoad(() =>
            {
                toolBarMenuManager.Init();

                if (_event.Args != null && _event.Args.Length > 0)
                {
                    SaveHandler.Load(_event.Args[0]);
                }
            });
        }
    }
}
