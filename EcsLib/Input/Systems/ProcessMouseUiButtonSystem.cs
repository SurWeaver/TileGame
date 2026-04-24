using Core.Input;
using EcsLib.Extensions;
using EcsLib.Input.Components;
using Leopotam.EcsLite;

namespace EcsLib.Input.Systems;

public class ProcessMouseUiButtonSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<ActionMouseButton> _buttonPool;
    private EcsPool<ActionMouseRegion> _regionPool;
    private EcsPool<Happened> _happenedPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<ActionMouseButton>()
            .Inc<ActionMouseRegion>()
            .Exc<Happened>()
            .End();

        _buttonPool = world.GetPool<ActionMouseButton>();
        _regionPool = world.GetPool<ActionMouseRegion>();
        _happenedPool = world.GetPool<Happened>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var button = ref _buttonPool.Get(entity).Button;
            ref var region = ref _regionPool.Get(entity).Rectangle;

            if (MouseController.IsJustReleased(button) && region.Contains(MouseController.Position))
            {
                _happenedPool.Add(entity, new Happened(true));
            }
        }
    }
}
