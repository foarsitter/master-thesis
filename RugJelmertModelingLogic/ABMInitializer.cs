using RugJelmertModelingLogic;
using RugJelmertModelingLogic.Model;

namespace RugJelmertModelingLogic
{
    public class ABMInitializer
    {
        public ABMInitializer()
        {
            
        }

        public AgentBasedModel LoadCSVFile(string data)
        {
            // create a new instance of the model.
            AgentBasedModel abm = new AgentBasedModel();
            
            //parse al the agents from the file. 
            CSVParserV2 parser = new CSVParserV2(abm);
            parser.parse(data);

            //init the grid with the maximum of rows/colums from the csv file
            abm.grid.initEmpty(parser.rows + 2, parser.colums + 2);

            //add all the agents the the right position.

            foreach (Agent agent in abm.Agents)
            {
                abm.addAgentToGrid(agent);
            }

            return abm;
        }


    }
}
