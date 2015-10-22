using System;

namespace GoldFever.Core.Generic
{
    public struct Vector
    {
        private static Vector emptyVector = new Vector(-1, -1);

        public static Vector Empty
        {
            get { return emptyVector; }
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector(int value)
        {
            X = Y = value;
        }

        public bool Equals(Vector other)
        {
            return (X == other.X) && (Y == other.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector)
                return Equals((Vector)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public Vector Facing(Direction direction)
        {
            switch(direction)
            {
                default:
                    return Empty;
                case Direction.North:
                    return new Vector(X, Y - 1);
                case Direction.East:
                    return new Vector(X + 1, Y);
                case Direction.South:
                    return new Vector(X, Y + 1);
                case Direction.West:
                    return new Vector(X - 1, Y);
            }
        }
    }
}
