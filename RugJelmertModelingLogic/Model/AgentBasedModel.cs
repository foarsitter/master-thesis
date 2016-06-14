using RugJelmertModelingLogic.Interaction;
using RugJelmertModelingLogic.Model.Measurements;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RugJelmertModelingLogic.Model
{
    public class AgentBasedModel
    {
        public const int AGENT_IMMIGRANT = -1;
        public const int AGENT_LOCAL = 1;

        public AgentBasedModel()
        {
            this.grid = new Grid();
        }
        /// <summary>
        /// Flat list of all the agents from the grid. 
        /// </summary>
        public List<Agent> Agents = new List<Agent>();
        public List<Agent> Locals = new List<Agent>();
        public List<Agent> Immigrants = new List<Agent>();

        public int extremeTotal = 0;
        public int extremeLocals = 0;
        public int extremeImmigrants = 0;

        /// <summary>
        /// Reference to Random 
        /// </summary>
        public Random Random = new Random();

        /// <summary>
        /// The underling grid structure for the agents. 
        /// </summary>
        public Grid grid;

        public int IterationCount { get; private set; }

        public void addAgent(Agent a)
        {
            this.Agents.Add(a);

            if ((int)a.fix(0) == AGENT_IMMIGRANT)
            {
                this.Immigrants.Add(a);
            }
            else
            {
                this.Locals.Add(a);
            }
        }

        public void addAgentToGrid(Agent a)
        {
            this.grid.push(a);
        }

        public void Run(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                this.RunIteration();

            }
        }

        /// <summary>
        /// TODO: get/set for the mechanism's.  
        /// </summary>
        //private IMechanism imech = new SimpleInteraction();
        private IMechanism imech = new NeumannNeighborhood();


        public void RunIteration()
        {
            this.extremeTotal = 0;
            this.extremeLocals = 0;
            this.extremeImmigrants = 0;

            //this correction is always the same, so we can calculate it once
            float size_correction = 1f / (2 * (this.Agents.Count) - 1);


            //Shuffle the array
            Agent[] cAgents = (Agent[])this.Agents.ToArray();
            cAgents.Shuffle();

            //This Parallel loop undermines the Shuffle method but i dont know the implecations of it. 
            //There is a random and the parallel adds an other random factor to it. Nobody knows his order. 
            for (int i = 0; i < cAgents.Length; i++)
            {

                //Pick the first agent from the shuffled list. 
                Agent iA = cAgents[i];


                // The sum of all adjustments 
                //     SIGMA ( W,ij (O,jk - O,ik) )
                // 
                double sum_part = 0;

                //Find the interaction Partners by the location of the current agent. 
                Agent[] partners = imech.InteractionPartners(this, iA.x, iA.y);

                //For each flexible arguments we calculate the adjustements. 
                //First flex(k) against all other agents' flex(k) 

                int k = 0;
                //All the agents' data stay's the same, so we can execute it parallel. 

                for (int j = 0; j < partners.Length; j++)
                {
                    Agent jA = partners[j];

                    if (jA != iA)
                    {
                        sum_part += iA.weigth(jA) * (jA.flex(k) - iA.flex(k));

                    }
                }

                var old = iA.flex(k);

                double newOpinion = iA.adjust_opinion(k, (iA.flex(k) + (size_correction * sum_part)));

                if(Math.Abs(newOpinion) > 0.90)
                {
                    this.extremeTotal++;

                    if((int)iA.fix(0) == AGENT_IMMIGRANT)
                    {
                        this.extremeImmigrants++;
                    }
                    else
                    {
                        this.extremeLocals++;
                    }
                }
            }

            if (this.IterationCount % (this.calcaluteMeasurementsEachN) == 0)
                this.calculateMeasures();

            this.IterationCount++;
        }


        // calculation en registration of measurements.
        public List<IMeasurement> measurements = new List<IMeasurement>();
        public int calcaluteMeasurementsEachN;

        public void calculateMeasures()
        {
            foreach (IMeasurement m in measurements)
            {
                m.calculate();
            }
        }

        public void ResetIterationCount()
        {
            this.IterationCount = 0;
        }

        public void AddMeasurement(IMeasurement m)
        {
            this.measurements.Add(m);
        }

        public void clearMeasurements()
        {
            this.measurements.Clear();
        }
    }

    /// <summary>
    /// Found at http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
    /// </summary>
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    /// <summary>
    /// http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
    /// </summary>
    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
