using Core.Input;
using EcsLib.Extensions;
using EcsLib.Input.Components;
using Leopotam.EcsLite;

namespace EcsLib.Input.Systems;

public class ProcessGamepadActionSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<PlayerAction> _actionPool;
    private EcsPool<ActionGamepadButton> _buttonPool;
    private EcsPool<Happened> _happenedPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<PlayerAction>()
            .Inc<ActionGamepadButton>()
            .Exc<Happened>()
            .End();

        _actionPool = world.GetPool<PlayerAction>();
        _buttonPool = world.GetPool<ActionGamepadButton>();
        _happenedPool = world.GetPool<Happened>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var action = ref _actionPool.Get(entity).Action;
            ref var button = ref _buttonPool.Get(entity).Button;

            if (GamepadController.IsPressed(button))
            {
                _happenedPool.Add(entity, new Happened(now: GamepadController.IsJustPressed(button)));
            }
        }
    }
}
