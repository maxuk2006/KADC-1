using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnightsAndDragonsCalculatorApplication.Calculator;

namespace KnightsAndDragonsCalculatorUnitTests
{
    [TestClass]
    public class KnightStatsTest
    {

        [TestMethod]
        public void TestMethod1()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(1025+237, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1485);
        }

        [TestMethod]
        public void TestMethod2()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(1050 + 237, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1515);
        }

        [TestMethod]
        public void TestMethod3()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(1043 + 316, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1600);
        }

        [TestMethod]
        public void TestMethod4()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(1124 + 316, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1694);
        }

        [TestMethod]
        public void TestMethod5()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(696 + 237, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1098);
        }

        [TestMethod]
        public void TestMethod6()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(794 + 237, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1213);
        }

        [TestMethod]
        public void TestMethod7()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(1083 + 316, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1646);
        }

        [TestMethod]
        public void TestMethod8()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(1168 + 316, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1747);
        }

        [TestMethod]
        public void TestMethod9()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(1103 + 316, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1670);
        }

        [TestMethod]
        public void TestMethod10()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(1190 + 316, 1.05m, 1.12m);

            Assert.AreEqual(knightStat, 1772);
        }

        [TestMethod]
        public void TestMethod11()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(626 + 237, 1.05m, 1.06m);

            Assert.AreEqual(knightStat, 961);
        }

        [TestMethod]
        public void TestMethod12()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(996 + 237, 1.05m, 1.06m);

            Assert.AreEqual(knightStat, 1373);
        }

        [TestMethod]
        public void TestMethod13()
        {
            int knightStat = new KnightsAndDragonsCalculator().GetKnightStat(847 + 237, 1.05m, 1.06m);

            Assert.AreEqual(knightStat, 1208);
        }
    }
}
