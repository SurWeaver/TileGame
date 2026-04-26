using Microsoft.Xna.Framework;

namespace EcsLib.Common.Components;

public struct DeltaPosition(Vector2 position)
{
    public Vector2 Vector = position;
}
