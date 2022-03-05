using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad
{
    public static class ThemeHandler
    {
        private static List<string> Params = new List<string>();
        private static MainWindow window = Application.Current.MainWindow as MainWindow;

        public static List<Theme> Themes = new List<Theme>()
        {
            new Theme()
            {
                Name = "Light",
                ThemeDataItems = new List<IThemeDataItem>()
                {
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:SettingsBackground]"),

                        brush = new SolidColorBrush(Color.FromRgb(238, 244, 248))
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:Background]"),

                        brush = new SolidColorBrush(Colors.White)
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:editorMenuBar]"),

                          brush = new SolidColorBrush(Color.FromRgb(238, 244, 248))
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:SettingsMenuBar]"),

                        brush = new SolidColorBrush()
                        {
                            Color = Color.FromRgb(255,255,255)
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:SettingsMenuBackground]"),

                        brush = new SolidColorBrush()
                        {
                            Color = Color.FromRgb(249,249,249)
                        },
                        foreGround = new SolidColorBrush(Colors.Black)
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:Text]"),

                        brush = new SolidColorBrush()
                        {
                            Color = Colors.Black
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:ToolBarButton]"),

                        foreGround = new SolidColorBrush()
                        {
                            Color = Colors.Black
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:ExpandButton]"),

                        brush = new SolidColorBrush()
                        {
                            Color = Color.FromRgb(231, 236, 240)
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new ParticularObject() { Object = window.InputText},

                        foreGround = new SolidColorBrush()
                        {
                            Color = Colors.Black
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new ParticularObject() { Object = window.InputText},

                        foreGround = new SolidColorBrush()
                        {
                            Color = Colors.Black
                        }
                    },
                    new MultiThemeImageDataItem()
                    {
                        Items = new List<ThemeImageDataItem>()
                        {
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:SettingsImage]"),
                                source = new BitmapImage(new Uri("./Assets/Light/Symbols/GearSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:AppThemeImage]"),
                                source = new BitmapImage(new Uri("./Assets/Light/Symbols/AppThemeSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:ArrowImage]"),
                                source = new BitmapImage(new Uri("./Assets/Light/Symbols/ArrowSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:BackArrowImage]"),
                                source = new BitmapImage(new Uri("./Assets/Light/Symbols/BackSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:MinimizeImage]"),
                                source = new BitmapImage(new Uri("./Assets/Light/Symbols/MinimizeSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:MaximizeImage]"),
                                source = new BitmapImage(new Uri("./Assets/Light/Symbols/MaximizeSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:CloseImage]"),
                                source = new BitmapImage(new Uri("./Assets/Light/Symbols/CloseSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:AdvancedSettingsImage]"),
                                source = new BitmapImage(new Uri("./Assets/Light/Symbols/AdvancedSettingsSymbol.png", UriKind.Relative))
                             }
                        }
                    }
                }
            },
            new Theme()
            {
                Name = "Dark",
                ThemeDataItems = new List<IThemeDataItem>()
                {
                    new ThemeDataItem()
                    {
                        Condition = new ConditionClass(()=> { return Params.Contains("Transparent"); }),

                        Object = new TaggedThemeObject("[Theme:SettingsBackground]", "[Theme:Background]"),

                        brush = new LinearGradientBrush()
                        {
                            StartPoint = new Point(0.5,0),
                            EndPoint = new Point(1,1),
                            GradientStops = new GradientStopCollection()
                            {
                                new GradientStop()
                                {
                                    Color = Color.FromArgb(210,16,18,23),
                                },
                                new GradientStop()
                                {
                                    Color = Color.FromRgb(34,28,23),
                                    Offset = 1
                                }
                            }
                        }
                    },
                    new ThemeDataItem()
                    {
                        Condition = new ConditionClass(()=> { return !Params.Contains("Transparent"); }),

                        Object = new TaggedThemeObject("[Theme:SettingsBackground]", "[Theme:Background]"),

                        brush = new LinearGradientBrush()
                        {
                            StartPoint = new Point(0.5,0),
                            EndPoint = new Point(1,1),
                            GradientStops = new GradientStopCollection()
                            {
                                new GradientStop()
                                {
                                    Color = Color.FromRgb(16,18,23),
                                },
                                new GradientStop()
                                {
                                    Color = Color.FromRgb(34,28,23),
                                    Offset = 1
                                }
                            }
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:editorMenuBar]"),

                        brush = new LinearGradientBrush()
                        {
                            StartPoint = new Point(0.5,0),
                            EndPoint = new Point(1,1),
                            GradientStops = new GradientStopCollection()
                            {
                                new GradientStop()
                                {
                                    Color = Color.FromArgb(228,18,20,25),
                                    Offset = 1
                                },
                                new GradientStop()
                                {
                                    Color = Color.FromArgb(228,17,22,25),
                                }
                            }
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:SettingsMenuBar]"),

                        brush = new SolidColorBrush()
                        {
                            Color = Color.FromRgb(39,43,55)
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:SettingsMenuBackground]"),

                        brush = new SolidColorBrush()
                        {
                            Color = Color.FromRgb(34,37,47)
                        },
                        foreGround = new SolidColorBrush(Colors.White)
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:Text]"),

                        brush = new SolidColorBrush()
                        {
                            Color = Colors.White
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:ToolBarButton]"),

                        foreGround = new SolidColorBrush()
                        {
                            Color = Colors.White
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:ExpandButton]"),

                        brush = new SolidColorBrush()
                        {
                            Color = Color.FromArgb(12, 231, 236, 240)
                        }
                    },
                    new ThemeDataItem()
                    {
                        Object = new ParticularObject() { Object = window.InputText},

                        foreGround = new SolidColorBrush()
                        {
                            Color = Colors.White
                        }
                    },
                    new ThemeStyleDataItem()
                    {
                        Object = new TaggedThemeObject("[Theme:ToolBarButton]"),
                        StyleName = "DarkBorderButton"
                    },
                    new MultiThemeImageDataItem()
                    {
                        Items = new List<ThemeImageDataItem>()
                        {
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:SettingsImage]"),
                                source = new BitmapImage(new Uri("./Assets/Dark/Symbols/GearSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:AppThemeImage]"),
                                source = new BitmapImage(new Uri("./Assets/Dark/Symbols/AppThemeSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:ArrowImage]"),
                                source = new BitmapImage(new Uri("./Assets/Dark/Symbols/ArrowSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:BackArrowImage]"),
                                source = new BitmapImage(new Uri("./Assets/Dark/Symbols/BackSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:MinimizeImage]"),
                                source = new BitmapImage(new Uri("./Assets/Dark/Symbols/MinimizeSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:MaximizeImage]"),
                                source = new BitmapImage(new Uri("./Assets/Dark/Symbols/MaximizeSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:CloseImage]"),
                                source = new BitmapImage(new Uri("./Assets/Dark/Symbols/CloseSymbol.png", UriKind.Relative))
                             },
                             new ThemeImageDataItem()
                             {
                                Object = new TaggedThemeObject("[Theme:AdvancedSettingsImage]"),
                                source = new BitmapImage(new Uri("./Assets/Dark/Symbols/AdvancedSettingsSymbol.png", UriKind.Relative))
                             }
                        }
                    }


                }
            }
        };

        public static void SetTheme(string Name, string param = "")
        {
            string ThemeName = Name;

            if (!string.IsNullOrEmpty(param))
            {
                if (!Params.Contains(param))
                {
                    Params.Add(param);
                }
                else
                {
                    Params.Remove(param);
                }
            }

            for (int i = 0; i < Themes.Count; i++)
            {
                if (Themes[i].Name == ThemeName)
                {
                    for (int j = 0; j < Themes[i].ThemeDataItems.Count; j++)
                    {
                        if (Themes[i].ThemeDataItems[j].Condition != null && !Themes[i].ThemeDataItems[j].Condition.Invoke()) { continue; }
                        if (Themes[i].ThemeDataItems[j] is ThemeDataItem)
                        {
                            var item = Themes[i].ThemeDataItems[j] as ThemeDataItem;

                            if (item.Object is TaggedThemeObject)
                            {
                                foreach (var itemObject in MainWindow.FindVisualChildren<FrameworkElement>(window.WindowGrid))
                                {
                                    for (int k = 0; k < (item.Object as TaggedThemeObject).Tag.Length; k++)
                                    {
                                        if (itemObject.Tag != null && itemObject.Tag.ToString().Contains((item.Object as TaggedThemeObject).Tag[k]))
                                        {
                                            setThemeObject(item, itemObject);
                                        }
                                    }
                                }
                            }
                            else if (item.Object is ParticularObject)
                            {
                                setThemeObject(item, (item.Object as ParticularObject).Object);
                            }
                            else if (item.Object is ThemeObjectList)
                            {
                                for (int k = 0; k < (item.Object as ThemeObjectList).Objects.Count; k++)
                                {
                                    setThemeObject(item, (item.Object as ThemeObjectList).Objects[k]);
                                }
                            }
                            else
                            {
                                throw new FormatException("Missmatch Item at: index [" + i + "]");
                            }
                        }
                        else if (Themes[i].ThemeDataItems[j] is ThemeImageDataItem)
                        {
                            var item = Themes[i].ThemeDataItems[j] as ThemeImageDataItem;

                            if (item.Object is TaggedThemeObject)
                            {
                                foreach (var itemObject in MainWindow.FindVisualChildren<FrameworkElement>(window.WindowGrid))
                                {
                                    for (int k = 0; k < (item.Object as TaggedThemeObject).Tag.Length; k++)
                                    {
                                        if (itemObject.Tag != null && itemObject.Tag.ToString().Contains((item.Object as TaggedThemeObject).Tag[k]))
                                        {
                                            (itemObject as Image).Source = item.source;
                                        }
                                    }
                                }

                                foreach (var itemObject in MainWindow.FindVisualChildren<Button>(window.WindowGrid))
                                {
                                    for (int k = 0; k < (item.Object as TaggedThemeObject).Tag.Length; k++)
                                    {
                                        if (itemObject.Content is Image && (itemObject.Content as Image).Tag != null && (itemObject.Content as Image).Tag.ToString().Contains((item.Object as TaggedThemeObject).Tag[k]))
                                        {
                                            (itemObject.Content as Image).Source = item.source;
                                        }
                                    }
                                }
                            }
                            else if (item.Object is ParticularObject<Image>)
                            {
                                (item.Object as ParticularObject<Image>).Object.Source = item.source;
                            }
                            else if (item.Object is ThemeObjectList<Image>)
                            {
                                for (int k = 0; k < (item.Object as ThemeObjectList).Objects.Count; k++)
                                {
                                    (item.Object as ThemeObjectList<Image>).Objects[k].Source = item.source;
                                }
                            }
                            else
                            {
                                throw new FormatException("Missmatch Item at: index [" + i + "]");
                            }
                        }
                        else if (Themes[i].ThemeDataItems[j] is MultiThemeImageDataItem)
                        {
                            for (int l = 0; l < (Themes[i].ThemeDataItems[j] as MultiThemeImageDataItem).Items.Count; l++)
                            {
                                var item = (Themes[i].ThemeDataItems[j] as MultiThemeImageDataItem).Items[l] as ThemeImageDataItem;

                                if (item.Object is TaggedThemeObject)
                                {
                                    foreach (var itemObject in MainWindow.FindVisualChildren<Image>(window.WindowGrid))
                                    {
                                        for (int k = 0; k < (item.Object as TaggedThemeObject).Tag.Length; k++)
                                        {
                                            if (itemObject.Tag != null && itemObject.Tag.ToString().Contains((item.Object as TaggedThemeObject).Tag[k]))
                                            {
                                                (itemObject as Image).Source = item.source;
                                            }
                                        }
                                    }

                                    foreach (var itemObject in MainWindow.FindVisualChildren<Button>(window.WindowGrid))
                                    {
                                        for (int k = 0; k < (item.Object as TaggedThemeObject).Tag.Length; k++)
                                        {
                                            if (itemObject.Content is Image && (itemObject.Content as Image).Tag != null && (itemObject.Content as Image).Tag.ToString().Contains((item.Object as TaggedThemeObject).Tag[k]))
                                            {
                                                (itemObject.Content as Image).Source = item.source;
                                            }
                                        }
                                    }
                                }
                                else if (item.Object is ParticularObject<Image>)
                                {
                                    (item.Object as ParticularObject<Image>).Object.Source = item.source;
                                }
                                else if (item.Object is ThemeObjectList<Image>)
                                {
                                    for (int k = 0; k < (item.Object as ThemeObjectList).Objects.Count; k++)
                                    {
                                        (item.Object as ThemeObjectList<Image>).Objects[k].Source = item.source;
                                    }
                                }
                                else
                                {
                                    throw new FormatException("Missmatch Item at: index [" + i + "]");
                                }
                            }

                        }
                        else if (Themes[i].ThemeDataItems[j] is ThemeStyleDataItem)
                        {
                            var item = Themes[i].ThemeDataItems[j] as ThemeStyleDataItem;

                            if (item.Object is TaggedThemeObject)
                            {
                                foreach (var itemObject in MainWindow.FindVisualChildren<FrameworkElement>(window.WindowGrid))
                                {
                                    for (int k = 0; k < (item.Object as TaggedThemeObject).Tag.Length; k++)
                                    {
                                        if (itemObject.Tag != null && itemObject.Tag.ToString().Contains((item.Object as TaggedThemeObject).Tag[k]))
                                        {
                                            itemObject.Style = (Style)window.FindResource(item.StyleName);
                                        }
                                    }
                                }
                            }
                            else if (item.Object is ParticularObject)
                            {
                                (item.Object as ParticularObject).Object.Style = (Style)window.FindResource(item.StyleName);
                            }
                            else if (item.Object is ThemeObjectList)
                            {
                                for (int k = 0; k < (item.Object as ThemeObjectList).Objects.Count; k++)
                                {
                                    (item.Object as ThemeObjectList).Objects[k].Style = (Style)window.FindResource(item.StyleName);
                                }
                            }
                            else
                            {
                                throw new FormatException("Missmatch Item at: index [" + i + "]");
                            }
                        }

                        //item.Object.Opacity = item.opacity ?? item.Object.Opacity;

                        //if (item.Object is Rectangle)
                        //{
                        //    (item.Object as Rectangle).Fill = item.brush ?? (item.Object as Rectangle).Fill;
                        //}
                    }
                }
            }
        }

        private static void setThemeObject(IThemeDataItem dataItem, FrameworkElement targetObject)
        {
            if (dataItem is ThemeDataItem)
            {
                var item = dataItem as ThemeDataItem;

                targetObject.Opacity = item.opacity ?? targetObject.Opacity;

                if (targetObject is Rectangle)
                {
                    (targetObject as Rectangle).Fill = item.brush ?? (targetObject as Rectangle).Fill;
                }
                else if (targetObject is Border)
                {
                    (targetObject as Border).Background = item.brush ?? (targetObject as Border).Background;
                }
                else if (targetObject is TextBlock)
                {
                    (targetObject as TextBlock).Foreground = item.brush ?? (targetObject as TextBlock).Foreground;
                }
                else if (targetObject is Button)
                {
                    (targetObject as Button).Background = item.brush ?? (targetObject as Button).Background;
                    (targetObject as Button).Foreground = item.foreGround ?? (targetObject as Button).Foreground;
                }
                else if (targetObject is TextBox)
                {
                    (targetObject as TextBox).Background = item.brush ?? (targetObject as TextBox).Background;
                    (targetObject as TextBox).Foreground = item.foreGround ?? (targetObject as TextBox).Foreground;
                    (targetObject as TextBox).CaretBrush = item.foreGround ?? (targetObject as TextBox).CaretBrush;
                }
            }
            else if (dataItem is ThemeImageDataItem)
            {
                var item = dataItem as ThemeImageDataItem;

                (targetObject as Image).Source = item.source ?? (targetObject as Image).Source;
            }
            else if (dataItem is ThemeStyleDataItem)
            {

            }


        }

        public class Theme
        {
            public string Name;
            public List<IThemeDataItem> ThemeDataItems;
        }


        #region ThemeItem


        public class IThemeDataItem
        {
            public ConditionClass Condition;
        }

        public delegate bool ConditionClass();

        public class ThemeDataItem : IThemeDataItem
        {
            public IThemeObject Object;
            public Brush brush;
            public Brush foreGround;
            public double? opacity;
        }

        public class ThemeImageDataItem : IThemeDataItem
        {
            public IThemeObject<Image> Object;
            public BitmapImage source;
        }

        public class ThemeStyleDataItem : IThemeDataItem
        {
            public IThemeObject Object;
            public string StyleName;
        }

        public class MultiThemeImageDataItem : IThemeDataItem
        {
            public List<ThemeImageDataItem> Items;
        }
        #endregion



        public interface IThemeObject { }
        public interface IThemeObject<T> { }




        public class ParticularObject : IThemeObject
        {
            public FrameworkElement Object;

            public ParticularObject() { }

            public ParticularObject(FrameworkElement Object)
            {
                this.Object = Object;
            }
        }

        public class ThemeObjectList : IThemeObject
        {
            public List<FrameworkElement> Objects;
        }

        public class TaggedThemeObject : IThemeObject, IThemeObject<Image>
        {
            public string[] Tag;

            public TaggedThemeObject(params string[] Tag)
            {
                this.Tag = Tag;
            }
        }


        public class ParticularObject<DataType> : IThemeObject<DataType>
        {
            public DataType Object;
        }

        public class ThemeObjectList<DataType> : IThemeObject<DataType>
        {
            public List<DataType> Objects;
        }
    }

    public partial class MainWindow : Window
    {

        public void SetTheme(string Theme, string param)
        {
            var duration = TimeSpan.FromSeconds(0.491);
            var offset = 3;

            var opacityAnimation = new DoubleAnimation()
            {
                To = 0,
                Duration = duration
            };

            var transitionOpacityAnimation = new DoubleAnimation()
            {
                To = 1,
                Duration = duration
            };

            var marginAnimation = new ThicknessAnimation()
            {
                Duration = duration
            };

            foreach (var item in FindVisualChildren<FrameworkElement>(WindowGrid))
            {
                if (!(item.Tag != null && item.Tag.ToString().Contains("TOA_Locked")))
                {
                    item.BeginAnimation(Rectangle.OpacityProperty, opacityAnimation);
                    marginAnimation.To = new Thickness(item.Margin.Left, item.Margin.Top + offset, item.Margin.Right, item.Margin.Bottom);
                    item.BeginAnimation(Rectangle.MarginProperty, marginAnimation);
                    this.Dispatcher.Invoke(() => { item.Tag = (item.Tag ?? "").ToString() + "[waitingOpacity:" + item.Opacity + "]"; });
                }
            }

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(duration);
                this.Dispatcher.Invoke(() =>
                {
                    transitionOpacityAnimation.To = 1;
                    Transition.BeginAnimation(Rectangle.OpacityProperty, transitionOpacityAnimation);
                });
                System.Threading.Thread.Sleep(duration);
                this.Dispatcher.Invoke(() =>
                {
                    ThemeHandler.SetTheme(Theme, param);
                    transitionOpacityAnimation.To = 0;
                    Transition.BeginAnimation(Rectangle.OpacityProperty, transitionOpacityAnimation);
                });
                System.Threading.Thread.Sleep(duration);
                this.Dispatcher.Invoke(() =>
                {
                    opacityAnimation.To = 1;
                    foreach (var item in FindVisualChildren<FrameworkElement>(WindowGrid))
                    {
                        if (item.Tag != null && item.Tag.ToString().Contains("[waitingOpacity:"))
                        {
                            try
                            {
                                double opacity = double.Parse(item.Tag.ToString().Split("[waitingOpacity:")[1].Split("]")[0]);
                                opacityAnimation.To = opacity;
                                item.Tag = item.Tag.ToString().Replace("[waitingOpacity:" + opacity + "]", "");
                            }
                            catch
                            {
                                opacityAnimation.To = 1;
                            }



                            item.BeginAnimation(Rectangle.OpacityProperty, opacityAnimation);
                            marginAnimation.To = new Thickness(item.Margin.Left, item.Margin.Top - offset, item.Margin.Right, item.Margin.Bottom);
                            item.BeginAnimation(Rectangle.MarginProperty, marginAnimation);

                        }
                    }
                });

            }).Start();
        }
    }
}
