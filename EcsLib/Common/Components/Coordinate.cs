using Microsoft.Xna.Framework;

namespace EcsLib.Common.Components;

public struct Coordinate(Point point)
{
    public Point Point = point;
}
