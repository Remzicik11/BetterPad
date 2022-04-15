using System;
using JustTools;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Notepad
{
    public class UserDataHandler
    {
        public static string RootPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BetterPad");
            }
        }

        private static string DataPath { get { return Path.Combine(RootPath, "Config."); } }

        private static List<Item> Items = new List<Item>();

        public static T Get<T>(string Key)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Key == Key)
                {
                    return (T)Items[i].Value;
                }
            }

            return default(T);
        }

        public static void Set(string Key, object Val)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Key == Key)
                {
                    Items[i].Value = Val;
                    Items[i].ValueType = Val.GetType();
                    return;
                }
            }

            Items.Add(new Item { Key = Key, Value = Val, ValueType = Val.GetType() });
        }

        public static void Delete(string Key)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Key == Key)
                {
                    Items.RemoveAt(i);
                }
            }
        }

        public static void Save()
        {
            Validate(DataPath);

            string result = "";

            for (int i = 0; i < Items.Count; i++)
            {
                result += Items[i].Key + ":" + Items[i].Value.ToString() + ":" + Items[i].ValueType + "\n";
            }

            File.WriteAllText(DataPath, result);
        }

        public static void Load()
        {
            Validate(DataPath);
            Items = new List<Item>();

            string[] data = File.ReadAllText(DataPath).Split("\n");


            for (int i = 0; i < data.Length; i++)
            {
                if (string.IsNullOrEmpty(data[i])) { continue; }
                var item = data[i].Split(":");
                
                Type valType = Type.GetType(item[2]);
                Items.Add(new Item() { Key = item[0], Value = Convert.ChangeType(item[1], valType), ValueType = valType });
                
            }
        }
        
        public static void Show()
        {
            Validate(DataPath);

            string result = "";

            for (int i = 0; i < Items.Count; i++)
            {
                result += Items[i].Key + ":" + Items[i].Value.ToString() + ":" + Items[i].ValueType + "\n";
            }

            Console.WriteLine(result);
        }

        public static void Validate(string TargetPath, string Default = "")
        {
            if (!File.Exists(TargetPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(TargetPath));
                File.WriteAllText(TargetPath, Default);
            }
        }

        public class Item
        {
            public string Key;
            public object Value;
            public Type ValueType;
        }
    }
}
