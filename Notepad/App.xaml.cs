using System;
using NPCore;
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
                
                PluginHandler.ReloadPlugins();
                PluginHandler.SetBehaviours();

                for (int i = 0; i < PluginHandler.Plugins.Count; i++)
                {
                    PluginHandler.InvokePluginMethod(PluginHandler.Plugins[i], "Main", "NPCore.Internal.Kernel", new object[] { System.Reflection.Assembly.LoadFile(PluginHandler.Plugins[i].DllPath) }, true);

                    Tabs.Load((IMenuItem[])PluginHandler.InvokePluginMethod(PluginHandler.Plugins[i], "GetCustomTabs", "NPCore.Internal.Kernel", null, true), PluginHandler.Plugins[i].Priority);

                    Tabs.Load((IMenuItem[])PluginHandler.InvokePluginMethod(PluginHandler.Plugins[i], "GetTabs", "NPCore.Internal.Kernel", null, true), PluginHandler.Plugins[i].Priority);
                }





                if (_event.Args != null && _event.Args.Length > 0)
                {
                    SaveHandler.Load(_event.Args[0]);
                }
                else
                {
                    SaveHandler.MarkUnsaved();
                }
            });
        }
    }
}
