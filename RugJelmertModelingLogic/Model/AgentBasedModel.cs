using RugJelmertModelingLogic.Interaction;
using RugJelmertModelingLogic.Model.Measurements;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RugJelmertModelingLogic.Model
{
    public class AgentBasedModel
    {
        public AgentBasedModel()
        {
            this.grid = new Grid();
        }
        /// <summary>
        /// Flat list of all the agents from the grid. 
        /// </summary>
        public List<Agent> Agents = new List<Agent>();

        /// <summary>
        /// Reference to Random 
        /// </summary>
        public Random Random = new Random();

        /// <summary>
        /// The underling grid structure for the agents. 
        /// </summary>
        public Grid grid;

        public int IterationCount  { get; private set; }

        public void addAgent(Agent a)
        {
            this.Agents.Add(a);
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
        private IMechanism imech = new SimpleInteraction();

        public void RunIteration()
        {
            int interactions = 0;

            //this correction is always the same, so we can calculate it once
            float size_correction = 1f / (2 * (Agent.agentCount) - 1);
            
           
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
                    for (int k = 0; k < Agent.numFlex; k++)
                    {
                        //All the agents' data stay's the same, so we can execute it parallel. 

                        for (int j = 0; j < partners.Length; j++)
                        {
                            Agent jA = partners[j];

                            if (jA != iA)
                            {
                                sum_part += iA.weigth(jA) * (jA.flex(k) - iA.flex(k));
                                interactions++;
                            }
                        }

                        var old = iA.flex(k);

                        iA.adjust_opinion(k, (iA.flex(k) + (size_correction * sum_part)));                        
                    }

                this.IterationCount++;

                this.calculateMeasures();
            }
        }


        // calculation en registration of measurements.
        public List<IMeasurement> measurements = new List<IMeasurement>();

        private void calculateMeasures()
        {
            foreach(IMeasurement m in measurements)
            {
                m.calculate(this.Agents.ToArray());
            }
        }   

        
        public void AddMeasurement(IMeasurement m)
        {
            this.measurements.Add(m);
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
    /// Found at http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
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
