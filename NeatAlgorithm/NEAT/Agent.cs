using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    public delegate void Draw();

    public abstract class Agent
    {
        public bool Gameover { get; set; }
        public int Execute { get; set; }
        public int DisplayDelay { get; set; }
        public int Score { get; set; }
        public Draw Drawer { get; set; }
        public Genome Current { get; private set; }

        public int InputMode { get; set; }

        protected Random random;

        public Agent()
        {

        }

        protected Agent(Random r)
        {
            Execute = 1;
            random = r;
        }


        public abstract long Evaluate(Genome g, DataDictionary dd);
        public virtual void Display(Genome g, DataDictionary dd)
        {
            Current = g;
        }
    }
}
