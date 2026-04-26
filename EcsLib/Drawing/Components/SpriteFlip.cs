using Microsoft.Xna.Framework.Graphics;

namespace EcsLib.Drawing.Components;

public struct SpriteFlip(SpriteEffects effect = SpriteEffects.None)
{
    public SpriteEffects Effect = effect;
}
