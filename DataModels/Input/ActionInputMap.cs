using System.Collections.Generic;
using Core;
using Core.Input;
using Microsoft.Xna.Framework.Input;

namespace DataModels.Input;

public class ActionInputMap
{
    public ActionType Action;

    public List<Keys> KeyboardKeys;
    public List<Buttons> GamepadButtons;
    public List<MouseButton> MouseButtons;
}
