using System;

namespace youngones {
    public class Tile {
        public Tile(String name, String glyph, bool isBlocked) {
            Name      = name;
            Glyph     = glyph;
            IsBlocked = isBlocked;
        }

        public string Glyph { get; }

        public string Name { get; }

        public bool IsBlocked { get; }

    }
}
