using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    public interface IWriter
    {
        void Start(Pool pool, int execute);
        void Record();
        void Write(Genome g, DataDictionary dd);
        void WriteGene(Genome g, DataDictionary dd);
        void WriteSpecies(List<Species> species, DataDictionary dd);
    }
}
