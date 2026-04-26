using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Tiles;

public struct AtlasTile(Point coordinate, Point size)
{
    public Point Coordinate = coordinate;
    public Point Size = size;
}
