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
        private List<double> diversity = new List<double>();
        public double calculate(Agent[] agents)
        {
            HashSet<double> unique_opinions = new HashSet<double>();

            foreach (var agent in agents)
            {
                for (int k = 0; k < Agent.numFlex; k++)
                {
                    unique_opinions.Add(Math.Round(agent.flex(k), 5));
                }
            }

            double diversity = (double)unique_opinions.Count() / ((double)agents.Length * (double)Agent.numFlex);

            this.diversity.Add(diversity);

            return diversity;
        }

        public double getItem(int index)
        {
            return this.diversity[index];
        }

        public List<Double> getResult()
        {
            return this.diversity;
        }
    }
}
