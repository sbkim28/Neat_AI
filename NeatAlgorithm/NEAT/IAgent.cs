using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    public interface IAgent
    {
        long Evaluate(Genome g, DataDictionary dd);
        void Display(Genome g, DataDictionary dd);
    }
}
