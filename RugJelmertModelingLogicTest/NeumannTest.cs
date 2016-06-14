using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RugJelmertModelingLogic.Model;
using RugJelmertModelingLogic;
using RugJelmertModelingLogic.Interaction;

namespace RugJelmertModelingLogicTest
{
    [TestClass]
    public class NeumannTest
    {
        [TestMethod]
        public void TestCenterEnBottomRight()
        {
            AgentBasedModel abm = new AgentBasedModel();

            abm.grid.initEmpty(3, 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Agent a = new Agent(new double[] { 1 },new double[] { 1 });
                    a.setPosition(i, j, 0);
                    abm.grid.push(a);
                }
            }
            
            NeumannNeighborhood n = new NeumannNeighborhood();

            Agent[] partners_top_right = n.InteractionPartners(abm,2, 2);

            Assert.AreEqual(partners_top_right.Length, 3);

            Agent[] partners_center = n.InteractionPartners(abm, 1, 1);

            Assert.AreEqual(partners_center.Length, 5);

      
        }
    }
}
