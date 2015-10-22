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

            foreach(var track in game.LevelManager.Level.Tracks)
            {
                Console.SetCursorPosition(track.Position.X * 2, track.Position.Y);
                Console.BackgroundColor = (track.Next != null ? ConsoleColor.Green : ConsoleColor.Red);
                Console.Write("^^");
            }

            Console.ReadKey();
        }
    }
}
