using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    // 개체.
    public class Genome
    {
        // 개체 아이디
        public int GenomeId { get; set; }
        // 개체가 가지고 있는 유전자
        public List<Gene> Genes { get; private set; }
        // 개체의 적합도
        public long Fitness { get; set; }
        public double AdjustedFitness { get; set; }
        // 개체가 만들어진 세대
        public int FromGeneration { get; set; }

        public int NodeIndex { get; private set; }

        public int Complexity { get; set; }
        public long ExecutionTime { get; set; }

        // 개체의 신경망. 개체의 유전자 정보를 바탕으로 구현됨.
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
            Genome clone = new Genome(Pool)
            {
                NodeIndex = NodeIndex
            };
            foreach (Gene g in Genes)
            {
                clone.Genes.Add(g.Clone());
            }
            return clone;
        }

        // 인공 신경망을 구현하는 부분
        // 개체에 있는 유전자의 정보(연결 정보)를 바탕으로 노드를 생성한다.
        public void GenerateNetwork()
        {
            Complexity = 0;
            Dictionary<int, Node> network = new Dictionary<int, Node>();
            for (int i = 0; i < Pool.Inputs; ++i)
            {
                network.Add(i, new Node());
            }

            for (int i = 0; i < Pool.Outputs; ++i)
            {
                network.Add(Pool.MaxNodes + i, new Node());
            }
            //Genes.Sort((a, b) => a.Out.CompareTo(b.Out));
            foreach (Gene g in Genes)
            {
                ++Complexity;
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
            Complexity += network.Keys.Count;
            Network = network;

        }

        // 인공 신경망이 작동하는 부분
        // Parameter로 입력값을 받고, 이를 계산하여 반환함.
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

        // 변이를 위해서 무작위의 노드를 선택함. nonInput이 참이라면 입력 노드를 제외한 노드 중에서 선택함.
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

        // 개체를 변이시키는 함수
        // 각각의 변이는 Pool에 설정된 확률에 근거하여 발생함.
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

        // 새로운 연결을 추가하는 변이
        // 두 개의 노드를 무작위로 선택해 (단, out에 해당하는 node는 입력 노드가 아님)
        // 두 노드 사이를 잇는 연결이 없다면 새로운 연결을 추가함
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
            if (n1 == n2) return;
            if(n1 > n2)
            {
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

        // 두 노드 사이를 잇는 연결이 존재하는지 유무
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

        // 새로운 노드를 추가함.
        // 기존의 연결 중 무작위로 선택해서 기존의 연결은 비활성화시키고,
        // 그 사이에 새로운 노드를 추가함.
        // in ==(w₁)==> out 이었다면
        // in =(w₂)=> new =(w₃)=> out과 같이 변이가 일어남. (w₁w₂w₃는 연결의 가중치)
        // 이때 구조적 혁신이 기존 성능에 주는 영향을 최소화하기 위해서 w₂= w₁, w₃= 1로 설정함.
        public void MutateNode(Random r)
        {
            if (Genes.Count == 0) return;
            Gene gene = RandomGenes(r);
            if (gene == null || !gene.Enable) return;
            
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

        public Gene RandomGenes(Random r)
        {
            List<Gene> genes = new List<Gene>();
            foreach(Gene g in Genes)
            {
                if (g.Out > NodeIndex)
                    genes.Add(g);
            }
            if (genes.Count == 0) return null;

            return genes[r.Next(genes.Count)];
        }

        // 연결의 가중치를 변이시킴
        // 일정 확률로 교란을 통해 유전자의 가중치된다.
        // 교란은 일정 범위 내에서 가중치의 값을 변화시키는 것을 말한다.
        // 유전자의 가중치를 변화시키는 변이가 발생할 때, perturbchance에 해당하면 교란을 통해서 값을 변화시키고 그렇지 않으면 무작위로 값을 변화시킨다.
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
        // 비활성화된 연결을 활성화시키거나, 활성화된 연결을 비활성화시키는 변이
        // enable이 참이면 활성화시키는 변이가, enable이 거짓이면 비활성화시키는 변이가 발생한다.
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

        // 개체 사이의 기능적 거리를 판단하기 위해서 disjoint 유전자와 excess 유전자의 수를 구하는 함수
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

        // 개체 사이의 기능적 거리를 판단하기 위해서 일치하는 유전자의 가중치의 차이의 평균을 구하는 함수
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

        // 개체 사이의 기능적 거리를 판단하는 함수
        public double GetDistance(Genome g)
        {
            return Pool.DeltaDisjoint * Disjoint(g) + Pool.DeltaWeight * Weights(g);
        }

        // 교차를 시키는 함수
        // (g1.fitness > g2.fitness)
        // 교차가 일어날 때, 기본적으로는 적합도가 높은 개체(g1)의 것을 따르되, 적합도가 높은 개체(g1)와 적합도가 낮은 개체(g2)에 혁신 수가 일치하는 연결이 있으면 무작위로 선택한다.
        // (혁신수가 일치하다는 말은 같은 종류의 연결이라는 뜻)
        public static Genome Crossover(Genome g1, Genome g2, Random r)
        {
            Genome child = new Genome(g1.Pool)
            {
                NodeIndex = g1.NodeIndex
            };
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
