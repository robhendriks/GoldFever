using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace GoldFever.Core.Graphics.Terminal
{
    public sealed class DoubleBuffer
    {
        private const int Width = 80,
                          Height = 25;

        private static DoubleBuffer _instance;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutput(
            SafeFileHandle hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

        private CharInfo[] _buffer;

        public CharInfo[] Buffer
        {
            get { return _buffer; }
        }

        public CharInfo this[int i]
        {
            get { return _buffer[i]; }
            set { _buffer[i] = value; }
        }

        private SafeFileHandle _handle;

        public SafeFileHandle Handle
        {
            get { return _handle; }
        }

        private SmallRect region;
        private Coord position, size;

        private DoubleBuffer()
        {
            _buffer = new CharInfo[Width * Height];
            _handle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

            region = new SmallRect()
            {
                Left = 0,
                Top = 0,
                Right = Width,
                Bottom = Height
            };

            position = new Coord(0, 0);
            size = new Coord(Width, Height);
        }

        public void Write(int x, int y, CharInfo info)
        {
            int offset = (y * Width) + x;

            _buffer[offset] = info;
        }

        public void Write(int x, int y, int length, CharInfo info)
        {
            int offset = (y * Width) + x;

            for (int i = 0; i < length; i++)
                _buffer[offset + i] = info;
        }

        public void Write(string str, int x, int y, CharInfo info)
        {
            var bytes = Encoding.ASCII.GetBytes(str);

            int offset = (y * Width) + x,
                index = 0;

            foreach (var b in bytes)
            {
                info.Char.AsciiChar = b;
                _buffer[offset + (index++)] = info;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _buffer.Length; i++)
                _buffer[i] = new CharInfo();
        }

        public bool Draw()
        {
            if (_handle.IsInvalid)
                return false;

            return WriteConsoleOutput(_handle, _buffer, size, position, ref region);
        }

        public static DoubleBuffer GetInstance()
        {
            return _instance ?? (_instance = new DoubleBuffer());
        }
    }
}
