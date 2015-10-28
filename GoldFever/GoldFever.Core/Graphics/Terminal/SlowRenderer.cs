using GoldFever.Core.Ship;
using GoldFever.Core.Track;
using System;
using System.Text;

namespace GoldFever.Core.Graphics.Terminal
{
    public sealed class SlowRenderer : IRenderer
    {
        #region Constants

        const int OffsetX = 4,
                  OffsetY = 8;

        #endregion


        #region Private Fields

        private Game game;

        #endregion


        #region Constructors

        public SlowRenderer(Game game)
        {
            this.game = game;
        }

        #endregion


        #region Methods

        private void RenderUI()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;

            var cur = game.Level.Port.Loading;

            if (game.State != GameState.Idle)
            {
                string score = $"{game.Score}".PadLeft(3, '0'),
                       carts = $"{game.Level.Carts.Count}".PadLeft(3, '0'),
                       ship = (cur != null ? $"{cur.Size}/{BaseShip.Capacity}" : "n/a");

                Console.SetCursorPosition(OffsetX, 2);
                Console.Write($"Punten: {score}");

                Console.SetCursorPosition(OffsetX, 3);
                Console.Write($"Karren: {carts}");

                Console.SetCursorPosition(OffsetX, 4);
                Console.Write($"Boten: {ship}");
            }
            else
            {
                Console.SetCursorPosition(OffsetX, 2);
                Console.Write("Gepauzeerd");
            }
        }

        Random r = new Random();

        private void RenderShips()
        {
            #region Water

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            int maxWidth = game.Level.Port.Size,
                height = 2,
                x,
                y = 6;

            for (int i = 0; i < maxWidth; i++)
            {
                x = OffsetX + (i * 2);

                for (int j = 0; j < height; j++)
                {
                    Console.SetCursorPosition(x, y + j);
                    Console.Write("\u2592\u2592");
                }
            }

            #endregion

            #region Ships

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkYellow;

            int width = BaseShip.Width,
                start,
                end;

            y = 7;

            foreach (var ship in game.Level.Port.Ships)
            {
                start = ship.Index;
                end = ship.Index + width;

                for (int i = start; i < end; i++)
                {
                    if (i < 0 || i >= maxWidth)
                        continue;

                    x = OffsetX + (i * 2);
                    Console.SetCursorPosition(x, y);
                    Console.Write("\u2591\u2591");
                }
            }

            #endregion
        }

        ASCIIEncoding ae = new ASCIIEncoding();

        private void RenderTracks()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Red;

            int x, y;
            string c;

            #region Tracks

            foreach (var track in game.Level.Tracks)
            {
                x = OffsetX + (track.X * 2);
                y = OffsetY + track.Y;

                if (track is StartTrack)
                    c = ">>";
                else if (track is EndTrack)
                    c = "<<";
                else if (track is SwitchInTrack)
                    c = ((SwitchInTrack)track).Mode == SwitchMode.Up ? "up" : "dn";
                else if (track is SwitchOutTrack)
                    c = ((SwitchOutTrack)track).Mode == SwitchMode.Up ? "dn" : "up";
                else if (track is HoldTrack)
                    c = "..";
                else if (track is DropTrack)
                    c = "^^";
                else
                    c = "  ";

                Console.SetCursorPosition(x, y);
                Console.Write($"{c}");
            }

            #endregion
        }

        private void RenderCarts()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Green;

            int x, y;

            #region Carts

            foreach (var cart in game.Level.Carts)
            {
                if (cart?.Current == null)
                    continue;

                x = OffsetX + (cart.Current.X * 2);
                y = OffsetY + cart.Current.Y;

                Console.SetCursorPosition(x, y);
                Console.Write(cart.IsEmpty ? "--" : "$$");
            }

            #endregion
        }

        public void Render()
        {
            Console.ResetColor();
            Console.Clear();

            RenderUI();
            RenderShips();
            RenderTracks();
            RenderCarts();
        }

        #endregion
    }
}
