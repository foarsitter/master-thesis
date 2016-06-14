using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RugJelmertModelingLogic;
using System.Collections;

namespace RugJelmertModelingLogic.Model.Measurements
{
    public class OpinionDiversity : IMeasurement
    {

        public List<double> diversityTotal = new List<double>();
        public List<double> diversityLocals = new List<double>();
        public List<double> diversityImmigrants = new List<double>();


        Agent[] agents;
        public OpinionDiversity(Agent[] agents)
        {
            this.agents = agents;
        }

        public void calculate()
        {
            HashSet<double> uniqueTotal = new HashSet<double>();
            HashSet<double> uniqueLocals = new HashSet<double>();
            HashSet<double> uniqueImmigrants = new HashSet<double>();

            int localCount = 0;
            int immigrantCount = 0;

            foreach (var agent in this.agents)
            {
                double opinion = Math.Round(agent.flex(0), 3);

                uniqueTotal.Add(opinion);

                if (agent.fix(0) == AgentBasedModel.AGENT_IMMIGRANT)
                {
                    uniqueImmigrants.Add(opinion);
                    immigrantCount++;
                }
                else
                {
                    localCount++;
                    uniqueLocals.Add(opinion);
                }


            }

            double diversityTotal = (double)uniqueTotal.Count() / (double)this.agents.Length;
            double diversityLocals = (double)uniqueLocals.Count() / (double)localCount; ;
            double diversityImmigrants = (double)uniqueImmigrants.Count() / (double)immigrantCount; ;

            this.diversityTotal.Add(diversityTotal);
            this.diversityLocals.Add(diversityLocals);
            this.diversityImmigrants.Add(diversityImmigrants);

        }

    }
}
