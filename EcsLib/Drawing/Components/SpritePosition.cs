using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Components;

public struct SpritePosition(Vector2 position)
{
    public Vector2 Position = position;
}
