using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    //NEAT 알고리즘이 실행되는 클래스
    public class Pool
    {
        //입력 노드의 수(bias node도 포함됨.)
        // ※ bias node란? 항상 1의 값을 가지는 노드를 말하며, 보다 단순한 구조로 문제를 해결할 수 있도록 도와주는 역할
        public int Inputs { get; set; }
        //출력 노드의 수
        public int Outputs { get; set; }
        // 최대 노드의 수
        public int MaxNodes { get; set; }
        // 개체 수
        public int Population { get; set; }
        // 세대
        public int Generation { get; private set; }

        // 혁신 수
        // 혁신 수는 서로 다른 위상 구조를 가진 개체들을 교배시킬 때, 일치하는 종류 (같은 뿌리)의 연결을 찾는데 사용됨
        public int Innovation { get; private set; }

        // δ의 문턱값. δ란, 개체 사이의 기능적 거리를 나타내는 값이며, 이 값에 따라서 종을 구분한다.
        // δ = c₁D/N +  c₂W (excess gene와 disjoint gene를 구별하지 않을 때)
        // 여기서 c₁과 c₂는 상수이며, 각각의 요소에 대한 가중치를 부여한다.
        // D는 disjoint gene와 excess gene의 개수를 나타낸다. 서로 다른 두 개체의 유전자를 혁신 수대로 나열하였을 때, 다음과 같다고 하자. (아래 값들은 유전자의 혁신 수를 나타낸다)
        // 1 2 3 4   6 7     
        // 1 2 3 4 5   7 8 9 
        // 이때 서로 일치하지 않으면서 사이에 있는 것들 (5, 6)은 disjoint gene에 해당하고,
        // 마지막에서 일치하지 않는 것들 (8, 9)는 excess gene에 해당한다.
        // 따라서 D = 2 + 2 = 4이다.
        // 이때 N은 유전자가 더 많은 개체의 유전자의 개수이다. 여기서는 밑의 개체의 유전자가 더 많으므로 N은 밑 개체의 유전자의 수인 8이 된다.
        // (원 제작자의 논문은 excess gene와 disjoint gene를 구별하여 δ = c₁D/N + c₂E/N + c₃W (D는 disjoint gene의 수, E는 excess gene의 수)와 같이 나타내었지만,
        // 둘의 큰 차이가 없고 논문에서 c₁과 c₂의 값을 같게 하였으며, 다른 neat를 활용한 연구에는 이를 구별하지 않는 경우도 많기에 우리는 이를 구별하지는 않겠다.)
        // 또 W는 일치하는 유전자의 가중치(weight)의 차이의 평균을 뜻한다. 여기서는 두 개체의 1, 2, 3, 4, 7번 유전자의 가중치의 차이의 합을 5로 나눈 값이 되겠다.
        // 두 개체 사이의 기능적 거리가 DeltaThreshold 이상이면 서로 다른 종으로 판단한다.
        public double DeltaThreshold { get; set; }
        // c₁
        public double DeltaDisjoint { get; set; }
        // c₂
        public double DeltaWeight { get; set; }

        public bool Cut { get; set; }

        // 교배 및 변이율 관련 값들
        // 유전자의 가중치가 변화될 확률
        public double LinkMutationChance { get; set; }
        // 교란을 통해 유전자의 가중치가 변화될 확률.
        // 교란은 일정 범위 내에서 가중치의 값을 변화시키는 것을 말한다.
        // 유전자의 가중치를 변화시키는 변이가 발생할 때, perturbchance에 해당하면 교란을 통해서 값을 변화시키고 그렇지 않으면 무작위로 값을 변화시킨다.
        public double PerturbChance { get; set; }
        // 교란이 일어날 때, 일정 범위를 말함. 즉 ±StepSize 내에서 값이 변화됨
        public double StepSize { get; set; }
        // 새로운 연결이 추가되는 변이가 일어날 확률
        public double ConnectionMutationChance { get; set; }
        // 새로운 노드 (은닉 노드)가 추가되는 변이가 일어나는 확률
        public double NodeMutationChance { get; set; }
        // 비활성화 상태의 연결을 활성화하는 변이가 일어날 확률
        public double EnableMutationChance { get; set; }
        // 활성화 상태의 연결을 비활성화하는 변이가 일어날 확률
        // EnableMutationChance와 DisableMutationChance는 원 논문에서는 구현되지 않았으나 이러한 변이가 진화에 큰 도움이 될지 확인하기 위해서 추가함
        public double DisableMutationChance { get; set; }
        // 교배가 일어날 확률. 자식을 생성할 때, 교배의 방식으로 생성하기도 하는 한편, 한 개체의 유전자에서 변이만 일으켜서 자식이 생성되는 경우도 있다.
        public double BreedChance { get; set; }

        // 이전 세대에서 살아남아 다음 세대로 갈 개체의 비율. 만약 0이면 각 종에서 가장 우수한 개체만 선정함
        public double SurviveRate { get; set; }
        // 발전이 이루어지지 않은 종을 제거하기 위한 값. staleness에 해당하는 세대 수만큼 종이 발전을 보이지 않으면 종이 제거된다.
        public int Staleness { get; set; }

        // 현재 적합도를 측정하고 있는 종
        private int speciesCursor;
        // 현재 적합도를 측정하고 있는 개체
        private int genomeCursor;

        // 가장 우수한 종이 작동하는 것을 몇번째 세대마다 보여줄 것인지 결정. 예를 들어 이 값이 5라면, 5, 10, 15, 20 ... 번째 세대에서 가장 적합한 종이 작동하는 모습을 보여줌
        public int DisplayTop;
        // 종 목록
        public List<Species> Species { get; private set; }

        // 개체의 적합도 중 가장 높은 적합도
        public long TopFitness { get; private set; }
        // 가장 높은 적합도를 가진 개체
        public Genome BestGenome { get; private set; }

        public IAgent Agent { get; set; }
        public DataDictionary DataDictionary { get; set; }
        public IWriter Writer { get;  set; }

        public bool WritePlayData { get; set; }
        public bool WriteSpecies { get; set; }

        // 개체 번호를 부여하기 위한 값
        public int GenomeId { get; private set; }
        // 종 번호를 부여하기 위한 값
        public int SpeciesId { get; private set; }


        
        public Random random;

        // 생성자
        // 초기 값들을 설정함
        public Pool(int inputs, int outputs, Random random)
        {
            Inputs = inputs + 1; // +1의 이유는 bias node를 고려하는 것
            Outputs = outputs;
            MaxNodes = 1000000;
            Population = 300;
            Innovation = 0;
            DeltaDisjoint = 1;
            DeltaWeight = 0.4;
            DeltaThreshold = 1;
            Cut = true;

            PerturbChance = 0.9;
            StepSize = 0.1;
            BreedChance = 0.75;
            LinkMutationChance = 0.75;
            ConnectionMutationChance = 0.05;
            NodeMutationChance = 0.03;
            EnableMutationChance = 0;
            DisableMutationChance = 0;
            TopFitness = long.MinValue;
            Staleness = 15;
            
            WritePlayData = false;
            SurviveRate = 0;
            DisplayTop = 1;
            Species = new List<Species>();
            this.random = random;
        }

        // 초기 개체를 생성하는 함수
        public void Initialize() 
        {
            Genome g = CreateBasicConnectedGenome();
            for(int i = 0; i < Population; ++i)
            {
                AddToSpecies(g.Clone());
            }
        }

        // 기본 개체를 생성함
        public Genome CreateBasicConnectedGenome()
        {
            Genome g = new Genome(this);
            for(int i = 0;i<Inputs - 1; ++i)
            {
                for (int j = 0; j < Outputs; ++j)
                {
                    g.Genes.Add(new Gene(i, MaxNodes + j, 0.5, true, NewInnovation()));
                }
            }
            return g;
        }

        // 적합도를 평가하고 이를 바탕으로 선택, 교차, 변이를 통해서 새로운 세대를 생성하는 함수
        // 
        // 다음 순서로 작동함
        // 1) 모든 개체의 적합도를 판단함
        // 2) Staleness가 높은 종을 판단하여 제거함
        // 3) 적합도를 바탕으로 하여 자식을 만듦. 이때 교차와 변이 모두를 통해서 자식을 만들기도 하며, 변이만으로 자식을 생성하기도 함.
        //   - 이때, 자식을 만들 확률은 종과 개체의 적합도에 따라서 다름. 즉 개체의 평균 적합도가 높은 종은 더 많은 자식을 만들고, 또 종 내에서 적합도가 높은 개체는 더 많은 자식을 만듦)
        // 4) 각 종마다 SurviveRate에 해당하는 개체만 남기고 나머지는 제거함
        // 5) 새로운 자식들을 기존의 종에 추가함.
        public void Evaluate()
        {
            if (Writer != null) Writer.Record();
            // 과정 1)
            while (true)
            {
                Genome g = GetCurrentGenome();
                g.GenerateNetwork();
                // 적합도 평가
                g.Fitness = Agent.Evaluate(g, DataDictionary);
                g.AdjustedFitness = (double)g.Fitness / Species[speciesCursor].Genomes.Count;

                Species[speciesCursor].AddFitness(g.AdjustedFitness);
                if (g.Fitness > TopFitness)
                {
                    TopFitness = g.Fitness;
                    BestGenome = g;
                }
                bool newGen = false;
                while (IsFitnessMeasured())
                {
                    if (NextGenome())
                    {
                        newGen = true;
                        break;
                    }
                }

                if (newGen) break;
            }

            if (Generation != 0 && Generation % DisplayTop == 0)
            {
                Agent.Display(BestGenome, DataDictionary);
            }
            if(Writer != null)
            {
                Writer.Write(BestGenome, DataDictionary);
                if (WritePlayData)
                {
                    Writer.WriteGene(BestGenome, DataDictionary);
                }
                if (WriteSpecies)
                {
                    Writer.WriteSpecies(Species, DataDictionary);
                }
            }
            if (DataDictionary != null)
            {
                DataDictionary.Clear();
            }

            // for debug
            Console.Write("Generation:" + Generation);
            Console.Write("\tHighest Fitness:" + TopFitness);
            Console.WriteLine("\tAmount Species:" + Species.Count);
            ++Generation;
            List<Genome> child = new List<Genome>();
            int survived = 0;
        
            // 과정 2)
            for (int i = Species.Count - 1; i>= 0;--i)
            {
                Species s = Species[i];
                Genome prevBest = s.Genomes[0];
                s.Genomes.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));
                Genome currentBest = s.Genomes[s.Genomes.Count - 1];

                // 종에서 기존의 가장 우수한 개체보다 더 적합도가 높은 개체가 등장하지 못했을 경우
                if (prevBest == currentBest)
                {
                    ++s.Staleness;
                    // staleness가 일정 수준 이상이고, 전체에서 가장 우수한 개체가 속한 종이 아니라면, 종을 제거함
                    if (s.Staleness > Staleness && BestGenome != currentBest)
                    {
                        Species.RemoveAt(i);
                    }
                }
                else // 종에서 기존의 가장 우수한 개체보다 더 적합도가 높은 개체가 등장할 경우
                {
                    s.Staleness = 0;
                }
            }
            
            // surviveRate를 바탕으로 생성할 자식 개체의 수를 계산
       
            if (SurviveRate == 0) survived = Species.Count;
            else
            {
                foreach (Species s in Species)
                {
                    survived += (int)Math.Ceiling(s.Genomes.Count * SurviveRate);
                }
            }
            
            // 과정 3)
            while (survived + child.Count < Population)
            {
                Genome c;
                // 교차 및 변이로 자식 생성
                if (random.NextDouble() < BreedChance)
                {
                    Species s = SelectSpecies(random);

                    Genome g1 = SelectGenome(s, random);
                    Genome g2 = SelectGenome(s, random);

                    
                    if (g1.Fitness > g2.Fitness)
                    {
                        c = Genome.Crossover(g1, g2, random);
                    }
                    else
                    {
                        c = Genome.Crossover(g2, g1, random);
                    }

                    c.Mutate(random);
                }
                else // 변이로만 자식 생성
                {
                    Species s = SelectSpecies(random);
                    Genome g1 = SelectGenome(s, random);
                    c = g1.Clone();

                    c.Mutate(random);
                }

                child.Add(c);

            }
            // 과정 4) SurviveRate에 따라서 기존의 종을 제거함  
            foreach (Species s in Species)
            {
                int cut = 0;
                if (SurviveRate == 0)
                {
                    cut = s.Genomes.Count - 1;
                }
                else
                {
                    cut = s.Genomes.Count - (int)Math.Ceiling(s.Genomes.Count * SurviveRate);
                }
                s.Genomes.RemoveRange(0, cut);
                s.Genomes[0].Fitness = 0;
            }
            
            // 과정 5) 새로 만들어진 자식 종을 추가함
            foreach(Genome g in child) {
                AddToSpecies(g);
            }
            
            TopFitness = long.MinValue;
            BestGenome = null;

        }

        // 자식을 생성할 때, 종 내에서 한 개체를 뽑음. 한 개체가 선택될 확률은 (개체의 적합도) / (종 내의 모든 개체의 적합도의 합) 이다.
        public Genome SelectGenome(Species s, Random random)
        {
            double sum = 0;
            foreach (Genome g in s.Genomes)
            {
                sum += g.AdjustedFitness;
            }
            double r = random.NextDouble() * sum;
            double count = 0;
            foreach (Genome g in s.Genomes)
            {
                count += g.AdjustedFitness;
                if (count >= r)
                {
                    return g;
                }
            }

            throw new Exception("Cannot find a genome. {size=" + Species.Count + "}");

        }

        // 자식을 생성할 때, 종을 뽑음. 한 종이 선택될 확률은 (종의 개체들의 평균 적합도) / (전체 종의 개체들의 평균 적합도의 합) 이다.
        public Species SelectSpecies(Random random)
        {
            double sum = 0;
            foreach (Species s in Species)
            {
                sum += s.TotalAdjustedFitness; 
            }
            double r = random.NextDouble() * sum;
            double count = 0;
            foreach(Species s in Species)
            {
                count += s.TotalAdjustedFitness;
                if(count >= r)
                {
                    return s;
                }
            }

            throw new Exception("Cannot find a species. {size=" + Species.Count + ", " + sum + ", " + count + "," + r +"}");

        }

        // 현재 Cursor가 나타내는 종이 적합도가 측정되었는지 확인
        public bool IsFitnessMeasured()
        {
            return GetCurrentGenome().Fitness != 0;
        }

        // Cursor 값을 바탕으로 현재 종을 가져옮
        public Genome GetCurrentGenome()
        {
            return Species[speciesCursor].Genomes[genomeCursor];
        }

        // Cursor을 다음 종의 위치로 이동시킴.
        // 이때, 지금 나타내는 값이 마지막 개체라면, 커서를 초기화시키는 동시에 참을 반환하고 그렇지 않으면 거짓을 반환함.
        public bool NextGenome()
        {
            ++genomeCursor;
            if (genomeCursor >= Species[speciesCursor].Genomes.Count)
            {
                genomeCursor = 0;
                ++speciesCursor;

                if (speciesCursor >= Species.Count)
                {

                    speciesCursor = 0;
                    return true;
                }
            }
            return false;
        }
        
        // 새로 만들어진 개체를 종을 추가함
        // 기존의 종에 속하는 개체면 기존의 종에 집어넣고, 어떠한 종에도 해당하지 않으면 새로운 종을 생성함.
        public void AddToSpecies(Genome g)
        {

            foreach(Species s in Species)
            {
                if(s.Genomes[0].GetDistance(g) < DeltaThreshold)
                {
                    // 기존의 종에 추가
                    s.Genomes.Add(g);
                    return;
                }
            }
            
            // 새로운 종 생성
            Species newSpecies = new Species();
            newSpecies.SpeciesId = ++SpeciesId;
            newSpecies.FromGen = Generation;
            newSpecies.Genomes.Add(g);
            
            Species.Add(newSpecies);
        }
    
        public int NewInnovation()
        {
            return Innovation++;
        }

        
        public int NewGenome()
        {
            return GenomeId++;
        }
        
        

    }
}
