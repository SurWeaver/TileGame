using EcsLib.Drawing.Components;
using EcsLib.Extensions;
using Leopotam.EcsLite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EcsLib.Drawing.Systems;

/// <summary>
/// Добавляет незаполненные поля спрайта за пользователя, используя стандартные значения.
/// Заполняет SourceRectangle, поэтому система должна стоять после стандартных вычислений SourceRectangle.
/// </summary>
public class FillSpriteSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<SpriteRectangle> _rectanglePool;
    private EcsPool<SpriteColor> _colorPool;
    private EcsPool<Rotation> _rotationPool;
    private EcsPool<SpriteOrigin> _originPool;
    private EcsPool<Scale> _scalePool;
    private EcsPool<SpriteFlip> _flipPool;
    private EcsPool<SpriteLayer> _layerPool;
    private EcsPool<FillSpriteRequest> _requestPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Sprite>()
            .Inc<FillSpriteRequest>()
            .End();

        _rectanglePool = world.GetPool<SpriteRectangle>();
        _colorPool = world.GetPool<SpriteColor>();
        _rotationPool = world.GetPool<Rotation>();
        _originPool = world.GetPool<SpriteOrigin>();
        _scalePool = world.GetPool<Scale>();
        _flipPool = world.GetPool<SpriteFlip>();
        _layerPool = world.GetPool<SpriteLayer>();

        _requestPool = world.GetPool<FillSpriteRequest>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            _requestPool.Del(entity);

            if (!_rectanglePool.Has(entity))
                _rectanglePool.Add(entity, new(null));

            if (!_colorPool.Has(entity))
                _colorPool.Add(entity, new(Color.White));

            if (!_rotationPool.Has(entity))
                _rotationPool.Add(entity, new(0f));

            if (!_originPool.Has(entity))
                _originPool.Add(entity, new(Vector2.Zero));

            if (!_scalePool.Has(entity))
                _scalePool.Add(entity, new(Vector2.One));

            if (!_flipPool.Has(entity))
                _flipPool.Add(entity, new(SpriteEffects.None));

            if (!_layerPool.Has(entity))
                _layerPool.Add(entity, new(0f));
        }
    }
}
