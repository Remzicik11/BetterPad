using System;
using NPCore;
using NPCore.DataTemplates;
using System.Collections.Generic;
using System.Text;

namespace Notepad
{
    public static class Tabs
    {
        public static void Load(IMenuItem[] Tabs, int Priority)
        {
            List<SettingsTab> SettingsTabs = new List<SettingsTab>();

            for (int i = 0; i < Tabs.Length; i++)
            {
                if (Tabs[i].GetType() == typeof(SettingsTab))
                {
                    SettingsTabs.Add(Tabs[i] as SettingsTab);
                }
            }

            SettingsMenuManager.LoadTabs(SettingsTabs, Priority);
        }
    }
}
