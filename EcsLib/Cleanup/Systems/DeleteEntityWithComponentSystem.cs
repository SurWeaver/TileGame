using Leopotam.EcsLite;

namespace EcsLib.Cleanup.Systems;

public class DeleteEntityWithComponentSystem<TComponent>
    : IEcsInitSystem, IEcsRunSystem where TComponent : struct
{
    private EcsWorld _world;
    private EcsFilter _filter;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<TComponent>()
            .End();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
            _world.DelEntity(entity);
    }
}
