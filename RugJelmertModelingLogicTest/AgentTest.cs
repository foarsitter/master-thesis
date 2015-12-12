using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RugJelmertModelingLogic;
using RugJelmertModelingLogic.Model;
using RugJelmertModelingLogic.Model.Measurements;

namespace RugJelmertModelingLogicTest
{
    /// <summary>
    /// Summary description for AgentTest
    /// </summary>
    [TestClass]
    public class AgentTest
    {

        public AgentTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestAgentCounter()
        {
            Agent a1 = new Agent(null, new double[] { 1.0 });
            Agent a2 = new Agent(null, new double[] { 1.0 });
            Agent a3 = new Agent(null, new double[] { 1.0 });

            Assert.AreEqual(3, Agent.agentCount, 0.001, "Agent counters does not add up.");

        }

        private AgentBasedModel intitTestModel()
        {
            AgentBasedModel model = new AgentBasedModel();

            model.grid = new Grid();
            model.grid.initEmpty(10, 10);


            double[] o_i = { 1, 1, 1, 1 };
            double[] o_j = { -1, -1, -1, -1 };

            Agent i = new Agent(o_i, new double[] { 1.0 });
            Agent j = new Agent(o_j, new double[] { -1.0 });

            model.addAgent(i);
            model.addAgent(j);

            return model;
        }

        [TestMethod]
        public void TestAgentWeigthExtreme()
        {
            double[] o_i = { 1, 1, 1, 1 };
            double[] o_j = { -1, -1, -1, -1 };
            Agent i = new Agent(o_i, new double[] { 1.0 });
            Agent j = new Agent(o_j, new double[] { -1.0 });

            double weight = i.weigth(j);

            Assert.AreEqual(-1, weight, "The weights calculation isn't correct");
        }

        [TestMethod]
        public void TestAgentWeigthDefault()
        {
            double[] o_i = { 1, 1, 1, 1 };

            Agent i = new Agent(o_i, new double[] { 1.0 });

            double weight = i.weigth(i);

            Assert.AreEqual(1, weight, "The weights calculation isn't correct");
        }
        //[TestMethod]
        //public void TestRandomDistribution()
        //{
        //    Random r = new Random();

        //    Dictionary<double, int> results = new Dictionary<double, int>();

        //    //results.Add(1, 0);
        //    int max = 1000;

        //    for (int i = 0; i < max; i++)
        //    {
        //        double next = r.NextDouble() * (0.95 - -0.95) + -0.95;
        //        double roundedNext = Math.Round(next, 1);

        //        if (results.ContainsKey(roundedNext))
        //            results[roundedNext] += 1;
        //        else
        //            results.Add(roundedNext, 1);
        //    }

        //    int expected = max / results.Count;

        //    double x2 = 0.0;

        //    foreach (KeyValuePair<double, int> entry in results)
        //    {
        //        double part = (Math.Pow(entry.Value - expected, 2) / expected);
        //        x2 += part;
        //    }

        //    int df = results.Count - 1;

        //    Assert.IsTrue(x2 < 20, "Not very significant... xi = " + x2);
        //}

    }
}
