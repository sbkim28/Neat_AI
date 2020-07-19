using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    
    public abstract class DataDictionary
    {
        private Dictionary<int, int[]> score;
        private Dictionary<int, int[]> lifetime;

        public DataDictionary()
        {
            score = new Dictionary<int, int[]>();
            lifetime = new Dictionary<int, int[]>();
        }

        public int[] GetScore(int id)
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

            score.Add(id, new int[len]);
        }

        public void AddScore(int id, int score, int index)
        {
            this.score[id][index] = score;
        }

        public abstract string GetDataAsString(int id);

    }
}
