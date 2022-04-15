using System;
using NPCore;
using NPCore.UIControls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Notepad
{
    public static class ResourceConverter
    {
        public static BitmapImage ConvertToImageSource(this ResourceUrl ResourceUrl, string Theme)
        {
            for (int i = 0; i < ResourceUrl.Items.Count; i++)
            {
                if (ResourceUrl.Items[i].Item1 == "<ALL>" || ResourceUrl.Items[i].Item1 == Theme)
                {
                    UriKind TargetType = UriKind.Relative;

                    if (ResourceUrl.Items[i].Item3 == ResourceType.Absolute)
                    {
                        TargetType = UriKind.Absolute;
                    }
                    else if (ResourceUrl.Items[i].Item3 == ResourceType.Both)
                    {
                        TargetType = UriKind.RelativeOrAbsolute;
                    }

                    return new BitmapImage(new Uri(ResourceUrl.Items[i].Item2, TargetType));
                }
            }

            return null;
        }
    }
}
