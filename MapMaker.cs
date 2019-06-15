
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace youngones {
    public class MapMaker {

        private Random random;

        public MapMaker(int seed) { 
            random = new Random(seed);
        }

        public TileMap MakeRooms(TileMap tileMap, int maxRooms, int minRoomSize, int maxRoomSize) {
            FillWithWalls(tileMap);

            // create a list of non-intersecting rooms:
            var rooms = new List<Rectangle>();
            Func<bool> tryToAddRoom = () => { 
                int roomWidth   = random.Next(minRoomSize, maxRoomSize);
                int roomHeight  = random.Next(minRoomSize, maxRoomSize);
                int roomX       = random.Next(1, tileMap.Width - roomWidth - 2);
                int roomY       = random.Next(1, tileMap.Height - roomHeight - 2);
                var newRoom     = new Rectangle(roomX, roomY, roomWidth, roomHeight);
                bool intersects = rooms.Any(room => newRoom.Intersects(room));
                if (!intersects) {
                    rooms.Add(newRoom);
                    return true;
                }
                return false;
            };
            var maxAttempts = 1000;
            while (rooms.Count < maxRooms) {
                tryToAddRoom();
                if (--maxAttempts < 0) {
                    maxRooms = rooms.Count;
                }
            }

            // create rooms from the list:
            foreach (var room in rooms) {
                CreateRoom(tileMap, room);
            }

            // add tunnels to connect rooms:
            for (int i=1; i<rooms.Count; i++) {
                var previous = rooms[i - 1].Center;
                var current = rooms[i].Center;
                if (random.Next(1,2) == 1) {
                    CreateHorizontalTunnel(tileMap, previous.X, current.X, previous.Y);
                    CreateVerticalTunnel(tileMap, previous.Y, current.Y, current.X);
                } else {
                    CreateVerticalTunnel(tileMap, previous.Y, current.Y, previous.X);
                    CreateHorizontalTunnel(tileMap, previous.X, current.X, current.Y);
                }
            }

            // done:
            return tileMap;
        }

        public void CreateHorizontalTunnel(TileMap tileMap, int startX, int endX, int y) {
            for (int x=Math.Min(startX, endX); x<=Math.Max(startX, endX); x++) {
                tileMap.SetTile(x, y, Tile.CreateFloor());
            }
        }
        public void CreateVerticalTunnel(TileMap tileMap, int startY, int endY, int x) {
            for (int y=Math.Min(startY, endY); y<=Math.Max(startY, endY); y++) {
                tileMap.SetTile(x, y, Tile.CreateFloor());
            }
        }

        public TileMap CreateRoom(TileMap tileMap, Rectangle room) {
            for (int x=room.Left+1; x<room.Right-1; x++) {
                for (int y = room.Top + 1; y < room.Bottom - 1; y++) {
                    tileMap.SetTile(x, y, Tile.CreateFloor());
                }
            }

            // put walls at perimeter:
            var perimeter = GetBorderCellLocations(tileMap, room);
            foreach (var position in perimeter) {
                tileMap.SetTile(position, Tile.CreateWall());
            }
            return tileMap;
        }

        public List<Point> GetBorderCellLocations(TileMap tileMap, Rectangle room) {
            //establish room boundaries
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            // build a list of room border cells using a series of
            // straight lines
            List<Point> borderCells = GetTileLocationsAlongLine(tileMap, xMin, yMin, xMax, yMin).ToList();
            borderCells.AddRange(GetTileLocationsAlongLine(tileMap, xMin, yMin, xMin, yMax));
            borderCells.AddRange(GetTileLocationsAlongLine(tileMap, xMin, yMax, xMax, yMax));
            borderCells.AddRange(GetTileLocationsAlongLine(tileMap, xMax, yMin, xMax, yMax));
            return borderCells;
        }

        // returns a collection of Points which represent
        // locations along a line
        public IEnumerable<Point> GetTileLocationsAlongLine(TileMap tileMap, int xOrigin, int yOrigin, int xDestination, int yDestination) {
            // prevent line from overflowing
            // boundaries of the map
            xOrigin      = ClampX(tileMap, xOrigin);
            yOrigin      = ClampY(tileMap, yOrigin);
            xDestination = ClampX(tileMap, xDestination);
            yDestination = ClampY(tileMap, yDestination);

            int dx = Math.Abs(xDestination - xOrigin);
            int dy = Math.Abs(yDestination - yOrigin);

            int sx = xOrigin < xDestination ? 1 : -1;
            int sy = yOrigin < yDestination ? 1 : -1;
            int err = dx - dy;

            while (true) {
                yield return new Point(xOrigin, yOrigin);
                if (xOrigin == xDestination && yOrigin == yDestination) {
                    break;
                }
                int e2 = 2 * err;
                if (e2 > -dy) {
                    err = err - dy;
                    xOrigin = xOrigin + sx;
                }
                if (e2 < dx) {
                    err = err + dx;
                    yOrigin = yOrigin + sy;
                }
            }
        }

        // sets X coordinate between right and left edges of map
        // to prevent any out-of-bounds errors
        private int ClampX(TileMap tileMap, int x) {
            if (x < 0) {
                x = 0;
            } else if (x > tileMap.Width - 1) {
                x = tileMap.Width - 1;
            }
            return x;
        }

        // sets Y coordinate between top and bottom edges of map
        // to prevent any out-of-bounds errors
        private int ClampY(TileMap tileMap, int y) {
            if (y < 0) {
                y = 0;
            } else if (y > tileMap.Height - 1) {
                y = tileMap.Height - 1;
            }
            return y;
        }

        public TileMap MakeRooms(int mapWidth, int mapHeight, int maxRooms, int minRoomSize, int maxRoomSize) {
            var tileMap = new TileMap(mapWidth, mapHeight);
            return MakeRooms(tileMap, maxRooms, minRoomSize, maxRoomSize);
        }

        public TileMap FillWithWalls(TileMap tileMap) {
            for (int y = 0; y < tileMap.Height; y++) {
                for (int x = 0; x < tileMap.Width; x++) {
                    tileMap.SetTile(x, y, Tile.CreateWall());
                }
            }
            return tileMap;
        }

        public TileMap MakeArena(TileMap tileMap) {
            for (int y = 0; y < tileMap.Height; y++) {
                for (int x = 0; x < tileMap.Width; x++) {
                    if (x == 0 || x == tileMap.Width - 1 || y == 0 || y == tileMap.Height - 1) {
                        tileMap.SetTile(x, y, Tile.CreateWall());
                    } else {
                        tileMap.SetTile(x, y, Tile.CreateFloor());
                    }
                }
            }
            return tileMap;
        }

        public TileMap MakeArena(int mapWidth, int mapHeight) { 
            var tileMap = new TileMap(mapWidth, mapHeight);
            for (int y = 0; y < mapHeight; y++) {
                for (int x = 0; x < mapWidth; x++) {
                    if (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1) {
                        tileMap.SetTile(x, y, Tile.CreateWall());
                    } else {
                        tileMap.SetTile(x, y, Tile.CreateFloor());
                    }
                }
            }
            return tileMap;
        }
    }
}
