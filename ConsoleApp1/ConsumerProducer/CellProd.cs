using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerProducer
{
    public class CellProd
    {
        Cell cell;
        int quantity = 1;
        public CellProd(Cell box, int request)
        {
            cell = box;
            quantity = request;
        }

        public void ThreadRun()
        {
            for(int looper=1; looper<=quantity; looper++)
            {
                cell.WriteToCell(looper);
            }
        }
    }

    public class CellCons
    {
        Cell cell;
        int quantity = 1;
        
        public CellCons(Cell box, int request)
        {
            cell = box;
            quantity = request;
        }

        public void ThreadRun()
        {
            int valReturned;
            for(int looper=1; looper<=quantity; looper++)
            {
                valReturned = cell.ReadFromCell();
            }
        }
    }

}
