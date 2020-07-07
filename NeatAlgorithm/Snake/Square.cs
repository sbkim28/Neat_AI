using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatAlgorithm.Snake
{
    public class Square
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Square()
        {
        }

        public Square(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            Square square = obj as Square;
            return square != null &&
                   X == square.X &&
                   Y == square.Y;
        }

        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }
    }
}
