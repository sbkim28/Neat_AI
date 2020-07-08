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
        private Dictionary<int, int[]> lifetime;

        public DataDictionary()
        {
            score = new Dictionary<int, long[]>();
            lifetime = new Dictionary<int, int[]>();
        }

        public long[] GetScore(int id)
        {
            return score[id];
        }

        public virtual void Clear()
        {
            score.Clear();
            lifetime.Clear();
        }

        public int[] GetLifetime(int id)
        {
            return lifetime[id];
        }

        public void AddLifetime(int id, int lifetime, int index)
        {
            this.lifetime[id][index] = lifetime;
        }

        public void CreateLifetime(int id, int execution)
        {
            lifetime[id] = new int[execution];
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
