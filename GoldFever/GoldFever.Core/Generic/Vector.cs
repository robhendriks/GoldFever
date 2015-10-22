using System;

namespace GoldFever.Core.Generic
{
    public struct Vector
    {
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

        public override string ToString()
        {
            return $"{{X:{X} Y:{Y}}}";
        }
    }
}
