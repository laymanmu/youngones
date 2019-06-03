using Microsoft.Xna.Framework;
using SadConsole.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace youngones {
    public class Mob : Entity, IMob {

        private ITileMap tileMap;

        public Mob(ITileMap tileMap) : base(1,1) {
            this.tileMap = tileMap;
        }

        public bool MoveTowards(Point direction) {
            Point dir = Helpers.ToDirection(direction);
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
