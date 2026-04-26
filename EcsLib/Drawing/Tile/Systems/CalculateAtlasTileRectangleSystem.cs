using EcsLib.Drawing.Components;
using EcsLib.Drawing.Tiles;
using EcsLib.Extensions;
using Leopotam.EcsLite;
using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Tile.Systems;

public class CalculateSpriteRectangleSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<AtlasTile> _tilePool;
    private EcsPool<AtlasTileDelta> _deltaTilePool;
    private EcsPool<SpriteRectangle> _rectanglePool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<AtlasTile>()
            .Exc<SpriteRectangle>()
            .End();

        _tilePool = world.GetPool<AtlasTile>();
        _deltaTilePool = world.GetPool<AtlasTileDelta>();
        _rectanglePool = world.GetPool<SpriteRectangle>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var tile = ref _tilePool.Get(entity);

            var coordinate = tile.Coordinate;
            if (_deltaTilePool.Has(entity))
                coordinate += _deltaTilePool.Get(entity).Coordinate;

            var position = coordinate * tile.Size;

            _rectanglePool.Add(entity, new SpriteRectangle(new Rectangle(position, tile.Size)));
        }
    }
}
