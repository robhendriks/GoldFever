using GoldFever.Core;
using GoldFever.UI.Views;
using GoldFever.UI.Views.Generic;
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
        }
    }
}
