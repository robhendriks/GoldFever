using GoldFever.Core;
using GoldFever.Core.Graphics.Terminal;
using System;

namespace GoldFever
{
    public class Program
    {
        #region Private Fields

        private Game game;

        #endregion


        #region Constructors

        public Program()
        {
            game = new Game();

            game.Renderer = new FastRenderer(game);
            game.Load();
        }

        #endregion


        #region Methods

        public void Run()
        {
            var view = new MenuView(game);
            view.Show();
        }

        #endregion


        #region Static Methods

        public static void Main(string[] args)
        {
            new Program().Run();
        }

        #endregion
    }
}
