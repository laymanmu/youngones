using Microsoft.Xna.Framework;
using SadConsole;

namespace youngones {
    public class Game {
        public int Width { get; }
        public int Height { get; }

        private SadConsole.ScrollingConsole mainConsole;
        private TileMap tileMap;
        private Player  player;
        private Rectangle viewPort;

        public Game(int width, int height) {
            Width  = width;
            Height = height;
        }

        public void Start() { 
            SadConsole.Game.Create(Width, Height);
            SadConsole.Game.OnInitialize = Initialize;
            SadConsole.Game.OnUpdate     = Update;
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private void Initialize() {
            InitTileMap();
            InitConsole();
            InitPlayer();
        }

        private void Update(GameTime time) {
            if (Keyboard.IsReleased(Keyboard.ActionName.ToggleFullScreen)) {
                SadConsole.Settings.ToggleFullScreen();
            }

            var action   = Keyboard.GetPressedAction();
            var tookTurn = player.TakeAction(action);

            if (tookTurn) {
                Log("tick");
            }
            mainConsole.CenterViewPortOnPoint(player.Position);
        }

        private void InitTileMap() {
            tileMap = new TileMap(Width*2, Height*2);
            System.Console.WriteLine(tileMap.ToText());
        }

        private void InitConsole() {
            viewPort    = new Rectangle(0, 0, Width, Height);
            mainConsole = new SadConsole.ScrollingConsole(tileMap.Width, tileMap.Height, SadConsole.Global.FontDefault, viewPort, tileMap.Tiles);
            SadConsole.Global.CurrentScreen = mainConsole;
        }

        private void InitPlayer() { 
            player = new Player(tileMap);
            player.Animation.CurrentFrame[0].Glyph = '@';
            player.Animation.CurrentFrame[0].Foreground = Color.HotPink;
            player.Animation.CurrentFrame[0].Background = Color.Transparent;
            player.Position = new Point(3, 3);
            player.Components.Add(new SadConsole.Components.EntityViewSyncComponent());
            mainConsole.Children.Add(player);
        }

        public void Log(string message) {
            System.Console.WriteLine(message);
        }
    }
}
