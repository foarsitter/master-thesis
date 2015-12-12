using RugJelmertModelingLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic.Interaction
{
    public class MooreNeighborhood : IMechanism
    {
        private int cells = 1;

        public MooreNeighborhood(int cellSteps = 1)
        {
            this.cells = cellSteps;
        }

        public Agent[] InteractionPartners(AgentBasedModel network, int x, int y)
        {
            List<Agent> moore = new List<Agent>();

            for (int i = (x - cells); i <= (x + cells); i++)
            {
                for (int j = (y - cells); j <= (y + cells); j++)
                {
                    if (i > 0 && j > 0 && j < network.grid.countRows() && i < network.grid.countRows())
                    {
                        var range = network.grid.getRow(i).get(j).getAgents();
                        if(range != null)
                            moore.AddRange(range);
                    }
                }
            }

            moore.AddRange(network.grid.get(x, y));

            return moore.ToArray();
        }
    }
}
