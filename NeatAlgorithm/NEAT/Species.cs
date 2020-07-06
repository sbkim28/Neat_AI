using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    // 종
    // 종은 NEAT에서 새로 생긴 구조적 혁신을 보호하는 기능을 함.
    // 새로 등장한 개체의 구조가 기존의 개체들의 구조보다 잠재력이 높지만, 당장은 적합도가 낮아서 도태되는 것을 방지하기 위함.
    
    public class Species
    {
        // 종에 속하는 개체
        public List<Genome> Genomes { get; set; }
        public double TotalAdjustedFitness { get; private set; }
        // 종 아이디
        public int SpeciesId { get; set; }
        // staleness. 이 값이 특정 값(Pool.Staleness)보다 커지면 발전이 없는 종으로 판단하고 종을 제거함
        public int Staleness { get; set; }
        // 종이 만들어진 세대
        public int FromGen { get; set; }

        public Species()
        {
            Genomes = new List<Genome>();
        }

        // 평균 적합도를 구하기 위해 종의 adjusted Fitness를 더함. adjusted Fitness란, 한 개체의 적합도를 종의 수로 나눈 값.
        public void AddFitness(double adjustedFitness)
        {
            TotalAdjustedFitness += adjustedFitness;
        }

        

    }
}
