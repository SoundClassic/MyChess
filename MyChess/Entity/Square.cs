using System.Collections;
using System.Collections.Generic;

namespace MyChess.Entity
{
    public struct Square
    {
        public static Square None = new Square(-1, -1);

        public int X { get; private set; }
        public int Y { get; private set; }
        public string Name => $"{'a' + X}{Y + 1}";

        public Square(int x, int y)
        {
            if (x >= 0 && x <= 8 &&
                y >= 0 && x <= 8)
            {
                X = x;
                Y = y;
            }
            else
            {
                this = None;
            }
        }

        public Square(string position)
        {
            if(position.Length == 2 &&
                position[0] >= 'a' && position[0] <= 'h' &&
                position[1] >= '1' && position[1] <= '8')
            {
                X = position[0] - 'a';
                Y = position[1] - '1';
            }
            else
            {
                this = None;
            }
        }

        public bool IsSquare() => this != None;

        public static bool operator ==(Square a, Square b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Square a, Square b) => !(a == b);

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
