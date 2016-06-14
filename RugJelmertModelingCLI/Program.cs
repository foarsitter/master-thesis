using Microsoft.Office.Interop.Excel;
using RugJelmertModelingLogic;
using RugJelmertModelingLogic.Model;
using RugJelmertModelingLogic.Model.Measurements;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace RugJelmertModelingCLI
{
    class Program
    {
        static string output_dir = "";

        [STAThread]
        static void Main(string[] args)
        {
            Properties.Settings config = RugJelmertModelingCLI.Properties.Settings.Default;

            Agent.numFlex = config.nFlexibleArguments;
            Agent.numFix = config.nFixexArguments;

            int nIterations = config.nIterations;
            int nRuns = config.nRuns;
            //int nThreads = config.nThreads;

            string city = args[0].Length > 0 ? args[0] : "groningen";
            string inputFile = "assets/" + city + ".output.v2.csv";

            Console.WriteLine("Master Thesis Model by Jelmer Draaijer!");
            Console.WriteLine(string.Format("Selected file: {0}", inputFile));
            Console.WriteLine(string.Format("Running  {0} runs and {1} iterations", nRuns, nIterations));

            output_dir = config.outputDirectory + Path.DirectorySeparatorChar + "RugJelmertModelingOutput";
            output_dir += Path.DirectorySeparatorChar + city;
            output_dir += Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd");
            //output_dir += Path.DirectorySeparatorChar + string.Format("r{0}_i{1}", nRuns, nIterations);

            Console.WriteLine("Output: " + output_dir);

            Directory.CreateDirectory(output_dir);

            string fileContents = File.ReadAllText(inputFile);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            
            for (var run = 0; run < nRuns; run++)
            {
                //random id for this run. 
                string modelId = Guid.NewGuid().ToString("D").Substring(0, 8);

                string modelPath = output_dir + Path.DirectorySeparatorChar
                        + modelId + Path.DirectorySeparatorChar;

                Directory.CreateDirectory(modelPath + "grid-dumps" + Path.DirectorySeparatorChar);

                ABMInitializer loader = new ABMInitializer();

                AgentBasedModel abm = loader.LoadCSVFile(fileContents);
                abm.calcaluteMeasurementsEachN = nIterations / 100;

                OpinionMeanAndVariance opinionMeanAndVariance = new OpinionMeanAndVariance(abm.Agents.ToArray());
                ExtremeCount ExtremeCountMeasurement = new ExtremeCount(abm);
                OpinionDiversity opinionDiversity = new OpinionDiversity(abm.Agents.ToArray());

                CellAVGDistance dist = new CellAVGDistance(abm.grid);

                OpinionPolarisation opinionPolarisation = new OpinionPolarisation(abm.Agents.OrderBy(item => abm.Random.Next()).ToArray());
                OpinionPolarisation localsPolarisation = new OpinionPolarisation(abm.Locals.OrderBy(item => abm.Random.Next()).ToArray());
                OpinionPolarisation immigrantsPolarsation = new OpinionPolarisation(abm.Immigrants.OrderBy(item => abm.Random.Next()).ToArray());

                abm.clearMeasurements();
                abm.AddMeasurement(opinionDiversity);

                //for each subgroup a different measurement object
                abm.AddMeasurement(opinionPolarisation);
                abm.AddMeasurement(localsPolarisation);
                abm.AddMeasurement(immigrantsPolarsation);
                abm.AddMeasurement(dist);

                abm.AddMeasurement(opinionMeanAndVariance);
                abm.AddMeasurement(ExtremeCountMeasurement);


                abm.ResetIterationCount();

                for (int i = 0; i < nIterations; i++)
                {
                    abm.RunIteration();

                    // eerste niet, laatste wel. 
                    if (i % (nIterations / 100) == 0)
                    {
                        File.WriteAllText(modelPath
                            + "grid-dumps"
                            + Path.DirectorySeparatorChar
                            + (i).ToString("D5") + ".grid.csv", abm.grid.Serialize());
                    }
                }

                abm.calculateMeasures();

                // write the final state
                File.WriteAllText(modelPath
                    + "grid-dumps"
                    + Path.DirectorySeparatorChar
                    + (nIterations).ToString("D5") + ".grid.csv", abm.grid.Serialize());


                using (StringWriter csv = new StringWriter())
                {
                    csv.WriteLine(string.Join(";", new string[]{
                            "Diversity",
                            "Diversity locals",
                            "Diversity immigrants",
                            "Polarisation",
                            "Polarisation locals",
                            "Polarisation immigrants",
                            "Extreme",
                            "Extreme locals",
                            "Extreme immigrants",
                            "Mean",
                            "Mean locals",
                            "Mean immigrants",
                            "Variance",
                            "Variance locals",
                            "Variance immigrants",
                            "AbsMean",
                            "AbsMean locals",
                            "AbsMean immigrants",
                            "AbsVariance",
                            "AbsVariance locals",
                            "AbsVariance immigrants", "RPBI","RPBI ABS","CellAVGDistance"}));

                    for (int i = 0; i < opinionDiversity.diversityTotal.Count; i++)
                    {
                        var newLine = string.Join(";", new double[] {
                                                    opinionDiversity.diversityTotal[i],
                                                    opinionDiversity.diversityLocals[i],
                                                    opinionDiversity.diversityImmigrants[i],
                                                    opinionPolarisation.polarization[i],
                                                    localsPolarisation.polarization[i],
                                                    immigrantsPolarsation.polarization[i],
                                                    ExtremeCountMeasurement.historyTotal[i],
                                                    ExtremeCountMeasurement.historyLocals[i],
                                                    ExtremeCountMeasurement.historyImmigrants[i],
                                                    opinionMeanAndVariance.meanAll[i],
                                                    opinionMeanAndVariance.meanLocals[i],
                                                    opinionMeanAndVariance.meanImmigrants[i],
                                                    opinionMeanAndVariance.varianceAll[i],
                                                    opinionMeanAndVariance.varianceLocals[i],
                                                    opinionMeanAndVariance.varianceImmigrants[i],
                                                    opinionMeanAndVariance.meanAllAbs[i],
                                                    opinionMeanAndVariance.meanLocalsAbs[i],
                                                    opinionMeanAndVariance.meanImmigrantsAbs[i],
                                                    opinionMeanAndVariance.varianceAllAbs[i],
                                                    opinionMeanAndVariance.varianceLocalsAbs[i],
                                                    opinionMeanAndVariance.varianceImmigrantsAbs[i],
                                                    opinionMeanAndVariance.rpbi[i],
                                                    opinionMeanAndVariance.absPpbi[i],
                                                    dist.cellAVG[i]

                            });

                        csv.WriteLine(newLine);
                    }

                    File.WriteAllText(modelPath + modelId + ".measures.model.csv", csv.ToString());


                }
                Console.WriteLine("Finished run " + run);
            }


            watch.Stop();
            Console.WriteLine("Time elapsed: {0:hh\\:mm\\:ss}", watch.Elapsed);
            Console.WriteLine("Done...");
            Console.ReadLine();
        }

        public static AgentBasedModel meta(string filename)
        {
            AgentBasedModel abm = null;

            int rows = 0;
            int colums = 0;
            int cells = 0;
            int agents = 0;
            int t_immigrants = 0;

            using (StreamReader reader = new StreamReader(filename))
            {
                string cvs_line;

                while ((cvs_line = reader.ReadLine()) != null)
                {
                    if (!cvs_line.StartsWith("x"))
                    {
                        string[] parts = cvs_line.Split(';');

                        int x = Convert.ToInt16(parts[0]);
                        int y = Convert.ToInt16(parts[1]);

                        double addresses = double.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture) / 1000;
                        double households = double.Parse(parts[4], System.Globalization.CultureInfo.InvariantCulture);
                        double immigrants = double.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture) / 100;
                        double residents = addresses * households;

                        if (residents > cells)
                            cells = (int)residents + 1;

                        if (x > rows)
                            rows = x + 1;
                        if (y > colums)
                            colums = y + 1;

                        agents += (int)residents;

                        t_immigrants += (int)Math.Round(immigrants * residents);

                    }

                }
            }

            abm = new AgentBasedModel();

            // the highest row/column values +1 one is the actual array lenght, because we start at 0. 
            abm.grid.initEmpty(rows + 2, colums + 2);

            Console.WriteLine(string.Format("Rows: {0}, Columns: {1}, Agents: {2}, Immigrants: {3}", rows, colums, agents, t_immigrants));

            return abm;
        }
    }
}
