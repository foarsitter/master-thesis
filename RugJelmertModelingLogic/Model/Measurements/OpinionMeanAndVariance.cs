using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RugJelmertModelingLogic;

namespace RugJelmertModelingLogic.Model.Measurements
{
    public class OpinionMeanAndVariance : IMeasurement
    {
        private List<double> mean = new List<double>();
        private List<double> variance = new List<double>();

        public double calculate(Agent[] agents)
        {
            double opinion_mean_sum = 0;

            foreach (Agent agent in agents)
            {
                for (int k = 0; k < Agent.numFlex; k++)
                {
                    opinion_mean_sum += agent.flex(k);
                }
            }

            double opinion_mean = (double)opinion_mean_sum / (double)(agents.Count() * Agent.numFlex);

            double opinion_variance_sum = 0;

            foreach (Agent agent in agents)
            {
                for (int k = 0; k < Agent.numFlex; k++)
                {
                    opinion_variance_sum += Math.Pow(agent.flex(k) - opinion_mean, 2);
                }
            }

            double opinion_variance = 1 / (double)(agents.Count() * Agent.numFlex - 1) * opinion_variance_sum;

            this.mean.Add(opinion_mean);
            this.variance.Add(opinion_variance);

            return opinion_mean;
        }

        public List<double> getResult()
        {
            return mean;
        }


        public double getItem(int index)
        {
            return this.mean[index];
        }

        public List<double> getVariance()
        {
            return variance;
        }
    }
}
