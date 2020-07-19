using NeatAlgorithm.NEAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Pacman
{
    class PacmanGameFactory : GameFactory
    {
        public Agent GetAgent(Random r)
        {
            return new PacmanAgent(r);
        }

        public DataDictionary GetDataDictionary()
        {
            return new PacmanDataDictionary();
        }
    }
}
