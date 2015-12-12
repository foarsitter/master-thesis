using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RugJelmertModelingLogic.Model;
using RugJelmertModelingLogic;
using RugJelmertModelingLogic.Model.Measurements;

namespace RugJelmertModelingLogicTest
{
    [TestClass]
    public class TestOpinionDiversity
    {
        [TestMethod]
        public void TestDifferent()
        {
            AgentBasedModel model = new AgentBasedModel();
            model.grid.initEmpty(10, 10);
            double[] o_i = { 0.1 };
            double[] o_j = { 0.0 };
            Agent.numFlex = 1;
            Agent i = new Agent(o_i, new double[] { 1.0 });
            Agent j = new Agent(o_j, new double[] { -1.0 });
            model.addAgent(i);
            model.addAgent(j);

            OpinionDiversity div = new OpinionDiversity();

            double res = div.calculate(model.Agents.ToArray());
            //2 agent with 2 different opinions, one each. 2/(2*1)=1
            Assert.AreEqual(res,1);
        }

        [TestMethod]
        public void TestSameOpinion()
        {
            AgentBasedModel model = new AgentBasedModel();
            model.grid.initEmpty(10, 10);
            double[] o_i = { 0.1 };
            double[] o_j = { 0.1 };
            Agent.numFlex = 1;
            Agent i = new Agent(o_i, new double[] { 1.0 });
            Agent j = new Agent(o_j, new double[] { -1.0 });
            model.addAgent(i);
            model.addAgent(j);

            OpinionDiversity div = new OpinionDiversity();

            double res = div.calculate(model.Agents.ToArray());
            //2 agent with same opinion, one each. 1/(2*1)=0.5
            Assert.AreEqual(res, 0.5);
        }

        [TestMethod]
        public void Test100x1Diff()
        {
            double init = 0.001;
            double step = 0.001;

            AgentBasedModel model = new AgentBasedModel();
            model.grid.initEmpty(10, 10);
            Agent.numFlex = 1;

            for (int i = 0; i < 100; i++)
            {
                Agent a = new Agent(new double[] { (init += step) }, new double[] { -1.0 });
                model.addAgent(a);
            }

            OpinionDiversity div = new OpinionDiversity();

            double res = div.calculate(model.Agents.ToArray());

            //100 agent with different opinions, one each. 100/(100*1)=1
            Assert.AreEqual(res, 1);
        }

        [TestMethod]
        public void Test25x4Diff()
        {
            double init = 0.001;
            double step = 0.001;

            AgentBasedModel model = new AgentBasedModel();
            model.grid.initEmpty(10, 10);
            Agent.numFlex = 1;

            for (int i = 0; i < 100; i++)
            {
                Agent a = new Agent(new double[] { (init += step), (init += step), (init += step), (init += step) }, new double[] { -1.0 });
                model.addAgent(a);
            }

            OpinionDiversity div = new OpinionDiversity();

            double res = div.calculate(model.Agents.ToArray());

            //25 agent with different opinions, 4 each. 100/(25*4)=1
            Assert.AreEqual(res, 1);
        }

        [TestMethod]
        public void Test25x4Same()
        {
            double init = 0.001;
            double step = 0.000;

            AgentBasedModel model = new AgentBasedModel();
            model.grid.initEmpty(10, 10);
            Agent.numFlex = 1;

            for (int i = 0; i < 100; i++)
            {
                Agent a = new Agent(new double[] { (init += step), (init += step), (init += step), (init += step) }, new double[] { -1.0 });
                model.addAgent(a);
            }

            OpinionDiversity div = new OpinionDiversity();

            double res = div.calculate(model.Agents.ToArray());

            //25 agent with same opinions, 4 each. 1/(25*4)=1
            Assert.AreEqual(res, 0.01);
        }
    }
}
