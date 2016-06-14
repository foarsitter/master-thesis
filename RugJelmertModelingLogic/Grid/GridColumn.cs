using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic
{
    /// <summary>
    /// A column/cell in the grid. 
    /// </summary>
    public class GridColumn
    {
        private List<Agent> agents = new List<Agent>();
        private int pointer = 0;

        private int locals = 0;
        private int immigrants = 0;

        public void push(Agent agent)
        {
            agent.z = pointer++;

            if(agent.fix(0) == -1)
            {
                this.immigrants++;
            }
            else
            {
                this.locals++;
            }

            this.agents.Add(agent);
        }

        internal Agent[] getAgents()
        {
            return agents.ToArray();
        }

        internal Agent getAgent(int agent)
        {
            return this.agents[agent];
        }   

        public bool isEmpty()
        {
            return (this.agents.Count == 0);
        }
        
        public double getMeanOpinion(bool absolute)
        {
            double opinionSum = 0;

            foreach (Agent agent in this.agents)
            {
                opinionSum += (absolute) ? Math.Abs(agent.flex(0)) : agent.flex(0);
            }

            return opinionSum / this.agents.Count;
        }     

        public bool isMixed()
        {
            return this.locals > 0 && this.immigrants > 0;            
        }

        public double getGroupDistance()
        {
            double localCount = 0;
            double immigrantCount = 0;



            foreach (Agent agent in this.agents)
            {
                if(agent.fix(0) == Agent.IMMIGRANT)
                {
                    immigrantCount += agent.flex(0);
                }
                else
                {
                    localCount += agent.flex(0);
                }
            }

            return (localCount / this.locals) - (immigrantCount / this.immigrants);
        }
    }
}
