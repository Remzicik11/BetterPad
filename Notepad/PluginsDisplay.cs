using System;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notepad
{
    public class PluginsDisplay
    {
        private const string PluginDataUrl = "https://betterpaddata.remzistudios.repl.co/Plugins.html";

        private static MainWindow staticWindow = Application.Current.MainWindow as MainWindow;

        public static void ReloadDisplay(Action Callback = null)
        {
            staticWindow.PluginsList.ItemsSource = null;
            PluginHandler.ReloadPlugins();

            new System.Threading.Thread(() =>
            {
                var PluginList = new List<MainWindow.PluginCard>();

                if (JustTools.SystemBehaviours.IsInternetAvaible)
                {
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            string[] Data = client.DownloadString(PluginDataUrl).Split("\n");

                            for (int i = 0; i < Data.Length; i++)
                            {
                                var ItemData = Data[i].Split("\\");
                                var HasPlugin = PluginHandler.HasPlugin(ItemData[0]);
                                PluginList.Add
                                (
                                    new MainWindow.PluginCard()
                                    {
                                        Title = ItemData[0],
                                        Description = ItemData[1].Split(AppLocalizaton.Localization.CurrentLanguage + "[")[1].Split("]")[0],
                                        ButtonContent = HasPlugin ? "Installed" : "Install",
                                        Tag = ItemData[0] + "|" + ItemData[2],
                                        UninstallVisibility = HasPlugin ? Visibility.Visible : Visibility.Collapsed
                                    }
                                );


                            }
                        }
                    }
                    catch
                    {
                        OnNoInternetConnection();
                    }
                }
                else
                {
                    OnNoInternetConnection();
                }

                if (Callback != null) { Callback.Invoke(); }

                staticWindow.Dispatcher.Invoke(() =>
                {
                    staticWindow.PluginsList.ItemsSource = PluginList;
                    Random random = new Random();

                    var animation = new DoubleAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(Math.Clamp(random.NextDouble(), 0.3, 0.8)),
                        From = 0,
                        To = 1
                    };

                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(staticWindow.PluginsList); i++)
                    {
                        staticWindow.PluginsList.getChild(i).BeginAnimation(Control.OpacityProperty, animation);
                    }
                });

            }).Start();

        }

        private static void OnNoInternetConnection()
        {
            HideLoadingObject();
        }

        public static void ShowLoadingObject()
        {
            var animation = new DoubleAnimation()
            {
                From = 0,
                To = 100,
                Duration = TimeSpan.FromSeconds(0.8),
                EasingFunction = new BackEase() { Amplitude = 0.7, EasingMode = EasingMode.EaseInOut }
            };


            staticWindow.PluginsLoadingObject.getChild(1).BeginAnimation(Control.WidthProperty, animation);
            staticWindow.PluginsLoadingObject.getChild(1).BeginAnimation(Control.HeightProperty, animation);


            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(200);
                staticWindow.Dispatcher.Invoke(() =>
                {
                    animation.To = 120;
                    staticWindow.PluginsLoadingObject.getChild(0).BeginAnimation(Control.WidthProperty, animation);
                    staticWindow.PluginsLoadingObject.getChild(0).BeginAnimation(Control.HeightProperty, animation);
                });
            }).Start();
        }



        public static void setDownlaodInfoTextContent(string Content)
        {
            staticWindow.DownloadProcessInformation.BeginAnimation(Control.OpacityProperty, new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                To = 0
            });
            staticWindow.DownloadProcessInformation.BeginAnimation(Control.MarginProperty, new ThicknessAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                From = new Thickness(0, 101, 0, 0),
                To = new Thickness(0, 91, 0, 0)
            });

            staticWindow.SetTimeOut(() =>
            {
                staticWindow.Dispatcher.Invoke(() =>
                {
                    staticWindow.DownloadProcessInformation.Text = Content;
                    staticWindow.DownloadProcessInformation.BeginAnimation(Control.OpacityProperty, new DoubleAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(0.3),
                        To = 1
                    });

                    staticWindow.DownloadProcessInformation.BeginAnimation(Control.MarginProperty, new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(0.3),
                        From = new Thickness(0, 101, 0, 0),
                        To = new Thickness(0, 91, 0, 0)
                    });
                });
            }, 400);

        }

        public static void hideDownlaodInfoText()
        {
            staticWindow.DownloadProcessInformation.BeginAnimation(Control.OpacityProperty, new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                To = 0
            });
            staticWindow.DownloadProcessInformation.BeginAnimation(Control.MarginProperty, new ThicknessAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                From = new Thickness(0, 101, 0, 0),
                To = new Thickness(0, 91, 0, 0)
            });
        }

        public static void HideLoadingObject()
        {
            var animation = new DoubleAnimation()
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.8),
                EasingFunction = new BackEase() { Amplitude = 0.7, EasingMode = EasingMode.EaseInOut }
            };

            staticWindow.PluginsLoadingObject.getChild(0).BeginAnimation(Control.WidthProperty, animation);
            staticWindow.PluginsLoadingObject.getChild(0).BeginAnimation(Control.HeightProperty, animation);


            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(200);
                staticWindow.Dispatcher.Invoke(() =>
                {
                    staticWindow.PluginsLoadingObject.getChild(1).BeginAnimation(Control.WidthProperty, animation);
                    staticWindow.PluginsLoadingObject.getChild(1).BeginAnimation(Control.HeightProperty, animation);
                });
            }).Start();
        }
    }

    public partial class MainWindow : Window
    {
        public class PluginCard
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tag { get; set; }
            public string ButtonContent { get; set; }
            public Visibility UninstallVisibility { get; set; }
        }

        public void SetTimeOut(Action callBack, int time)
        {
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(time);
                callBack.Invoke();
            }).Start();
        }

        private void AddPlugin(object sender, RoutedEventArgs _event)
        {
            var senderObject = sender as FrameworkElement;
            if (senderObject.Tag == null) { return; }

            if ((sender as Button).Content.ToString() == "Installed") { return; }
            Random random = new Random();

            var animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(Math.Clamp(random.NextDouble(), 0.3, 0.4)),
                From = 1,
                To = 0
            };

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(PluginsList); i++)
            {
                PluginsList.getChild(i).BeginAnimation(Control.OpacityProperty, animation);
            }


            PluginsDisplay.setDownlaodInfoTextContent("Downloading");
            PluginsDisplay.ShowLoadingObject();

            var data = senderObject.Tag.ToString().Split("|");

            SetTimeOut(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    PluginHandler.DownloadPlugin(data[1], data[0], () =>
                    {
                        PluginsDisplay.setDownlaodInfoTextContent("Configuring Settings");
                    });

                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(PluginsList); i++)
                    {
                        PluginsList.ItemsSource = new List<PluginCard>();
                    }
                });

            }, 800);


        }

        private void UninstallPlugin(object sender, RoutedEventArgs _event)
        {
            var senderObject = sender as FrameworkElement;
            if (senderObject.Tag == null) { return; }

            if (PluginHandler.UninstallPlugin(senderObject.Tag.ToString().Split("|")[0]))
            {
                PluginsDisplay.ShowLoadingObject();
                PluginsDisplay.setDownlaodInfoTextContent("Configuring...");
                Random random = new Random();

                var animation = new DoubleAnimation()
                {
                    Duration = TimeSpan.FromSeconds(Math.Clamp(random.NextDouble(), 0.3, 0.4)),
                    From = 1,
                    To = 0
                };

                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(PluginsList); i++)
                {
                    PluginsList.getChild(i).BeginAnimation(Control.OpacityProperty, animation);
                }


                SetTimeOut(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        var list = PluginsList.ItemsSource as List<PluginCard>;

                        for (int i = 0; i < list.Count; i++)
                        {
                            if (senderObject.Tag.ToString() == list[i].Tag)
                            {
                                list.RemoveAt(i);
                                break;
                            }
                        }

                        PluginsList.ItemsSource = list;
                        PluginsDisplay.HideLoadingObject();
                        PluginsDisplay.hideDownlaodInfoText();
                        PluginsDisplay.ReloadDisplay();
                        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(PluginsList); i++)
                        {
                            PluginsList.ItemsSource = new List<PluginCard>();
                        }
                    });
                }, 800);
            }

        }
    }
}
