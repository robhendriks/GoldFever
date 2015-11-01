using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoldFever.Core.Level;
using GoldFever.Core;
using GoldFever.Core.Model;
using GoldFever.Core.Track;
using GoldFeverTests.Utilities;
using GoldFever.Core.Cart;
using GoldFever.Core.Content;

namespace GoldFeverTests
{
    [TestClass]
    public class GameTest
    {
        private BaseLevel correct;
        private BaseLevel incorrect;

        [TestInitialize]
        public void GameTest_Initialize()
        {
            correct = LevelGenerator.Generate();
            incorrect = LevelGenerator.Generate(false);
        }

        [TestMethod]
        [TestCategory("Level")]
        public void LevelShouldBeLoaded()
        {
            var level = LevelGenerator.GenerateFromJson();

            Assert.AreEqual(3, level.Tracks.Length, "Expecting 3 tracks in total.");
            Assert.IsInstanceOfType(level.Tracks[0], typeof(StartTrack), "Start track expected at index 0.");
            Assert.IsInstanceOfType(level.Tracks[1], typeof(DefaultTrack), "Default track expected at index 1.");
            Assert.IsInstanceOfType(level.Tracks[2], typeof(EndTrack), "End track expected at index 2.");
        }

        [TestMethod]
        [TestCategory("Track")]
        public void NextShouldNotBeNull()
        {
            var level = correct;
            var track = level.Tracks[0];

            Assert.IsNotNull(track.Next, "The track should have a target.");
        }

        [TestMethod]
        [TestCategory("Track")]
        public void NextShouldBeNull()
        {
            var level = incorrect;
            var track = level.Tracks[0];

            Assert.IsNull(track.Next, "The track shoudn\'t have a target.");
        }

        [TestMethod]
        [TestCategory("Cart")]
        public void CartShouldBeAbleToEnter()
        {
            var level = correct;

            var cart = new BaseCart();
            var tracks = level.Tracks;

            cart.Current = tracks[1];
            tracks[1].Cart = cart;

            Assert.IsTrue(tracks[0].CanEnter(cart), "The cart should be able to enter.");
        }

        [TestMethod]
        [TestCategory("Cart")]
        public void CartShouldNotBeAbleToEnter()
        {
            var level = correct;

            var cart = new BaseCart();
            var track = level.Tracks[1];

            cart.Current = track;
            track.Cart = cart;

            Assert.IsFalse(track.CanEnter(cart), "The cart should not be able to enter.");
        }

        [TestMethod]
        [TestCategory("Cart")]
        [ExpectedException(typeof(GameOverException))]
        public void CartShouldThrowException()
        {
            var level = correct;
            var tracks = level.Tracks;

            var cart1 = new BaseCart();
            var cart2 = new BaseCart();

            tracks[0].Cart = cart1;
            cart1.Current = tracks[0];

            tracks[1].Cart = cart2;
            cart2.Current = tracks[1];

            cart1.IgnoreTick = true;
            cart1.Update();
        }
    }
}
