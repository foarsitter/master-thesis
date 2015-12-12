using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RugJelmertModelingLogic;

namespace RugJelmertModelingLogicTest
{
    [TestClass]
    public class GridTest
    {
        [TestMethod]
        public void TestGrid()
        {
            Agent i = new Agent(new double[] { },new double[] { });
            Agent j = new Agent(new double[] { }, new double[] { });
            Agent k = new Agent(new double[] { }, new double[] { });

            Grid g = new Grid();
            g.initEmpty(10, 10);

            int x = 5, y = 5;

            g.push(x,y,i);
            g.push(x, y, j);
            g.push(x, y, k);

            Agent i2 = g.get(x, y, 0);
            Agent j2 = g.get(x, y, 1);
            Agent k2 = g.get(x, y, 2);

            Assert.AreEqual<Agent>(i, i2);
            Assert.AreNotEqual<Agent>(i, j);
            Assert.AreEqual<Agent>(k2, k);

            Assert.AreEqual(i.z, i2.z);
            Assert.AreNotEqual(i.z, j2.z);
        }
    }
}
