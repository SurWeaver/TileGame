using Microsoft.Xna.Framework.Input;

namespace EcsLib.Input.Components;

public struct ActionGamepadButton(Buttons button)
{
    public Buttons Button = button;
}
