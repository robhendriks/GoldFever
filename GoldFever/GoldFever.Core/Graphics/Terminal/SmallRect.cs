using System;
using System.Runtime.InteropServices;

namespace GoldFever.Core.Graphics.Terminal
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }
}
