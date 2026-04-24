using Core.Input;
using EcsLib.Extensions;
using EcsLib.Input.Components;
using Leopotam.EcsLite;

namespace EcsLib.Input.Systems;

public class ProcessKeyboardActionSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<PlayerAction> _actionPool;
    private EcsPool<ActionKey> _keyPool;
    private EcsPool<Happened> _happenedPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<PlayerAction>()
            .Inc<ActionKey>()
            .End();

        _actionPool = world.GetPool<PlayerAction>();
        _keyPool = world.GetPool<ActionKey>();
        _happenedPool = world.GetPool<Happened>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var action = ref _actionPool.Get(entity).Action;
            ref var key = ref _keyPool.Get(entity).Key;

            if (KeyboardController.IsPressed(key))
            {
                _happenedPool.Add(entity, new Happened(now: KeyboardController.IsJustPressed(key)));
            }
        }
    }
}
