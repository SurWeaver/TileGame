using Microsoft.Xna.Framework;

namespace EcsLib.Common.Components;

public struct Position(Vector2 vector)
{
    public Vector2 Vector = vector;
}
