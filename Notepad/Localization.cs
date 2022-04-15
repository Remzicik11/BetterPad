using System;
using System.Collections.Generic;
using System.Text;

namespace AppLocalizaton
{
    public class Localization
    {
        public static string CurrentLanguage { get; set; } = LocalizationConfig.DefaultLanguage;
    }

    public class Language
    {
        public string Name { get; set; }
        public List<Item> Items = new List<Item>();
    }

    public class Item
    {
        public string ID { get; set; }
        public string Value { get; set; }

        public Item(string ID, string Value)
        {
            this.ID = ID;
            this.Value = Value;
        }
    }
}
