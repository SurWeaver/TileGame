using EcsLib.Cleanup.Systems;
using EcsLib.Common.Components;
using EcsLib.Common.Systems;
using EcsLib.Drawing.Systems;
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
            // Вычисляет позицию
            .Add(new CalculateCoordinatePositionSystem(tileSize: new Point(32)))
            // Вычисляет SourceRectangle
            .Add(new CalculateSpriteRectangleSystem())
            // Вычисляет SpriteOrigin
            .Add(new CalculateSpriteOriginSystem())
            // Вычисляет всё остальное
            .Add(new FillSpriteSystem())

            .Add(new CalculateParentPositionSystem())
            .Add(new CalculateParentRotationSystem());

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

    public static IEcsSystems AddCleanup(this IEcsSystems systems)
    {
        systems
            .Add(new DeleteEntityWithComponentSystem<DeleteAfterFrameEnd>())
            .Add(new DeleteComponentSystem<Happened>())
            .Add(new CleanAfterEmptyParentSystem());

        return systems;
    }
}
