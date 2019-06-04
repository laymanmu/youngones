﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace youngones {
    public class TileMap { 

        public List<Mob> Mobs { get; }
        public int Width { get; }
        public int Height { get; }

        private Tile   nullTile;

        public TileMap(int width, int height) {
            Width    = width;
            Height   = height;
            Mobs     = new List<Mob>();
            Tiles    = new Tile[width * height];
            nullTile = Tile.CreateNull();
            for (int y=0; y<Height; y++) {
                for (int x=0; x<Width; x++) {
                    if (x==0 || x==Width-1 || y==0 || y==Height-1) {
                        SetTile(x, y, Tile.CreateWall());
                    } else {
                        SetTile(x, y, Tile.CreateFloor());
                    }
                }
            }
        }

        public Tile[] Tiles { get; }

        public string ToText() {
            var sb = new StringBuilder();
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    sb.Append(Convert.ToChar(GetTile(x, y).Glyph));
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public Tile GetTile(int x, int y) {
            if (IsInBounds(x, y)) {
                return Tiles[x+y*Width];
            } else {
                return nullTile;
            }
        }
        public Tile GetTile(Point position) {
            return GetTile(position.X, position.Y);
        }

        public void SetTile(int x, int y, Tile tile) {
            if (IsInBounds(x, y)) {
                Tiles[x+y*Width] = tile;
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
