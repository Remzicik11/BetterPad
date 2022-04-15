using System;
using NPCore.Internal;
using System.Collections.Generic;
using System.Text;

namespace NPCore
{
    public static class ThemeHandler
    {
        public static void SetTheme(string ThemeName, string Params = "")
        {
            Behaviours.GetBehaviour("SetTheme").Invoke(new object[] { ThemeName, Params });
        }

        public static void SetThemeSmooth(string ThemeName, string Params = "")
        {
            Behaviours.GetBehaviour("SetThemeSmooth").Invoke(new object[] { ThemeName, Params });
        }
    }
}
