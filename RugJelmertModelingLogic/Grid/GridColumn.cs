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

        public void push(Agent agent)
        {
            agent.z = pointer++;
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
    }
}
