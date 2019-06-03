using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace youngones {
    public class Keyboard {

        public enum ActionName { None, ToggleFullScreen, MoveN, MoveS, MoveE, MoveW, MoveNE, MoveSE, MoveSW, MoveNW, Rest };

        public static readonly Dictionary<ActionName,Keys[]> Controls = new Dictionary<ActionName, Keys[]> {
            {ActionName.ToggleFullScreen,  new Keys[]{Keys.F5 }},

            {ActionName.MoveN, new Keys[]{Keys.NumPad8,Keys.Up}},
            {ActionName.MoveS, new Keys[]{Keys.NumPad2,Keys.Down}},
            {ActionName.MoveE, new Keys[]{Keys.NumPad6,Keys.Right}},
            {ActionName.MoveW, new Keys[]{Keys.NumPad4,Keys.Left}},

            {ActionName.MoveNE, new Keys[]{Keys.NumPad9}},
            {ActionName.MoveSE, new Keys[]{Keys.NumPad3}},
            {ActionName.MoveSW, new Keys[]{Keys.NumPad1}},
            {ActionName.MoveNW, new Keys[]{Keys.NumPad7}},

            {ActionName.Rest, new Keys[]{Keys.NumPad5}},
        };

        public static ActionName GetActionName(Keys key) {
            foreach (var name in Controls.Keys) {
                foreach (var control in Controls[name]) {
                    if (key == control) {
                        return name;
                    }
                }
            }
            return ActionName.None;
        }

        public static bool IsReleased(ActionName actionName) {
            foreach (var key in Controls[actionName]) {
                if (SadConsole.Global.KeyboardState.IsKeyReleased(key)) {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPressed(ActionName actionName) {
            foreach (var key in Controls[actionName]) {
                if (SadConsole.Global.KeyboardState.IsKeyPressed(key)) {
                    return true;
                }
            }
            return false;
        }

        public static List<ActionName> GetPressedActions() {
            List<ActionName> pressedActions = new List<ActionName>();
            foreach (var actionName in Controls.Keys) {
                if (IsPressed(actionName)) {
                    pressedActions.Add(actionName);
                }
            }
            return pressedActions;
        }

        public static ActionName GetPressedAction() {
            var pressedActions = GetPressedActions();
            if (pressedActions.Count > 0) {
                return pressedActions[0];
            }
            return ActionName.None;
        }
    }
}
