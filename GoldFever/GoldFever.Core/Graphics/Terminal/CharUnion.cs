using System;
using System.Runtime.InteropServices;

namespace GoldFever.Core.Graphics.Terminal
{
    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
        [FieldOffset(0)]
        public char UnicodeChar;
        [FieldOffset(0)]
        public byte AsciiChar;
    }
}
