using Microsoft.Xna.Framework;
using SadConsole.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace youngones {
    public class Mob : Entity {

        private TileMap tileMap;

        public Mob(TileMap tileMap) : base(1,1) {
            this.tileMap = tileMap;
        }

        public bool MoveTowards(Direction direction) {
            Point dir = direction.ToPoint();
            Point pos = new Point(Position.X + dir.X, Position.Y + dir.Y);
            return MoveTo(pos); 
        }

        public bool MoveTo(Point position) {
            if (tileMap.IsBlocked(position)) {
                return false;
            }
            Position = position;
            return true;
        }
    }
}
