using System;

namespace GoldFever.Core.Level
{
    public struct LevelMetrics
    {
        private static LevelMetrics zeroMetrics = new LevelMetrics(0, 0);

        public static LevelMetrics Zero
        {
            get { return zeroMetrics; }
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public LevelMetrics(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
