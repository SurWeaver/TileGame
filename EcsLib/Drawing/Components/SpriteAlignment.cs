using EcsLib.Drawing.Enums;

namespace EcsLib.Drawing.Components;

public struct SpriteAlignment(Alignment horizontal, Alignment vertical)
{
    public Alignment Horizontal = horizontal;
    public Alignment Vertical = vertical;
}
