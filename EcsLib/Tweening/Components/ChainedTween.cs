using Leopotam.EcsLite;

namespace EcsLib.Tweening.Components;

public struct ChainedTween(EcsPackedEntity tweenEntity)
{
    public EcsPackedEntity Entity = tweenEntity;
}
