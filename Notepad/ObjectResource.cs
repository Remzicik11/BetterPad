using System;
using System.Windows;
using NPCore;

namespace Notepad
{
    public class ApplicationObject : DependencyObject
    {
        public static readonly DependencyProperty TargetResource =
        DependencyProperty.RegisterAttached(
          "TargetResource",
          typeof(ResourceUrl),
          typeof(ApplicationObject)
        );

        public static void SetTargetResource(UIElement element, ResourceUrl value)
        {
            element.SetValue(TargetResource, value);
        }

        public static ResourceUrl GetTargetResource(UIElement element)
        {
            return (ResourceUrl)element.GetValue(TargetResource);
        }
    }

    public static class ApplicationObjectResource
    {
        public static void SetTargetResource(this UIElement element, ResourceUrl Url)
        {
            ApplicationObject.SetTargetResource(element, Url);
        }

        public static ResourceUrl GetTargetResource(this UIElement element) => ApplicationObject.GetTargetResource(element);

    }
}
