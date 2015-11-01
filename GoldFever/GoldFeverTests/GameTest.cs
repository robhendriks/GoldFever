using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoldFever.Core.Level;
using GoldFever.Core;
using GoldFever.Core.Model;
using GoldFever.Core.Track;
using GoldFeverTests.Utilities;
using GoldFever.Core.Cart;

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
        [TestCategory("Tracks")]
        public void NextShouldNotBeNull()
        {
            var level = correct;
            var track = level.Tracks[0];

            Assert.IsNotNull(track.Next, "The track should have a target.");
        }

        [TestMethod]
        [TestCategory("Tracks")]
        public void NextShouldBeNull()
        {
            var level = incorrect;
            var track = level.Tracks[0];

            Assert.IsNull(track.Next, "The track shoudn\'t have a target.");
        }

        [TestMethod]
        [TestCategory("Carts")]
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
        [TestCategory("Carts")]
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
        [TestCategory("Carts")]
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
