using Microsoft.Xna.Framework;

namespace youngones {

    public interface IMob {
        bool MoveTowards(Point direction);
        bool MoveTo(Point position);
    }

    public interface ITile {
        string Name { get; }
        string Glyph { get; }
        bool IsBlocked { get; }
    }

    public interface ITileMap {
        int Width { get; }
        int Height { get; }
        ITile GetTile(Point position);
        ITile GetTile(int x, int y);
        bool IsBlocked(Point position);
        bool IsBlocked(int x, int y);
    }
}
