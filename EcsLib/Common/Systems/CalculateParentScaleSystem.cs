using EcsLib.Common.Components;
using EcsLib.Drawing.Components;
using Leopotam.EcsLite;

namespace EcsLib.Common.Systems;

public class CalculateParentScaleSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<ParentEntity> _parentPool;
    private EcsPool<Scale> _scalePool;
    private EcsPool<DeltaScale> _deltaScalePool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<ParentEntity>()
            .Inc<Scale>()
            .Inc<DeltaScale>()
            .End();

        _parentPool = _world.GetPool<ParentEntity>();
        _scalePool = _world.GetPool<Scale>();
        _deltaScalePool = _world.GetPool<DeltaScale>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var packedParent = ref _parentPool.Get(entity).Entity;
            if (!packedParent.Unpack(_world, out int parentId))
                continue;

            ref var parentScale = ref _scalePool.Get(parentId).Vector;
            ref var deltaScale = ref _deltaScalePool.Get(entity).Vector;

            _scalePool.Get(entity).Vector = parentScale * deltaScale;
        }
    }
}
