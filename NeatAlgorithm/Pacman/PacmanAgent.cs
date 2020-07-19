using NeatAlgorithm.NEAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NeatAlgorithm.Pacman
{
    public class PacmanAgent : Agent
    {
        
        public static readonly int[,] BasicCells = new int[,] { // y, x
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 1
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 4, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 4, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1}, //5
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1}, // 10
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 1, 3, 3, 3, 3, 3, 3, 1, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 1, 2, 0, 0, 0, 1, 3, 3, 3, 3, 3, 3, 1, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0}, // 15
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 1, 3, 3, 3, 3, 3, 3, 1, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1}, // 20 $34
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2 ,1, 1, 1, 1, 2, 1},
            {1, 4, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 4, 1},
            {1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1}, // 25 $39
            {1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1},
            {1, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1},
            {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1}, // 30
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} };

        public static readonly Location GHOST_TARGET = new Location(13, 11);

        public static readonly Location[] SCATTER_TARGET = new Location[] { new Location(0, 31), new Location(27, 31), new Location(2, -4), new Location(25, -4) };
       

        public int[,] Cells { get; private set; } // x 28 // y 31
        public int LeftDots { get; private set; }
        public int Time { get; set; }
        

        public Ghost[] Ghosts { get; set; } // 0 for clyde, 1 for inky, 2 for pinky, 3 for blinky
        public Player Player { get; set; }
        public bool ActiveGhosts { get; set; }
        private LinkedList<Location>[] cellLog;

        public PacmanAgent(Random r) : base(r)
        {
            Execute = 1;
            ActiveGhosts = true;
        }

        public void Initialize()
        {
            Cells = new int[31, 28];
            for (int y = 0; y < 31; ++y)
            {
                for (int x = 0; x < 28; ++x)
                {
                    Cells[y, x] = BasicCells[y, x];
                }
            }
            Ghosts = new Ghost[] { new Ghost(), new Ghost(), new Ghost(), new Ghost() };
            Gameover = false;
            Score = 0;
            Time = 0;
            foreach (Ghost g in Ghosts)
            {
                g.Location = GHOST_TARGET;
            }
            LeftDots = 240;
            Player = new Player();
            Player.Location = new Location(13, 23);
            cellLog = new LinkedList<Location>[4];
            for (int j = 0; j < 4; ++j)
            {
                cellLog[j] = new LinkedList<Location>();
            }
        }

        public void Tick()
        {
            ++Time;
            int index = 0;
            if (ActiveGhosts)
            {
                foreach (Ghost g in Ghosts)
                {
                    bool move = false;

                    if (g.Killed)
                    {
                        move = !g.Location.Equals(GHOST_TARGET);
                        g.Target = GHOST_TARGET;
                        if (!move)
                        {
                            ++g.KilledTime;
                            if (g.KilledTime == 8)
                            {
                                g.KilledTime = 0;
                                g.Killed = false;
                            }
                        }
                    }
                    else if (g.Frighten)
                    {
                        move = Time % 4 == 0;
                        g.Target = new Location(random.Next(28), random.Next(31));
                        cellLog[index].AddLast(g.Target);
                    }

                    else if (g.State == 2)
                    {
                        move = Time % 2 == 0;
                        ++g.Time;
                        if (g.Time == 70)
                        {
                            g.State = 1;
                            g.Time = 0;
                            g.CurrentDirection = (Direction)(((int)g.CurrentDirection + 2) % 4);
                        }
                        g.Target = SCATTER_TARGET[index];
                    }
                    else if (g.State == 1)
                    {
                        move = Time % 2 == 0;
                        ++g.Time;
                        if (g.Time == 280 && g.ScatterCount < 3)
                        {
                            if (index != 3 || LeftDots > 50)
                            {
                                g.State = 3;
                                ++g.ScatterCount;
                                g.Time = 0;
                                g.CurrentDirection = (Direction)(((int)g.CurrentDirection + 2) % 4);
                            }
                        }

                        if (index == 0)
                        {
                            int dx = Player.Location.X - g.Location.X;
                            int dy = Player.Location.Y - g.Location.Y;
                            g.Target = dx * dx + dy * dy < 64 ? SCATTER_TARGET[0] : Player.Location;
                        }
                        else if (index == 1)
                        {
                            Location intermediate = Instance.MoveDirection(Player.Location, Player.CurrentDirection, 2);
                            int newX = 2 * intermediate.X - Ghosts[3].Location.X;
                            int newY = 2 * intermediate.Y - Ghosts[3].Location.Y;
                            g.Target = new Location(newX, newY);
                        }
                        else if (index == 2)
                        {
                            g.Target = Instance.MoveDirection(Player.Location, Player.CurrentDirection, 4);
                        }
                        else if (index == 3)
                        {
                            g.Target = Player.Location;
                        }

                    }

                    if (move)
                    {
                        g.Move(GetMovables(g.Location));
                    }

                    if (g.State == 0 && g.Location.Equals(GHOST_TARGET))
                    {
                        g.State = 3;
                        g.ScatterCount = 1;

                    }
                    ++index;

                    if (Player.Location.Equals(g.Location) && !g.Killed)
                    {
                        if (g.Frighten)
                        {
                            g.Killed = true;
                            g.Frighten = false;
                        }
                        else
                        {
                            Player.Killed = true;
                            Gameover = true;
                        }
                    }
                }
            }

            if (Gameover) return;

            if(Time % 2 == 0)
            {
                Player.Input = GetInputs();
                Player.Move(GetMovables(Player.Location));

                if(Cells[Player.Location.Y, Player.Location.X] == 2)
                {
                    Score+= 10;
                    Cells[Player.Location.Y, Player.Location.X] = 0;
                    --LeftDots;
                }else if(Cells[Player.Location.Y, Player.Location.X] == 4)
                {
                    Score += 50;


                    Cells[Player.Location.Y, Player.Location.X] = 0;

                    Player.State = 2;
                    Player.Time = 0;

                    foreach(Ghost g in Ghosts)
                    {
                        g.CurrentDirection = (Direction)(((int)g.CurrentDirection + 2) % 4);
                        g.Frighten = true;
                    }
                }

                if (Player.State == 2)
                {
                    ++Player.Time;
                    if(Player.Time == 70)
                    {
                        foreach (Ghost g in Ghosts)
                        {
                            g.Frighten = false;
                        }
                        Player.State = 1;
                    }
                }


            }
        }

        public double[] GetInputs()
        {
            // todo
            return new double[]{ Time % 2 };
        }

        public override long Evaluate(Genome g, DataDictionary dd)
        {
            PacmanDataDictionary pdd = dd as PacmanDataDictionary;
            long best = long.MinValue;
            long sum = 0;

            dd.CreateScore(g.GenomeId, Execute);

            for(int i = 0; i < Execute; ++i)
            {
                Initialize();
                Player.Genome = g;
                while (!Gameover)
                {
                    Tick();
                }

                pdd.AddScore(g.GenomeId, Score, i);
                long f = Fitness();
                sum += f;
                if(f > best)
                {
                    best = f;
                    pdd.AddFrightenData(g.GenomeId, cellLog);
                }
            }


            return sum / Execute;
        }

        public long Fitness()
        {
            return Score +1;
        }


        public Direction[] GetMovables(Location loc)
        {
            int x = loc.X;
            int y = loc.Y;
            int count = 0;
            int data = 0;
            // up = 1, left = 2 down = 4 right = 8 
            if (Cells[y - 1, x] != 1)
            {
                data += 1;
                ++count;
            }
            if (Cells[y, x - 1] != 1)
            {
                data += 2;
                ++count;
            }
            if (Cells[y + 1, x] != 1)
            {
                data += 4;
                ++count;
            }
            if (Cells[y, x +1] != 1)
            {
                data += 8;
                ++count;
            }
            int index = 0;

            Direction[] able = new Direction[count];
            for(int i = 0; i < 4; ++i)
            {
                if(((data >> i) & 1) == 1)
                {
                    able[index++] = (Direction)i;
                }
            }
            return able;
        }





        public override void Display(Genome genome, DataDictionary dd)
        {
            base.Display(genome, dd);
            Initialize();
            Player.Genome = genome;
            LinkedList<Location>[] cell = (dd as PacmanDataDictionary).GetFrightenData(genome.GenomeId);
            while (!Gameover)
            {
                //Tick
                LinkedListNode<Location>[] cellNode = new LinkedListNode<Location>[4];
                for(int i = 0; i < 4; ++i)
                {
                    cellNode[i] = cell[i].First;
                }

                ++Time;
                int index = 0;

                if (ActiveGhosts)
                {
                    foreach (Ghost g in Ghosts)
                    {
                        bool move = false;

                        if (g.Killed)
                        {
                            move = !g.Location.Equals(GHOST_TARGET);
                            g.Target = GHOST_TARGET;
                            if (!move)
                            {
                                ++g.KilledTime;
                                if (g.KilledTime == 8)
                                {
                                    g.KilledTime = 0;
                                    g.Killed = false;
                                }
                            }
                        }
                        else if (g.Frighten)
                        {
                            move = Time % 4 == 0;
                            g.Target = cellNode[index].Value;
                            cellNode[index] = cellNode[index].Next;
                        }

                        else if (g.State == 2)
                        {
                            move = Time % 2 == 0;
                            ++g.Time;
                            if (g.Time == 70)
                            {
                                g.State = 1;
                                g.Time = 0;
                                g.CurrentDirection = (Direction)(((int)g.CurrentDirection + 2) % 4);
                            }
                            g.Target = SCATTER_TARGET[index];
                        }
                        else if (g.State == 1)
                        {
                            move = Time % 2 == 0;
                            ++g.Time;
                            if (g.Time == 280 && g.ScatterCount < 3)
                            {
                                if (index != 3 || LeftDots > 50)
                                {
                                    g.State = 3;
                                    ++g.ScatterCount;
                                    g.Time = 0;
                                    g.CurrentDirection = (Direction)(((int)g.CurrentDirection + 2) % 4);
                                }
                            }

                            if (index == 0)
                            {
                                int dx = Player.Location.X - g.Location.X;
                                int dy = Player.Location.Y - g.Location.Y;
                                g.Target = dx * dx + dy * dy < 64 ? SCATTER_TARGET[0] : Player.Location;
                            }
                            else if (index == 1)
                            {
                                Location intermediate = Instance.MoveDirection(Player.Location, Player.CurrentDirection, 2);
                                int newX = 2 * intermediate.X - Ghosts[3].Location.X;
                                int newY = 2 * intermediate.Y - Ghosts[3].Location.Y;
                                g.Target = new Location(newX, newY);
                            }
                            else if (index == 2)
                            {
                                g.Target = Instance.MoveDirection(Player.Location, Player.CurrentDirection, 4);
                            }
                            else if (index == 3)
                            {
                                g.Target = Player.Location;
                            }

                        }

                        if (move)
                        {
                            g.Move(GetMovables(g.Location));
                        }

                        if (g.State == 0 && g.Location.Equals(GHOST_TARGET))
                        {
                            g.State = 3;
                            g.ScatterCount = 1;

                        }
                        ++index;

                        if (Player.Location.Equals(g.Location) && !g.Killed)
                        {
                            if (g.Frighten)
                            {
                                g.Killed = true;
                                g.Frighten = false;
                            }
                            else
                            {
                                Player.Killed = true;
                                Gameover = true;
                            }
                        }
                    }
                }

                //PlayerTick
                if (Time % 2 == 0)
                {
                    Player.Input = GetInputs();
                    Player.Move(GetMovables(Player.Location));

                    if (Cells[Player.Location.Y, Player.Location.X] == 2)
                    {
                        Score += 10;
                        Cells[Player.Location.Y, Player.Location.X] = 0;
                        --LeftDots;
                    }
                    else if (Cells[Player.Location.Y, Player.Location.X] == 4)
                    {
                        Score += 50;


                        Cells[Player.Location.Y, Player.Location.X] = 0;

                        Player.State = 2;
                        Player.Time = 0;

                        foreach (Ghost g in Ghosts)
                        {
                            g.CurrentDirection = (Direction)(((int)g.CurrentDirection + 2) % 4);
                            g.Frighten = true;
                        }
                    }

                    if (Player.State == 2)
                    {
                        ++Player.Time;
                        if (Player.Time == 70)
                        {
                            foreach (Ghost g in Ghosts)
                            {
                                g.Frighten = false;
                            }
                        }
                    }
                }

                //Draw
                Draw();

             
            }
        }

       public void Draw()
        {
            Console.Clear();
            for (int y = 0; y < 31; ++y)
            {
                for (int x = 0; x < 28; ++x)
                {
                    if (Player.Location.X == x && Player.Location.Y == y)
                    {
                        Console.Write("○ ");
                    }
                    else
                        if (Ghosts[0].Location.X == x && Ghosts[0].Location.Y == y && ActiveGhosts)
                    {

                        Console.Write(Ghosts[0].Frighten ? "F  " : Ghosts[0].Killed ? "\"\" " : "C  ");
                    }
                    else if (Ghosts[1].Location.X == x && Ghosts[1].Location.Y == y && ActiveGhosts)
                    {
                        Console.Write(Ghosts[1].Frighten ? "F  " : Ghosts[1].Killed ? "\"\" " : "I  ");

                    }
                    else if (Ghosts[2].Location.X == x && Ghosts[2].Location.Y == y && ActiveGhosts)
                    {
                        Console.Write(Ghosts[2].Frighten ? "F  " : Ghosts[2].Killed ? "\"\" " : "P  ");
                    }
                    else if (Ghosts[3].Location.X == x && Ghosts[3].Location.Y == y && ActiveGhosts)
                    {
                        Console.Write(Ghosts[3].Frighten ? "F  " : Ghosts[3].Killed ? "\"\" " : "B  ");

                    }
                    else if (Cells[y, x] == 1)
                    {
                        Console.Write("■ ");
                    }
                    else if (Cells[y, x] == 2)
                    {
                        Console.Write("· ");
                    }
                    else if (Cells[y, x] == 4)
                    {
                        Console.Write("★ ");
                    }
                    else
                    {
                        Console.Write("　 ");
                    }
                }
                Console.WriteLine();
            }
            Thread.Sleep(140);
            
        }

        public override string ToString()
        {
            return "Pacman";
        }
    }
    
    public struct Location
    {
        public int X { get; }
        public int Y { get; }

        public Location(int x, int y) 
        {
            X = x;
            Y = y;
        }
   

        public override bool Equals(object obj)
        {
            if (!(obj is Location))
            {
                return false;
            }

            Location location = (Location)obj;
            return X == location.X &&
                   Y == location.Y;
        }

        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }
    }
}
