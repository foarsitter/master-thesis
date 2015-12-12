using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic
{
    /// <summary>
    /// The main grid element containing rows, columns and agents. 
    /// </summary>
    public class Grid
    {
        private GridRow[] rows;

        public void initEmpty(int rows, int columns)
        {
            this.rows = new GridRow[rows];

            for (int i = 0; i < rows; i++)
            {
                this.rows[i] = new GridRow();
                this.rows[i].initEmpty(columns);
            }
        }

        /// <summary>
        /// Ad an agent to the grid to his own position
        /// </summary>
        /// <param name="agent">The agent to add tot the grid</param>
        public void push(Agent agent)
        {
            this.getRow(agent.x).get(agent.y).push(agent);
        }

        internal GridRow getRow(int i)
        {
            return this.rows[i];
        }

        /// <summary>
        /// Add an agent to the grid with and set his position
        /// </summary>
        /// <param name="x">X coord</param>
        /// <param name="y">Y coor</param>
        /// <param name="agent"></param>
        public void push(int x, int y, Agent agent)
        {
            agent.x = x;
            agent.y = y;
            this.push(agent);
        }

        /// <summary>
        /// Get a single agent from a position on a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colum"></param>
        /// <param name="agent"></param>
        /// <returns></returns>
        public Agent get(int row, int colum, int agent)
        {
            return this.rows[row].get(colum).getAgent(agent);
        }

        /// <summary>
        /// Get all the agents of a cell 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colum"></param>
        /// <returns></returns>
        public Agent[] get(int row, int colum)
        {
            return this.rows[row].get(colum).getAgents();
        }

        public int countRows()
        {
            return this.rows.Count();
        }
    }
}
