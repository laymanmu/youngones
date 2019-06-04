using Microsoft.Xna.Framework;
using SadConsole;

namespace youngones {
    public class Tile : Cell {
        public Tile(Color fg, Color bg, int glyph, bool isBlocked=false, bool isBlockedLOS=false, string name="") : base(fg, bg, glyph) { 
            IsBlocked    = isBlocked;
            IsBlockedLOS = isBlockedLOS;
            Name         = name;
        }

        public string Name { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsBlockedLOS { get; set; }

        public static Tile CreateWall() {
            return new Tile(Color.DarkSlateBlue, Color.DarkOrchid, '#', true, true, "wall");
        }
        public static Tile CreateFloor() {
            return new Tile(Color.DarkSeaGreen, Color.Black, '.', false, false, "floor");
        }
        public static Tile CreateNull() {
            return new Tile(Color.DarkOliveGreen, Color.DarkRed, '?', false, false, "null");
        }
    }
}
