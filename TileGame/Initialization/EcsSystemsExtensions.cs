using EcsLib.Common;
using EcsLib.Input.Components;
using EcsLib.Input.Systems;
using EcsLib.Systems;
using Leopotam.EcsLite;

namespace TileGame.Initialization;


public static class EcsSystemsExtensions
{
    public static IEcsSystems AddActionProcessing(this IEcsSystems systems)
    {
        systems
            .Add(new ProcessGamepadActionSystem())
            .Add(new ProcessKeyboardActionSystem())
            .Add(new ProcessMouseActionSystem())
            .Add(new ProcessMouseUiButtonSystem());

        return systems;
    }

    public static IEcsSystems AddCleaningOneFrameComponentsAndEntities(this IEcsSystems systems)
    {
        systems
            .Add(new DeleteEntityWithComponentSystem<OneFrameEvent>())
            .Add(new DeleteComponentSystem<Happened>());

        return systems;
    }
}
