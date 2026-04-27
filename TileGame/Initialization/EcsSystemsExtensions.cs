using EcsLib.Cleanup.Systems;
using EcsLib.Common.Components;
using EcsLib.Common.Systems;
using EcsLib.Drawing.Systems;
using EcsLib.Input.Components;
using EcsLib.Input.Systems;
using Leopotam.EcsLite;

namespace TileGame.Initialization;


public static class EcsSystemsExtensions
{
    public static IEcsSystems AddVisualComponentEvaluations(this IEcsSystems systems)
    {
        systems
            // Вычисляет позицию
            .Add(new CalculateCoordinatePositionSystem(Game1.TileSize))
            // Вычисляет SourceRectangle
            .Add(new CalculateSpriteRectangleSystem())
            // Вычисляет SpriteOrigin
            .Add(new CalculateSpriteOriginSystem())
            // Вычисляет всё остальное
            .Add(new FillSpriteSystem())

            .Add(new CalculateParentScaleSystem())
            .Add(new CalculateParentRotationSystem())
            .Add(new CalculateParentPositionSystem())
            ;

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
