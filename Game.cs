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
            tileMap = new TileMap(width, height);

            for (int y=0; y<height; y++) {
                string line = "";
                for (int x=0; x<width; x++) {
                    line += tileMap.GetTile(x, y).Glyph;
                }
                System.Console.WriteLine(line);
            }
        }

        public void Start() { 
            SadConsole.Game.Create(Width, Height);
            SadConsole.Game.OnInitialize = () => {
                InitConsole();
                InitEntities();
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
                    player.MoveTowards(Direction.N);
                    tookTurn = true;
                    break;
                case Keyboard.ActionName.MoveNE:
                    player.MoveTowards(Direction.NE);
                    tookTurn = true;
                    break;
                case Keyboard.ActionName.MoveE:
                    player.MoveTowards(Direction.E);
                    tookTurn = true;
                    break;
                case Keyboard.ActionName.MoveSE:
                    player.MoveTowards(Direction.SE);
                    tookTurn = true;
                    break;
                case Keyboard.ActionName.MoveS:
                    player.MoveTowards(Direction.S);
                    tookTurn = true;
                    break;
                case Keyboard.ActionName.MoveSW:
                    player.MoveTowards(Direction.SW);
                    tookTurn = true;
                    break;
                case Keyboard.ActionName.MoveW:
                    player.MoveTowards(Direction.W);
                    tookTurn = true;
                    break;
                case Keyboard.ActionName.MoveNW:
                    player.MoveTowards(Direction.NW);
                    tookTurn = true;
                    break;
                case Keyboard.ActionName.Rest:
                    tookTurn = true;
                    break;
            }

            if (tookTurn) {
                Log("tick");
            }
        }

        private void InitEntities() {
            player = new Mob(tileMap);
            player.Animation.CurrentFrame[0].Glyph = '@';
            player.Animation.CurrentFrame[0].Foreground = Color.HotPink;
            player.Position = new Point(20, 10);
            mainConsole.Children.Add(player);
        }

        private void InitConsole() {
            mainConsole = new SadConsole.Console(Width, Height);
            SadConsole.Global.CurrentScreen = mainConsole;
        }

        public void Log(string message) {
            System.Console.WriteLine(message);
        }
    }
}
