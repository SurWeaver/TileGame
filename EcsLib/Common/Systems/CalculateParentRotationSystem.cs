using EcsLib.Common.Components;
using EcsLib.Drawing.Components;
using Leopotam.EcsLite;

namespace EcsLib.Common.Systems;

public class CalculateParentRotationSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<DeltaRotation> _deltaRotationPool;
    private EcsPool<Rotation> _rotationPool;
    private EcsPool<ParentEntity> _parentPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<ParentEntity>()
            .Inc<Rotation>()
            .Inc<DeltaRotation>()
            .End();

        _deltaRotationPool = _world.GetPool<DeltaRotation>();
        _rotationPool = _world.GetPool<Rotation>();
        _parentPool = _world.GetPool<ParentEntity>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var packedParent = ref _parentPool.Get(entity).Entity;
            if (!packedParent.Unpack(_world, out int parentId))
                continue;

            ref var parentRotation = ref _rotationPool.Get(parentId).Radians;
            ref var deltaRotation = ref _deltaRotationPool.Get(entity).Radians;

            ref var rotation = ref _rotationPool.Get(entity).Radians;
            rotation = parentRotation + deltaRotation;
        }
    }
}
