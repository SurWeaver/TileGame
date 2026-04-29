using Leopotam.EcsLite;

namespace EcsLib.Tweening.Components;

public struct TweenEntity(EcsPackedEntity entity)
{
    public EcsPackedEntity Entity = entity;
}
