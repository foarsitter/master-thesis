using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingResultView
{
    class SimpleGrid
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, List<double>>>> _grid;

        public Dictionary<string, string> _group = new Dictionary<string, string>();
        public int pointer = 0;


        public bool IsAbsolute = false;

        public SimpleGrid(bool isAbsolute)
        {
            this.IsAbsolute = isAbsolute;

            this._grid = new Dictionary<string, Dictionary<string, Dictionary<string, List<double>>>>();
        }

        public void pushNewAgent(string x, string y, string z, string group, double opinion)
        {
            if (!this._grid.ContainsKey(x))
            {
                this._grid.Add(x, new Dictionary<string, Dictionary<string, List<double>>>());
            }

            if (!this._grid[x].ContainsKey(y))
            {
                this._grid[x].Add(y, new Dictionary<string, List<double>>());

            }

            if (!this._grid[x][y].ContainsKey(z))
            {
                this._grid[x][y].Add(z, new List<double>());
                this._group.Add(string.Concat(x, ",", y, ",", z), group);
            }

            this.pushOpinion(x, y, z, 0, opinion);
        }

        public void pushOpinion(string x, string y, string z, int iteration, double opinion)
        {
            if (this.IsAbsolute)
                this._grid[x][y][z].Add(Math.Abs(opinion));
            else
                this._grid[x][y][z].Add(opinion);
        }

        public void WriteFile(string path)
        {
            StringBuilder build = new StringBuilder();

            foreach (string x in _grid.Keys)
            {
                foreach (string y in _grid[x].Keys)
                {
                    foreach (string z in _grid[x][y].Keys)
                    {
                        List<double> opinions = _grid[x][y][z].ToList<double>();

                        double mean = opinions.Sum() / opinions.Count;

                        string group = this._group[string.Concat(x, ",", y, ",", z)];

                        build.AppendLine(string.Format("{0};{1};{2};{3};{4};{5}", x, y, z, group, mean, string.Join(";", opinions)));
                    }
                }
            }

            File.WriteAllText(path, build.ToString());
        }

        public void meanDistance(string path)
        {
            // voor elke x,y moeten we het absolute gemiddelde uitrekenen van de twee groeps gemiddeldenden. 

            StringBuilder build = new StringBuilder();

            foreach (string x in _grid.Keys)
            {
                foreach (string y in _grid[x].Keys)
                {
                    double localsTotal = 0;
                    int localsCount = 0;

                    double immigrantsTotal = 0;
                    int immigrantsCount = 0;

                    foreach (string z in _grid[x][y].Keys)
                    {
                        string group = this._group[string.Concat(x, ",", y, ",", z)];

                        List<double> opinions = _grid[x][y][z].ToList<double>();

                        if (group == "1")
                        {
                            localsCount++;
                            localsTotal += opinions.Last();
                        }
                        else
                        {
                            immigrantsCount++;
                            immigrantsTotal += opinions.Last();
                        }
                    }                    

                    if(immigrantsCount > 0 && localsCount > 0)
                    {
                        double meanLocals = localsTotal / localsCount;
                        double meanImmigrants = immigrantsTotal / immigrantsCount;

                        build.AppendLine(string.Join(";",new object[] { x,y,Math.Abs(meanLocals-meanImmigrants) }));
                    }
                }
            }

            File.WriteAllText(path, build.ToString());
        }
    }

    public static class MyListExtensions
    {
        public static double Mean(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.Mean(0, values.Count);
        }

        public static double Mean(this List<double> values, int start, int end)
        {
            double s = 0;

            for (int i = start; i < end; i++)
            {
                s += values[i];
            }

            return s / (end - start);
        }

        public static double Variance(this List<double> values)
        {
            return values.Variance(values.Mean(), 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean)
        {
            return values.Variance(mean, 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean, int start, int end)
        {
            double variance = 0;

            for (int i = start; i < end; i++)
            {
                variance += Math.Pow((values[i] - mean), 2);
            }

            int n = end - start;
            if (start > 0) n -= 1;

            return variance / (n);
        }

        public static double StandardDeviation(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.StandardDeviation(0, values.Count);
        }

        public static double StandardDeviation(this List<double> values, int start, int end)
        {
            double mean = values.Mean(start, end);
            double variance = values.Variance(mean, start, end);

            return Math.Sqrt(variance);
        }
    }
}
