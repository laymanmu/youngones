using Microsoft.Xna.Framework;
using System;

namespace youngones {
    public static class Program {
        static void Main() {
            SadConsole.Game.Create(80, 25);
            SadConsole.Game.OnInitialize = Init;
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        static void Init() {
            var console = new SadConsole.Console(80, 25);
            console.FillWithRandomGarbage();
            console.Fill(new Rectangle(3, 3, 23, 3), Color.Violet, Color.Black, 0, 0);
            console.Print(4, 4, "hello");
            SadConsole.Global.CurrentScreen = console;
        }
    }
}
