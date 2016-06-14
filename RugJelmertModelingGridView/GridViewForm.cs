using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RugJelmertModelingGridView
{
    public partial class GridViewForm : Form
    {
        public enum showTypes { IMMIGRANTS = -1, TOTAL = 0,LOCALS = 1,IMMIGRANTS_COUNT = 2 };

        string currentPath;
        string currentDirectoryName;
        public GridViewForm(string path)
        {
            this.currentPath = path;
            InitializeComponent();


            string[] parts = path.Split(Path.DirectorySeparatorChar);
            this.currentDirectoryName = parts[parts.Length - 2];

            this.changeFormTitle(this.currentDirectoryName);
        }


        private void changeFormTitle(string title)
        {
            this.Text = "Grid View " + title;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private bool _once = false;

        private string[] getFiles(string dir)
        {
            string[] files = Directory.GetFiles(dir, "*grid.csv", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                string[] parts = file.Split(Path.DirectorySeparatorChar);

                int t = parts[parts.Length - 1].Length;
                int u = "20000.grid.csv".Length;

                if (t != u)
                {
                    if (t == u - 1)
                    {
                        parts[parts.Length - 1] = "0" + parts[parts.Length - 1];
                    }

                    File.Move(file, string.Join(Path.DirectorySeparatorChar.ToString(), parts));
                }



            }

            return Directory.GetFiles(dir, "*grid.csv", SearchOption.TopDirectoryOnly);
        }

        string[] fileEntries;

        private string getContents(string path)
        {
            return File.ReadAllText(path);
        }

        private string getContents(int index)
        {
            return File.ReadAllText(this.fileEntries[index]);
        }

        int currentIndex = 0;

        private showTypes showType = 0;

        private void gridPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_once)
            {
                string contents = getContents(this.currentIndex);

                Console.Write(this.fileEntries[this.currentIndex]);

                List<SimpleAgent> agents = new List<SimpleAgent>();

                int maxX = 0;
                int maxY = 0;

                using (StringReader reader = new StringReader(contents))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        SimpleAgent sAgent = new SimpleAgent(line);


                        if ((int)this.showType == sAgent.fix || showType == 0)
                        {
                            agents.Add(sAgent);
                            
                        }

                        if(showType == showTypes.IMMIGRANTS_COUNT)
                        {
                            if(sAgent.fix == -1)
                            {
                                agents.Add(sAgent);
                            }
                        }

                        if (sAgent.x > maxX)
                            maxX = sAgent.x;

                        if (sAgent.y > maxY)
                            maxY = sAgent.y;
                    }

                }

                maxX += 1;
                maxY += 1;

                int[,] sum = new int[maxX, maxY];
                int[,] count = new int[maxX, maxY];
                int[,] avg = new int[maxX, maxY];
                //Dictionary<int, Dictionary<int, List<SimpleAgent>>> grid = new Dictionary<int, Dictionary<int, List<SimpleAgent>>>();

                foreach (var agent in agents)
                {
                    //grid[agent.x][agent.y].Add(agent);

                    int x = agent.x;// - 1;
                    int y = agent.y;// - 1;


                    sum[x, y] += agent.flex;
                    count[x, y]++;
                }

                int iMin = avg.GetUpperBound(0);
                int jMin = avg.GetUpperBound(1);                

                for (int i = 0; i < sum.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < sum.GetUpperBound(1); j++)
                    {
                        if (count[i, j] != 0)
                        {
                            avg[i, j] = sum[i, j] / count[i, j];

                            if (j < jMin)
                                jMin = j;

                            if (i < iMin)
                                iMin = i;
                        }
                            
                    }
                }                

                int iMax = (avg.GetUpperBound(0) - iMin ) * 10;
                int jMax = (avg.GetUpperBound(1) - jMin) * 10;

                this.Size = new Size(iMax, jMax);

                for (int i = 0; i < avg.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < avg.GetUpperBound(1); j++)
                    {
                        if (count[i, j] != 0)
                        {
                            if(showType == showTypes.IMMIGRANTS_COUNT)
                            {
                                int x = 0 + count[i, j]*20;

                                //Color.FromArgb(120,100 - avg[i, j], 100 - avg[i, j], 100 - avg[i, j])
                                SolidBrush brush = new SolidBrush(Color.FromArgb(x, 0, 0));

                                e.Graphics.FillRectangle(brush, new Rectangle((j-jMin) * 10 + 25, (i-iMin) * 10 + 25, 10, 10));
                            }
                            else
                            {
                                int x = 100 + avg[i, j];

                                //Color.FromArgb(120,100 - avg[i, j], 100 - avg[i, j], 100 - avg[i, j])
                                SolidBrush brush = new SolidBrush(Color.FromArgb(x + 55, 255 - x, 0));

                                e.Graphics.FillRectangle(brush, new Rectangle((j - jMin) * 10, (i - iMin) * 10, 10, 10));
                            }                            
                        }
                    }
                }

                if (save)
                {
                    string imageName = this.fileEntries[this.currentIndex] + showType.ToString() + ".png";

                    if (!File.Exists(imageName))
                    {

                        Graphics gfx = e.Graphics;

                        Bitmap bmp = new Bitmap(this.gridPictureBox.Width, this.gridPictureBox.Height);
                        this.gridPictureBox.DrawToBitmap(bmp, new Rectangle(0, 0, this.gridPictureBox.Width, this.gridPictureBox.Height));


                        EncoderParameters ep = new EncoderParameters();
                        ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);


                        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                        int ji = 0;
                        for (ji = 0; ji < codecs.Length; ji++)
                        {
                            if (codecs[ji].MimeType == "image/png") break;
                        }

                        bmp.Save(imageName, codecs[ji], ep);
                    }
                    save = false;
                }

                _once = false;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //if(DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                string path = this.currentPath; // @"C:\Users\jelmer\Downloads\scriptie\schiedam\schiedam\2016-03-20\t4_r25_i20000\nu\grid-dumps"; //folderBrowserDialog1.SelectedPath;

                this.fileEntries = getFiles(path);
                this.currentIndex = 0;
                this._once = true;
                this.gridPictureBox.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {



        }

        private void prev()
        {
            this.currentIndex -= 1;

            if (this.currentIndex <= 0)
            {
                this.currentIndex = this.fileEntries.Length - 1;

            }

            this._once = true;
            this.gridPictureBox.Invalidate();

        }

        private void next()
        {
            this.currentIndex += 1;

            if (this.currentIndex >= this.fileEntries.Length)
            {
                this.currentIndex = 0;
            }

            this._once = true;
            this.gridPictureBox.Invalidate();

        }

        private void gridPictureBox_Click(object sender, EventArgs e)
        {

        }

        private bool save = false;

        private void GridViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Up)
            {
                this.next();
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Down)
            {
                this.prev();
            }

            if(e.KeyCode == Keys.S)
            {
                this.save = true;
            }

            if (e.KeyCode == Keys.NumPad0)
                this.showType = showTypes.TOTAL;
            if (e.KeyCode == Keys.NumPad1)
                this.showType = showTypes.LOCALS;
            if (e.KeyCode == Keys.NumPad2)
                this.showType = showTypes.IMMIGRANTS;
            if (e.KeyCode == Keys.NumPad3)
                this.showType = showTypes.IMMIGRANTS_COUNT;

            this._once = true;
            this.gridPictureBox.Invalidate();

            this.changeFormTitle(this.currentDirectoryName + " i=" + this.currentIndex.ToString());

        }
    }
}
