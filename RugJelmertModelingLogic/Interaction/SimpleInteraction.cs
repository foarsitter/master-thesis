using RugJelmertModelingLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic.Interaction
{
    public class SimpleInteraction : IMechanism
    {
        public Agent[] InteractionPartners(AgentBasedModel network, int x, int y)
        {
            Agent[] agents = network.Agents.ToArray();

            agents.Shuffle();

            return agents;
        }
    }
}
