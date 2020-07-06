using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    public class Gene
    {
        public int Innovation { get; set; }
        public bool Enable { get; set; }
        public double Weight { get; set; }
        public int In { get; set; }
        public int Out { get; set; }

        public Gene(int @in, int @out) : this(@in, @out, 0, true, 0)
        {

        }

        public Gene(int @in, int @out, double weight, bool enable, int innovation)
        {
            In = @in;
            Out = @out;
            Weight = weight;
            Enable = enable;
            Innovation = innovation;
        }

        public Gene Clone()
        {
            return new Gene(In, Out, Weight, Enable, Innovation);
        }
        
        public override string ToString()
        {
            return "{in=" + In + ", out=" + Out + ", weight=" + Weight + ", enable=" + Enable + ", innovation=" + Innovation + "}";
        }
    }

}
