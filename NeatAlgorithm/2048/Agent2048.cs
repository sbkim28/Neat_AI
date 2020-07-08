﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using NeatAlgorithm.NEAT;

namespace NeatAlgorithm._2048
{
    class Agent2048 : IAgent
    {
        public long Score { get; set; }
        public int FailCount { get; private set; }
        public int FailLimit { get; set; }
        public int[] Cells { get; set; }
        public bool Gameover { get; set; }
        public int Execute { get; set; }
        

        private Random random;

        public Agent2048(Random random)
        {
            this.random = random;
            FailLimit = int.MaxValue;
            Execute = 1;
        }
        
        public void Initialize(int id, LinkedList<CreatedCells> cellLog)
        {
            Cells = new int[16];
            Score = 0;
            FailCount = 0;
            Gameover = false;
            CreateRandom(16, cellLog);
            CreateRandom(15, cellLog);
        }
        

        public virtual double[] GetInputs()
        {
            double[] inputs = new double[16];
            for(int i = 0; i < 16; ++i)
            {
                inputs[i] = Cells[i];
            }
            return inputs;
        }

        public long Evaluate(Genome g, DataDictionary dd)
        {
            Data2048Dictionary d2d = dd as Data2048Dictionary;
            long best = long.MinValue;
            long sum = 0;

            dd.CreateScore(g.GenomeId, Execute);

            for (int i = 0; i < Execute; ++i)
            {
                LinkedList<CreatedCells> cellLog = new LinkedList<CreatedCells>();
                Initialize(g.GenomeId, cellLog);
                while (!Gameover)
                {
                    double[] output = g.EvaluateNetwork(GetInputs());
                    int[] order = new int[4] { 0, 1, 2, 3 };
                    Array.Sort(output, order);

                    int index = 3;
                    while (!Shift((Direction)order[index]))
                    {
                        if (++FailCount >= FailLimit)
                        {
                            Gameover = true;
                            break;
                        }

                        --index;
                    }
                    CreateRandom(SizeEmpty(), cellLog);
                    SetGameover();
                }
                sum += GetFitness();
                 
                if(GetFitness() > best)
                {
                    best = GetFitness();
                    d2d.AddCells(g.GenomeId, cellLog);
                    
                }

                dd.AddScore(g.GenomeId, Score, i);
            }
            return sum / Execute;
        }

        public long GetFitness()
        {
            return Score;
        }

        public virtual void Display(Genome g, DataDictionary dd)
        {
            Data2048Dictionary d2d = dd as Data2048Dictionary;
            Console.Clear();
            Cells = new int[16];
            Score = 0;
            FailCount = 0;
            Gameover = false;
            LinkedList<CreatedCells> cc = d2d.getCells(g.GenomeId);
            LinkedListNode<CreatedCells> cnode = cc.First;
            Cells[cnode.Value.index] = cnode.Value.isTwo ? 1 : 2;
            cnode = cnode.Next;
            Cells[cnode.Value.index] = cnode.Value.isTwo ? 1 : 2;
            while (!Gameover)
            {
                double[] output = g.EvaluateNetwork(GetInputs());
                int[] order = new int[4] { 0, 1, 2, 3 };
                Array.Sort(output, order);

                int index = 3;
                while (!Shift((Direction)order[index]))
                {
                    if (++FailCount >= FailLimit)
                    {
                        Gameover = true;
                        break;
                    }
                    --index;
                }

                int i = 0;

                cnode = cnode.Next;
                Cells[cnode.Value.index] = cnode.Value.isTwo ? 1 : 2;
                SetGameover();
                Console.Clear();
                Console.WriteLine("Direction : " + (Direction)order[index] + ", Score : " + Score);
                foreach (int cell in Cells)
                {
                    Console.Write((cell == 0 ? "" : "" + Util.MathUtils.Pow(2, cell)) + "\t");
                    if (++i == 4)
                    {
                        i = 0;
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
                Thread.Sleep(50);

            }
            
        }

        public void CreateRandom(int emptySize, LinkedList<CreatedCells> cellLog)
        {
            int n = random.Next(10) < 9 ? 1 : 2;
            int insertIndex = GetEmptyCells(emptySize)[random.Next(emptySize)];
            Cells[insertIndex] = n;
            cellLog.AddLast(new CreatedCells(insertIndex, n == 1));
        }

        public void SetGameover()
        {
            if (SizeEmpty() != 0) return;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (Cells[i * 4 + j] == Cells[i * 4 + j + 4] || Cells[i * 4 + j] == Cells[i * 4 + j + 1])
                    {
                        return;
                    }
                }
            }

            for (int i = 0; i < 3; ++i)
            {
                if (Cells[i * 4 + 3] == Cells[i * 4 + 7]) return;
                if (Cells[12 + i] == Cells[13 + i]) return;
            }
            Gameover = true;
        }

