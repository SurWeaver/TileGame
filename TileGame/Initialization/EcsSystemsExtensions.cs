using EcsLib.Cleanup.Systems;
using EcsLib.Common.Components;
using EcsLib.Drawing.Systems;
using EcsLib.Drawing.Tile.Systems;
using EcsLib.Input.Components;
using EcsLib.Input.Systems;
using Leopotam.EcsLite;
using Microsoft.Xna.Framework;

namespace TileGame.Initialization;


public static class EcsSystemsExtensions
{
    public static IEcsSystems AddVisualComponentEvaluations(this IEcsSystems systems)
    {
        systems
            .Add(new CalculatePositionSystem(tileSize: new Point(32)))
            .Add(new CalculateSpriteRectangleSystem())
            .Add(new CalculateSpriteOriginSystem())
            .Add(new FillSpriteSystem());

        return systems;
    }

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
            .Add(new DeleteEntityWithComponentSystem<DeleteAfterFrameEnd>())
            .Add(new DeleteComponentSystem<Happened>());

        return systems;
    }
}
