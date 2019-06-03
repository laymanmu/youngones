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
            tileMap = new TileMap(10, 10);
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
            if (Keyboard.IsPressed(Keyboard.ActionName.MoveN)) {
                player.Position += new Point(0, -1);
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
    }
}
