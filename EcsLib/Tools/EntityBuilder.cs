using EcsLib.Extensions;
using Leopotam.EcsLite;

namespace EcsLib.Tools;


public static class EntityBuilder
{
    private static EcsWorld _world;

    public static void Initialize(EcsWorld world)
    {
        _world = world;
    }

    public static InnerBuilder NewEntity()
    {
        var entity = _world.NewEntity();
        return new InnerBuilder(entity);
    }

    public static EcsPackedEntity GetPacked(int entity)
    {
        return _world.PackEntity(entity);
    }

    public class InnerBuilder
    {
        private readonly int _entity;

        internal InnerBuilder(int entity) => _entity = entity;

        public InnerBuilder With<TComponent>(TComponent component) where TComponent : struct
        {
            var pool = _world.GetPool<TComponent>();
            pool.Add(_entity, component);
            return this;
        }
        public int End() => _entity;
    }

}

