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
        public int sample = 0;
        public Agent[] selection;
        public List<double> polarization = new List<double>();

        public OpinionPolarisation(Agent[] selection)
        {
            this.sample = selection.Count();

            if (selection.Length > 200)
            {
                this.sample = 200;
            }

            this.selection = selection.ToArray();                
        }

        public void calculate()
        {
            double sum = 0;

            double average_d = 0;

            for (int i = 0; i < this.sample; i++)
            {
                for (int j = 0; j < this.sample; j++)
                {
                    if (i != j)
                    {
                        sum += selection[i].flexibleDistance(selection[j]);
                    }
                }
            }

            average_d = sum / (this.sample * this.sample);

            double variation_d = 0;

            for (int i = 0; i < this.sample; i++)
            {
                for (int j = 0; j < this.sample; j++)
                {
                    if (i != j)
                    {
                        variation_d += Math.Pow(this.selection[i].flexibleDistance(selection[j]) - average_d, 2);
                    }
                }
            }
            double dsample = (double)this.sample;
            double pdiv = (1.0 / (dsample * (dsample - 1.0)));

            double polar = pdiv * variation_d;

            this.polarization.Add(polar);                        
        }
    }
}
