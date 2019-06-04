using Microsoft.Xna.Framework;

namespace youngones {
    public class Game {
        public int Width { get; }
        public int Height { get; }

        private SadConsole.Console mainConsole;
        private TileMap tileMap;
        private Mob player;

        public Game(int width, int height) {
            Width  = width;
            Height = height;
        }

        public void Start() { 
            SadConsole.Game.Create(Width, Height);
            SadConsole.Game.OnInitialize = () => {
                InitTileMap();
                InitConsole();
                InitPlayer();
            };
            SadConsole.Game.OnUpdate = Update;
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private void Update(GameTime time) {
            if (Keyboard.IsReleased(Keyboard.ActionName.ToggleFullScreen)) {
                SadConsole.Settings.ToggleFullScreen();
            }

            var tookTurn = false;
            var action   = Keyboard.GetPressedAction();

            switch (action) {
                case Keyboard.ActionName.MoveN:
                    tookTurn = player.MoveTowards(Direction.N);
                    break;
                case Keyboard.ActionName.MoveNE:
                    tookTurn = player.MoveTowards(Direction.NE);
                    break;
                case Keyboard.ActionName.MoveE:
                    tookTurn = player.MoveTowards(Direction.E);
                    break;
                case Keyboard.ActionName.MoveSE:
                    tookTurn = player.MoveTowards(Direction.SE);
                    break;
                case Keyboard.ActionName.MoveS:
                    tookTurn = player.MoveTowards(Direction.S);
                    break;
                case Keyboard.ActionName.MoveSW:
                    tookTurn = player.MoveTowards(Direction.SW);
                    break;
                case Keyboard.ActionName.MoveW:
                    tookTurn = player.MoveTowards(Direction.W);
                    break;
                case Keyboard.ActionName.MoveNW:
                    tookTurn = player.MoveTowards(Direction.NW);
                    break;
                case Keyboard.ActionName.Rest:
                    tookTurn = true;
                    break;
            }

            if (tookTurn) {
                Log("tick");
            }
        }

        private void InitTileMap() {
            tileMap = new TileMap(Width, Height);
            System.Console.WriteLine(tileMap.ToText());
        }


        private void InitPlayer() { 
            player = new Mob(tileMap);
            player.Animation.CurrentFrame[0].Glyph = '@';
            player.Animation.CurrentFrame[0].Foreground = Color.HotPink;
            player.Position = new Point(20, 10);
            mainConsole.Children.Add(player);
        }

        private void InitConsole() {
            //mainConsole = new SadConsole.Console(Width, Height);
            mainConsole = new SadConsole.ScrollingConsole(Width, Height, SadConsole.Global.FontDefault, new Rectangle(0, 0, Width, Height), tileMap.Tiles);
            SadConsole.Global.CurrentScreen = mainConsole;

        }

        public void Log(string message) {
            System.Console.WriteLine(message);
        }
    }
}
