using EcsLib.Timers.Components;
using Leopotam.EcsLite;

namespace EcsLib.Timers.Systems;

public class CalculateTimerPercentageSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<Timer> _timerPool;
    private EcsPool<Percentage> _percentPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Timer>()
            .Inc<Percentage>()
            .Exc<Paused>()
            .End();

        _timerPool = world.GetPool<Timer>();
        _percentPool = world.GetPool<Percentage>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var timer = ref _timerPool.Get(entity);
            ref var percent = ref _percentPool.Get(entity).Percent;

            percent = (timer.SecondsPassed == 0)
                ? 0
                : timer.SecondsPassed / timer.Duration;
        }
    }
}
