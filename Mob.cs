using Microsoft.Xna.Framework;
using SadConsole.Entities;

namespace youngones {
    public class Mob : Entity {
        public TileMap TileMap { get; set; }

        public Mob() : base(1,1) {
        }

        public Mob(TileMap tileMap) : base(1,1) {
            TileMap = tileMap;
        }

        public bool MoveTowards(Direction direction) {
            Point dir = direction.ToPoint();
            Point pos = new Point(Position.X + dir.X, Position.Y + dir.Y);
            return MoveTo(pos); 
        }

        public bool MoveTo(Point position) {
            if (TileMap.IsBlocked(position)) {
                return false;
            }
            Position = position;
            return true;
        }
    }
}
