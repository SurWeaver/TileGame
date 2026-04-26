using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Components;

public struct SpriteColor(Color color)
{
    public Color Color = color;
}
