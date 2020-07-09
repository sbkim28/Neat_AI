using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Pacman
{
    public abstract class Instance
    {
        public Location Location { get; set; }
        public Direction CurrentDirection { get; set; }

        public int MoveTime { get; set; } // 1 for slow Ghost, 2 for normal speed, 4 for eaten
        public int Time { get; set; }
        public int State { get; set; } // 1 for normal, 2 for scattered.
        public bool Killed { get; set; }

        public Instance()
        {
            State = 1;

            Killed = false;
        }

        public abstract void Move(Direction[] m);

        public static Location MoveDirection(Location location, Direction direction)
        {
            Location l;
            switch (direction)
            {
                case Direction.DOWN:
                    l = new Location(location.X, location.Y + 1);
                    break;
                case Direction.UP:
                    l = new Location(location.X, location.Y - 1);
                    break;
                case Direction.LEFT:
                    l = new Location(location.X - 1, location.Y);
                    break;
                case Direction.RIGHT:
                    l = new Location(location.X + 1, location.Y);
                    break;
                default:
                    l = location;
                    break;
            }
            return l;
        }
        public static Location MoveDirection(Location location, Direction direction, int distance)
        {
            Location l;
            switch (direction)
            {
                case Direction.DOWN:
                    l = new Location(location.X, location.Y + distance);
                    break;
                case Direction.UP:
                    l = new Location(location.X, location.Y - distance);
                    break;
                case Direction.LEFT:
                    l = new Location(location.X - distance, location.Y);
                    break;
                case Direction.RIGHT:
                    l = new Location(location.X + distance, location.Y);
                    break;
                default:
                    l = location;
                    break;
            }
            return l;
        }
    }
}
