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
        public int nRows = 0;
        public int nCol = 0; 

        private GridRow[] rows;

        public GridRow[] getRows()
        {
            return this.rows;
        }

        public void initEmpty(int rows, int columns)
        {
            this.rows = new GridRow[rows];

            this.nRows = rows;
            this.nCol = columns;

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
        public Agent[] get(int row, int column)
        {
            return this.rows[row].get(column).getAgents();
        }

        public GridColumn getColumn(int row, int column)
        {
            return this.rows[row].get(column);
        }

        public int countRows()
        {
            return this.rows.Count();
        }

        public string Serialize()
        {
            return this.Serialize(false);
        }

        public String Serialize(bool round)
        {
            StringBuilder gridString = new StringBuilder();

            for (int x = 0; x < this.nRows; x++)
            {
                for (int y = 0;y  < this.rows[x].Count(); y++)
                {
                    for (int z = 0; z < this.rows[x].get(y).getAgents().Length; z++)
                    {
                        Agent cur = this.rows[x].get(y).getAgent(z);

                        string opinion;

                        if (round)
                            opinion = Math.Round(cur.flex(0) * 100).ToString();
                        else
                            opinion = cur.flex(0).ToString();

                        gridString.AppendLine(string.Format("{0};{1};{2};{3};{4}",x,y,z,(int)cur.fix(0),opinion)); 
                    }
                }
            }

            return gridString.ToString();
        }
    }
}
