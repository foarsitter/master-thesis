using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingResultView
{
    class CronbachAlphaAgentByCell
    {
        //we arent interested in the neighbours, so a flat structures is good enough. 
        public Dictionary<string, Dictionary<string, Dictionary<string, List<double>>>> _runsTotal;
        public Dictionary<string, Dictionary<string, Dictionary<string, List<double>>>> _runsLocals;
        public Dictionary<string, Dictionary<string, Dictionary<string, List<double>>>> _runsImmigrants;
        private Dictionary<string, Dictionary<string, double>> _cronbachAlphaTotal;
        private Dictionary<string, Dictionary<string, double>> _cronbachAlphaLocals;
        private Dictionary<string, Dictionary<string, double>> _cronbachAlphaImmigrants;

        private bool _isAbsolute = false;

        public CronbachAlphaAgentByCell(bool isAbsolute)
        {
            this._isAbsolute = isAbsolute;

            this._runsTotal = new Dictionary<string, Dictionary<string, Dictionary<string, List<double>>>>();
            this._runsLocals = new Dictionary<string, Dictionary<string, Dictionary<string, List<double>>>>();
            this._runsImmigrants = new Dictionary<string, Dictionary<string, Dictionary<string, List<double>>>>();

            this._cronbachAlphaTotal = new Dictionary<string, Dictionary<string, double>>();
            this._cronbachAlphaLocals = new Dictionary<string, Dictionary<string, double>>();
            this._cronbachAlphaImmigrants = new Dictionary<string, Dictionary<string, double>>();
        }

        public void pushNewAgent(string x, string y, string runId, string groupId, double opinion)
        {
            if (!this._runsTotal.ContainsKey(x))
            {
                this._runsTotal.Add(x, new Dictionary<string, Dictionary<string, List<double>>>());
                this._runsLocals.Add(x, new Dictionary<string, Dictionary<string, List<double>>>());
                this._runsImmigrants.Add(x, new Dictionary<string, Dictionary<string, List<double>>>());

                this._cronbachAlphaTotal.Add(x, new Dictionary<string, double>());
                this._cronbachAlphaLocals.Add(x, new Dictionary<string, double>());
                this._cronbachAlphaImmigrants.Add(x, new Dictionary<string, double>());
            }

            if (!this._runsTotal[x].ContainsKey(y))
            {
                this._runsTotal[x].Add(y, new Dictionary<string, List<double>>());
                this._runsLocals[x].Add(y, new Dictionary<string, List<double>>());
                this._runsImmigrants[x].Add(y, new Dictionary<string, List<double>>());
            }

            if (!this._runsTotal[x][y].ContainsKey(runId))
            {
                this._runsTotal[x][y].Add(runId, new List<double>());
                this._runsLocals[x][y].Add(runId, new List<double>());
                this._runsImmigrants[x][y].Add(runId, new List<double>());
            }

            this.pushAgent(x, y, runId, groupId, opinion);
        }

        public void pushAgent(string x, string y, string runId, string groupId, double opinion)
        {
            if (!this._runsTotal[x][y].ContainsKey(runId))
            {
                this._runsTotal[x][y].Add(runId, new List<double>());
                this._runsLocals[x][y].Add(runId, new List<double>());
                this._runsImmigrants[x][y].Add(runId, new List<double>());
            }

            if (this._isAbsolute)
            {
                opinion = Math.Abs(opinion);
            }

            this._runsTotal[x][y][runId].Add(opinion);

            if (groupId == "-1")
            {
                this._runsImmigrants[x][y][runId].Add(opinion);
            }
            else
            {
                this._runsLocals[x][y][runId].Add(opinion);
            }
        }

        public void calculate(SimpleGrid grid)
        {
            foreach (var x in this._runsTotal.Keys)
            {
                foreach (var y in this._runsTotal[x].Keys)
                {
                    List<double> variances = new List<double>();
                    List<double> totals = new List<double>();

                    List<double> variancesL = new List<double>();
                    List<double> totalsL = new List<double>();

                    List<double> variancesI = new List<double>();
                    List<double> totalsI = new List<double>();

                    int k = 0;
                    int kL = 0;
                    int kI = 0;

                    foreach (var runId in this._runsTotal[x][y].Keys)
                    {
                        //total group
                        variances.Add(this._runsTotal[x][y][runId].Variance());
                        totals.Add(this._runsTotal[x][y][runId].Sum());
                        k += this._runsTotal[x][y][runId].Count;

                        //locals group
                        variancesL.Add(this._runsLocals[x][y][runId].Variance());
                        totalsL.Add(this._runsLocals[x][y][runId].Sum());
                        kL += this._runsLocals[x][y][runId].Count;

                        //immigrant group
                        variancesI.Add(this._runsImmigrants[x][y][runId].Variance());
                        totalsI.Add(this._runsImmigrants[x][y][runId].Sum());
                        kI += this._runsImmigrants[x][y][runId].Count;
                    }


                    List<double> columnVarTotal = new List<double>();
                    List<double> columnVarLocals = new List<double>();
                    List<double> columnVarImmigrants = new List<double>();

                    foreach (var z in grid._grid[x][y].Keys)
                    {
                        columnVarTotal.Add(grid._grid[x][y][z].ToList<double>().Sum());

                        string id = string.Concat(x, ",", y, ",", z);

                        if (grid._group[id] == "-1")
                        {
                            columnVarImmigrants.Add(grid._grid[x][y][z].ToList<double>().Sum());
                        }
                        else
                        {
                            columnVarLocals.Add(grid._grid[x][y][z].ToList<double>().Sum());
                        }

                    }

                    //totals 
                    double sum_var_total = variances.Sum();
                    double var_individual_total = columnVarTotal.Variance();

                    double cronbach_alpha_total;

                    if (sum_var_total == 0)
                    {
                        cronbach_alpha_total = 1;
                    }
                    else
                    {
                        cronbach_alpha_total = (k / (k - 1)) * (1 - sum_var_total / var_individual_total);
                    }

                    _cronbachAlphaTotal[x].Add(y, cronbach_alpha_total);

                    //locals 
                    double sum_var_locals = variancesL.Sum();
                    double var_individual_locals = columnVarTotal.Variance();

                    double cronbach_alpha_locals;

                    if (sum_var_locals == 0 && kL > 1)
                    {
                        cronbach_alpha_locals = 1;
                    }
                    else
                    {
                        cronbach_alpha_locals = (kL / (kL - 1)) * (1 - sum_var_locals / var_individual_locals);
                    }

                    _cronbachAlphaLocals[x].Add(y, cronbach_alpha_locals);

                    //immigrants 
                    double sum_var_immigrants = variancesL.Sum();
                    double var_individual_immigrants = columnVarTotal.Variance();

                    double cronbach_alpha_immigrants;

                    if (sum_var_immigrants == 0 && kI > 1)
                    {
                        cronbach_alpha_immigrants = 1;
                    }
                    else
                    {
                        cronbach_alpha_immigrants = (kI / (kI - 1)) * (1 - sum_var_immigrants / var_individual_immigrants);
                    }

                    _cronbachAlphaImmigrants[x].Add(y, cronbach_alpha_immigrants);

                }
            }
        }

        public void WriteFile(string path)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("x;y;total;k;local;k;immigrants;k");

            foreach (var x in this._cronbachAlphaTotal.Keys)
            {
                foreach (var y in this._cronbachAlphaTotal[x].Keys)
                {
                    builder.AppendLine(string.Join(";", 
                        new object[] { x, y,
                            this._cronbachAlphaTotal[x][y],
                            this._runsTotal[x][y].First().Value.Count(),
                            this._cronbachAlphaLocals[x][y],
                            this._runsLocals[x][y].First().Value.Count(),
                            this._cronbachAlphaImmigrants[x][y],
                            this._runsImmigrants[x][y].First().Value.Count(),
                        }));
                }
            }

            File.WriteAllText(path, builder.ToString());
        }
    }
}
