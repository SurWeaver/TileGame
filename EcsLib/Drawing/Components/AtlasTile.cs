using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Components;

public struct AtlasTile(Point coordinate, Point size)
{
    public Point Coordinate = coordinate;
    public Point Size = size;
}
