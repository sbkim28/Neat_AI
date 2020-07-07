using NeatAlgorithm.NEAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Snake
{
    class SnakeDataDictionary : DataDictionary
    {

        private Dictionary<int, LinkedList<Square>> foodLog;

        public SnakeDataDictionary() : base()
        {
            foodLog = new Dictionary<int, LinkedList<Square>>();
        }

        public override void Clear()
        {
            base.Clear();
            foodLog.Clear();
        }

        public LinkedList<Square> GetFood(int id)
        {
            return foodLog[id];
        }

        public void AddFood(int id, LinkedList<Square> foodList)
        {
            if (foodLog.ContainsKey(id))
            {
                foodLog[id] = foodList;
            }
            else
            {
                foodLog.Add(id, foodList);
            }
        }

        public override string GetDataAsString(int id)
        {
            LinkedList<Square> s = foodLog[id];

            LinkedListNode<Square> link = s.First;
            StringBuilder sb = new StringBuilder("\"food\":[");
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
