using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Components;

public struct SpriteRectangle(Rectangle? sourceRectangle)
{
    public Rectangle? Rectangle = sourceRectangle;
}
