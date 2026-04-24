using Core.Input;

namespace EcsLib.Input.Components;

public struct ActionMouseButton(MouseButton button)
{
    public MouseButton Button = button;
}
