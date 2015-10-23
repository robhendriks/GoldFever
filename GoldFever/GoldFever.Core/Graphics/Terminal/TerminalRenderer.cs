using System;

namespace GoldFever.Core.Graphics.Terminal
{
    public sealed class TerminalRenderer : IRenderer
    {
        const int OffsetX = 4,
                  OffsetY = 6;

        private Game game;
        private DoubleBuffer buffer;

        public TerminalRenderer(Game game)
        {
            this.game = game;
            this.buffer = DoubleBuffer.GetInstance();
        }

        private void RenderUI()
        {
            var info = new CharInfo()
            {
                Attributes = Color.ForegroundWhite | Color.BackgroundBlack
            };

            buffer.Write("Goudkoorts", OffsetX, 2, info);
            buffer.Write($"Score: {game.Score}", OffsetX, 3, info);
            buffer.Write($"Carts: {game.Level.Carts.Count}", OffsetX, 4, info);
        }

        private void RenderTracks()
        {
            CharInfo info;
            int x, y;

            foreach(var track in game.Level.Tracks)
            {
                x = OffsetX + (track.X * 2);
                y = OffsetY + track.Y;

                info = new CharInfo();
                info.Attributes = track.Attributes();
                info.Char.AsciiChar = track.Char();

                buffer.Write(x, y, 2, info);
            }
        }

        private void RenderCarts()
        {
            var info = new CharInfo()
            {
                Attributes = Color.ForegroundGreen | Color.BackgroundDarkRed
            };

            int x, y;

            foreach (var cart in game.Level.Carts)
            {
                if (cart?.Current == null)
                    continue;

                x = OffsetX + (cart.Current.X * 2);
                y = OffsetY + cart.Current.Y;

                info.Char.AsciiChar = cart.Char();
                
                buffer.Write(x, y, 2, info);
            }
        }

        public void Render()
        {
            buffer.Clear();

            RenderUI();
            RenderTracks();
            RenderCarts();

            buffer.Draw();
        }
    }
}
