using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NeatAlgorithm.NEAT;
using NeatAlgorithm.Snake;
using NeatAlgorithm._2048;
using NeatAlgorithm.Pacman;
using System.IO;
using NeatAlgorithm.Util;

namespace NeatAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            Pool p = new Pool(2, 1, r);
            p.Population = 1000;
            p.WritePlayData = true;
            XORAget xor = new XORAget();
            Writer w = new Writer("D://NEAT/XOR.json");
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



        class XORAget : IAgent
        {
            public void Display(Genome g, DataDictionary dd)
            {
                
            }

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
