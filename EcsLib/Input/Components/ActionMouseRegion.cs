using Microsoft.Xna.Framework;

namespace EcsLib.Input.Components;

public struct ActionMouseRegion(Rectangle rectangle)
{
    public Rectangle Rectangle = rectangle;
}
