using System;
using System.Windows;
using JustTools.MathUtility;
using System.IO;
using JustTools;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Notepad
{
    public class PluginHandler
    {
        private static MainWindow staticWindow = Application.Current.MainWindow as MainWindow;

        private static string RootPath = Path.Combine(UserDataHandler.RootPath, "Plugins");

        public static List<Plugin> Plugins = new List<Plugin>();

        public static void DownloadPlugin(string Url, string Name, Action DoneCallback)
        {
            WebClient client = new WebClient();

            var result = client.DownloadString(new Uri(Url));
            if (!(result.Contains("App") && result.Contains("Version")))
            {
                Console.WriteLine("Missing Information...");
                return;
            }

            var PluginRoot = Path.Combine(RootPath, Name);
            if (!Directory.Exists(PluginRoot)) { Directory.CreateDirectory(PluginRoot); }

            var UrlData = Url.Split("/");
            var RootUrl = Url.Split(UrlData[UrlData.Length - 1])[0];

            var PackData = GetPackData(result);

            MultiWebClient MultiClient = new MultiWebClient();

            (string Url, string Path)[] TargetItems = new (string Url, string Path)[PackData.Items.Length + 2];
            TargetItems[0] = (RootUrl + PackData.AppPath, Path.Combine(RootPath, Name, PackData.AppPath));
            TargetItems[1] = (RootUrl + PackData.IconPath, Path.Combine(RootPath, Name, PackData.IconPath));

            for (int i = 0; i < PackData.Items.Length; i++)
            {
                TargetItems[i + 2] = (RootUrl + PackData.Items[i].Replace(" ", ""), Path.Combine(RootPath, Name, PackData.Items[i].Replace(" ", "")));
            }

            MultiClient.DownloadFilesAsync(TargetItems);
            MultiClient.OnDownloadFileCompleted += () =>
            {
                staticWindow.Dispatcher.Invoke(() => { PluginsDisplay.setDownlaodInfoTextContent("Configuring..."); });
                staticWindow.SetTimeOut(() =>
                {
                    staticWindow.Dispatcher.Invoke(() =>
                    {
                        File.WriteAllText(Path.Combine(RootPath, Name, "PackData."), "Version:" + PackData.AppVersion + "\nDllPath;" + Path.Combine(RootPath, Name, PackData.AppPath));

                        PluginsDisplay.hideDownlaodInfoText();
                        PluginsDisplay.HideLoadingObject();
                        PluginsDisplay.ReloadDisplay();
                        client.Dispose();
                        MultiClient.DisposeClient();
                    });
                }, 1000);




            };
        }

        public static bool HasPlugin(string Name)
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                if (Plugins[i].Name == Name)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool UninstallPlugin(string Name)
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                if (Plugins[i].Name == Name)
                {
                    if (Directory.GetParent(Plugins[i].DllPath).Exists)
                    {

                        Console.WriteLine(Plugins[i].DllPath);
                        Directory.GetParent(Plugins[i].DllPath).Delete(true);
                        Plugins.RemoveAt(i);
                        return true;
                    }
                    break;
                }
            }
            return false;
        }


        public static Plugin GetPlugin(string Name)
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                if (Plugins[i].Name == Name)
                {
                    return Plugins[i];
                }
            }
            return null;
        }

        public static void InvokeMethod(string MethodName, string ClassPath, object[] paramList, bool IsStatic = false)
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                InvokePluginMethod(Plugins[i], MethodName, ClassPath, paramList, IsStatic);
            }
        }
        
        public static void SetBehaviours()
        {
            InvokeMethod("AddBehaviour", "NPCore.Internal.Behaviours", new object[] { "SetTheme", new Action<object[]>((Params) => { ThemeHandler.SetTheme((string)Params[0], (string)Params[1]); }), false }, true);
            
            InvokeMethod("AddBehaviour", "NPCore.Internal.Behaviours", new object[] { "SetThemeSmooth", new Action<object[]>((Params) => { ThemeHandler.SetThemeSmooth((string)Params[0], (string)Params[1]); }), false }, true);
        }

        public static void test()
        {

            var defaultList = new List<TestItem>()
            {
                new TestItem() { Name = "Sample Menu 1"},
                new TestItem() { Name = "Sample Menu 2"}
            };

            var result = defaultList;


            var pluginList = new List<TestPlugin>()
            {
                new TestPlugin()
                {
                    Priority = 1,
                    Items = new List<TestItem>()
                    {
                        null,
                        new TestItem() { Name = "Modified Menu 1" }
                    }
                },

                new TestPlugin()
                {
                    Priority = 3,
                    Items = new List<TestItem>()
                    {
                        null,
                        new TestItem() { Name = "Modified Menu 2" }
                    }
                },

                new TestPlugin()
                {
                    Priority = 2,

                    Items = new List<TestItem>()
                    {
                        null,
                        new TestItem() { Name = "Modified Menu 3" }
                    }
                }
            };










            for (int i = 0; i < defaultList.Count; i++)
            {
                for (int j = 0; j < pluginList.Count; j++)
                {
                    if (pluginList[j].Items[i] != null)
                    {
                        result[i] = pluginList[j].Items[i];
                    }
                }
            }




            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i].Name);
            }
        }


        private class TestPlugin
        {
            public int Priority;

            public List<TestItem> Items;
        }

        private class TestItem
        {
            public string Name;
        }

        public static object InvokePluginMethod(Plugin PluginInstance, string MethodName, string ClassPath, object[] paramList, bool IsStatic = false)
        {
            var assembly = Assembly.LoadFile(PluginInstance.DllPath).GetReferencedAssembly("NPCore");

            var Type = assembly.GetType(ClassPath);

            var method = Type.GetMethod(MethodName);

            return method.Invoke(IsStatic ? null : Activator.CreateInstance(Type), paramList);
        }

        public static void ReloadPlugins()
        {
            if (!Directory.Exists(RootPath)) { Directory.CreateDirectory(RootPath); }

            var list = Directory.GetDirectories(RootPath);

            Plugins.Clear();

            for (int i = 0; i < list.Length; i++)
            {
                var packDataPath = Path.Combine(list[i], "PackData.");
                if (!File.Exists(packDataPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Corrupted Plugin: " + list[i] + "...");
                    Console.ForegroundColor = ConsoleColor.White;
                }


                var packData = GetPackData(File.ReadAllText(packDataPath));
                Plugins.Add(new Plugin()
                {
                    Name = new DirectoryInfo(list[i]).Name,
                    Version = AutoParse.FloatParse(packData.AppVersion).Value,
                    DllPath = packData.AppPath
                });
            }
        }



        public static void PrintPlugins()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Loaded Plugins:");

            for (int i = 0; i < Plugins.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(Plugins[i].Name + "\n[");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  DllPath:" + Plugins[i].DllPath + ",\n  Version: " + Plugins[i].Version + ",\n  Namespace: " + Plugins[i].Namespace);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("]");
            }
        }

        private static (string AppPath, string IconPath, string AppVersion, string[] Items, string AppNamespace) GetPackData(string Data)
        {
            string App = "";
            string Icon = "";
            string Version = "";
            string[] AppItems = null;
            string Namespace = null;

            var list = Data.Split("\n");

            for (int i = 0; i < list.Length; i++)
            {
                var itemData = list[i].Replace("\n", "").Replace("\r", "");
                var item = itemData.Contains(";") ? itemData.Split(";") : itemData.Split(":");


                if (item[0] == "App")
                {
                    App = item[1];
                }
                else if (item[0] == "DllPath")
                {
                    App = item[1];
                }
                else if (item[0] == "Icon")
                {
                    Icon = item[1];
                }
                else if (item[0] == "Version")
                {
                    Version = item[1];
                }
                else if (item[0] == "Items")
                {
                    try
                    {
                        if (item[1].Contains("[") && item[1].Contains("]"))
                        {
                            var data = item[1].Split("[")[1].Split("]")[0];

                            if (data.Contains(","))
                            {
                                AppItems = data.Split(",");
                            }
                            else
                            {
                                AppItems = new string[] { data };
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Corrupted Items");
                    }
                }
            }

            return (App, Icon, Version, AppItems, Namespace);
        }


        public class Plugin
        {
            public string Name { get; set; }
            public float Version { get; set; }
            public string DllPath { get; set; }
            public string Namespace { get; set; }
            public byte Priority { get; set; }
        }
    }
}
