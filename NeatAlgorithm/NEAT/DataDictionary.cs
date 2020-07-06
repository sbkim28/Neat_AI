using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    
    public abstract class DataDictionary
    {
        private Dictionary<int, long[]> score;

        public DataDictionary()
        {
            score = new Dictionary<int, long[]>();
        }

        public long[] GetScore(int id)
        {
            return score[id];
        }

        public virtual void Clear()
        {
            score.Clear();
        }

        public void CreateScore(int id, int len)
        {
            if (score.ContainsKey(id)) return;

            score.Add(id, new long[len]);
        }

        public void AddScore(int id, long score, int index)
        {
            this.score[id][index] = score;
        }

        public abstract string GetDataAsString(int id);

    }
}
