using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic.Model.Measurements
{
    public class CellAVGDistance : IMeasurement
    {
        public List<double> cellAVG = new List<double>();

        Grid _grid;
        public CellAVGDistance(Grid grid)
        {
            this._grid = grid;
        }
        public void calculate()
        {
            double cellCount = 0;
            double cellTotal = 0;

            foreach (GridRow item in this._grid.getRows())
            {
                foreach(GridColumn col in item.getColumns())
                {
                    if(col.isMixed())
                    {
                        cellCount++;
                        cellTotal += Math.Abs(col.getGroupDistance());
                    }
                }
            }

            this.cellAVG.Add(cellTotal / cellCount);
        }


    }
}
