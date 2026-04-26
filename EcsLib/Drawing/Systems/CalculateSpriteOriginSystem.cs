using EcsLib.Drawing.Components;
using EcsLib.Extensions;
using Leopotam.EcsLite;
using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Systems;

public class CalculateSpriteOriginSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<Sprite> _spritePool;
    private EcsPool<AtlasTile> _tilePool;
    private EcsPool<SpriteAlignment> _alignmentPool;
    private EcsPool<SpriteOrigin> _originPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Sprite>()
            .Inc<SpriteAlignment>()
            .Exc<SpriteOrigin>()
            .End();

        _spritePool = world.GetPool<Sprite>();
        _tilePool = world.GetPool<AtlasTile>();
        _alignmentPool = world.GetPool<SpriteAlignment>();
        _originPool = world.GetPool<SpriteOrigin>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var texture = ref _spritePool.Get(entity).Texture;
            ref var alignment = ref _alignmentPool.Get(entity);
            Point spriteSize = _tilePool.Has(entity)
                ? _tilePool.Get(entity).Size
                : texture.Bounds.Size;

            var origin = new Vector2(
                spriteSize.X * ((float)alignment.Horizontal) * 0.5f,
                spriteSize.Y * ((float)alignment.Vertical) * 0.5f
            );

            _originPool.Add(entity, new SpriteOrigin(origin));
        }
    }
}
