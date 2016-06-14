using RugJelmertModelingLogic.Model;
using System;
using System.Globalization;
using System.IO;

namespace RugJelmertModelingLogic
{
    /// <summary>
    /// Input file comes separeted by a semicolon (;) 
    /// x;y;immigrants;addresses;households
    /// </summary>
    public class CSVParserV2
    {
        private AgentBasedModel abm;

        public CSVParserV2(AgentBasedModel abm)
        {
            this.abm = abm;
        }

        public void parse(string contents)
        {
            using (StringReader reader = new StringReader(contents))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.StartsWith("x"))
                    {
                        Agent agent = this.AgentsFromString(line);

                        abm.addAgent(agent);
                    }
                }
            }
        }

        public int rows = 0;
        public int colums = 0;

        /// <summary>
        /// Initialize the agents for this input string
        /// x;y;immigrants;addresses;households
        /// </summary>
        /// <param name="line">The line of a csv file</param>
        /// <returns></returns>
        public Agent AgentsFromString(string line)
        {
            String[] row = line.Split(';');

            int x = int.Parse(row[0]);
            int y = int.Parse(row[1]);
            int z = int.Parse(row[2]);
            int group = int.Parse(row[3]);

            double[] flexible = new double[Agent.numFlex];

            if (row[4] == "random")
            {
                flexible = Agent.randomFlexible(this.abm);
            }
            else
            {
                flexible[0] = double.Parse(row[4].Replace(",","."),CultureInfo.InvariantCulture);
            }

            if (x > this.rows)
                this.rows = x;

            if (y > this.colums)
                this.colums = y;

            
            Agent agent = new Agent(flexible,new double[] { group});
            agent.setPosition(x, y, z);
            return agent;
            
        }

    }
}
