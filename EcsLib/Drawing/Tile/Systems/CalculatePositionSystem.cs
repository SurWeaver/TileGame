using EcsLib.Common.Components;
using EcsLib.Extensions;
using Leopotam.EcsLite;
using Microsoft.Xna.Framework;

namespace EcsLib.Drawing.Tile.Systems;

public class CalculatePositionSystem(Point tileSize)
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<Coordinate> _coordinatePool;
    private EcsPool<Position> _positionPool;

    private readonly Vector2 _tileOffset = tileSize.ToVector2() * 0.5f;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Coordinate>()
            .Exc<Position>()
            .End();

        _coordinatePool = world.GetPool<Coordinate>();
        _positionPool = world.GetPool<Position>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var coordinate = ref _coordinatePool.Get(entity).Point;

            var coordinatePosition = (coordinate * tileSize).ToVector2() + _tileOffset;

            _positionPool.Add(entity, new Position(coordinatePosition));
        }
    }
}
