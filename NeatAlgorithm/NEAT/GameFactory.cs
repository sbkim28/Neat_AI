using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    public interface GameFactory
    {
        Agent GetAgent(Random r);
        DataDictionary GetDataDictionary();
    }
}
