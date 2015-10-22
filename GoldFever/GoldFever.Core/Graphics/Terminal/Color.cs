using System;

namespace GoldFever.Core.Graphics.Terminal
{
    public static class Color
    {
        const short ForegroundR = 0x0004,
                    ForegroundG = 0x0002,
                    ForegroundB = 0x0001,
                    ForegroundA = 0x0008;

        public const short ForegroundBlack = 0,
                           ForegroundDarkRed = ForegroundR,
                           ForegroundRed = ForegroundR + ForegroundA,
                           ForegroundDarkGreen = ForegroundG,
                           ForegroundGreen = ForegroundG + ForegroundA,
                           ForegroundDarkBlue = ForegroundB,
                           ForegroundBlue = ForegroundB + ForegroundA,
                           ForegroundDarkYellow = ForegroundR + ForegroundG,
                           ForegroundYellow = ForegroundR + ForegroundG + ForegroundA,
                           ForegroundDarkCyan = ForegroundB + ForegroundG,
                           ForegroundCyan = ForegroundB + ForegroundG + ForegroundA,
                           ForegroundDarkMagenta = ForegroundR + ForegroundB,
                           ForegroundMagenta = ForegroundR + ForegroundB + ForegroundA,
                           ForegroundGrey = ForegroundR + ForegroundG + ForegroundB,
                           ForegroundWhite = ForegroundR + ForegroundG + ForegroundB + ForegroundA;

        const short BackgroundR = 0x0040,
                    BackgroundG = 0x0020,
                    BackgroundB = 0x0010,
                    BackgroundA = 0x0080;

        public const short BackgroundBlack = 0,
                           BackgroundDarkRed = BackgroundR,
                           BackgroundRed = BackgroundR + BackgroundA,
                           BackgroundDarkGreen = BackgroundG,
                           BackgroundGreen = BackgroundG + BackgroundA,
                           BackgroundDarkBlue = BackgroundB,
                           BackgroundBlue = BackgroundB + BackgroundA,
                           BackgroundDarkYellow = BackgroundR + BackgroundG,
                           BackgroundYellow = BackgroundR + BackgroundG + BackgroundA,
                           BackgroundDarkCyan = BackgroundB + BackgroundG,
                           BackgroundCyan = BackgroundB + BackgroundG + BackgroundA,
                           BackgroundDarkMagenta = BackgroundR + BackgroundB,
                           BackgroundMagenta = BackgroundR + BackgroundB + BackgroundA,
                           BackgroundGrey = BackgroundR + BackgroundG + BackgroundB,
                           BackgroundWhite = BackgroundR + BackgroundG + BackgroundB + BackgroundA;
    }
}
