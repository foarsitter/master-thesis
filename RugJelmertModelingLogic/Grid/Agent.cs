using RugJelmertModelingLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic
{
    /// <summary>
    /// The most important object: the network agent! 
    /// </summary>
    public class Agent
    { 
        /// <summary>
        /// All the agents in the network.
        /// </summary>
        public static int agentCount = 0;

        /// <summary>
        /// The amount of flexible attributes
        /// </summary>
        public static int numFlex = 4;

        /// <summary>
        /// The amount of fixed attributes
        /// </summary>
        public static int numFix = 1;

        /// <summary>
        /// Flexible attributes
        /// </summary>
        public double[] Flex;

        /// <summary>
        /// List of flexible attributes of the past 
        /// </summary>
        public double[,] PreviousFlex;

        /// <summary>
        /// Fixed attributes, do not change over time. 
        /// </summary>
        public double[] Fix;
        
        /// <summary>
        /// The coordinates of the agent in the grid
        /// </summary>
        public int x, y, z;

        /// <summary>
        /// Get a fixed attribute
        /// </summary>
        /// <param name="index">The index of the attribute</param>
        /// <returns>The value from the specified attribute</returns>
        public double fix(int index)
        {
            return this.Fix[index];
        }

        /// <summary>
        /// Get a flexible attribute
        /// </summary>
        /// <param name="index">The index of the attribute</param>
        /// <returns>The value from the specified attribute</returns>
        public double flex(int index)
        {
            return this.Flex[index];
        }

        /// <summary>
        /// Initialize an agent without a known position
        /// </summary>
        /// <param name="flexible">The flexible attributes</param>
        /// <param name="fix">The fixed attributes</param>
        public Agent(double[] flexible, double[] fix)
        {
            this.Flex = flexible;
            this.Fix = fix;

            Agent.agentCount++;
        }

        /// <summary>
        /// Initialize an agent with a known position in the grid. The Z coord is determined by the grid himsels. 
        /// </summary>
        /// <param name="flexible">The flexible attributes</param>
        /// <param name="fix">The fixed attributes</param>
        /// <param name="x">X coord</param>
        /// <param name="y">Y coord</param>
        public Agent(double[] flexible, double[] fix,int x, int y) : this(flexible,fix)
        {           
            this.x = x;
            this.y = y;          
        }

        /// <summary>
        /// The position of the agent in the 3d grid
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void setPosition(int x,int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Adjust the flexible attribute with the given index. A flexible attribute can have a value between -1 and 1. 
        /// Values outside of this range are truncated.
        /// </summary>
        /// <param name="index">The position (index) of the flexible attribute to be altered</param>
        /// <param name="opinion">The new value for this attribute</param>
        public void adjust_opinion(int index, double opinion)
        {
            if (opinion < -1)
                this.Flex[index] = -1;
            else if (opinion > 1)
                this.Flex[index] = 1;
            else
                this.Flex[index] = opinion;
        }

        /// <summary>
        /// The calculation of the weigth value. See for the formula MacyFlache2009 
        /// </summary>
        /// <param name="j">The agent to compare the weight against.</param>
        /// <returns>The weight value (similarity) between agents. 
        /// A value closer to 1 means that all the fixed and flexible attributes are more the less the same. A value closer to -1 means dissimilarity.</returns>

        public double weigth(Agent j)
        {
            double sum_fixed = 0;
            double sum_flex = 0;

            int D = this.Fix.Length;


            for (int d = 0; d < D; d++)
            {
                sum_fixed += Math.Abs(j.Fix[d] - this.Fix[d]);
            }

            int K = this.Flex.Length;

            for (int k = 0; k < K; k++)
            {
                sum_flex += Math.Abs(j.Flex[k] - this.Flex[k]);
            }

            return 1 - ((sum_fixed + sum_flex) / (D + K));
        }

        /// <summary>
        /// Calculates the average distance between all the flexible attributes with this agent. 
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        public double flexibleDistance(Agent j)
        {
            int K = this.Flex.Length;

            double sum_flex = 0;

            for (int k = 0; k < K; k++)
            {
                sum_flex += Math.Abs(j.Flex[k] - this.Flex[k]);
            }

            return sum_flex / K;
        }


        /// <summary>
        /// Is a helper method to provide a random opinion (flexible attribute) array
        /// </summary>
        /// <returns></returns>
        public static double[] randomFlexible(AgentBasedModel abm)
        {
            double[] d = new double[Agent.numFlex];

            for (int i = 0; i < Agent.numFlex; i++)
            {
                d[i] = abm.Random.NextDouble() * (1 - -1) + -1;
            }

            return d;
        }
    }
}
