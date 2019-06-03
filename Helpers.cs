using Microsoft.Xna.Framework;

namespace youngones {
    public static class Helpers {

        /// <summary>
        /// Returns an "identity" like point (all values are either 0 or 1) to represent a direction rather than a distance.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Point - represents a direction. all values (X,Y) are guaranteed to be either 0 or 1.</returns>
        public static Point ToDirection(Point point) {
            var d = new Point();
            if (point.X > 0) {
                d.X = 1;
            } else if (point.X < 0) {
                d.X = -1;
            }
            if (point.Y > 0) {
                d.Y = 1;
            } else if (point.Y < 0) {
                d.Y = -1;
            }
            return d;
        }
    }
}
