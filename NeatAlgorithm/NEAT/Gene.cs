using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    // 유전자. 
    // 유전자는 노드와 노드 사이의 연결(Connection)을 나타냄.
    // 각 개체는 이 유전자를 바탕으로 신경망을 구현함.
    public class Gene
    {
        // 혁신 수
        public int Innovation { get; set; }
        // 연결의 활성화 여부. 연결이 비활성화되었으면 작동하지 않음.
        public bool Enable { get; set; }
        // 가중치
        public double Weight { get; set; }
        // 연결이 나오는 노드
        public int In { get; set; }
        // 연결이 향하는 노드
        public int Out { get; set; }

        public Gene(int @in, int @out) : this(@in, @out, 0, true, 0)
        {

        }

        public Gene(int @in, int @out, double weight, bool enable, int innovation)
        {
            if (@in == @out) Console.Write("HELLO!");
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
