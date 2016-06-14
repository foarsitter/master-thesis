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
        public List<double> meanAll = new List<double>();
        public List<double> meanLocals = new List<double>();
        public List<double> meanImmigrants = new List<double>();

        public List<double> varianceAll = new List<double>();
        public List<double> varianceLocals = new List<double>();
        public List<double> varianceImmigrants = new List<double>();


        public List<double> meanAllAbs = new List<double>();
        public List<double> meanLocalsAbs = new List<double>();
        public List<double> meanImmigrantsAbs = new List<double>();

        public List<double> varianceAllAbs = new List<double>();
        public List<double> varianceLocalsAbs = new List<double>();
        public List<double> varianceImmigrantsAbs = new List<double>();

        public List<double> rpbi = new List<double>();
        public List<double> absPpbi = new List<double>();
               

        Agent[] agents;

        public OpinionMeanAndVariance(Agent[] agents)
        {
            this.agents = agents;
        }

        public void calculate()
        {
            double opinionMeanTotalSum = 0;
            double opinionMeanLocalsSum = 0;
            double opinionMeanImmigrantsSum = 0;

            double absOpinionMeanTotalSum = 0;
            double absOpinionMeanLocalsSum = 0;
            double absOpinionMeanImmigrantsSum = 0;

            int countLocals = 0;
            int countImmigrants = 0;

            foreach (Agent agent in this.agents)
            {
                int k = 0;

                opinionMeanTotalSum += agent.flex(k);
                absOpinionMeanTotalSum += Math.Abs(agent.flex(k));

                if (agent.fix(0) == 1)
                {
                    opinionMeanLocalsSum += agent.flex(k);
                    absOpinionMeanLocalsSum += Math.Abs(agent.flex(k));
                    countLocals++;
                }
                else
                {
                    opinionMeanImmigrantsSum += agent.flex(k);
                    absOpinionMeanImmigrantsSum += Math.Abs(agent.flex(k));
                    countImmigrants++;
                }
            }

            double opinionMeanAll = (double)opinionMeanTotalSum / (double)(this.agents.Length * Agent.numFlex);
            double opinionMeanLocals = (double)opinionMeanLocalsSum / (double)(countLocals * Agent.numFlex);
            double opinionMeanImmigrants = (double)opinionMeanImmigrantsSum / (double)(countImmigrants * Agent.numFlex);

            double opinionMeanAllAbs = (double)absOpinionMeanTotalSum / (double)(this.agents.Length * Agent.numFlex);
            double opinionMeanLocalsAbs = (double)absOpinionMeanLocalsSum / (double)(countLocals * Agent.numFlex);
            double opinionMeanImmigrantsAbs = (double)absOpinionMeanImmigrantsSum / (double)(countImmigrants * Agent.numFlex);

            double sumOpinionVarianceAll = 0;
            double sumOpinionVarianceLocals = 0;
            double sumOpinionVarianceImmigrants = 0;

            double opinionVarianceSumAllAbs = 0;
            double opinionVarianceSumLocalsAbs = 0;
            double opinionVarianceSumImmigrantsAbs = 0;

            foreach (Agent agent in this.agents)
            {
                sumOpinionVarianceAll += Math.Pow(agent.flex(0) - opinionMeanAll, 2);
                opinionVarianceSumAllAbs += Math.Pow(Math.Abs(agent.flex(0)) - opinionMeanAllAbs, 2);

                if (agent.fix(0) == 1)
                {
                    sumOpinionVarianceLocals += Math.Pow(agent.flex(0) - opinionMeanLocals, 2);
                    opinionVarianceSumLocalsAbs += Math.Pow(Math.Abs(agent.flex(0)) - opinionMeanLocalsAbs, 2);
                }                    
                else
                {
                    sumOpinionVarianceImmigrants += Math.Pow(agent.flex(0) - opinionMeanImmigrants, 2);
                    opinionVarianceSumImmigrantsAbs += Math.Pow(Math.Abs(agent.flex(0)) - opinionMeanImmigrantsAbs, 2);
                }                    

            }

            double opinionVarianceAll = 1 / (double)(this.agents.Length * Agent.numFlex - 1) * sumOpinionVarianceAll;
            double opinionVarianceLocals = 1 / (double)(countLocals * Agent.numFlex - 1) * sumOpinionVarianceLocals;
            double opinionVarianceImmigrants = 1 / (double)(countImmigrants * Agent.numFlex - 1) * sumOpinionVarianceImmigrants;

            double opinionVarianceAllAbs = 1 / (double)(this.agents.Length * Agent.numFlex - 1) * opinionVarianceSumAllAbs;
            double opinionVarianceLocalsAbs = 1 / (double)(countLocals * Agent.numFlex - 1) * opinionVarianceSumLocalsAbs;
            double opinionVarianceImmigrantsAbs = 1 / (double)(countImmigrants * Agent.numFlex - 1) * opinionVarianceSumImmigrantsAbs;

            this.meanAll.Add(opinionMeanAll);
            this.meanLocals.Add(opinionMeanLocals);
            this.meanImmigrants.Add(opinionMeanImmigrants);

            this.varianceAll.Add(opinionVarianceAll);
            this.varianceLocals.Add(opinionVarianceLocals);
            this.varianceImmigrants.Add(opinionVarianceImmigrants);

            this.meanAllAbs.Add(opinionMeanAllAbs);
            this.meanLocalsAbs.Add(opinionMeanLocalsAbs);
            this.meanImmigrantsAbs.Add(opinionMeanImmigrantsAbs);

            this.varianceAllAbs.Add(opinionVarianceAllAbs);
            this.varianceLocalsAbs.Add(opinionVarianceLocalsAbs);
            this.varianceImmigrantsAbs.Add(opinionVarianceImmigrantsAbs);

            double p = (double)countLocals / (double)this.agents.Length;
            double q = (double)countImmigrants / (double)this.agents.Length;

            double pq = Math.Sqrt(p * q);

            this.rpbi.Add((opinionMeanLocals - opinionMeanImmigrants) / Math.Sqrt(opinionVarianceAll) * pq);
            this.absPpbi.Add((opinionMeanLocalsAbs - opinionMeanImmigrantsAbs) / Math.Sqrt(opinionVarianceAllAbs) * pq);

        }
    }
}
