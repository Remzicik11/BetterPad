using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Text;

namespace Notepad
{
    public class ToolBarMenuManager
    {
        private MainWindow window = Application.Current.MainWindow as MainWindow;

        private static MainWindow staticWindow = Application.Current.MainWindow as MainWindow;
        
        private static byte CurrentMenu;

        private Menu[] Menus = new Menu[]
        {
            new Menu()
            {
                ID = "Files",
                Label = "Dosya",

                Items = new MenuItem[]
                {
                    new MenuItem()
                    {
                        Label = "Kaydet",
                        ID = "Save",
                        ShortCut = "Ctrl + S",
                        ShortCutKeys = new int[] { 17, 83 },
                        OnSelected = () => { SaveHandler.Save(false); }
                    },

                    new MenuItem()
                    {
                        Label = "Farklı Kaydet",
                        ID = "SaveAs",
                        ShortCut = "Ctrl + Shift + S",
                        ShortCutKeys = new int[] { 17, 16, 83 },
                        OnSelected = () => { SaveHandler.Save(true); }
                    }
                }
            }
        };

        public void Init()
        {
            ReloadMenus();

            new System.Threading.Thread(() =>
            {
                while (true)
                {
                    for (int i = 0; i < Menus.Length; i++)
                    {
                        for (int j = 0; j < Menus[i].Items.Length; j++)
                        {
                            if (Menus[i].Items[j].ShortCutKeys != null && InputManager.GetKeyDown(Menus[i].Items[j].ShortCutKeys))
                            {
                                Menus[i].Items[j].OnSelected.Invoke();
                            }
                        }
                    }
                }
            }).Start();
        }

        private void ReloadMenus()
        {
            window.ToolsMenuBar.Children.Clear();
            for (int i = 0; i < Menus.Length; i++)
            {
                var button = new Button()
                {
                    Content = new TextBlock() { Text = Menus[i].Label, Margin = new Thickness(8, 0, 8, 0) },
                    BorderBrush = null,
                    Background = null
                };
                button.SetResourceReference(Control.StyleProperty, "LightBorderButton");
                button.Margin = new Thickness(2, 0, 2, 0);
                button.Tag = "[ToolBarMenu:Files] [Theme:ToolBarButton]";
                button.Click += new RoutedEventHandler((sender, _event) => { ToolBarMenuManager.ShowToolBarMenu(button, App.toolBarMenuManager); });
                window.ToolsMenuBar.Children.Add
                (
                    new Border()
                    {
                        Child = button
                    }
                );
            }
        }

        public static void SelectToolBarmenuItem(FrameworkElement sender, ToolBarMenuManager Instance)
        {
            for (int i = 0; i < Instance.Menus[CurrentMenu].Items.Length; i++)
            {
                if (sender.Tag != null && sender.Tag.ToString().Contains("[ToolBarMenuItem:" + Instance.Menus[CurrentMenu].Items[i].ID + "]"))
                {
                    Instance.Menus[CurrentMenu].Items[i].OnSelected.Invoke();
                    break;
                }
            }
            HideToolBarMenu();
        }
        public static void ShowToolBarMenu(FrameworkElement sender, ToolBarMenuManager Instance)
        {
            staticWindow.ToolBarMenu.MaxHeight = 349;
            staticWindow.ToolBarMenuOut.Visibility = Visibility.Visible;

            staticWindow.ToolBarMenu.BeginAnimation(Rectangle.OpacityProperty,
                new DoubleAnimation()
                {
                    Duration = TimeSpan.FromSeconds(0.3),
                    To = 1,
                }
            );

            for (int i = 0; i < Instance.Menus.Length; i++)
            {
                if (sender.Tag != null && sender.Tag.ToString().Contains("[ToolBarMenu:" + Instance.Menus[i].ID + "]"))
                {
                    CurrentMenu = (byte)i;
                    staticWindow.ToolBarMenuContent.Children.Clear();
                    for (int j = 0; j < Instance.Menus[i].Items.Length; j++)
                    {
                        var Item = new Grid()
                        {
                            Margin = new Thickness(21, j == 0 ? 3 : 8, 21, 0)
                        };
                        
                        Item.Children.Add(new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Margin = new Thickness(0, 0, 81, 0),
                            Text = Instance.Menus[i].Items[j].Label,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Tag = "[Theme:Text]"
                        });

                        Item.Children.Add
                        (
                            new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Margin = new Thickness(104, 0, 0, 0),
                                HorizontalAlignment = HorizontalAlignment.Right,
                                Text = Instance.Menus[i].Items[j].ShortCut,
                                Tag = "[Theme:Text]"
                            }
                        );

                        var button = new Button()
                        {
                            Tag = "[ToolBarMenuItem:" + Instance.Menus[i].Items[j].ID + "]",
                            Opacity = 0.3,
                            Margin = new Thickness(-10, -3, -10, -3),
                            BorderBrush = null,
                            Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0))
                        };

                        button.SetResourceReference(Control.StyleProperty, "LightBorderButton");

                        button.Click += new RoutedEventHandler((sender, _event) => { SelectToolBarmenuItem(button, App.toolBarMenuManager); });

                        Item.Children.Add(button);


                        staticWindow.ToolBarMenuContent.Children.Add(Item);

                        ThemeHandler.ReloadTheme(staticWindow.ToolBarMenuContent);
                    }
                    break;
                }
            }




        }

        public static void HideToolBarMenu()
        {
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(200);

                staticWindow.Dispatcher.Invoke(() =>
                {
                    staticWindow.ToolBarMenu.MaxHeight = 0;

                    staticWindow.ToolBarMenuOut.Visibility = Visibility.Hidden;
                });


            }).Start();

            staticWindow.ToolBarMenu.BeginAnimation(Rectangle.OpacityProperty,
                new DoubleAnimation()
                {
                    Duration = TimeSpan.FromSeconds(0.2),
                    To = 0,
                }
            );
        }


        private class Menu
        {
            public string ID;
            public string Label;
            public MenuItem[] Items;
        }

        private class MenuItem
        {
            public string Label;
            public string ShortCut;
            public string ID;
            public int[] ShortCutKeys;
            public Action OnSelected;
        }
    }

    public partial class MainWindow : Window
    {
        private void SelectToolBarMenuItem(object sender, RoutedEventArgs _event)
        {
            ToolBarMenuManager.SelectToolBarmenuItem(sender as FrameworkElement, App.toolBarMenuManager);
        }

        private void ShowToolBarMenu(object sender, RoutedEventArgs _event)
        {
            ToolBarMenuManager.ShowToolBarMenu(sender as FrameworkElement, App.toolBarMenuManager);
        }

        private void HideToolBarMenu(object sender, MouseButtonEventArgs _event)
        {
            ToolBarMenuManager.HideToolBarMenu();
        }
    }

}
