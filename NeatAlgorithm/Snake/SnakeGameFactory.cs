using NeatAlgorithm.NEAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Snake
{
    class SnakeGameFactory : GameFactory
    {
        private int x;
        private int y;

        public SnakeGameFactory(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Agent GetAgent(Random r)
        {
            return new SnakeAgent(x, y, r);
        }

        public DataDictionary GetDataDictionary()
        {
            return new SnakeDataDictionary();
        }
    }
}
