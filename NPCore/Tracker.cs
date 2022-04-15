using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace NPCore
{
    public static class Tracker
    {
        public static List<(string ID, Type Type, ReferenceInfo[] References)> Memory = new List<(string ID, Type Type, ReferenceInfo[] References)>();

        public static void Track(string ID, object Target)
        {
            for (int i = 0; i < Memory.Count; i++)
            {
                if (Memory[i].ID == ID)
                {
                    for (int j = 0; j < Memory[i].References.Length; j++)
                    {
                        Console.WriteLine(Memory[i].References[j].Type + "|" + Memory[i].References[j].Values[0]);

                    }
                    return;
                }
            }

            var References = new List<ReferenceInfo>();
            GetData(ref References, Target);
            Memory.Add((ID, Target.GetType(), References.ToArray()));
        }

        private static void GetData(ref List<ReferenceInfo> References, object Target)
        {

            var GenericArguements = Target.GetType().GetGenericArguments();


            if (GenericArguements.Length > 0)
            {
                Type Interface = typeof(ICollection<>);

                Type generic = Interface.MakeGenericType(GenericArguements);
                if (generic.IsAssignableFrom(Target.GetType()))
                {
                    var method = typeof(Enumerable).GetMethod("ToArray");
                    method = method.MakeGenericMethod(GenericArguements);
                    Array array = (Array)method.Invoke(null, new object[] { Target });
                    if (array.Length > 0)
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            var Item = array.GetValue(i);
                            References.Add(new ReferenceInfo() { Type = ReferenceInfo.Types.ListIndex, Values = new object[] { Target, i } });
                            GetData(ref References, Item);
                        }
                    }
                    return;
                }
            }
            var InstanceFields = GetInstanceFields(Target);
            if (InstanceFields.Length > 0)
            {
                for (int i = 0; i < InstanceFields.Length; i++)
                {
                    References.Add(new ReferenceInfo() { Type = ReferenceInfo.Types.Field, Values = new object[] { InstanceFields[i] } });
                    GetData(ref References, InstanceFields[i]);
                }
                return;
            }
        } 



        private static FieldInfo[] GetInstanceFields(object Value)
        {
            var FieldList = Value.GetType().GetFields();
            List<FieldInfo> NonStaticFields = new List<FieldInfo>();

            for (int i = 0; i < FieldList.Length; i++)
            {
                if (!FieldList[i].IsStatic)
                {
                    NonStaticFields.Add(FieldList[i]);
                }
            }

            return NonStaticFields.ToArray();
        }

        public static Array ToArray(this object Target)
        {
            var GenericArguements = Target.GetType().GetGenericArguments();

            var method = typeof(Enumerable).GetMethod("ToArray");
            method = method.MakeGenericMethod(GenericArguements);

            return (Array)method.Invoke(null, new object[] { Target });
        }
    }

    public class ReferenceInfo
    {
        public Types Type;

        public object[] Values;

        public enum Types
        {
            Field,
            ListIndex
        }
    }
}