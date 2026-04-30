using EcsLib.Common.Components;
using EcsLib.Drawing.Components;
using Leopotam.EcsLite;

namespace EcsLib.Common.Systems;

public class CalculateParentColorSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<ParentEntity> _parentPool;
    private EcsPool<DeltaColor> _deltaColorPool;
    private EcsPool<SpriteColor> _colorPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<ParentEntity>()
            .Inc<DeltaColor>()
            .Inc<SpriteColor>()
            .End();

        _parentPool = _world.GetPool<ParentEntity>();
        _deltaColorPool = _world.GetPool<DeltaColor>();
        _colorPool = _world.GetPool<SpriteColor>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var parentEntity = ref _parentPool.Get(entity).Entity;
            if (!parentEntity.Unpack(_world, out int parentId))
                continue;

            var parentColor = _colorPool.Get(parentId).Color;
            ref var deltaColor = ref _deltaColorPool.Get(entity).Color;

            ref var finalColor = ref _colorPool.Get(entity).Color;
            finalColor = deltaColor * parentColor;
        }
    }
}
