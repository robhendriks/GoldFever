using GoldFever.Core.Model;
using GoldFever.Core.Track;
using System;

namespace GoldFever.Core.Level
{
    public sealed class LevelManager
    {
        #region Constants

        private const string Path = "Level.json";

        #endregion


        #region Properties

        private Game _game;

        public Game Game
        {
            get { return _game; }
        }

        private BaseLevel _level;

        public BaseLevel Level
        {
            get { return _level; }
        }

        #endregion


        #region Constructors

        public LevelManager(Game game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            _game = game;
        }

        #endregion


        #region Methods

        private void Load(LevelModel data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            _level = new BaseLevel(this, data);
        }

        public void Load()
        {
            try
            {
                Load(_game.ContentManager.LoadObject<LevelModel>(Path));
            }
            catch(LevelLoadException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new LevelLoadException("Unable to load level.", ex);
            }
        }

        #endregion
    }
}
