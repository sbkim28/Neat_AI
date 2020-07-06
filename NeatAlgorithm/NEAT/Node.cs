using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    public class Node
    {
        public List<Gene> Incomings { get; private set;}

        public double value;

        public Node()
        {
            Incomings = new List<Gene>();

        }
    }
}
