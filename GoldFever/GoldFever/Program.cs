using GoldFever.Core;
using GoldFever.Core.Graphics.Terminal;
using GoldFever.Core.Model;
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
            game.Run();

            Console.ReadKey();
        }
    }
}
