using System;
using EcsLib.Extensions;
using EcsLib.Timers.Components;
using EcsLib.Tweening.Components;
using Leopotam.EcsLite;

namespace EcsLib.Tweening.Systems;

public class CalculateTweenPairSystem<TComponent, TValue>(Func<TComponent, TValue> getComponentValue)
    : IEcsInitSystem, IEcsRunSystem where TComponent : struct
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<TweenRelativePair<TValue>> _relativePool;
    private EcsPool<TweenValuePair<TValue>> _pairPool;
    private EcsPool<TComponent> _componentPool;
    private EcsPool<TweenEntity> _tweenEntityPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<TweenRelativePair<TValue>>()
            // Флаг, фильтрующий нужные сущности, так как изменяемый компонент лежит в TweenEntity
            .Inc<TComponent>()
            .Inc<TweenEntity>()
            .Exc<Paused>()
            .Exc<TweenValuePair<TValue>>()
            .End();

        _relativePool = _world.GetPool<TweenRelativePair<TValue>>();
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

            var currentValue = getComponentValue(_componentPool.Get(targetId));
            ref var finalValue = ref _relativePool.Get(entity).FinalValue;

            _pairPool.Add(entity, new(currentValue, finalValue));
            _relativePool.Del(entity);
        }
    }
}