        public bool Shift(Direction direction)
        {
            int act = 0;
            if (direction == Direction.LEFT)
            {
                for (int i = 0; i < 4; ++i)
                {
                    for (int j = 1; j < 4; ++j)
                    {
                        for (int k = j; k > 0; --k)
                        {
                            int index = i * 4 + k;
                            int prev = index - 1;
                            if (Cells[index] <= 0) break;
                            if (Cells[prev] != 0 && Cells[prev] != Cells[index]) break;

                            if (Cells[prev] == 0)
                            {
                                Cells[prev] = Cells[index];
                            }
                            else if (Cells[index] == Cells[prev])
                            {
                                Cells[prev] += 1;
                                Cells[prev] *= -1;
                                Score += Util.MathUtils.Pow(2, Cells[index] + 1);
                            }
                            Cells[index] = 0;
                            ++act;
                        }
                    }
                }
            }
            else if (direction == Direction.RIGHT)
            {
                for (int i = 0; i < 4; ++i)
                { 
                    for (int j = 2; j >= 0; --j)
                    {
                        for (int k = j; k < 3; ++k)
                        {
                            int index = i * 4 + k;
                            int prev = index + 1;
                            if (Cells[index] <= 0) break;
                            if (Cells[prev] != 0 && Cells[prev] != Cells[index]) break;

                            if (Cells[prev] == 0)
                            {
                                Cells[prev] = Cells[index];
                            }
                            else if (Cells[index] == Cells[prev])
                            {
                                Cells[prev] += 1;
                                Cells[prev] *= -1;
                                Score += Util.MathUtils.Pow(2, Cells[index] + 1);
                            }
                            Cells[index] = 0;
                            ++act;
                        }
                    }
                }
            }
            else if(direction == Direction.DOWN)
            {
                for (int i = 0; i < 4; ++i)
                { 
                    for (int j = 2; j >= 0; --j)
                    {
                        for (int k = j; k < 3; ++k)
                        {
                            int index = k * 4 + i;
                            int prev = index + 4;
                            if (Cells[index] <= 0) break;
                            if (Cells[prev] != 0 && Cells[prev] != Cells[index]) break;

                            if (Cells[prev] == 0)
                            {
                                Cells[prev] = Cells[index];
                            }
                            else if (Cells[index] == Cells[prev])
                            {
                                Cells[prev] += 1;
                                Cells[prev] *= -1;
                                Score += Util.MathUtils.Pow(2, Cells[index] + 1);
                            }
                            Cells[index] = 0;
                            ++act;
                        }
                    }
                }
            }
            else if(direction == Direction.UP)
            {
                for (int i = 0; i < 4; ++i)
                { 
                    for (int j = 1; j < 4; ++j)
                    {
                        for (int k = j; k > 0; --k)
                        {
                            int index = k * 4 + i;
                            int prev = index - 4;
                            if (Cells[index] <= 0) break;
                            if (Cells[prev] != 0 && Cells[prev] != Cells[index]) break;

                            if (Cells[prev] == 0)
                            {
                                Cells[prev] = Cells[index];
                            }
                            else if (Cells[index] == Cells[prev])
                            {
                                Cells[prev] += 1;
                                Cells[prev] *= -1;
                                Score += Util.MathUtils.Pow(2, Cells[index] + 1);
                            }
                            Cells[index] = 0;
                            ++act;
                        }
                    }
                }
            }


            for(int i = 0; i < 16; ++i)
            {
                if(Cells[i] < 0)
                {
                    Cells[i] *= -1;
                }
            }

            return act != 0;
        }

        public int SizeEmpty()
        {
            int cell = 0;

            foreach(int i in Cells)
            {
                if (i == 0) ++cell;
            }
            return cell;
        }
        
        public int[] GetEmptyCells(int emptySize)
        {
            int[] emptyCell = new int[emptySize];
            int j = 0;
            for(int i = 0; i < 16; ++i)
            {
                if(Cells[i] == 0)
                {
                    emptyCell[j++] = i;
                }
            }
            return emptyCell;
        }

        public int[] GetEmptyCells()
        {
            return GetEmptyCells(SizeEmpty());
        }

        
    }
    public struct CreatedCells
    {
        public int index;
        public bool isTwo;

        public CreatedCells(int index, bool isTwo)
        {
            this.index = index;
            this.isTwo = isTwo;
        }

        public override string ToString()
        {
            return "[" + index + ", " + (isTwo ? 2 : 4)+ "]";
        }
    }
}
