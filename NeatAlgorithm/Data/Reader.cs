using NeatAlgorithm._2048;
using NeatAlgorithm.NEAT;
using NeatAlgorithm.Pacman;
using NeatAlgorithm.Snake;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Data
{
    public class Reader
    {
        public int Execution { get; private set; }
        public int Population { get; private set; }
        public double DeltaThreshold { get; private set; }
        public double DeltaDisjoint { get; private set; }
        public double DeltaWeight { get; private set; }
        public double PerturbChance { get; private set; }
        public double StepSize { get; private set; }
        public double LinkMutationChance { get; private set; }
        public double ConnectionMutationChance { get; private set; }
        public double NodeMutationChance { get; private set; }
        public double EnableMutationChance { get; private set; }
        public double DisableMutationChance { get; private set; }
        public double SurviveRate { get; private set; }
        public int Staleness { get; private set; }
        public int InputMode { get; private set; }

        public int Gen { get; private set; }
        public int Species { get; private set; }

        public GameFactory GameFactory { get; private set; }

        public string Filename { get; set; }
        private Pool pool;
        public List<Genome> Best { get; }


        public Reader(string file)
        {
            Filename = file;
            Best = new List<Genome>();
        }
        
        

        public DataDictionary Read()
        {
            DataDictionary d = null;
            StreamReader sr = new StreamReader(Filename);
            string data;
            while ((data = sr.ReadLine()) != null)
            {
                JObject j;
                if (data.StartsWith("\"Settings\""))
                {
                    j = JObject.Parse(data.Substring(11));
                    Execution = j["Execution"].ToObject<int>();
                    Population = j["Population"].ToObject<int>();
                    DeltaThreshold = j["DeltaThreshold"].ToObject<double>();
                    DeltaDisjoint = j["DeltaDisjoint"].ToObject<double>();
                    DeltaWeight = j["DeltaWeight"].ToObject<double>();
                    PerturbChance = j["PerturbChance"].ToObject<double>();
                    StepSize = j["StepSize"].ToObject<double>();
                    LinkMutationChance = j["LinkMutationChance"].ToObject<double>();
                    ConnectionMutationChance = j["ConnectionMutationChance"].ToObject<double>();
                    NodeMutationChance = j["NodeMutationChance"].ToObject<double>();
                    EnableMutationChance = j["EnableMutationChance"].ToObject<double>();
                    DisableMutationChance = j["DisableMutationChance"].ToObject<double>();
                    SurviveRate = j["SurviveRate"].ToObject<double>();
                    Staleness = j["Staleness"].ToObject<int>();
                    JToken inputToken = j["InputMode"];
                    if(inputToken == null)
                    {
                        InputMode = 0;
                    }
                    else
                    {
                        InputMode = inputToken.ToObject<int>();
                    }
                    JToken gametoken = j["Game"];
                    if (gametoken == null)
                    {
                        GameFactory = new SnakeGameFactory(16, 16);
                        pool = new Pool(24, 4, null);
                    }
                    else
                    {
                        string game = gametoken.ToString();
                        if (game.StartsWith("Snake"))
                        {
                            string[] ds = game.Split('_');

                            GameFactory = new SnakeGameFactory(int.Parse(ds[1]), int.Parse(ds[2]));
                            pool = new Pool(24, 4, null);
                        }
                        else if (game.Equals("2048"))
                        {
                            GameFactory = new Game2048Factory();
                            pool = new Pool(16, 4, null);
                        }
                        else if (game.Equals("Pacman"))
                        {
                            GameFactory = new PacmanGameFactory();
                        }
                    }
                    d = GameFactory.GetDataDictionary();
                }
                else if (data.StartsWith("\"Gen\""))
                {
                    j = JObject.Parse(data.Substring(6));
                    Gen = j["gen"].ToObject<int>();
                    Genome g = new Genome(pool);
                    Best.Add(g);
                    g.GenomeId = j["BestGenome"]["Id"].ToObject<int>();
                    g.FromGeneration = j["BestGenome"]["From"].ToObject<int>();
                    g.Fitness = j["BestGenome"]["Fitness"].ToObject<long>();
                    d.CreateScore(g.GenomeId, 1);
                    d.AddScore(g.GenomeId, j["BestGenome"]["topScore"].ToObject<int>(), 0);
                    g.ExecutionTime = j["BestGenome"]["ExecutionTime"].ToObject<long>();
                }
                else if (data.StartsWith("\"Genome\""))
                {
                    j = JObject.Parse(data.Substring(9));
                    var ja = j.SelectToken("Genes");
                    foreach (JObject item in ja)
                    {
                        Gene gene = new Gene(item["In"].ToObject<int>(), item["Out"].ToObject<int>(),
                            item["Weight"].ToObject<double>(), item["Enable"].ToObject<bool>(), item["Innovation"].ToObject<int>());
                        Best[Gen].Genes.Add(gene);
                    }

                    ja = j.SelectToken("food");
                    if (ja != null)
                    {
                        SnakeDataDictionary sdd = d as SnakeDataDictionary;
                        LinkedList<Square> foods = new LinkedList<Square>();
                        foreach (var food in ja)
                        {
                            foods.AddLast(new Square(food[0].ToObject<int>(), food[1].ToObject<int>()));
                        }
                        sdd.AddFood(Best[Gen].GenomeId, foods);
                    }
                    ja = j.SelectToken("cells");
                    if(ja != null)
                    {
                        Data2048Dictionary d2d = d as Data2048Dictionary;
                        LinkedList<CreatedCells> cc = new LinkedList<CreatedCells>();
                        foreach(var cells in ja)
                        {
                            cc.AddLast(new CreatedCells(cells[0].ToObject<int>(), cells[1].ToObject<int>() == 2));
                        }
                        d2d.AddCells(Best[Gen].GenomeId, cc);
                    }

                } else if (data.StartsWith("\"Species\""))
                {

                }

            }

            return d;
        }
    }
}
