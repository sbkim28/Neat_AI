using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using NeatAlgorithm.NEAT;
using System.Diagnostics;

namespace NeatAlgorithm.Snake
{
    public class SnakeAgent : Agent
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction previousMove;
        public LinkedList<Square> Snake { get; private set; }
        public Square Food { get; set; }
        
        public int Hunger { get; private set; }
        public int LifeTime { get; private set; }
       


        public SnakeAgent(int x, int y, Random random) : base(random)
        {
            X = x; Y = y;
            Drawer = new Draw(ConsoleDraw);
            DisplayDelay = 25;
        }

        public void Initialize()
        {
            Gameover = false;
            previousMove = Direction.DOWN;
            Snake = new LinkedList<Square>();
            int midX = X / 2;
            int midY = Y / 2;
            Snake.AddFirst(new Square(midX, midY - 1));
            Snake.AddFirst(new Square(midX, midY));
            Snake.AddFirst(new Square(midX, midY + 1));
            Hunger = 256;
            if(random != null)
                CreateFood(random);
            LifeTime = 0;
            Score = 0;
        }
        

        public override void Display(Genome g, DataDictionary dd)
        {
            base.Display(g, dd);
            Initialize();
            LinkedList<Square> food = (dd as SnakeDataDictionary).GetFood(g.GenomeId);
            LinkedListNode<Square> foodNode = food.First;
            Food = foodNode.Value;
            while (!Gameover)
            {
                //ConsoleDraw();
                double[] output = g.EvaluateNetwork(GetInputs());
                double max = output[0];
                int index = 0;
                for (int i = 1; i < 4; ++i)
                {
                    if (output[i] > max)
                    {
                        max = output[i];
                        index = i;
                    }
                }
                Move((Direction)index);
                
                if (foodNode.Value != Food)
                {
                    foodNode = foodNode.Next;
                    Food = foodNode.Value;
                }
                Drawer();
                Thread.Sleep(DisplayDelay);

            }
            //Console.WriteLine("END");
        }

        public const string SNAKE_BLOCK = "■";
        public const string EMPTY_BLOCK = "□";

        private void ConsoleDraw()
        {
            int[,] cells = new int[X, Y];
            foreach (Square s in Snake)
            {
                cells[s.Y, s.X] = -1;
            }
            cells[Food.Y, Food.X] = 1;


            Console.Clear();
           /* Console.WriteLine("Gen :" + g.Pool.Generation + ", Gene : " + g.GenomeId + "");
            Console.WriteLine("Score :" + Score + ", Hunger : " + Hunger + "");
            Console.WriteLine("Max_Fitness :" + g.Pool.BestGenome.Fitness + ", Species : " + g.Pool.Species.Count + ", Population : " + g.Pool.Population);*/
            Console.WriteLine("Play of Best Genome");
            
            for (int i = 0; i < Y; ++i)
            {
                for (int j = 0; j < X; ++j)
                {
                    if (cells[j, i] == 0)
                    {
                        Console.Write(EMPTY_BLOCK);
                    }
                    else if (cells[j, i] == -1)
                    {
                        Console.Write(SNAKE_BLOCK);
                    }
                    else
                    {
                        Console.Write("☆");
                    }

                }
                Console.WriteLine();
            }
        }

        public double[] GetInputs()
        {
            double[] d = new double[24];
            double[] look = LookDirection(1, 0);
            d[0] = look[0];
            d[1] = look[1];
            d[2] = look[2];
            look = LookDirection(1, 1);
            d[3] = look[0];
            d[4] = look[1];
            d[5] = look[2];
            look = LookDirection(0, 1);
            d[6] = look[0];
            d[7] = look[1];
            d[8] = look[2];
            look = LookDirection(-1, 1);
            d[9] = look[0];
            d[10] = look[1];
            d[11] = look[2];
            look = LookDirection(-1, 0);
            d[12] = look[0];
            d[13] = look[1];
            d[14] = look[2];
            look = LookDirection(-1, -1);
            d[15] = look[0];
            d[16] = look[1];
            d[17] = look[2];
            look = LookDirection(0, -1);
            d[18] = look[0];
            d[19] = look[1];
            d[20] = look[2];
            look = LookDirection(1, -1);
            d[21] = look[0];
            d[22] = look[1];
            d[23] = look[2];
            return d;
        }

