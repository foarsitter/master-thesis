using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RugJelmertModelingLogic;

namespace RugJelmertModelingLogic.Model.Measurements
{
    public class OpinionPolarisation : IMeasurement
    {
        private List<double> polarization = new List<double>();
        public double calculate(Agent[] agents)
        {
            int sample;
            Agent[] selection;

            if (agents.Count() < 200)
            {
                sample = agents.Count();
                selection = agents.ToArray();
            }
            else
            {
                sample = agents.Count() / 100;
                selection = agents.OrderBy(item => new Random().Next()).ToArray();
            }

            double sum = 0;

            double average_d = 0;

            for (int i = 0; i < sample; i++)
            {
                for (int j = 0; j < sample; j++)
                {
                    if (i != j)
                    {
                        sum += selection[i].flexibleDistance(selection[j]);
                    }
                }
            }

            average_d = sum / (sample * sample);

            double variation_d = 0;

            for (int i = 0; i < sample; i++)
            {
                for (int j = 0; j < sample; j++)
                {
                    if (i != j)
                    {
                        variation_d += Math.Pow(selection[i].flexibleDistance(selection[j]) - average_d, 2);
                    }
                }
            }
            double dsample = (double)sample;
            double pdiv = (1.0 / (dsample * (dsample - 1.0)));

            double polar = pdiv * variation_d;

            this.polarization.Add(polar);

            return polar;
            
        }

        public List<double> getResult()
        {
            return this.polarization;
        }
    }
}
