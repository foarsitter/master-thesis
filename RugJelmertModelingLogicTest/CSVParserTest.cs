using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RugJelmertModelingCLI;
using RugJelmertModelingLogic.Model;
using RugJelmertModelingLogic;
using System.Collections.Generic;

namespace RugJelmertModelingLogicTest
{
    [TestClass]
    public class CSVParserTest
    {
        [TestMethod]
        public void TestSingleLine()
        {            
            AgentBasedModel abm = new AgentBasedModel(); 

            CSVParser p = new CSVParser(abm);
                        
            // x;y;immigrants;addresses;households
            string line = "5;5;0;1000;1";

            List<Agent> agents = p.AgentsFromString(line);

            Assert.AreEqual(agents.Count, 1);
            Assert.AreEqual(agents[0].x, 5);
            Assert.AreEqual(agents[0].y, 5);
            Assert.AreEqual(agents[0].fix(0), 1);

        }

        [TestMethod]
        public void TestCSV()
        {
            AgentBasedModel abm = new AgentBasedModel();
            abm.grid.initEmpty(10, 10);
            CSVParser p = new CSVParser(abm);

            // x;y;immigrants;addresses;households
            p.parse("assets/flat_town.csv");                

            Assert.AreEqual(Agent.agentCount, 100);
        }
    }
}
