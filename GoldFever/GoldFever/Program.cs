using GoldFever.Core;
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

            game.Run();

            Console.ReadKey();
        }
    }
}
