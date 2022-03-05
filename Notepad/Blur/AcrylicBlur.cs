using System;
using System.Runtime.InteropServices;

namespace BlurredLoginUIWindow.Class
{
    public enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 5
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct AccentPolicy
    {
        public AccentState AccentState;
        public uint AccentFlags;
        public uint GradientColor;
        public uint AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }
    internal enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }
}