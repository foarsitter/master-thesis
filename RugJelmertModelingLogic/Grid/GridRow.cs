using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic
{
    public class GridRow
    {
        private GridColumn[] colums;        

        public void initEmpty(int columns)
        { 
            this.colums = new GridColumn[columns];

            for (int i = 0; i < columns; i++)
            {
                this.colums[i] = new GridColumn();         
            }
        }

        public int Count()
        {
            return this.colums.Length;
        }

        public GridColumn get(int column)
        {
            return this.colums[column];
        }

        public GridColumn[] getColumns()
        {
            return this.colums;
        }
      
    }
}
