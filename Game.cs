using Microsoft.Xna.Framework;
using SadConsole;
using System;

namespace youngones {
    public class Game {
        public int Width { get; }
        public int Height { get; }

        private SadConsole.ScrollingConsole mainConsole;
        private SadConsole.Font mainFont;
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
            InitConsoles();
            InitPlayer();
        }

        private void Update(GameTime time) {
            if (Keyboard.IsReleased(Keyboard.ActionName.ToggleFullScreen)) {
                SadConsole.Settings.ToggleFullScreen();
            }
            var action   = Keyboard.GetPressedAction();
            var tookTurn = player.TakeAction(action);
            if (tookTurn) {
                Log($"tick | player at: {player.Position}");
            }
            mainConsole.CenterViewPortOnPoint(player.Position);
        }

        private void InitConsoles() {
            viewPort    = new Rectangle(0, 0, 40, 20);
            mainFont    = SadConsole.Global.FontDefault.Master.GetFont(Font.FontSizes.One);
            mainConsole = new SadConsole.ScrollingConsole(tileMap.Width, tileMap.Height, mainFont, viewPort, tileMap.Tiles);
            SadConsole.Global.CurrentScreen = mainConsole;
        }

        private void InitTileMap() {
            var seed = (int)DateTime.Now.ToBinary();
            tileMap  = new MapMaker(seed).MakeRooms(100, 100, 100, 8, 14);
        }

        private void InitPlayer() { 
            player = new Player(tileMap);
            player.Animation.CurrentFrame[0].Glyph = '@';
            player.Animation.CurrentFrame[0].Foreground = Color.HotPink;
            player.Animation.CurrentFrame[0].Background = Color.Transparent;
            player.Position = tileMap.GetRandomUnblockedPosition(); 
            player.Components.Add(new SadConsole.Components.EntityViewSyncComponent());
            player.Font = mainFont;
            mainConsole.Children.Add(player);
        }

        public void Log(string message) {
            System.Console.WriteLine(message);
        }
    }
}
