using System;
using System.Collections.Generic;
using System.Text;

namespace NPCore.Internal
{
    public static class Behaviours
    {
        public static List<(string Key, Action<object[]> Value, bool Dispose)> Items = new List<(string Key, Action<object[]> Action, bool Dispose)>();

        public static Action<object[]> GetBehaviour(string Key)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if(Items[i].Key == Key)
                {
                    Action<object[]> Val = Items[i].Value;
                    
                    if(Items[i].Dispose) { Items.RemoveAt(i); }
                    return Val;
                }
            }

            return null;
        }

        public static void AddBehaviour(string Key, Action<object[]> Value, bool Dispose = false)
        {
            Items.Add((Key, Value, Dispose));
        }

    }
}
