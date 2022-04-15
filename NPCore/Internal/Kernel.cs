using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace NPCore.Internal
{
    public abstract class Kernel
    {
        public static Assembly Module { get; private set; }

        public static void Main(Assembly Module)
        {
            Kernel.Module = Module;
            var Item = new TestItem() { Name = "Item", Val = new TestItemVal() { Value = "ChildItem" } };
         
            Tracker.Track("Test", Item);
        }

        public static IMenuItem[] GetTabs()
        {
            var Tabs = ReflectiveEnumerator.GetEnumerableOfType<Tab>() as List<Tab>;

            var result = new List<IMenuItem>(Tabs.Count);

            for (int i = 0; i < Tabs.Count; i++)
            {

                if (!(bool)Tabs[i].GetType().GetField("Changed").GetValue(Tabs[i])) { result.Capacity--; continue; }

                var ItemAttribute = (MenuItem)Attribute.GetCustomAttribute(Tabs[i].GetType(), typeof(MenuItem));

                var Item = (IMenuItem)Activator.CreateInstance(ItemAttribute.TargetType);

                Item.ImportFields(Tabs[i], true);
                result.Add(Item);
            }

            return result.ToArray();
        }


        public static IMenuItem[] GetCustomTabs()
        {
            var Tabs = ReflectiveEnumerator.GetEnumerableOfType<CustomTab>(Module) as List<CustomTab>;

            if (Tabs == null) { return null; }

            var result = new IMenuItem[Tabs.Count];

            for (int i = 0; i < Tabs.Count; i++)
            {
                var ItemAttribute = (MenuItem)Attribute.GetCustomAttribute(Tabs[i].GetType(), typeof(MenuItem));

                var Item = (IMenuItem)Activator.CreateInstance(ItemAttribute.TargetType);

                Item.ImportFields(Tabs[i], false);
                result[i] = Item;
            }
            return result;
        }

        private static void BeginInit()
        {
            var list = ReflectiveEnumerator.GetEnumerableOfType<Script>(Module) as List<Script>;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Init();
            }
        }

    }


    public class TestItem
    {
        public string Name;
        public TestItemVal Val;
        public List<string> List = new List<string>() { "Item1", "Item2" };
    }

    public class TestItemVal
    {
        public string Value;
    }


    public static class ObjectExtension
    {
        public static T CopyObject<T>(this object objSource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(stream, objSource);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }

}
