using RugJelmertModelingGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;

namespace RugJelmertModelingResultView
{
    public partial class ResultViewForm : Form
    {
        public ResultViewForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //openDirBrowser();
        }

        private void openDirectoryMenuItem_Click(object sender, EventArgs e)
        {
            openDirBrowser();
        }

        private void openDirBrowser()
        {
            DialogResult d = browseFolderDialog.ShowDialog();

            if (d == DialogResult.OK)
            {
                openDirectory(browseFolderDialog.SelectedPath);
            }
        }


        private void changeFormTitle(string title)
        {
            this.Text = "Result View (" + title + ")";
        }

        string[] fileEntries;
        string currentPath;
        SearchOption _currentSearchOption = SearchOption.AllDirectories;

        private void openDirectory(string path)
        {
            this.currentPath = path;
            this.changeFormTitle(path);
            fileEntries = Directory.GetFiles(path, "*.model.csv", this._currentSearchOption);

            List<dynamic> dynList = new List<dynamic>();

            foreach (string file in fileEntries)
            {
                string[] parts = Path.GetDirectoryName(file).Split(Path.DirectorySeparatorChar);

                dynList.Add(new { Name = parts[parts.Length - 1], Path = file });
            }

            modelRunsListBox.DataSource = dynList;

            modelRunsListBox.DisplayMember = "Name";
            modelRunsListBox.ValueMember = "Path";
            modelRunsListBox.Visible = true;
            modelRunsListBox.Enabled = true;
        }

        Dictionary<string, List<double>> csv = null;

        string selectedRun;

        private void showSelectedData(object sender, EventArgs e)
        {
            ListBox listbox = (ListBox)sender;

            string path = ((dynamic)listbox.SelectedItem).Path;


            this.selectedRun = Path.GetDirectoryName(path);

            csv = loadCSV(path);
            loadSeries();

            if (this.current_action == "all")
            {
                drawAllCharts();
            }
            else
            {
                drawChartSerie(this.current_action);
            }

        }

        private Dictionary<string, List<double>> loadCSV(string path)
        {
            Dictionary<string, List<double>> csv = null;

            using (StreamReader reader = new StreamReader((path)))
            {
                string line;

                string[] heading = null;

                csv = new Dictionary<string, List<double>>();

                while ((line = reader.ReadLine()) != null)
                {
                    String[] row = line.Split(';');

                    if (heading == null)
                    {
                        heading = row;

                        foreach (var item in row)
                        {
                            csv.Add(item, new List<double>());
                        }
                    }
                    else
                    {
                        for (int i = 0; i < row.Length; i++)
                        {
                            csv[heading[i]].Add(double.Parse(row[i].Replace(",", "."), CultureInfo.InvariantCulture));
                        }
                    }
                }
            }

            return csv;
        }

        HashSet<string> chart_names = new HashSet<string>();

