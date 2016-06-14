using RugJelmertModelingLogic;
using RugJelmertModelingLogic.Model;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RugJelmertModelingResultView
{
    class CronbachAlphaGridByCell
    {
        /// <summary>
        /// Row/Column and Column/Row so we can easy call the mean(), sum() and variance() methods. 
        /// </summary>
        Dictionary<string, List<double>> byPostion = new Dictionary<string, List<double>>();

        private bool _absolute = false;

        public CronbachAlphaGridByCell(bool abs)
        {
            this._absolute = abs;
        }

        public void AddRun(string data)
        {
            ABMInitializer init = new ABMInitializer();

            AgentBasedModel abm = init.LoadCSVFile(data);

            for (int x = 0; x < abm.grid.nRows; x++)
            {
                for (int y = 0; y < abm.grid.nCol; y++)
                {
                    GridColumn col = abm.grid.getColumn(x, y);

                    if (!col.isEmpty())
                    {
                        double mean = col.getMeanOpinion(this._absolute);

                        Add(x, y, mean);
                    }
                }
            }
        }

        private void Add(int x, int y, double mean)
        {
            string key1 = string.Format("{0};{1}", x, y);

            if (!this.byPostion.ContainsKey(key1))
                this.byPostion.Add(key1, new List<double>());


            this.byPostion[key1].Add(mean);            
        }

        public void writeFile(string path)
        {
            StringBuilder builder = new StringBuilder();

            foreach (KeyValuePair<string,List<double>> item in this.byPostion)
            {
                string values = string.Join(";", item.Value);

                builder.AppendLine(string.Join(";", new object[]{ item.Key.ToString(), values }));
            }

            string text = builder.ToString();

            File.WriteAllText(path, text);

        }


    }
}
