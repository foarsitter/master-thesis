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
        
        /// <summary>
        /// The method to test in this example
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public int sum(int param1, int param2)
        {
            return param1 + param2;
        }
    }
}
