using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarshipGenerator;

namespace StarshipTester
{
    [TestClass]
    public class DiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DiceRoll d1 = new DiceRoll(2, 1, 4);
            DiceRoll d2 = new DiceRoll("2d10+1d5+4");
            DiceRoll d3 = new DiceRoll("1d10");
            DiceRoll d4 = new DiceRoll("5");
            DiceRoll d5 = new DiceRoll(2, 0, 0);

            Assert.AreEqual(d1.ToString(), d2.ToString());

            Assert.AreEqual("1d10+5", (d3 + d4).ToString());

            Assert.AreEqual("2d10+1d5-1", (d2 - d4).ToString());

            Assert.AreEqual("3d10", (d5 + d3).ToString());
        }
    }
}
