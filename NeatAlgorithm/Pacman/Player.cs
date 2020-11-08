using NeatAlgorithm.NEAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Pacman
{
    public class Player : Instance
    {
        public bool invalid;
        public bool initiated;
        public Genome Genome { get; set; }
        public double[] Input { get; set; }

        public Player()
        {
            this.invalid = false;
            CurrentDirection = Direction.LEFT;
        }

        public override void Move(Direction[] m)
        {

            double[] outs = Genome.EvaluateNetwork(Input);

            int index = 0;
            double max = outs[0];
            for(int i = 1; i < 4; ++i)
            {
                if(max < outs[i])
                {
                    max = outs[i];
                    index = i;
                }
            }

            Move(m, (Direction)index);

            /* for test
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.LeftArrow:
                    Move(m, Direction.LEFT);
                    break;
                case ConsoleKey.RightArrow:
                    Move(m, Direction.RIGHT);
                    break;

                case ConsoleKey.UpArrow:
                    Move(m, Direction.UP);
                    break;
                case ConsoleKey.DownArrow:
                    Move(m, Direction.DOWN);

                    break;
                default:
                    Move(m, CurrentDirection);
                    break;
            }
            */ 
        }

        public void Move(Direction[] m, Direction d)
        {
            invalid = false;
            bool flag = false;
            bool flag2 = false;
            foreach(Direction able in m)
            {
                if(able == CurrentDirection)
                {
                    flag2 = true;
                }
                if(able == d)
                {
                    Location = MoveDirection(Location, able);
                    CurrentDirection = able;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                invalid = true;
                if (flag2) Location = MoveDirection(Location, CurrentDirection);
                
            }

        }
    }
}
