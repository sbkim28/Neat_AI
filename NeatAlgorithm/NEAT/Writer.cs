using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Stopwatch sw;

        public Writer(String file) : this(new FileInfo(file))
        {
            sw = new Stopwatch();
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
            sw.Reset();
            sw.Start();
        }


        private double GetDeviation(long[] scores, double avg)
        {
            double deviation = 0;

            foreach (long s in scores)
            {
                deviation = (s - avg) * (s - avg);
            }
            deviation /= scores.Length;
            deviation = Math.Sqrt(deviation);
            return deviation;
        }
        

        public void Write(Genome g, DataDictionary dd)
        {
            sw.Stop();
            long[] bestScores = dd.GetScore(g.GenomeId);

            Species bestSpecies = null;
            Pool p = g.Pool;
            long scoreSum = 0;
            long bestScoreSum = 0;
            long fitnessSum = 0;
            foreach (Species s in g.Pool.Species)
            {
                foreach (Genome genomes in s.Genomes)
                {
                    fitnessSum += genomes.Fitness;
                    long[] scoreset = dd.GetScore(genomes.GenomeId);
                    long best = 0;
                    double scoreAv = 0;
                    foreach (long score in scoreset)
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
            double bestScoreAvg = (double)bestScoreSum / p.Population;
            double fitnessAvg = (double)fitnessSum / p.Population;

            StringBuilder sb = new StringBuilder();
            sb.Append("\"Gen\" : {\"gen\":").Append(p.Generation).Append(", \"Species\":").Append(p.Species.Count)
                .Append(", \"BestGenome\":");
            WriteGene(sb, g, bestScores);

            sb.Append( bestSpecies.SpeciesId).Append(", \"staleness\":").Append( bestSpecies.Staleness )
                .Append(", \"From\":").Append(bestSpecies.FromGen ).Append( ", \"Genomes\":").Append( bestSpecies.Genomes.Count ).Append( "}, \"ScoreAvg\":")
                .Append( scoreAvg).Append(", \"BestScoreAvg\":").Append(bestScoreAvg).Append( ", \"FitnessAvg\":" ).Append( fitnessAvg ).Append( ", \"Time\":" ).Append(sw.ElapsedMilliseconds).Append( "}");

            Sw.WriteLine(sb.ToString());
        }

        private void WriteGene(StringBuilder sb, Genome g, long[] scores)
        {

            double avgScore = 0;
            long topScore = 0;
            foreach (long s in scores)
            {
                avgScore += s;
                if (s > topScore) topScore = s;
            }
            avgScore /= scores.Length;
            double deviation = GetDeviation(scores, avgScore);

            sb.Append("{\"Id\":").Append(g.GenomeId).Append(", \"From\":").Append(g.FromGeneration).Append(", \"Fitness\":").Append(g.Fitness)
                .Append(", \"topScore\":").Append(topScore).Append(", \"avgScore\":").Append(avgScore).Append(", \"deviation\":").Append(deviation)
                .Append("}");
        }

        public void WriteGene(Genome g, DataDictionary dd)
        {
            try
            {
                Sw.WriteLine("\"Genome\":{" + g.GenesToString() + ", " + dd.GetDataAsString(g.GenomeId) + "}");
            }catch(NotImplementedException e)
            {
                Sw.WriteLine("\"Genome\":{" + g.GenesToString() + "}");
            }
            
        }

        public void WriteSpecies(List<Species> species, DataDictionary dd)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("\"Species\":[");

            foreach(Species s in species)
            {
                long minFit = long.MinValue;
                Genome best = null;
                foreach(Genome g in s.Genomes)
                {
                    if(g.Fitness > minFit)
                    {
                        minFit = g.Fitness;
                        best = g;
                    }
                }
                long[] scores = dd.GetScore(best.GenomeId);

                sb.Append("{\"Id\":").Append(s.SpeciesId)
                    .Append(", \"Count\":").Append(s.Genomes.Count)
                    .Append(", \"From\":").Append(s.FromGen)
                    .Append(", \"Best:\"").Append("\"Genome\":");

                WriteGene(sb, best, scores);
                sb.Append(", ")
                    .Append(best.GenesToString()).Append(", ").Append( dd.GetDataAsString(best.GenomeId)).Append( "}");

            }
            Sw.Write(sb.ToString());

        }

        public void Set(string key, object val)
        {

        }
    }
}
