using Microsoft.Office.Interop.Excel;
using RugJelmertModelingLogic;
using RugJelmertModelingLogic.Model;
using RugJelmertModelingLogic.Model.Measurements;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace RugJelmertModelingCLI
{
    class Program
    {

        static Stopwatch time10kOperations = Stopwatch.StartNew();

        [STAThread]
        static void Main(string[] args)
        {            
            AgentBasedModel abm = new AgentBasedModel();
            Agent.numFlex = 1;

            //commit!

            OpenFileDialog ofd = new OpenFileDialog();

            DialogResult result = ofd.ShowDialog();

            if(result == DialogResult.OK)
            {
                Console.WriteLine(string.Format("Loading file: {0}", ofd.FileName));

                initialize(ofd.FileName, abm);
                initMeasurements(abm);

                System.Threading.Timer t = new System.Threading.Timer(TimerCallback, null, 0, 1000);

                Thread runThreat = new Thread(delegate() {
                    Console.WriteLine("Start...");

                    for (int i = 0; i < 100; i++)
                    {
                        abm.RunIteration();
                    }


                    handleMeasurements(abm);
                    createEXCEL();

                    t.Dispose();
                    Console.WriteLine("");
                    Console.WriteLine("Done...");
                });

                runThreat.Start();
            }
            
            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {
            Console.CursorLeft -= Console.CursorLeft;
            
            long mili = time10kOperations.ElapsedMilliseconds;
            long seconds = mili / 1000;
            long minutes = seconds / 60;

            TimeSpan ts = time10kOperations.Elapsed;
                        
            Console.Write(ts.ToString("hh\\:mm\\:ss"));           
        } 
            
        public static void initialize(string filename, AgentBasedModel abm)
        {
            abm.grid.initEmpty(10, 10);

            CSVParser parser = new CSVParser(abm);

            parser.parse(filename);                 
        }

        static OpinionDiversity opinionDiversity = new OpinionDiversity();
        static OpinionMeanAndVariance opinionMeanAndVariance = new OpinionMeanAndVariance();
        static OpinionPolarisation opinionPolarisation = new OpinionPolarisation();

        public static void initMeasurements(AgentBasedModel abm)
        {
            abm.AddMeasurement(opinionDiversity);
            abm.AddMeasurement(opinionMeanAndVariance);
            abm.AddMeasurement(opinionPolarisation);
        }

        public static void handleMeasurements(AgentBasedModel abm)
        {
            
            //before your loop
            var csv = new StringBuilder();
            
            csv.AppendLine(string.Format("{0};{1};{2}", "opinionDiversity", "opinionMeanAndVariance", "opinionPolarisation"));

            for (int i = 0; i< abm.IterationCount; i++)
            {
                //in your loop              
                var newLine = string.Format("{0};{1};{2}", opinionDiversity.getItem(i), opinionMeanAndVariance.getItem(i), opinionPolarisation.getItem(i));
                csv.AppendLine(newLine);
            }

            File.WriteAllText("results.csv", csv.ToString());
        }


        public static void createEXCEL()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


            xlWorkSheet.Cells[1, 1] = "Diversity";
            xlWorkSheet.Cells[2, 1] = "Polarisation";
            xlWorkSheet.Cells[3, 1] = "Variance";
            xlWorkSheet.Cells[4, 1] = "Mean";
            Excel.Range r1 = xlWorkSheet.get_Range("B1", "CW1");
            Excel.Range r2 = xlWorkSheet.get_Range("B2", "CW2");
            Excel.Range r3 = xlWorkSheet.get_Range("B3", "CW3");
            Excel.Range r4 = xlWorkSheet.get_Range("B4", "CW4");

            double[] d1 = opinionDiversity.getResult().ToArray();
            double[] d2 = opinionPolarisation.getResult().ToArray();
            double[] d3 = opinionMeanAndVariance.getResult().ToArray();
            double[] d4 = opinionMeanAndVariance.getVariance().ToArray();
            r1.Value = d1;
            r2.Value = d2;
            r3.Value = d3;
            r4.Value = d4;
            
            //xlWorkSheet.get_Range("B2", "CW2").Value = opinionMeanAndVariance.getResult().ToArray();
            //xlWorkSheet.get_Range("B3", "CW2").Value = opinionPolarisation.getResult().ToArray();

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            Excel.Chart chartPage = myChart.Chart;
            
            chartPage.SetSourceData(xlWorkSheet.get_Range("A1","CW1"), misValue);
            chartPage.ChartType = Excel.XlChartType.xlLine;
            //chartPage.Name = "Diversity";
            

            Excel.ChartObjects xlCharts2 = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart2 = (Excel.ChartObject)xlCharts.Add(350, 80, 300, 250);
            Excel.Chart chartPage2 = myChart2.Chart;

            chartPage2.SetSourceData(xlWorkSheet.get_Range("A2", "CW2"), misValue);
            chartPage2.ChartType = Excel.XlChartType.xlLine;
            //chartPage2.Name = "Mean";


            Excel.ChartObjects xlCharts3 = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart3 = (Excel.ChartObject)xlCharts.Add(10, 350, 300, 250);
            Excel.Chart chartPage3 = myChart3.Chart;

            chartPage3.SetSourceData(xlWorkSheet.get_Range("A3", "CW3"), misValue);
            chartPage3.ChartType = Excel.XlChartType.xlLine;
            //chartPage3.Name = "Variance";


            Excel.ChartObjects xlCharts4 = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart4 = (Excel.ChartObject)xlCharts.Add(350, 350, 300, 250);
            Excel.Chart chartPage4 = myChart4.Chart;

            chartPage4.SetSourceData(xlWorkSheet.get_Range("A4", "CW4"), misValue);
            chartPage4.ChartType = Excel.XlChartType.xlLine;
            //chartPage4.Name = "Polarisation";
            xlApp.Visible = true;
            //xlWorkBook.SaveAs("csharp.net-informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
