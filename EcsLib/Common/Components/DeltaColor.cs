using Microsoft.Xna.Framework;

namespace EcsLib.Common.Components;

public struct DeltaColor(Color color)
{
    public Color Color = color;
}
