
namespace youngones {
    public class Player : Mob {

        public Player(TileMap tileMap) : base(tileMap) {
        }

        public Player() : base() {
        }

        public bool TakeAction(Keyboard.ActionName action) {
            var tookTurn = false;
            switch (action) {
                case Keyboard.ActionName.MoveN:
                    tookTurn = MoveTowards(Direction.N);
                    break;
                case Keyboard.ActionName.MoveNE:
                    tookTurn = MoveTowards(Direction.NE);
                    break;
                case Keyboard.ActionName.MoveE:
                    tookTurn = MoveTowards(Direction.E);
                    break;
                case Keyboard.ActionName.MoveSE:
                    tookTurn = MoveTowards(Direction.SE);
                    break;
                case Keyboard.ActionName.MoveS:
                    tookTurn = MoveTowards(Direction.S);
                    break;
                case Keyboard.ActionName.MoveSW:
                    tookTurn = MoveTowards(Direction.SW);
                    break;
                case Keyboard.ActionName.MoveW:
                    tookTurn = MoveTowards(Direction.W);
                    break;
                case Keyboard.ActionName.MoveNW:
                    tookTurn = MoveTowards(Direction.NW);
                    break;
                case Keyboard.ActionName.Rest:
                    tookTurn = true;
                    break;
            }
            return tookTurn;
        }

    }
}
