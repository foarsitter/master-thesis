using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic.Model.Measurements
{
    public class ExtremeCount : IMeasurement
    {
        public List<double> historyTotal = new List<double>();
        public List<double> historyLocals = new List<double>();
        public List<double> historyImmigrants = new List<double>();

        AgentBasedModel abm;

        public ExtremeCount(AgentBasedModel abm)
        {
            this.abm = abm;
        }

        public void calculate()
        {
            
            historyTotal.Add((double)this.abm.extremeTotal / (double)this.abm.Agents.Count);
            historyLocals.Add((double)this.abm.extremeLocals / (double)this.abm.Locals.Count);
            historyImmigrants.Add(this.abm.extremeImmigrants / (double)this.abm.Immigrants.Count);            
        }
     
    }
}
