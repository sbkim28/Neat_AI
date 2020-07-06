using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    public class Genome
    {
        public int GenomeId { get; set; }
        public List<Gene> Genes { get; private set; }
        public long Fitness { get; set; }
        public double AdjustedFitness { get; set; }
        public int FromGeneration { get; private set; }

        public int NodeIndex { get; private set; }

        public Dictionary<int, Node> Network { get; private set; }

        public Pool Pool { get; set; }

        public Genome(Pool pool)
        {
            Pool = pool;
            NodeIndex = pool.Inputs;
            Genes = new List<Gene>();
            FromGeneration = pool.Generation;
            GenomeId = pool.NewGenome();
        }

        public Genome Clone()
        {
            Genome clone = new Genome(Pool);

            foreach(Gene g in Genes)
            {
                clone.Genes.Add(g.Clone());
            }
            return clone;
        }

        public void GenerateNetwork()
        {
            Dictionary<int, Node> network = new Dictionary<int, Node>();
            for (int i = 0; i < Pool.Inputs; ++i)
            {
                network.Add(i, new Node());
            }

            for (int i = 0; i < Pool.Outputs; ++i)
            {
                network.Add(Pool.MaxNodes + i, new Node());
            }
            Genes.Sort((a, b) => a.Out.CompareTo(b.Out));
            foreach (Gene g in Genes)
            {
                if (g.Enable)
                {
                    if (!network.ContainsKey(g.Out))
                    {
                        network.Add(g.Out, new Node());
                    }
                    Node outnode = network[g.Out];
                    outnode.Incomings.Add(g);
                    if (!network.ContainsKey(g.In))
                    {
                        network.Add(g.In, new Node());
                    }
                }
            }
            Network = network;

        }

        public double[] EvaluateNetwork(double[] input)
        {
            if (input.Length + 1 != Pool.Inputs)
            {
                throw new ArgumentException("Incorrect number of inputs");
            }

            for (int i = 0; i < input.Length; ++i)
            {
                Network[i].value = input[i];
            }
            Network[input.Length].value = 1;

            List<int> keys = new List<int>(Network.Keys);
            keys.Sort();
            foreach (int key in keys)
            {
                double sum = 0;
                Node n = Network[key];
                foreach (Gene connection in n.Incomings)
                {
                    sum += Network[connection.In].value * connection.Weight;
                }
                if (n.Incomings.Count > 0)
                {
                    n.value = Util.MathUtils.Sigmoid(sum);
                }
            }

            double[] outputs = new double[Pool.Outputs];
            for (int i = 0; i < Pool.Outputs; ++i)
            {
                outputs[i] = Network[Pool.MaxNodes + i].value;
            }
            foreach(int key in keys)
            {
                Network[key].value = 0;
            }
            return outputs;

        }

        public int RandomNode(bool nonInput, Random random)
        {
            HashSet<int> set = new HashSet<int>();
            if (!nonInput)
            {
                for (int i = 0; i < Pool.Inputs; ++i)
                {
                    set.Add(i);
                }
            }
            for (int i = 0; i < Pool.Outputs; ++i)
            {
                set.Add(Pool.MaxNodes + i);
            }
            foreach (Gene gene in Genes)
            {
                if (!nonInput || gene.In >= Pool.Inputs)
                {
                    set.Add(gene.In);
                }
                if (!nonInput || gene.Out >= Pool.Inputs)
                {
                    set.Add(gene.Out);
                }
            }

            int n = random.Next(set.Count);

            foreach (int k in set)
            {
                if (n-- == 0)
                {
                    return k;
                }
            }

            return 0;
        }

        public void Mutate(Random random)
        {
            if(random.NextDouble() < Pool.LinkMutationChance)
            {
                MutateLink(random);
            }
            double p = Pool.ConnectionMutationChance;
            while(p > 0)
            {
                if(random.NextDouble() < p)
                {
                    MutateConnection(random);
                }
                --p;
            }
            p = Pool.NodeMutationChance;
            while (p > 0)
            {
                if (random.NextDouble() < p)
                {
                    MutateNode(random);
                }
                --p;
            }
            p = Pool.EnableMutationChance;
            while (p > 0)
            {
                if (random.NextDouble() < p)
                {
                    MutateEnable(true, random);
                }
                --p;
            }
            p = Pool.DisableMutationChance;
            while (p > 0)
            {
                if (random.NextDouble() < p)
                {
                    MutateEnable(false, random);
                }
                --p;
            }
        }

        public void MutateConnection(Random random)
        {
            int n1 = RandomNode(false, random);
            int n2 = RandomNode(true, random);

            if (n2 < Pool.Inputs)
            {
                if (n1 < Pool.Inputs)
                {
                    return;
                }
                int _tmp = n1;

                n1 = n2;
                n2 = _tmp;
            }

            if (ContainsLink(n1, n2))
            {
                return;
            }

            Gene gene = new Gene(n1, n2)
            {
                Weight = random.NextDouble() * 4 - 2,
                Enable = true,
                Innovation = Pool.NewInnovation()
        };

            Genes.Add(gene);

        }

        public bool ContainsLink(int inNode, int outNode)
        {
            foreach (Gene gene in Genes)
            {
                if (gene.In == inNode && gene.Out == outNode)
                {
                    return true;
                }
            }
            return false;
        }

        public void MutateNode(Random r)
        {
            if (Genes.Count == 0) return;
            Gene gene = Genes[r.Next(Genes.Count)];
            if (!gene.Enable) return;
            
            gene.Enable = false;
            
            Gene gene1 = gene.Clone();
            gene1.Out = NodeIndex;
            gene1.Weight = 1.0;
            gene1.Innovation = Pool.NewInnovation();
            gene1.Enable = true;
            Genes.Add(gene1);

            Gene gene2 = gene.Clone();
            gene2.In = NodeIndex;
            gene2.Innovation = Pool.NewInnovation();
            gene2.Enable = true;
            Genes.Add(gene2);


            ++NodeIndex;

        }

        public void MutateLink(Random random)
        {
            foreach(Gene g in Genes)
            {
                if(random.NextDouble() < Pool.PerturbChance)
                {
                    g.Weight += Pool.StepSize * random.NextDouble() * 2 - Pool.StepSize;
                }
                else
                {
                    g.Weight = random.NextDouble() * 4 - 2;
                }
            }

        }
        public void MutateEnable(bool enable, Random r)
        {
            List<Gene> candidates = new List<Gene>();
            foreach (Gene gene in Genes)
            {
                if (gene.Enable == !enable)
                {
                    candidates.Add(gene);
                }
            }

            if (candidates.Count == 0) return;

            Gene g = candidates[r.Next(candidates.Count)];
            g.Enable = !g.Enable;
        }

        public double Disjoint(Genome genome)
        {
            HashSet<int> innov1 = new HashSet<int>();
            HashSet<int> innov2 = new HashSet<int>();

            foreach (Gene gene in Genes)
            {
                innov1.Add(gene.Innovation);
            }


            foreach (Gene gene in genome.Genes)
            {
                innov2.Add(gene.Innovation);
            }

            int disjointGenes = 0;
            foreach (Gene gene in Genes)
            {

                if (!innov2.Contains(gene.Innovation))
                {
                    ++disjointGenes;
                }
            }
            foreach (Gene gene in genome.Genes)
            {

                if (!innov1.Contains(gene.Innovation))
                {
                    ++disjointGenes;
                }
            }


            if (Pool.Cut)
            {
                int n = Math.Max(Genes.Count, genome.Genes.Count);
                if (n == 0) n = 1;
                return (double)disjointGenes / n;
            }
            else
            {
                return (double)disjointGenes;
            }

        }

        
        public double Weights(Genome genome)
        {
            Dictionary<int, Gene> i2 = new Dictionary<int, Gene>();
            foreach (Gene gene in genome.Genes)
            {
                i2.Add(gene.Innovation, gene);
            }

            double sum = 0;
            int coincident = 0;
            foreach (Gene gene in Genes)
            {
                if (i2.ContainsKey(gene.Innovation))
                {
                    Gene gene2 = i2[gene.Innovation];

                    sum += Math.Abs(gene.Weight - gene2.Weight);
                    ++coincident;
                }
            }

            return coincident == 0 ? 0 : sum / coincident;
        }

        public double GetDistance(Genome g)
        {
            return Pool.DeltaDisjoint * Disjoint(g) + Pool.DeltaWeight * Weights(g);
        }

        //g1.fitness > g2.fitness
        public static Genome Crossover(Genome g1, Genome g2, Random r)
        {
            Genome child = new Genome(g1.Pool);
            Dictionary<int, Gene> innovation2 = new Dictionary<int, Gene>();
            foreach (Gene gene in g2.Genes)
            {
                innovation2.Add(gene.Innovation, gene);
            }
            foreach (Gene gene in g1.Genes)
            {
                if (innovation2.ContainsKey(gene.Innovation)
                    && r.Next(2) == 1)
                {
                    child.Genes.Add(innovation2[gene.Innovation].Clone());
                }
                else
                {
                    child.Genes.Add(gene.Clone());
                }
            }
            return child;
        }

        public override string ToString()
        {
            return "Genome{GenomeId=" + GenomeId + ", Fitness=" + Fitness +
                ", FromGeneration=" + FromGeneration + ", Genes=[]}";

        }

        public string GenesToString()
        {
            StringBuilder sb = new StringBuilder("\"Genes\":[");
            foreach(Gene g in Genes)
            {
                sb.Append("{\"In\":").Append(g.In)
                    .Append(", \"Out\":").Append(g.Out)
                    .Append(", \"Enable\":").Append(g.Enable ? "true" : "false")
                    .Append(", \"Weight\":").Append(g.Weight)
                    .Append(", \"Innovation\":").Append(g.Innovation)
                    .Append("},");
                
            }
            sb.Remove(sb.Length - 1, 1).Append("]");
            return sb.ToString();
        }

    }
}
