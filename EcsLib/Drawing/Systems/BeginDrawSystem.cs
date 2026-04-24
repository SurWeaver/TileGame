using Leopotam.EcsLite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EcsLib.Drawing.Systems;

public class BeginDrawSystem(SpriteBatch spriteBatch, SpriteSortMode spriteSortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null,
        RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null)
    : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        spriteBatch.Begin(spriteSortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
    }
}