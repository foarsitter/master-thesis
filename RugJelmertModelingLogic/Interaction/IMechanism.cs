using RugJelmertModelingLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic.Interaction
{
    public interface IMechanism
    {
        Agent[] InteractionPartners(AgentBasedModel model,int x,int y);
    }
}
