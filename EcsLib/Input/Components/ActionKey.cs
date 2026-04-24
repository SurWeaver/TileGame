using Microsoft.Xna.Framework.Input;

namespace EcsLib.Input.Components;

public struct ActionKey(Keys key)
{
    public Keys Key = key;
}
