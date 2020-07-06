using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.NEAT
{
    class Writer : IWriter
    {
        public FileInfo File { get; private set; }
        private int currentTime;
        public StreamWriter Sw { get; private set; }

        public Writer(String file) : this(new FileInfo(file))
        {

        }

        public Writer(FileInfo file)
        {
            this.File = file;
            if (!file.Exists) file.Create().Close();

            Sw = file.AppendText();
            currentTime = Environment.TickCount;

            Sw.WriteLine("StartTime=" + currentTime);
        }

        public void Start(Pool pool, int execute)
        {
            Sw.WriteLine("\"Settings\":{\"Execution\":" + execute + ", \"Population\":" + pool.Population + ", \"DeltaThreshold\":" + pool.DeltaThreshold +
                ", \"DeltaDisjoint\":" + pool.DeltaDisjoint + ", \"DeltaWeight\":" + pool.DeltaWeight + ", \"PerturbChance\":" + pool.PerturbChance + ", \"StepSize\":" + pool.StepSize +
                ", \"LinkMutationChance\":" + pool.LinkMutationChance + ", \"ConnectionMutationChance\":" + pool.ConnectionMutationChance + ", \"NodeMutationChance\":" + pool.NodeMutationChance +
                ", \"EnableMutationChance\":" + pool.EnableMutationChance + ", \"DisableMutationChance\":" + pool.DisableMutationChance + ", \"SurviveRate\":" + pool.SurviveRate + ", \"Staleness\":" + pool.Staleness + "}");
        }

        public void Record()
        {
            currentTime = Environment.TickCount;
        }

        public void Write(Genome g, DataDictionary dd)
        {
            long[] bestScores = dd.GetScore(g.GenomeId);
            double avgScore = 0;
            double deviation = 0;
            long topScore = 0;
            foreach (int s in bestScores)
            {
                avgScore += s;
                if (s > topScore) topScore = s;
            }
            avgScore /= bestScores.Length;
            foreach (int s in bestScores)
            {
                deviation = (s - avgScore) * (s - avgScore);
            }
            deviation /= bestScores.Length;
            deviation = Math.Sqrt(deviation);

            Species bestSpecies = null;
            Pool p = g.Pool;
            int scoreSum = 0;
            int bestScoreSum = 0;
            long fitnessSum = 0;
            foreach (Species s in g.Pool.Species)
            {
                foreach (Genome genomes in s.Genomes)
                {
                    fitnessSum += genomes.Fitness;
                    long[] scoreset = dd.GetScore(genomes.GenomeId);
                    int best = 0;
                    double scoreAv = 0;
                    foreach (int score in scoreset)
                    {
                        if (best < score) best = score;
                        scoreSum += score;
                    }
                    bestScoreSum += best;
                    if (genomes == g)
                    {
                        bestSpecies = s;
                    }
                }
            }
            double scoreAvg = (double)scoreSum / p.Population / bestScores.Length;
            double fitnessAvg = (double)fitnessSum / p.Population;

            Sw.WriteLine("\"Gen\" : {\"gen\":" + p.Generation + ", \"Species\":" + p.Species.Count + ", \"BestGenome\": {\"Id\":" + g.GenomeId + ", \"From\":" + g.FromGeneration + ", \"Fitness\":" + g.Fitness
                + ", \"topScore\":" + topScore + ", \"avgScore\":" + avgScore + ", \"deviation\":" + deviation + "}, \"BestSpecies\":{\"id\":" + bestSpecies.SpeciesId + ", \"staleness\":" + bestSpecies.Staleness +
                ", \"From\":" + bestSpecies.FromGen + ", \"Genomes\":" + bestSpecies.Genomes.Count + "}, \"ScoreAvg\":" + scoreAvg + ", \"FitnessAvg\":" + fitnessAvg +
                ", \"Time\":" + (Environment.TickCount - currentTime) + "}");
        }

        public void WriteGene(Genome g, DataDictionary dd)
        {
            try
            {
                Sw.WriteLine("Genome{" + g.GenesToString() + ", " + dd.GetDataAsString(g.GenomeId) + "}");
            }catch(NotImplementedException e)
            {
                Sw.WriteLine("Genome{" + g.GenesToString() + "}");
            }
            
        }
    }
}
