using System;
using System.Collections.Generic;
using System.Text;

namespace AppLocalizaton
{
    public static class LocalizationConfig
    {
        public static string DefaultLanguage = "tr";

        public static List<Language> Languages = new List<Language>()
        {
            new Language
            {
                Name = "tr",
                Items = new List<Item>()
                {
                    new Item("test", "dw")
                }
            },

            new Language()
            {
                Name = "en",
                Items = new List<Item>()
                {
                    new Item("test", "dw")
                }
            }
        };
    }
}
