using EcsLib.Common.Components;
using EcsLib.Drawing.Components;
using Leopotam.EcsLite;
using Microsoft.Xna.Framework.Graphics;

namespace EcsLib.Drawing.Systems;

public class DrawSpriteSystem(SpriteBatch spriteBatch)
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<Sprite> _spritePool;
    private EcsPool<Position> _positionPool;
    private EcsPool<SpriteRectangle> _rectanglePool;
    private EcsPool<SpriteColor> _colorPool;
    private EcsPool<Rotation> _rotationPool;
    private EcsPool<SpriteOrigin> _originPool;
    private EcsPool<Scale> _scalePool;
    private EcsPool<SpriteFlip> _flipPool;
    private EcsPool<SpriteLayer> _layerPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Sprite>()
            .Inc<Position>()
            .Inc<SpriteRectangle>()
            .Inc<SpriteColor>()
            .Inc<Rotation>()
            .Inc<SpriteOrigin>()
            .Inc<Scale>()
            .Inc<SpriteFlip>()
            .Inc<SpriteLayer>()
            .End();

        _spritePool = world.GetPool<Sprite>();
        _positionPool = world.GetPool<Position>();
        _rectanglePool = world.GetPool<SpriteRectangle>();
        _colorPool = world.GetPool<SpriteColor>();
        _rotationPool = world.GetPool<Rotation>();
        _originPool = world.GetPool<SpriteOrigin>();
        _scalePool = world.GetPool<Scale>();
        _flipPool = world.GetPool<SpriteFlip>();
        _layerPool = world.GetPool<SpriteLayer>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var texture = ref _spritePool.Get(entity).Texture;
            ref var position = ref _positionPool.Get(entity).Vector;
            ref var rectangle = ref _rectanglePool.Get(entity).Rectangle;
            ref var color = ref _colorPool.Get(entity).Color;
            ref var rotation = ref _rotationPool.Get(entity).Radians;
            ref var origin = ref _originPool.Get(entity).Vector;
            ref var scale = ref _scalePool.Get(entity).Vector;
            ref var effects = ref _flipPool.Get(entity).Effect;
            ref var layer = ref _layerPool.Get(entity).Depth;

            spriteBatch.Draw(texture, position, rectangle, color, rotation, origin, scale, effects, layer);
        }
    }
}
