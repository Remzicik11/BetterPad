using System;
using NPCore.Internal;

namespace NPCore.DataTemplates
{
    public class SettingsTab : IMenuItem
    {
        public string Title;

        [Optional] public ResourceUrl IconUrl;

        [Optional] public string Description;

        [Optional] public UIContent Content;

        [Optional] public string ID;
    }
}
