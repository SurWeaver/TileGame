using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Tiles;

public struct AtlasTileDelta(Point coordinate)
{
    public Point Coordinate = coordinate;
}
