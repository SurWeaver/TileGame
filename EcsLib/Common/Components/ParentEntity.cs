using Leopotam.EcsLite;

namespace EcsLib.Common.Components;

public struct ParentEntity(EcsPackedEntity entity)
{
    public EcsPackedEntity Entity = entity;
}
