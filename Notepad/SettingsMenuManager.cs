using System;
using NPCore.DataTemplates;
using NPCore;
using NPCore.UIControls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text;

namespace Notepad
{
    public abstract class SettingsMenuManager
    {
        private static MainWindow staticWindow = Application.Current.MainWindow as MainWindow;

        public static List<Item> list = new List<Item>()
        {
            new Item()
            {
                Title = "App Theme",

                Description = "Choose the theme for The App that you want to use.",

                Content = new List<ItemContent>()
                {
                    new ItemContent()
                    {
                        Type = "NPCore.UIControls.SelectionList",

                        Content = new SelectionList()
                        {
                            Selection = 0,

                            Items = new List<SelectionList.SelectionItem>()
                            {
                                new SelectionList.SelectionItem()
                                {
                                    Name = "Aydınlık",
                                    Action = new Action(()=>{  ThemeHandler.SetThemeSmooth("Light", ""); })
                                },
                                new SelectionList.SelectionItem()
                                {
                                    Name = "Koyu",
                                    Action = new Action(()=>{ThemeHandler.SetThemeSmooth("Dark", ""); })
                                },
                                new SelectionList.SelectionItem()
                                {
                                    Name = "Şeffaf",
                                    Action = new Action(()=>{ThemeHandler.SetThemeSmooth("Dark", "Transparent"); })
                                },

                            }
                        }
                    }
                },


        IconUrl = new ResourceUrl(new List<(string, string, ResourceType)>
                {
                    ("Light", "./Assets/Light/Symbols/AppThemeSymbol.png", ResourceType.Relative),
                    ("Dark", "./Assets/Dark/Symbols/AppThemeSymbol.png", ResourceType.Relative)
                }),
                ID = "SDATT"
            }
        };

        public static void LoadTabs(List<SettingsTab> Tabs, int Priority)
        {
            for (int i = 0; i < Tabs.Count; i++)
            {
                bool Override = false;

                if (!string.IsNullOrEmpty(Tabs[i].ID))
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (Tabs[i].ID == list[i].ID && list[i].Priority < Priority)
                        {
                            list[i] = Item.CreateInstance(Tabs[i], Priority);

                            Override = true;
                            break;
                        }
                    }
                }

                if (Override) { continue; }

                list.Add(Item.CreateInstance(Tabs[i], Priority));
            }



            staticWindow.SettingsTabs.ItemsSource = null;
            staticWindow.SettingsTabs.ItemsSource = list;
            staticWindow.SettingsTabs.Items.Refresh();



            staticWindow.SetTimeOut(() =>
            {
                staticWindow.Dispatcher.Invoke(() =>
                {
                    ThemeHandler.SetTheme(ThemeHandler.CurrentTheme, "", staticWindow.SettingsSections);
                });

            }, 0);


        }

        public class Item
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string ID { get; set; }
            public List<ItemContent> Content { get; set; }
            public ResourceUrl IconUrl { get; set; }
            public int Priority = -1;

            public static Item CreateInstance(SettingsTab Target, int Priority)
            {
                return new Item()
                {
                    Title = Target.Title,
                    ID = Target.ID,
                    IconUrl = Target.IconUrl,
                    Content = Target.Content == null ? null : ItemContent.Convert(Target.Content.Items),
                    Priority = Priority
                };
            }
        }

        public class ItemContent
        {
            public string Type { get; set; }

            public IContent Content { get; set; }

            public static List<ItemContent> Convert(List<IContent> ContentList)
            {
                var result = new List<ItemContent>();

                for (int i = 0; i < ContentList.Count; i++)
                {
                    result.Add(new ItemContent()
                    {
                        Type = ContentList[i].GetType().ToString(),
                        Content = ContentList[i]
                    });
                }

                return result;
            }
        }
    }
}
