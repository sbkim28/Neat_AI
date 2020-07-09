using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NeatAlgorithm.NEAT;
using System.IO;
using NeatAlgorithm.Util;
using NeatAlgorithm.Data;
using NeatAlgorithm.Snake;
using NeatAlgorithm._2048;

namespace NeatAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Random r = new Random();
            
            Pacman(r);
        }

        static void Pacman(Random r)
        {

        }

        static void _2048(Random r)
        {
        }

        static void Snake(Random r)
        {
            
        }
        
        
        // XOR 분류를 실행해보자
        // XOR 분류는 AND나 OR과는 달리 비선형 분류에 해당한다.
        // 입력층과 출력층으로만 구성된 단순한 유전자는 단지 선형 분류만을 수행하기 때문에 XOR 분류를 해결하지 못한다.
        // 즉 XOR을 해결하기 위해서는 신경망에서 구조의 발전이 있어야 한다, 
        // 이는 NEAT에서 위상 구조의 증가가 실제로 발생하며, 문제 해결에 기여하는지를 확인할 수 있는 쉬운 예제에 해당하므로
        // 우리는 XOR 문제를 NEAT를 통해서 우리가 작성한 NEAT의 코드가 정상적으로 잘 작동하는지 확인할 것이다.
        // Case1과 Case2로 나누어 300번 진행할 것이며 두 경우에서 XOR 분류가 정상적으로 되는지 확인해보도록 하겠다.
        static void XOR(Random r)
        {
            for (int i = 0; i < 300; ++i)
            {
                Case1(r, i);
                Case2(r, i);
            }
        }    


        // 결과 Case1과 Case2의 데이터를 300개 확보하였으며, 데이터 분석을 통해서 XOR 문제가 두 경우 모두 해결되었는지 확인하도록 하겠다.
       
        // Case1 : XOR 분류를 수행함. 이때 적합도를 평가하는 것은 아래 XORAgent의 Evaluate 함수에서 이루어짐.
        static void Case1(Random r, int index)
        {
            Pool p = new Pool(2, 1, r);
            p.Population = 1000;
            p.WritePlayData = true;
            XORAgent xor = new XORAgent();
            Writer w = new Writer("D://NEAT/XOR_CASE1_" + index + ".steamlog");
            p.Writer = w;
            p.Agent = xor;
            w.Start(p, 1);
            p.Initialize();
            DataDictionary dd = new ScoreDataDictionary();
            p.DataDictionary = dd;
            for (int i = 0; i < 100; ++i)
            {
                p.Evaluate();
                w.Sw.Flush();
            }
            
        }

        // Case2 : XOR 분류를 수행함. 이때 적합도를 평가하는 것은 아래 FixedXORAgent의 Evaluate 함수에서 이루어짐.
        static void Case2(Random r, int index)
        {
            Pool p = new Pool(2, 1, r);
            p.Population = 1000;
            p.WritePlayData = true;
            FixedXORAgent xor = new FixedXORAgent();
            Writer w = new Writer("D://NEAT/XOR_CASE2_" + index +".steamlog");
            p.Writer = w;
            p.Agent = xor;
            w.Start(p, 1);
            p.Initialize();
            DataDictionary dd = new ScoreDataDictionary();
            p.DataDictionary = dd;
            for (int i = 0; i < 100; ++i)
            {
                p.Evaluate();
                w.Sw.Flush();
            }
        }

        // Case 1
        class XORAgent : IAgent
        {
            public void Display(Genome g, DataDictionary dd)
            {

            }

            // 순서대로 (1,0), (0,1), (1,1), (0,0)을 입력값에 넣었을 때,
            // (1,0)과 (0,1)에서는 참을 나타내는 0보다 큰 값,
            // (0,0)과 (1,1)에서는 거짓을 나타내는 0보다 작은 값(이 기준은 임의로 잡은 것이다.)이 나오면 맞는 결과라 보고
            // 맞춘 횟수를 적합도로 설정하였다.
            public long Evaluate(Genome g, DataDictionary dd)
            {
                int score = 0;
                if(g.EvaluateNetwork(new double[] { 1,0})[0] >0)
                {
                    ++score;
                }
                if (g.EvaluateNetwork(new double[] { 0, 1 })[0] > 0)
                {
                    ++score;
                }
                if (g.EvaluateNetwork(new double[] { 1, 1 })[0] < 0)
                {
                    ++score;
                }
                if (g.EvaluateNetwork(new double[] { 0, 0 })[0] < 0)
                {
                    ++score;
                }
                dd.CreateScore(g.GenomeId, 1);
                dd.AddScore(g.GenomeId, score, 0);
                return score + 1;
            }
        }

        // Case 2
        class FixedXORAgent : IAgent
        {
            public void Display(Genome g, DataDictionary dd)
            { }

            // 순서대로 (1,0), (0,1), (1,1), (0,0)을 입력값에 넣었을 때,
            // 정답이 '참'인 경우 출력값이 1보다 적으면 정답에서 출력값의 차를 제곱한 값과,
            // 정답이 '거짓'인 경우 출력값이 -1보다 크면 정답에서 출력값의 차를 제곱한 값의 합을 계산하고, (정답이 참 또는 거짓이라는게 정답을 맞췄다는 의미가 아니다. xor 연산의 결과가 참 또는 거짓인지를 의미한다.)
            // 그 값의 역수에 1000을 곱한 값을 적합도 함수로 사용하였다.
            public long Evaluate(Genome g, DataDictionary dd)
            {
                int score = 0;
                double fitness = 0;
                double data;
                double val;
                if ((data = g.EvaluateNetwork(new double[]{ 1,0 })[0]) > 0)
                {
                    ++score;
                }
                val = MathUtils.Sigmoid(data) - 1;
                if (val > 0) val = 0;
                fitness += val * val;
                if ((data = g.EvaluateNetwork(new double[] { 0, 1 })[0]) > 0)
                {
                    ++score;
                }
                val = MathUtils.Sigmoid(data) - 1;
                if (val > 0) val = 0;
                fitness += val * val;
                if ((data = g.EvaluateNetwork(new double[] { 1, 1 })[0]) < 0)
                {
                    ++score;
                }
                val = MathUtils.Sigmoid(data) + 1;
                if (val < 0) val = 0;
                fitness += val * val;
                if ((data = g.EvaluateNetwork(new double[] { 0, 0 })[0]) < 0)
                {
                    ++score;
                }
                val = MathUtils.Sigmoid(data) + 1;
                if (val < 0) val = 0;
                fitness += val * val;
                

                dd.CreateScore(g.GenomeId, 1);
                dd.AddScore(g.GenomeId, score, 0);
                return (long) (1 / fitness * 1000);
            }
        }
      
    }
}
