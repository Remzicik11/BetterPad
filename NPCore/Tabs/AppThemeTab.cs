using System;
using NPCore.UIControls;
using NPCore;
using NPCore.Internal;
using System.Collections.Generic;
using System.Text;

namespace NPCore
{
    public static class AppThemeTab
    {
        public static string Title
        {
            get => LocalTitle;

            set
            {
                LocalTitle = value;
                Changed = true;
            }
        }

        public static string Description
        {
            get => LocalDescription;

            set
            {
                LocalDescription = value;
                Changed = true;
            }
        }


        public static UIContent Content
        {
            get => LocalContent;

            set
            {
                LocalContent = value;
                Changed = true;
            }
        }

        public static ResourceUrl IconUrl { get => LocalIconUrl; }

        private static UIContent LocalContent = new UIContent()
        {
            Items = new List<IContent>()
            {
                new SelectionList()
                {
                    Items = new List<SelectionList.SelectionItem>()
                    {
                        new SelectionList.SelectionItem()
                        {
                            Name = "Aydınlık",
                            Action = new Action(()=>{  ThemeHandler.SetThemeSmooth("Light"); })
                        },
                        new SelectionList.SelectionItem()
                        {
                            Name = "Koyu",
                            Action = new Action(()=>{ThemeHandler.SetThemeSmooth("Dark"); })
                        },
                        new SelectionList.SelectionItem()
                        {
                            Name = "Şeffaf",
                            Action = new Action(()=>{ThemeHandler.SetThemeSmooth("Dark", "Transparent"); })
                        },

                    }
                }
            }
        };

       

        private static string LocalTitle = "App Theme Tab";

        private static string LocalDescription = "Choose the theme for The App that you want to use.";

        private static ResourceUrl LocalIconUrl = new ResourceUrl(new List<(string, string, ResourceType)>
        {
            ("Light", "./Assets/Light/Symbols/AppThemeSymbol.png", ResourceType.Relative),
            ("Dark", "./Assets/Dark/Symbols/AppThemeSymbol.png", ResourceType.Relative)
        });

        public static bool Changed;
    }
}

namespace NPCore.Tabs
{
    [MenuItem(typeof(DataTemplates.SettingsTab))]
    public class AppThemeTab : Tab
    {
        public string Title = NPCore.AppThemeTab.Title;
        
        public string Description = NPCore.AppThemeTab.Description;

        public ResourceUrl IconUrl = NPCore.AppThemeTab.IconUrl;

        public bool Changed = NPCore.AppThemeTab.Changed;

        public UIContent Content = NPCore.AppThemeTab.Content;

        public string ID = "SDATT";

    }
}
