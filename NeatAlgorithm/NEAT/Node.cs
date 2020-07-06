using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    // 노드, 인공 신경망을 계산하는데 사용
    public class Node
    {
        // 이 노드로 들어오는 모든 연결
        public List<Gene> Incomings { get; private set;}

        //노드의 값.
        public double value;

        public Node()
        {
            Incomings = new List<Gene>();

        }
    }
}
