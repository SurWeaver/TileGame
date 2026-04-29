using EcsLib.Timers.Components;
using EcsLib.Tweening.Components;
using Leopotam.EcsLite;

namespace EcsLib.Tweening.Systems;

public class ProcessTweenPongSystem<TValue>
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<TweenValuePair<TValue>> _pairPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<TweenValuePair<TValue>>()
            .Inc<Finished>()
            .Inc<PongRepeat>()
            .Inc<Looping>()
            .End();

        _pairPool = world.GetPool<TweenValuePair<TValue>>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var pair = ref _pairPool.Get(entity);
            (pair.Start, pair.End) = (pair.End, pair.Start);
        }
    }
}
