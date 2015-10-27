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
            : base("Goudkoorts", "Selecteer één item:")
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
                new DefaultListViewItem(0, "Spelen"),
                new DefaultListViewItem(1, "Afsluiten")
            });

            Selected += MenuView_Selected;
        }

        private void Game_GameOver(object sender)
        {
            var view = new View("Spel Afgelopen", "Druk op de escape toets.");
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
            var manager = ViewManager.GetInstance();
            manager.Close();

            game.Resume();
        }

        private void Quit()
        {
            var alert = new AlertView(
                "Goudkoorts",
                "Weet je zeker dat je wilt afsluiten?",
                AlertViewButtons.YesNo);

            alert.Selected += (sender, e) =>
            {
                if (e.Result == AlertViewResult.Yes)
                    ViewManager.GetInstance().Close();
                else
                    alert.Dismiss();
            };

            alert.Show();
        }
    }
}
