using GoldFever.Core;
using GoldFever.UI.Views;
using GoldFever.UI.Views.Generic;
using System;

namespace GoldFever
{
    public sealed class MenuView : DefaultListView
    {
        private Game game;

        public MenuView(Game game)
            : base("GoldFever", "Welcome, please select an item.")
        {
            Behaviour = ViewBehaviour.Ignore;

            this.game = game;

            Initialize();
        }

        private void Initialize()
        {
            game.GameOver += Game_GameOver;

            Items.AddRange(new DefaultListViewItem[]
            {
                new DefaultListViewItem(0, "Play"),
                new DefaultListViewItem(1, "Quit"),
            });

            Selected += MenuView_Selected;
        }

        private void Game_GameOver(object sender)
        {
            var view = new View("Game Over", "Aww snap!");
            view.Show();
        }

        private void MenuView_Selected(object sender, ListViewEventArgs<int, string> e)
        {
            switch(e.Item.Key)
            {
                case 0: Play(); break;
                case 1: Quit(); break;
            }
        }

        private void Play()
        {
            ViewManager.GetInstance().Shutdown();
            game.Resume();
        }

        private void Quit()
        {
            var alert = new AlertView(
                "GoldFever",
                "Are you sure?",
                AlertViewButtons.YesNo);

            alert.Selected += (sender, e) =>
            {
                if (e.Result == AlertViewResult.Yes)
                    ViewManager.GetInstance().Shutdown();
                else
                    alert.Dismiss();
            };

            alert.Show();
        }
    }
}
