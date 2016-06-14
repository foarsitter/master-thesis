using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RugJelmertModelingLogic.Model;

namespace RugJelmertModelingLogic.Interaction
{
    public class NeumannNeighborhood : IMechanism
    {
        public Agent[] InteractionPartners(AgentBasedModel network, int x, int y)
        {
            List<Agent> neumann = new List<Agent>();

            /* Current position and the adjacent neigbors */

            int left = x - 1;
            int right = x + 1;

            int top = y + 1;
            int bottom = y - 1;

            if(left >= 0)
                neumann.AddRange(network.grid.get(left, y));

            if(right < network.grid.nCol)
                neumann.AddRange(network.grid.get(right, y));

            if(top < network.grid.nRows)
                neumann.AddRange(network.grid.get(x,top));

            if(bottom >= 0)
                neumann.AddRange(network.grid.get(x, bottom));
            
            neumann.AddRange(network.grid.get(x, y));
            return neumann.ToArray();

        }
    }
}
