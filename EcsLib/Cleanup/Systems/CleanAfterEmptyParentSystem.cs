using EcsLib.Common.Components;
using Leopotam.EcsLite;

namespace EcsLib.Cleanup.Systems;

/// <summary>
/// Удаляет пустой компонент <see cref="ParentEntity"/>, если на сущности есть <see cref="StayAfterParent"/>.
/// В противном случае удаляет всю сущность.
/// </summary>
public class CleanAfterEmptyParentSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<ParentEntity> _parentPool;
    private EcsPool<StayAfterParent> _stayAfterParentPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<ParentEntity>()
            .End();

        _parentPool = _world.GetPool<ParentEntity>();
        _stayAfterParentPool = _world.GetPool<StayAfterParent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var parent = ref _parentPool.Get(entity);
            if (!parent.Entity.Unpack(_world, out _))
            {
                if (_stayAfterParentPool.Has(entity))
                    _parentPool.Del(entity);
                else
                    _world.DelEntity(entity);
            }
        }
    }
}
