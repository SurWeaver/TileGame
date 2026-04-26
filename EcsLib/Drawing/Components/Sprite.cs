using System;
using Microsoft.Xna.Framework.Graphics;

namespace EcsLib.Drawing.Components;

public struct Sprite(Texture2D texture)
{
    public Texture2D Texture = texture;
}
