using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingGridView
{
    class SimpleAgent
    {
        public int x;
        public int y;
        public int z;
        public int fix;
        public int flex;

        public SimpleAgent(string line)
        {
            string[] parts = line.Split(';');

            this.x = int.Parse(parts[0]);
            this.y = int.Parse(parts[1]);
            this.z = int.Parse(parts[2]);
            this.fix = int.Parse(parts[3]);
            this.flex = (int)Math.Round(double.Parse(parts[4].Replace(',','.'),CultureInfo.InvariantCulture)*100);
        }
    }
}
