using EcsLib.Common.Components;
using EcsLib.Drawing.Components;
using Leopotam.EcsLite;
using Microsoft.Xna.Framework;

namespace EcsLib.Common.Systems;

public class CalculateParentPositionSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<DeltaPosition> _deltaPosPool;
    private EcsPool<ParentEntity> _parentPool;
    private EcsPool<Position> _positionPool;
    private EcsPool<Rotation> _rotationPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<ParentEntity>()
            .Inc<Position>()
            .Inc<DeltaPosition>()
            .End();

        _deltaPosPool = _world.GetPool<DeltaPosition>();
        _parentPool = _world.GetPool<ParentEntity>();
        _positionPool = _world.GetPool<Position>();
        _rotationPool = _world.GetPool<Rotation>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var packedParent = ref _parentPool.Get(entity).Entity;
            if (!packedParent.Unpack(_world, out int parentId))
                continue;

            ref var deltaPosition = ref _deltaPosPool.Get(entity).Vector;

            Vector2 finalDeltaPosition = deltaPosition;
            if (_rotationPool.Has(parentId))
            {
                var parentRotation = _rotationPool.Get(parentId).Radians;
                finalDeltaPosition = Vector2.Rotate(deltaPosition, parentRotation);
            }

            ref var parentPosition = ref _positionPool.Get(parentId).Vector;
            ref var entityPosition = ref _positionPool.Get(entity);
            entityPosition.Vector = parentPosition + finalDeltaPosition;
        }
    }
}
