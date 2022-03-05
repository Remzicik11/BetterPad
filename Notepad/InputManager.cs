using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Notepad
{
    public class InputManager
    {
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern short GetKeyState(int nVirtKey);

        private static List<KeyPressedData> KeyData = new List<KeyPressedData>();


        private static (bool Status, int Index) GetKeyData(int[] ID)
        {
            for (int i = 0; i < KeyData.Count; i++)
            {
                if (KeyData[i].KeyID == ID)
                {
                    return (KeyData[i].Status, i);
                }
            }

            return (false, -1);
        }

        public static bool GetKeyUp(params int[] Keys)
        {
            var keyData = GetKeyData(Keys);
            var PressedData = GetKeys(Keys);

            if (keyData.Index == -1)
            {
                KeyData.Add(new KeyPressedData() { KeyID = Keys });
            }
            else
            {
                KeyData[keyData.Index].Status = PressedData;
            }

            return keyData.Status != PressedData && !PressedData;
        }

        public static bool GetKeyDown(params int[] Keys)
        {
            var keyData = GetKeyData(Keys);
            var PressedData = GetKeys(Keys);

            if (keyData.Index == -1)
            {
                KeyData.Add(new KeyPressedData() { KeyID = Keys });
            }
            else
            {
                KeyData[keyData.Index].Status = PressedData;
            }

            return keyData.Status != PressedData && PressedData;
        }

        public static bool GetKey(int VirtKey)
        {
            return GetKeyState(VirtKey) < 0;
        }

        public static bool GetKeys(params int[] VirtKeys)
        {
            for (int i = 0; i < VirtKeys.Length; i++)
            {
                if (!GetKey(VirtKeys[i]))
                {
                    return false;
                }
            }
            return true;
        }


        public static void DisposeKeyPressedData()
        {
            KeyData.Clear();
        }

        private class KeyPressedData
        {
            public int[] KeyID;
            public bool Status;
        }
    }
}
