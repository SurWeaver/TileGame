using Core.Tweening;
using EcsLib.Extensions;
using EcsLib.Timers.Components;
using EcsLib.Tweening.Components;
using Leopotam.EcsLite;

namespace EcsLib.Tweening.Systems;

public class UpdateTweenEasePercentSystem()
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<TweenEasing> _easingPool;
    private EcsPool<Percentage> _percentPool;
    private EcsPool<EasingPercentage> _easePercentPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<TweenEasing>()
            .Inc<Percentage>()
            .Exc<Paused>()
            .End();

        _easingPool = world.GetPool<TweenEasing>();
        _percentPool = world.GetPool<Percentage>();
        _easePercentPool = world.GetPool<EasingPercentage>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            var easingType = _easingPool.Get(entity).Type;
            var easingFunction = EasingFunctions.GetEaseFunction(easingType);

            var percent = _percentPool.Get(entity).Percent;
            var easingPercent = easingFunction((float)percent);

            ref var easePercentComponent = ref _easePercentPool.Get(entity);
            easePercentComponent.Percent = easingPercent;
        }
    }
}