        public double[] LookDirection(int ix, int iy)
        {
            double[] look = new double[3];
            int posX = Snake.First.Value.X;
            int posY = Snake.First.Value.Y;
            int distance = 1;

            posX += ix;
            posY += iy;
            bool foodFound = false;
            bool bodyFound = false;
            while(posX >= 0 && posX < X && posY >= 0 && posY < Y)
            {
                posX += ix;
                posY += iy;

                if(!foodFound && posX == Food.X && posY == Food.Y)
                {
                    foodFound = true;
                    look[1] = 1;
                }
                if(!bodyFound && Snake.Contains(new Square(posX, posY)))
                {
                    bodyFound = true;
                    look[2] = 1.0 / distance;
                }
                ++distance;
            }
            look[0] = 1.0 / distance;
            return look;
        }

        public override long Evaluate(Genome g, DataDictionary dd)
        {
            SnakeDataDictionary fdd =  dd as SnakeDataDictionary;
            fdd.CreateLifetime(g.GenomeId, Execute);
            fdd.CreateScore(g.GenomeId, Execute);
            long best = long.MinValue ;
            long sum = 0;
            for (int k = 0; k < Execute; ++k)
            {
                Initialize();
                LinkedList<Square> foodList = new LinkedList<Square>();
                Square food = Food;
                foodList.AddLast(Food);
                
                while (!Gameover)
                {
                    double[] output = g.EvaluateNetwork(GetInputs());
                    double max = output[0];
                    int index = 0;
                    for (int i = 1; i < 4; ++i)
                    {
                        if (output[i] > max)
                        {
                            max = output[i];
                            index = i;
                        }
                    }

                    Move((Direction)index);
                    if (food != Food)
                    {
                        food = Food;
                        foodList.AddLast(food);
                        
                    }

                }

                sum += Fitness();
                if(best < Fitness())
                {
                    best = Fitness();
                    fdd.AddFood(g.GenomeId, foodList);
                }
                fdd.AddScore(g.GenomeId, Score, k);
                fdd.AddLifetime(g.GenomeId, LifeTime, k);
            }
            return sum / Execute; 
        }

        public void Move(Direction direction)
        {
            if (((int)direction + 2) % 4 == (int)previousMove)
            {
                direction = previousMove;
            }
            else
            {
                previousMove = direction;
            }
            Square head = Snake.First.Value;
            int headX = head.X;
            int headY = head.Y;
            switch (direction)
            {
                case Direction.UP:
                    headY -= 1;
                    if (headY < 0)
                    {
                        Gameover = true;
                        return;
                    }
                    break;
                case Direction.LEFT:
                    headX -= 1;
                    if (headX < 0)
                    {
                        Gameover = true;
                        return;
                    }
                    break;
                case Direction.DOWN:
                    headY += 1;
                    if (headY >= Y)
                    {
                        Gameover = true;
                        return;
                    }
                    break;
                case Direction.RIGHT:
                    headX += 1;
                    if (headX >= X)
                    {
                        Gameover = true;
                        return;
                    }
                    break;
            }
            Square newHead = new Square(headX, headY);

            if (Snake.Contains(newHead))
            {
                Gameover = true;
                return;
            }
            Snake.AddFirst(newHead);
            if (newHead.Equals(Food))
            {
                Hunger = 256;
                if (X * Y - Snake.Count == 0)
                {
                    Gameover = true; return;
                }
                CreateFood(random);
                Score += 1;
            }
            else
            {
                Snake.RemoveLast();

            }

            --Hunger;
            if (Hunger <= 0)
            {
                Gameover = true;
            }
            ++LifeTime;
        }

        public long Fitness()
        {
            long fitness;
            /*fitness = (int )Math.Min(LifeTime * LifeTime, 10000 + (LifeTime * 0.1)); 
            if(Score < 10)
            {
                fitness *= (int)Util.MathUtils.Pow(2, Score);
            }
            else
            {
                fitness *= 2048;
                fitness *= Score - 9; 
            }*/
            fitness = 5 * Score * Score;

            return fitness;
        }

        public void CreateFood(Random r)
        {
            HashSet<int> cells = new HashSet<int>();
            foreach (Square s in Snake)
            {
                cells.Add(s.X + s.Y * X);
            }
            int index = r == null ? 0 : r.Next(X * Y - cells.Count);
            bool flag = false;
            for (int i = 0; i < X * Y; ++i)
            {
                if (cells.Contains(i)) continue;
                if (index <= 0)
                {
                    flag = true;
                    this.Food = new Square(i % X, i / X);
                    
                    break;
                }
                --index;
            }
            if (!flag) Console.WriteLine("ILLEGAL");

        }

        public override string ToString()
        {
            return "Snake_" + X + "_" + Y; 
        }
    }
}
