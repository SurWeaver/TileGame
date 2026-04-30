using EcsLib.Cleanup.Systems;
using EcsLib.Common.Components;
using EcsLib.Common.Systems;
using EcsLib.Drawing.Components;
using EcsLib.Drawing.Systems;
using EcsLib.Input.Components;
using EcsLib.Input.Systems;
using EcsLib.Timers.Systems;
using EcsLib.Tweening.Systems;
using Leopotam.EcsLite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileGame.Initialization;


public class SystemInitializer
{
    public static void InitializeUpdateSystems(IEcsSystems processSystems) => processSystems
            .Add(new ProcessTimerSystem())
            .Add(new CalculateTimerPercentageSystem())
            // Вычисление визуальных параметров
            .Add(new CalculateCoordinatePositionSystem(Game1.TileSize))
            .Add(new CalculateSpriteRectangleSystem())
            .Add(new CalculateSpriteOriginSystem())
            .Add(new FillSpriteSystem())

            .Add(new UpdateTweenEasePercentSystem())
            .Add(new UpdateTweenValueSystem<Position, Vector2>(Vector2.Lerp, (vector) => new(vector)))
            .Add(new UpdateTweenValueSystem<Scale, Vector2>(Vector2.Lerp, (vector) => new(vector)))
            .Add(new UpdateTweenValueSystem<Rotation, float>(float.Lerp, (rotation) => new(rotation)))
            .Add(new UpdateTweenValueSystem<SpriteColor, Color>(Color.Lerp, (color) => new(color)))

            .Add(new UpdateTweenValueSystem<DeltaPosition, Vector2>(Vector2.Lerp, (vector) => new(vector)))
            .Add(new UpdateTweenValueSystem<DeltaScale, Vector2>(Vector2.Lerp, (vector) => new(vector)))
            .Add(new UpdateTweenValueSystem<DeltaRotation, float>(float.Lerp, (rotation) => new(rotation)))
            .Add(new UpdateTweenValueSystem<DeltaColor, Color>(Color.Lerp, (color) => new(color)))

            .Add(new UpdateChainedTweenSystem())

            // Относительное позиционирование
            .Add(new CalculateParentScaleSystem())
            .Add(new CalculateParentRotationSystem())
            .Add(new CalculateParentPositionSystem())
            .Add(new CalculateParentColorSystem())

            // Пользовательский ввод
            .Add(new ProcessGamepadActionSystem())
            .Add(new ProcessKeyboardActionSystem())
            .Add(new ProcessMouseActionSystem())
            .Add(new ProcessMouseUiButtonSystem())

            .Add(new ProcessTweenPongSystem<Vector2>())
            .Add(new ProcessTweenPongSystem<float>())
            .Add(new ProcessTweenPongSystem<Color>())

            // Очистка
            .Add(new DeleteEntityWithComponentSystem<DeleteAfterFrameEnd>())
            .Add(new DeleteComponentSystem<Happened>())
            .Add(new CleanAfterEmptyParentSystem())
            .Init();

    public static void InitializeDrawSystems(IEcsSystems drawSystems, SpriteBatch spriteBatch) => drawSystems
            .Add(new BeginDrawSystem(spriteBatch, samplerState: SamplerState.PointClamp, blendState: BlendState.NonPremultiplied))
            .Add(new DrawSpriteSystem(spriteBatch))
            .Add(new EndDrawSystem(spriteBatch))
            .Init();
}
