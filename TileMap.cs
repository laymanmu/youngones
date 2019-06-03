using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace youngones {
    public class TileMap : ITileMap {

        private ITile nullTile;
        private ITile floorTile;
        private ITile wallTile;

        public TileMap(int width, int height) {
            Width     = width;
            Height    = height;
            Tiles     = new ITile[width * height];
            Mobs      = new List<IMob>();
            nullTile  = new Tile("null",  "?", true);
            floorTile = new Tile("floor", ".", false);
            wallTile  = new Tile("wall",  "#", true);
            for (int y=0; y<Height; y++) {
                for (int x=0; x<Width; x++) {
                    if (x==0 || x==Width-1 || y==0 || y==Height-1) {
                        SetTile(x, y, wallTile);
                    } else {
                        SetTile(x, y, floorTile);
                    }
                }
            }
        }

        public List<IMob> Mobs { get; }
        public ITile[] Tiles { get; }
        public int Width { get; }
        public int Height { get; }

        public ITile GetTile(int x, int y) {
            if (IsInBounds(x, y)) {
                return Tiles[x + y * x];
            } else {
                return nullTile;
            }
        }

        public ITile GetTile(Point position) {
            return GetTile(position.X, position.Y);
        }

        public void SetTile(int x, int y, ITile tile) {
            if (IsInBounds(x, y)) {
                Tiles[x + y * x] = tile;
            }
        }

        public bool IsBlocked(Point position) {
            return GetTile(position).IsBlocked;
        }

        public bool IsBlocked(int x, int y) {
            return GetTile(x, y).IsBlocked;
        }

        public bool IsInBounds(int x, int y) {
            return x > -1 && x < Width && y > -1 && y < Height;
        }
    }
}
