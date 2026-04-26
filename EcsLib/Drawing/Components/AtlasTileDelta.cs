using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Components;

public struct AtlasTileDelta(Point coordinate)
{
    public Point Coordinate = coordinate;
}
