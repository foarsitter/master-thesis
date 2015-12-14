using RugJelmertModelingLogic;
using RugJelmertModelingLogic.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace RugJelmertModelingCLI
{
    /// <summary>
    /// Input file comes separeted by a semicolon (;) 
    /// x;y;immigrants;addresses;households
    /// </summary>
    public class CSVParser
    {
        private AgentBasedModel abm;

        public CSVParser(AgentBasedModel abm)
        {
            this.abm = abm;
        }

        public void parse(string filename)
        {
            List<string> list = new List<string>();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if(!line.StartsWith("x"))
                    {
                        List<Agent> agents = this.AgentsFromString(line);

                        foreach (var a in agents)
                        {
                            abm.addAgent(a);
                        }
                    }
                        
                }
            }
        }

        int rows = 0;
        int colums = 0;
        int cells = 0;
        int agents = 0;     
        int immigrants = 0;

        /// <summary>
        /// Initialize the agents for this input string
        /// x;y;immigrants;addresses;households
        /// </summary>
        /// <param name="line">The line of a csv file</param>
        /// <returns></returns>
        public List<Agent> AgentsFromString(string line)
        {
            String[] row = line.Split(';');

            int x = int.Parse(row[0]);
            int y = int.Parse(row[1]);

            //the replacing is necessary when the comma is used as decimal separator 
            double immigrants = double.Parse(row[2].Replace(',', '.'));
            double addresses = double.Parse(row[3].Replace(',', '.'));
            double households = double.Parse(row[4].Replace(',', '.'));

            //addresses is per km2 and immigrants as percentage 
            addresses = addresses / 1000;
            immigrants = immigrants / 100;

            int residents = (int)(addresses * households);           

            int immigrant_actors = (int)(residents * immigrants);            
                             
            double whites = 1 - immigrants;            

            int w_max = (int)Math.Round(whites * residents);
            int i_max = (int)Math.Round(immigrants * residents);

            List<Agent> agents = new List<Agent>();

            //Add both agent groups to the grid. 
            for (int i = 0; i < w_max; i++)
            {
                agents.Add(new Agent(Agent.randomFlexible(this.abm), new double[] { 1 },x,y ));
            }          

            for (int i = 0; i < i_max; i++)
            {
                agents.Add(new Agent(Agent.randomFlexible(this.abm), new double[] { -1 }, x, y));
            }

            //score keeping
            this.immigrants += i_max;
            this.agents += i_max + w_max;

            return agents;
        }

    }
}
