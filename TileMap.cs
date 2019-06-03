using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace youngones {
    public class TileMap { 

        public List<Mob> Mobs { get; }
        public int Width { get; }
        public int Height { get; }

        private Tile[] tiles;
        private Tile   nullTile;
        private Tile   floorTile;
        private Tile   wallTile;

        public TileMap(int width, int height) {
            Width     = width;
            Height    = height;
            Mobs      = new List<Mob>();
            tiles     = new Tile[width * height];
            nullTile  = new Tile("null",  "?", true);
            floorTile = new Tile("floor", ".", false);
            wallTile  = new Tile("wall",  "#", true);
            for (int y=0; y<Height; y++) {
                for (int x=0; x<Width; x++) {
                    if (x==0 || x==Width-1 || y==0 || y==Height-1) {
                        System.Console.WriteLine("wall at " + x + "," + y);
                        SetTile(x, y, wallTile);
                    } else {
                        SetTile(x, y, floorTile);
                    }
                }
            }
        }

        public Tile GetTile(int x, int y) {
            if (IsInBounds(x, y)) {
                return tiles[x+y*Width];
            } else {
                return nullTile;
            }
        }
        public Tile GetTile(Point position) {
            return GetTile(position.X, position.Y);
        }

        public void SetTile(int x, int y, Tile tile) {
            if (IsInBounds(x, y)) {
                tiles[x+y*Width] = tile;
            }
        }
        public void SetTile(Point position, Tile tile) {
            SetTile(position.X, position.Y, tile);
        }

        public bool IsBlocked(int x, int y) {
            return GetTile(x, y).IsBlocked;
        }
        public bool IsBlocked(Point position) {
            return GetTile(position.X, position.Y).IsBlocked;
        }

        public bool IsInBounds(int x, int y) {
            return x > -1 && x < Width && y > -1 && y < Height;
        }
        public bool IsInBounds(Point position) {
            return IsInBounds(position.X, position.Y);
        }
    }
}
