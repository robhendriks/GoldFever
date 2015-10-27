using System;

namespace GoldFever.Core.Level
{
    public struct LevelMetrics
    {
        #region Static fields

        private static LevelMetrics zeroMetrics = new LevelMetrics(0, 0);

        #endregion


        #region Static Properties

        public static LevelMetrics Zero
        {
            get { return zeroMetrics; }
        }

        #endregion


        #region Properties

        public int Width { get; set; }
        public int Height { get; set; }

        #endregion


        #region Constructors

        public LevelMetrics(int width, int height)
        {
            Width = width;
            Height = height;
        }

        #endregion
    }
}
