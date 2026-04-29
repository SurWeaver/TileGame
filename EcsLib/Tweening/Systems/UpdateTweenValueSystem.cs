using System;
using EcsLib.Timers.Components;
using EcsLib.Tweening.Components;
using Leopotam.EcsLite;

namespace EcsLib.Tweening.Systems;

public class UpdateTweenValueSystem<TComponent, TValue>(
    Func<TValue, TValue, float, TValue> lerp,
    Func<TValue, TComponent> getComponentValue)
    : IEcsInitSystem, IEcsRunSystem where TComponent : struct
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<EasingPercentage> _percentPool;
    private EcsPool<TweenValuePair<TValue>> _pairPool;
    private EcsPool<TComponent> _componentPool;
    private EcsPool<TweenEntity> _tweenEntityPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<EasingPercentage>()
            .Inc<TweenValuePair<TValue>>()
            // На Tween'е компонент является флагом, обозначающим, что мы этот компонент достанем у TweenEntity и изменим её там.
            .Inc<TComponent>()
            .Inc<TweenEntity>()
            .Exc<Paused>()
            .End();

        _percentPool = _world.GetPool<EasingPercentage>();
        _pairPool = _world.GetPool<TweenValuePair<TValue>>();
        _componentPool = _world.GetPool<TComponent>();
        _tweenEntityPool = _world.GetPool<TweenEntity>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var targetEntity = ref _tweenEntityPool.Get(entity).Entity;
            if (!targetEntity.Unpack(_world, out int targetId))
                continue;

            var percent = _percentPool.Get(entity).Percent;
            ref var pair = ref _pairPool.Get(entity);

            var currentValue = lerp(pair.Start, pair.End, percent);

            ref var targetComponent = ref _componentPool.Get(targetId);
            targetComponent = getComponentValue(currentValue);
        }
    }
}
