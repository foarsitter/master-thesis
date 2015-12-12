using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RugJelmertModelingLogicTest
{
    [TestClass]
    public class SumTest
    {
        [TestMethod]
        public void TestSumMethod()
        {
            Assert.AreEqual(this.sum(1, 1), 2);
            Assert.AreEqual(this.sum(2, 1), 3);
            Assert.AreEqual(this.sum(3, 10), 13);
        }



        public int sum(int param1, int param2)
        {
            return param1 + param2;
        }
    }
}
