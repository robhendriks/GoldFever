using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoldFever.Core.Level;
using GoldFever.Core;
using GoldFever.Core.Model;
using GoldFever.Core.Track;
using GoldFeverTests.Utilities;

namespace GoldFeverTests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var level = GameTestUtilities.CreateLevel();
        }
    }
}
