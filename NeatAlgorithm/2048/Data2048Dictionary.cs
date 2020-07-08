using NeatAlgorithm.NEAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm._2048
{
    class Data2048Dictionary : DataDictionary
    {


        private Dictionary<int, LinkedList<CreatedCells>> cellLog;

        public Data2048Dictionary() : base()
        {
            cellLog = new Dictionary<int, LinkedList<CreatedCells>>();
        }

        public override void Clear()
        {
            base.Clear();
            cellLog.Clear();
        }

        public LinkedList<CreatedCells> getCells(int id)
        {
            return cellLog[id];
        }

        public void AddCells(int id, LinkedList<CreatedCells> cells)
        {
            if (cellLog.ContainsKey(id))
            {
                cellLog[id] = cells;
            }
            else
            {
                cellLog.Add(id, cells);
            }
        }

        public override string GetDataAsString(int id)
        {
            LinkedList<CreatedCells> cells = cellLog[id];

            LinkedListNode<CreatedCells> link = cells.First;
            StringBuilder sb = new StringBuilder("\"cells\":[");
            do
            {
                sb.Append(link.Value.ToString());
                sb.Append(", ");
            } while ((link = link.Next) != null);
            sb.Remove(sb.Length - 2, 2).Append("]");
            return sb.ToString();
        }
    }
}
