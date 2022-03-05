using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad
{
    class SettingsOptionHandler
    {
    }

    public partial class MainWindow : Window
    {
        private void expandMenu(object sender, MouseButtonEventArgs _event)
        {
            FrameworkElement senderObject = sender as FrameworkElement;
            var Root = (FrameworkElement)VisualTreeHelper.GetParent(senderObject);
            var MainRoot = (FrameworkElement)VisualTreeHelper.GetParent(Root);

            var expandArrow = (FrameworkElement)VisualTreeHelper.GetChild(Root, 1);
            int targetHeight = VisualTreeHelper.GetChildrenCount(MainRoot.getChild(0).getChild(1).getChild(0).getChild(0)) * 29 + 81;

            Storyboard storyboard = new Storyboard();
            storyboard.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                To = (expandArrow.RenderTransform as RotateTransform).Angle < 0 ? 0 : -90,
                Duration = storyboard.Duration,
                EasingFunction = new PowerEase()
            };

            DoubleAnimation GridHeightAnimation = new DoubleAnimation()
            {
                To = (expandArrow.RenderTransform as RotateTransform).Angle < 0 ? targetHeight : 61,
                Duration = storyboard.Duration,
                EasingFunction = new PowerEase()
            };

            //ThicknessAnimation expandAnimation = new ThicknessAnimation()
            //{
            //    To = (expandArrow.RenderTransform as RotateTransform).Angle < 0 ? new Thickness(0, 0, 0, 0) : new Thickness(0, 0, 0, 108),
            //    Duration = storyboard.Duration,
            //    EasingFunction = new PowerEase()
            //};

            Storyboard.SetTarget(rotateAnimation, expandArrow);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            MainRoot.BeginAnimation(Rectangle.HeightProperty, GridHeightAnimation);
            //expandGrid.BeginAnimation(Rectangle.MarginProperty, expandAnimation);

            storyboard.Children.Add(rotateAnimation);
            storyboard.Begin();



        }

        public void setOption(object sender)
        {
            FrameworkElement optionObject = sender as FrameworkElement;
            var Root = ((FrameworkElement)optionObject.Parent).Parent;

            var duration = TimeSpan.FromSeconds(0.3);

            DoubleAnimation widthAnimation = new DoubleAnimation()
            {
                Duration = duration,
                To = 0,
                EasingFunction = new PowerEase()
            };

            DoubleAnimation heightAnimation = new DoubleAnimation()
            {
                Duration = duration,
                To = 0,
                EasingFunction = new PowerEase()
            };

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(Root); i++)
            {
                var RootItem = VisualTreeHelper.GetChild(Root, i);
                var optionItem = (FrameworkElement)VisualTreeHelper.GetChild(RootItem, 1);

                if (RootItem == optionObject.Parent)
                {
                    widthAnimation.To = 10;
                    heightAnimation.To = 10;
                    optionItem.BeginAnimation(Rectangle.WidthProperty, widthAnimation);
                    optionItem.BeginAnimation(Rectangle.HeightProperty, heightAnimation);
                }
                else
                {
                    widthAnimation.To = 0;
                    heightAnimation.To = 0;
                    optionItem.BeginAnimation(Rectangle.WidthProperty, widthAnimation);
                    optionItem.BeginAnimation(Rectangle.HeightProperty, heightAnimation);
                }
            }

        }
    }

    public static class objectChild
    {
        public static FrameworkElement getChild(this UIElement obj, int index)
        {
            return (FrameworkElement)VisualTreeHelper.GetChild(obj, index);
        }
    }
}
