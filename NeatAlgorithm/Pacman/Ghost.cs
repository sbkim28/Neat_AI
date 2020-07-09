using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Pacman
{
    public  class Ghost : Instance
    {
        public int ScatterCount;
        public bool Frighten { get; set; }
        public int KilledTime { get; set; }
        public Location Target { get; set; }

        public Ghost() : base()
        {
            this.MoveTime = 2;
            ScatterCount = 1;
            State = 2;
            Frighten = false;
        }

        // up, left, down, right
        public override void Move(Direction[] m)
        {
            bool flag = false;
            Direction op = (Direction)(((int)CurrentDirection + 2) % 4);
            for (int i = 0; i < m.Length; ++i)
            {
                if (((int)m[i] + 2) % 4 != (int)CurrentDirection && m[i] != CurrentDirection)
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                Location = MoveDirection(Location, CurrentDirection);
            }
            else
            {
                int close = int.MaxValue;
                Direction dir = CurrentDirection;
                foreach (Direction d in m)
                {
                    if (d == op) continue;
                    Location move = MoveDirection(Location, d);
                    int x = move.X;
                    int y = move.Y;
                    int euclid = (x - Target.X) * (x - Target.X) + (y - Target.Y) * (y - Target.Y);
                    if (euclid < close)
                    {
                        dir = d;
                        close = euclid;
                    }
                }
                Location = MoveDirection(Location, dir);
                CurrentDirection = dir;
                

            }

        }

    }
}
