using EcsLib.Timers.Components;
using EcsLib.Tweening.Components;
using Leopotam.EcsLite;

namespace EcsLib.Tweening.Systems;

public class UpdateChainedTweenSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<ChainedTween> _chainedTweenPool;
    private EcsPool<Paused> _pausePool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<ChainedTween>()
            .Inc<Finished>()
            .End();

        _chainedTweenPool = _world.GetPool<ChainedTween>();
        _pausePool = _world.GetPool<Paused>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var chainedTween = ref _chainedTweenPool.Get(entity).Entity;

            if (!chainedTween.Unpack(_world, out int tweenId))
                continue;

            if (_pausePool.Has(tweenId))
                _pausePool.Del(tweenId);
        }
    }
}
