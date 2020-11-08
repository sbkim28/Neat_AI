using NeatAlgorithm.NEAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Pacman
{
    class PacmanDataDictionary : DataDictionary
    {
        private Dictionary<int, LinkedList<Location>[]> frightenData;

        public PacmanDataDictionary() :base()
        {
            frightenData = new Dictionary<int, LinkedList<Location>[]>();

        }

        public override void Clear()
        {
            base.Clear();
            frightenData.Clear();
        }

        public void AddFrightenData(int id, LinkedList<Location>[] frightenData)
        {
            if (this.frightenData.ContainsKey(id))
            {
                this.frightenData[id] = frightenData;
            }
            else
            {
                this.frightenData.Add(id, frightenData);
            }

        }
        public LinkedList<Location>[] GetFrightenData(int id)
        {
            return frightenData[id];
        }

        public override string GetDataAsString(int id)
        {
            LinkedList<Location>[] cells = frightenData[id];

            StringBuilder sb = new StringBuilder("\"frighten\":[");
            for (int k = 0; k < 4; ++k)
            {
                sb.Append("[");
                LinkedListNode<Location> link = cells[k].First;
                if (link != null)
                {
                    bool flag = false;
                    do
                    {
                        sb.Append(link.Value.ToString());
                        sb.Append(", ");
                        flag = true;
                    } while ((link = link.Next) != null);

                    if (flag)
                    {
                        sb.Remove(sb.Length - 2, 2);
                    }
                }
                sb.Append("], ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");

            return sb.ToString();
        }
    }
}
