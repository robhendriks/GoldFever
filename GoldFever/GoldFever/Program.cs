using GoldFever.Core;
using GoldFever.Core.Graphics.Terminal;
using System;

namespace GoldFever
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game(new GameOptions()
            {
                ContentPath = "GoldFever.Content"
            });

            game.Renderer = new TerminalRenderer(game);
            game.Setup();

            var view = new MenuView(game);
            view.Show();
        }
    }
}
