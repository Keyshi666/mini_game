using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_minigame
{
    class Coords
    {
        public Coords()
            : this(0, 0)
        { }

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString() => $"({X},{Y})";

        public override bool Equals(Object obj)
        {
            Coords coords = obj as Coords;
            return (X.CompareTo(coords.X) == 0 && Y.CompareTo(coords.Y) == 0);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return X.GetHashCode() + Y.GetHashCode();
            }
        }
    }
}
