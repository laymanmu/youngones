using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace youngones {
    public enum Direction {
        N, NE, E, SE, S, SW, W, NW
    }

    public static class DirectionExtensions {
        public static Point ToPoint(this Direction direction) {
            switch (direction) {
                case Direction.N:  return new Point(0,  -1);
                case Direction.NE: return new Point(1,  -1);
                case Direction.E:  return new Point(1,   0);
                case Direction.SE: return new Point(1,   1);
                case Direction.S:  return new Point(0,   1);
                case Direction.SW: return new Point(-1,  1);
                case Direction.W:  return new Point(-1,  0);
                case Direction.NW: return new Point(-1, -1);
                default: return new Point(0, 0);
            }
        }
    }
}
