using NeatAlgorithm.NEAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm._2048
{
    public sealed class Game2048Factory : GameFactory
    {
        public Agent GetAgent(Random r)
        {
            return new Agent2048(r);
        }

        public DataDictionary GetDataDictionary()
        {
            return new Data2048Dictionary();
        }
    }
}
