using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RugJelmertModelingLogic.Model;

namespace RugJelmertModelingLogic.Interaction
{
    class NeumannNeighborhood : IMechanism
    {
        public Agent[] InteractionPartners(AgentBasedModel network, int x, int y)
        {
            List<Agent> neumann = new List<Agent>();

            /* Current position and the adjacent neigbors */

            neumann.AddRange(network.grid.get(x-1, y));
            neumann.AddRange(network.grid.get(x - +1, y));
            neumann.AddRange(network.grid.get(x, y - 1));
            neumann.AddRange(network.grid.get(x, y + 1));
            neumann.AddRange(network.grid.get(x, y));

            return neumann.ToArray();

        }
    }
}
