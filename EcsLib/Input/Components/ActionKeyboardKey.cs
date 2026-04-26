using Microsoft.Xna.Framework.Input;

namespace EcsLib.Input.Components;

public struct ActionKeyboardKey(Keys key)
{
    public Keys Key = key;
}
