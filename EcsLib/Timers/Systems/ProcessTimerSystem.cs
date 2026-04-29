using System;
using Core.Context;
using EcsLib.Common.Components;
using EcsLib.Timers.Components;
using Leopotam.EcsLite;

namespace EcsLib.Timers.Systems;

public class ProcessTimerSystem
    : IEcsInitSystem, IEcsRunSystem, IEcsPostRunSystem
{
    private EcsFilter _filter;
    private EcsFilter _restartFinishedFilter;
    private EcsPool<Timer> _timerPool;
    private EcsPool<Finished> _finishedPool;
    private EcsPool<DeleteAfterFrameEnd> _deletePool;
    private EcsPool<Looping> _loopingPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Timer>()
            .Exc<Paused>()
            .End();

        _restartFinishedFilter = world.Filter<Timer>()
            .Inc<Finished>()
            .Inc<Looping>()
            .End();

        _timerPool = world.GetPool<Timer>();
        _finishedPool = world.GetPool<Finished>();
        _loopingPool = world.GetPool<Looping>();
        _deletePool = world.GetPool<DeleteAfterFrameEnd>();
    }

    public void Run(IEcsSystems systems)
    {
        if (_filter.GetEntitiesCount() == 0)
            return;

        var deltaTime = GameContext.DeltaTime;

        foreach (var entity in _filter)
        {
            ref var timer = ref _timerPool.Get(entity);

            timer.SecondsPassed = Math.Min(timer.SecondsPassed + deltaTime.TotalSeconds, timer.Duration);

            if (timer.SecondsPassed >= timer.Duration)
            {
                _finishedPool.Add(entity);
                if (!_loopingPool.Has(entity))
                    _deletePool.Add(entity);
            }
        }
    }

    public void PostRun(IEcsSystems systems)
    {
        foreach (var entity in _restartFinishedFilter)
        {
            ref var timer = ref _timerPool.Get(entity);

            timer.SecondsPassed = 0;
            _finishedPool.Del(entity);
        }
    }
}