        private void loadSeries()
        {
            foreach (string item in this.csv.Keys.ToArray<string>())
            {
                string[] parts = item.Split(' ');
                this.chart_names.Add(parts[0]);
            }

            updateSeriesMenuItem();
        }



        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawAllCharts();
        }

        private void updateSeriesMenuItem()
        {
            seriesDropDown.DropDownItems.Clear();

            foreach (string item in this.chart_names.ToArray<string>())
            {
                string[] parts = item.Split(' ');

                ToolStripMenuItem serieItem = new ToolStripMenuItem();

                serieItem.Click += delegate (System.Object o, System.EventArgs _e)
                {
                    drawChartSerie(((ToolStripMenuItem)o).Text);
                };

                serieItem.Text = parts[0];
                seriesDropDown.DropDownItems.Add(serieItem);
            }
        }

        private string current_action = "Mean";

        private void drawChartSerie(string serie)
        {
            this.current_action = serie;

            splitContainer1.Panel2.Invalidate();
            splitContainer1.Panel2.Controls.Clear();

            int width = splitContainer1.Panel2.Width;
            int height = width / 2;
            int i = 0;

            splitContainer1.Panel2.AutoScroll = false;

            Chart a = new Chart();

            ChartArea b = a.ChartAreas.Add(serie);

            foreach (KeyValuePair<string, List<double>> entry in this.csv)
            {
                if (entry.Key.Split(' ')[0] == serie)
                {
                    Series s = a.Series.Add(entry.Key);

                    s.Name = entry.Key;
                    s.Points.DataBindY(entry.Value);
                    s.ChartType = SeriesChartType.Line;
                }
            }

            a.Titles.Add(serie);
            a.Dock = DockStyle.Fill;
            a.Location = new System.Drawing.Point(0, i * height);
            a.Size = new System.Drawing.Size(width, height);
            i++;

            a.Legends.Add("MyLegend");
            splitContainer1.Panel2.Controls.Add(a);


        }

        private void drawAllCharts()
        {

            splitContainer1.Panel2.Invalidate();
            splitContainer1.Panel2.Controls.Clear();

            int width = splitContainer1.Panel2.Width;
            int height = width / 2;
            int i = 0;

            Dictionary<string, Chart> charts = new Dictionary<string, Chart>();

            foreach (string chart_name in chart_names)
            {
                charts.Add(chart_name, new Chart());
            }

            foreach (KeyValuePair<string, List<double>> entry in this.csv)
            {
                Chart a = charts[entry.Key.Split(' ')[0]];
                ChartArea b = a.ChartAreas.Add(entry.Key);
                Series s = a.Series.Add(entry.Key);
                s.Name = entry.Key;

                s.Points.DataBindY(entry.Value);
                s.ChartType = SeriesChartType.Line;
            }


            foreach (KeyValuePair<string, Chart> item in charts)
            {
                Chart a = item.Value;
                a.Titles.Add(item.Key);
                a.Location = new System.Drawing.Point(0, i * height);
                a.Size = new System.Drawing.Size(width, height);
                i++;

                a.Legends.Add("MyLegend");
                splitContainer1.Panel2.Controls.Add(a);
            }

        }

        private void byAVGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<double>> sum = sumAllFiles();

            Dictionary<string, List<double>> result = new Dictionary<string, List<double>>();

            int bucketSize = 10;

            string outRefBucketSize = string.Empty;

            if (InputBox("Bucketsize", "Hoe groot moet de bucket zijn?", ref outRefBucketSize) == DialogResult.OK)
            {
                bucketSize = int.Parse(outRefBucketSize);
            }

            foreach (KeyValuePair<string, List<double>> item in sum)
            {
                int lenght = item.Value.Count() / bucketSize;

                result[item.Key] = new List<double>();

                for (int i = 0; i < lenght; i++)
                {
                    result[item.Key].Add(item.Value.GetRange(i * bucketSize, bucketSize).Sum() / (bucketSize * fileEntries.Length));
                }
            }

            writeToExcel(result);
        }

        private void writeToExcel(Dictionary<string, List<double>> data)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet dataSheet;

            //Start Excel and get Application object.
            oXL = new Excel.Application();


            //Get a new workbook.
            oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));

            Excel._Worksheet chartSheet = (Excel._Worksheet)oWB.ActiveSheet;
            dataSheet = oWB.Worksheets.Add();

            int column = 1;
            int maxI = 0;
            foreach (KeyValuePair<string, List<double>> item in data)
            {
                dataSheet.Cells[1, column] = item.Key;

                for (int i = 0; i < item.Value.Count(); i++)
                {
                    dataSheet.Cells[2 + i, column] = item.Value[i];
                    maxI = i;
                }

                column++;
            }

            // diversity A B C
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(0, 0, 300, 250);
            Excel.Chart chartPage = myChart.Chart;

            chartPage.SetSourceData(dataSheet.get_Range("A1", "C" + maxI), System.Reflection.Missing.Value);
            chartPage.ChartType = Excel.XlChartType.xlLine;

            // polarisation D E F 
            Excel.ChartObjects xlCharts2 = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart2 = (Excel.ChartObject)xlCharts.Add(300, 0, 300, 250);
            Excel.Chart chartPage2 = myChart2.Chart;

            chartPage2.SetSourceData(dataSheet.get_Range("D1", "F" + maxI), System.Reflection.Missing.Value);
            chartPage2.ChartType = Excel.XlChartType.xlLine;

            // extreme count  G H I 
            Excel.ChartObjects xlCharts3 = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart3 = (Excel.ChartObject)xlCharts.Add(600, 0, 300, 250);
            Excel.Chart chartPage3 = myChart3.Chart;

            chartPage3.SetSourceData(dataSheet.get_Range("G1", "I" + maxI), System.Reflection.Missing.Value);
            chartPage3.ChartType = Excel.XlChartType.xlLine;

            // mean J K L 
            Excel.ChartObjects xlCharts4 = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart4 = (Excel.ChartObject)xlCharts.Add(0, 300, 300, 250);
            Excel.Chart chartPage4 = myChart4.Chart;

            chartPage4.SetSourceData(dataSheet.get_Range("J1", "L" + maxI), System.Reflection.Missing.Value);
            chartPage4.ChartType = Excel.XlChartType.xlLine;

            // variance M N O
            Excel.ChartObjects xlCharts5 = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart5 = (Excel.ChartObject)xlCharts.Add(300, 300, 300, 250);
            Excel.Chart chartPage5 = myChart5.Chart;

            chartPage5.SetSourceData(dataSheet.get_Range("M1", "O" + maxI), System.Reflection.Missing.Value);
            chartPage5.ChartType = Excel.XlChartType.xlLine;

            // meanABs P Q R 
            Excel.ChartObjects xlCharts6 = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart6 = (Excel.ChartObject)xlCharts.Add(0, 600, 300, 250);
            Excel.Chart chartPage6 = myChart6.Chart;

            chartPage6.SetSourceData(dataSheet.get_Range("P1", "R" + maxI), System.Reflection.Missing.Value);
            chartPage6.ChartType = Excel.XlChartType.xlLine;

            // varianceABS S T U
            Excel.ChartObjects xlCharts7 = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart7 = (Excel.ChartObject)xlCharts.Add(300, 600, 300, 250);
            Excel.Chart chartPage7 = myChart7.Chart;

            chartPage7.SetSourceData(dataSheet.get_Range("S1", "U" + maxI), System.Reflection.Missing.Value);
            chartPage7.ChartType = Excel.XlChartType.xlLine;

            // Point-biserial correlation coefficients V W
            Excel.ChartObjects xlCharts8 = (Excel.ChartObjects)chartSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart8 = (Excel.ChartObject)xlCharts.Add(600, 600, 300, 250);
            Excel.Chart chartPage8 = myChart8.Chart;

            chartPage8.SetSourceData(dataSheet.get_Range("V1", "W" + maxI), System.Reflection.Missing.Value);
            chartPage8.ChartType = Excel.XlChartType.xlLine;

            oXL.Visible = true;
        }

        private Dictionary<string, List<double>> sumAllFiles()
        {
            Dictionary<string, List<double>> sum = null;

            foreach (string csvFile in fileEntries)
            {
                Dictionary<string, List<double>> data = loadCSV(csvFile);

                if (sum == null)
                {
                    sum = data;
                }
                else
                {
                    foreach (KeyValuePair<string, List<double>> item in data)
                    {
                        sum[item.Key] = sum[item.Key].Zip(item.Value, (a, b) => (a + b)).ToList<double>();
                    }
                }
            }

            return sum;
        }


        private void byIterationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<double>> sum = sumAllFiles();

            Dictionary<string, List<double>> result = new Dictionary<string, List<double>>();

            foreach (KeyValuePair<string, List<double>> item in sum)
            {
                result.Add(item.Key, sum[item.Key].Select(x => x / fileEntries.Length).ToList());
            }

            writeToExcel(result);
        }

        private void Series_Click(object sender, EventArgs e)
        {

        }

        private void ResultViewForm_Shown(object sender, EventArgs e)
        {
            //openDirBrowser();
            openDirectory(@"C:\RugJelmertModelingOutput\RugJelmertModelingOutput\Breda");
        }

        private void subDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            subDirectoriesToolStripMenuItem.Checked = !subDirectoriesToolStripMenuItem.Checked;

            if (subDirectoriesToolStripMenuItem.Checked)
            {
                this._currentSearchOption = SearchOption.AllDirectories;
            }
            else
            {
                this._currentSearchOption = SearchOption.TopDirectoryOnly;
            }
        }

        /// <summary>
        /// Borrowed from http://www.csharp-examples.net/inputbox/
        /// </summary>
        /// <param name="title"></param>
        /// <param name="promptText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void gridByAVGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.GridSummaryCalc(true);
        }

        private void GridSummaryCalc(bool abosolute)
        {
            SimpleGrid grid = new SimpleGrid(abosolute);
            CronbachAlphaAgentByCell alpha = new CronbachAlphaAgentByCell(abosolute);

            bool firstTime = true;

            string[] grids = Directory.GetFiles(currentPath, "20000.grid.csv", SearchOption.AllDirectories);

            for (int i = 0; i < grids.Length; i++)
            {
                using (StringReader reader = new StringReader(File.ReadAllText(grids[i])))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');

                        string x = parts[0];
                        string y = parts[1];
                        string z = parts[2];
                        string group = parts[3];

                        double opinion = double.Parse(parts[4].Replace(',', '.'), CultureInfo.InvariantCulture);

                        if (firstTime)
                        {
                            grid.pushNewAgent(x, y, z, group, opinion);
                            alpha.pushNewAgent(x, y, i.ToString(), group, opinion);
                        }
                        else
                        {
                            grid.pushOpinion(x, y, z, i, opinion);
                            alpha.pushAgent(x, y, i.ToString(), group, opinion);
                        }
                    }

                    firstTime = false;
                }

                grid.pointer++;
            }

            alpha.calculate(grid);

            string abs = (abosolute) ? "abs." : "";

            grid.WriteFile(currentPath + Path.DirectorySeparatorChar + abs + "summary.grid.csv");

            if (!abosolute)
                grid.meanDistance(currentPath + Path.DirectorySeparatorChar + "cell.avg.csv");

            alpha.WriteFile(currentPath + Path.DirectorySeparatorChar + abs + "crombach.alpha.grid.csv");
        }


        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridViewForm gvv = new GridViewForm(this.selectedRun + Path.DirectorySeparatorChar + "grid-dumps");
            gvv.Show();
        }

        private void gridByAVGToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.GridSummaryCalc(false);
        }

        private void aLLToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            string directory = this.currentPath + Path.DirectorySeparatorChar + "_aggregated" + Path.DirectorySeparatorChar; 
            
            Directory.CreateDirectory(directory);

            bool clear = true;

            foreach (string csvFile in fileEntries)
            {
                Dictionary<string, List<double>> data = loadCSV(csvFile);
                
                foreach (KeyValuePair<string, List<double>> item in data)
                {
                    string toWrite = string.Join(";", item.Value.ToArray());

                    string filename = directory + item.Key.ToLower().Replace(" ", "_") + ".aggregated.csv";

                    if (clear)
                    {
                        File.Delete(filename);
                        clear = false;
                    }

                    File.AppendAllText(filename, toWrite + Environment.NewLine);
                }

            }

        }

        private void cBAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] grids = Directory.GetFiles(currentPath, "20000.grid.csv", SearchOption.AllDirectories);

            CronbachAlphaGridByCell gbac = new CronbachAlphaGridByCell(false);
            CronbachAlphaGridByCell gbacAbs = new CronbachAlphaGridByCell(true);

            CronbachAlphaGridByNeighbourhood gban = new CronbachAlphaGridByNeighbourhood(false);
            CronbachAlphaGridByNeighbourhood gbanAbs = new CronbachAlphaGridByNeighbourhood(true);


            foreach (string grid in grids)
            {
                string data = File.ReadAllText(grid);

                gbac.AddRun(data);
                gbacAbs.AddRun(data);

                gban.AddRun(data);
                gbanAbs.AddRun(data);
            }

            gbac.writeFile(this.currentPath + Path.DirectorySeparatorChar + "cbac_cell.csv");
            gbacAbs.writeFile(this.currentPath + Path.DirectorySeparatorChar + "cbac_abs_cell.csv");

            gban.writeFile(this.currentPath + Path.DirectorySeparatorChar + "cban_cell.csv");
            gbanAbs.writeFile(this.currentPath + Path.DirectorySeparatorChar + "cban_abs_cell.csv");

        }
    }
}
