using Microsoft.Xna.Framework;

namespace youngones {
    public class Game {
        public int Width { get; }
        public int Height { get; }

        private SadConsole.Console mainConsole;
        private SadConsole.Entities.Entity player;

        public Game(int width, int height) {
            Width  = width;
            Height = height;
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
            player = new SadConsole.Entities.Entity(1, 1);
            player.Animation.CurrentFrame[0].Glyph = '@';
            player.Animation.CurrentFrame[0].Foreground = Color.HotPink;
            player.Position = new Point(20, 10);
            mainConsole.Children.Add(player);
        }

        private void InitConsole() {
            mainConsole = new SadConsole.Console(Width, Height);
            //mainConsole.FillWithRandomGarbage();
            mainConsole.Fill(new Rectangle(3, 3, 23, 3), Color.Violet, Color.Gray, 0, 0);
            mainConsole.Print(4, 4, "hello");
            SadConsole.Global.CurrentScreen = mainConsole;
        }
    }
}
