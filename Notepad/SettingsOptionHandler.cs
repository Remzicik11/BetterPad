using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

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
            var Root = senderObject.getParent();
            var MainRoot = Root.getParent();


            var ItemsRoot = MainRoot.getChild(0).getChild(1);
            var ContentHeight = 0;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(ItemsRoot); i++)
            {
                var ItemStack = ItemsRoot.getChild(i).getChild(0).getChild(0);
                ContentHeight += (int)ItemStack.ActualHeight;
            }

            var expandArrow = Root.getChild(2);

            Storyboard storyboard = new Storyboard();
            storyboard.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                To = MainRoot.Height > 61 ? -90 : 0,
                Duration = storyboard.Duration,
                EasingFunction = new PowerEase()
            };

            DoubleAnimation GridHeightAnimation = new DoubleAnimation()
            {
                To = 61 + (MainRoot.Height > 61 ? 0 : ContentHeight),
                Duration = storyboard.Duration,
                EasingFunction = new PowerEase()
            };

            Storyboard.SetTarget(rotateAnimation, expandArrow);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            MainRoot.BeginAnimation(Rectangle.HeightProperty, GridHeightAnimation);

            storyboard.Children.Add(rotateAnimation);
            storyboard.Begin();



        }

        public static void setOption(object sender)
        {
            var senderObject = sender as FrameworkElement;
            var Selection = senderObject.getParent(1);
            var Content = Selection.getParent(7);
            var Tab = Content.getParent(7);
            var SelectionList = SettingsMenuManager.list[(int)Tab.Tag].Content[(int)Content.Tag].Content as NPCore.UIControls.SelectionList;


            int SelectionIndex = (int)Selection.Tag;

            if (SelectionList.Items[SelectionIndex].Action != null) { SelectionList.Items[SelectionIndex].Action.Invoke(); }
            SelectionList.Selection = SelectionIndex;





            var Animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.3)
            };

            var SelectionsGroup = Selection.getParent(2);

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(SelectionsGroup); i++)
            {
                var Target = SelectionsGroup.getChild(i).getChild(0).getChild(1);

                Animation.To = i == SelectionIndex ? 10 : 0;
                Target.BeginAnimation(WidthProperty, Animation);
                Target.BeginAnimation(HeightProperty, Animation);
            }

        }
    }

    public static class objectChild
    {
        public static FrameworkElement getChild(this UIElement obj, int index)
        {
            return (FrameworkElement)VisualTreeHelper.GetChild(obj, index);
        }

        public static FrameworkElement getParent(this UIElement obj, int Length = 1)
        {
            var result = obj;
            for (int i = 0; i < Length; i++)
            {
                result = (UIElement)VisualTreeHelper.GetParent(result);
            }

            return (FrameworkElement)result;
        }
    }
}
