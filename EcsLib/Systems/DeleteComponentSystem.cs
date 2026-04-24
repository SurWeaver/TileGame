using Leopotam.EcsLite;

namespace EcsLib.Systems;

public class DeleteComponentSystem<TComponent>
    : IEcsInitSystem, IEcsRunSystem where TComponent : struct
{
    private EcsFilter _filter;
    private EcsPool<TComponent> _pool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<TComponent>()
            .End();

        _pool = world.GetPool<TComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            _pool.Del(entity);
        }
    }
}
