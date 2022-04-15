using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad
{
    public partial class MainWindow : Window
    {
        private void Drag(object sender, MouseButtonEventArgs _event)
        {
            if (_event.ChangedButton == MouseButton.Left)
            {
                this.WindowState = WindowState.Normal;
                this.DragMove();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs _event)
        {
            WindowGrid.Margin = this.WindowState == WindowState.Maximized ? new Thickness(4, 4, 4, 4) : new Thickness(0, 0, 0, 0);
        }

        private void MaximizeWindow(object sender, MouseButtonEventArgs _event)
        {
            this.BeginAnimation(
                 OpacityProperty,
                 new System.Windows.Media.Animation.DoubleAnimation()
                 {
                     To = 0,
                     EasingFunction = new System.Windows.Media.Animation.PowerEase() { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut },
                     Duration = TimeSpan.FromSeconds(0.18)
                 }
             );


            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(300);

                Dispatcher.Invoke(() =>
                {
                    BeginAnimation(OpacityProperty, new System.Windows.Media.Animation.DoubleAnimation() { To = 1, Duration = TimeSpan.FromSeconds(0.3) });
                    WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                });
            }).Start();
        }

        private void MinimizeWindow(object sender, MouseButtonEventArgs _event)
        {
            this.BeginAnimation(
                 OpacityProperty,
                 new System.Windows.Media.Animation.DoubleAnimation()
                 {
                     To = 0,
                     EasingFunction = new System.Windows.Media.Animation.PowerEase() { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut },
                     Duration = TimeSpan.FromSeconds(0.2)
                 }
             );

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(300);

                Dispatcher.Invoke(() =>
                {
                    BeginAnimation(OpacityProperty, new System.Windows.Media.Animation.DoubleAnimation() { To = 1, Duration = TimeSpan.FromSeconds(0) });
                    WindowState = WindowState.Minimized;
                });
            }).Start();
        }

        private FrameworkElement GetObjectByTag(string Tag)
        {
            foreach (FrameworkElement obj in FindVisualChildren<UIElement>(this))
            {
                if (obj.Tag != null && obj.Tag.ToString().Contains(Tag))
                {
                    return obj;
                }
            }
            return null;
        }

        private void MultiTrigger(object sender, MouseButtonEventArgs _event)
        {
            var senderObject = sender as FrameworkElement;
            if (senderObject.Tag != null && senderObject.Tag.ToString().Contains("MultiTrigger:["))
            {
                var list = senderObject.Tag.ToString().Split("MultiTrigger:[")[1].Split("]")[0].Split(",");

                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i].Contains(">"))
                    {
                        var paramList = list[i].Split(">");


                        object[] paramsObject = new object[paramList.Length - 1];

                        for (int j = 1; j < paramList.Length; j++)
                        {
                            if (paramList[j] == "this")
                            {
                                paramsObject[j - 1] = sender;
                            }
                            else if (paramList[j] != "null")
                            {
                                paramsObject[j - 1] = paramList[j];
                            }
                        }
                        typeof(MainWindow).GetMethod(paramList[0]).Invoke(this, paramsObject);

                    }
                    else
                    {
                        typeof(MainWindow).GetMethod(list[i]).Invoke(this, null);
                    }
                }
            }
        }

        public void Close(object sender, MouseButtonEventArgs _event)
        {
            this.BeginAnimation(
                 OpacityProperty,
                 new System.Windows.Media.Animation.DoubleAnimation()
                 {
                     From = 1,
                     To = 0,
                     EasingFunction = new System.Windows.Media.Animation.PowerEase() { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut },
                     Duration = TimeSpan.FromSeconds(0.18)
                 }
             );

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(180);
                Environment.Exit(0);
            }).Start();
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
            {
                yield return null;
            }
            else
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }

        public static void SetAccentState(BlurredLoginUIWindow.Class.AccentState state)
        {
            Application.Current.MainWindow.DataContext = new BlurredLoginUIWindow.Class.WindowBlureffect(Application.Current.MainWindow, state) { BlurOpacity = 100 };
        }
    }
}
