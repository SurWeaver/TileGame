using Core.Input;
using DataModels;
using DataModels.Input;
using EcsLib.Common.Components;
using EcsLib.Drawing.Components;
using EcsLib.Drawing.Systems;
using EcsLib.Input.Components;
using EcsLib.Tools;
using Leopotam.EcsLite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileGame.Initialization;

namespace TileGame;

public class Game1 : Game
{
    public static readonly Point TileSize = new(32);
    public static readonly Point GridSize = new(24, 12);
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private EcsWorld _world;

    private EcsSystems _updateSystems;
    private EcsSystems _drawSystems;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = GridSize.X * TileSize.X;
        _graphics.PreferredBackBufferHeight = GridSize.Y * TileSize.Y;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _world = new EcsWorld();
        EntityBuilder.Initialize(_world);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        LoadInputToWorld();

        InitializeSystems();
    }

    private void InitializeSystems()
    {
        _updateSystems = new EcsSystems(_world);
        _updateSystems
            .AddVisualComponentEvaluations()

            .AddActionProcessing()

            .AddCleanup()
            .Init();

        _drawSystems = new EcsSystems(_world);
        _drawSystems
            .Add(new BeginDrawSystem(_spriteBatch, samplerState: SamplerState.PointClamp))
            .Add(new DrawSpriteSystem(_spriteBatch))
            .Add(new EndDrawSystem(_spriteBatch))
            .Init();
    }

    private void LoadInputToWorld()
    {
        var input = Content.Load<InputMapping>("PlayerInput");

        foreach (var action in input.Actions)
        {
            foreach (var key in action.KeyboardKeys)
                EntityBuilder.NewEntity()
                    .With(new PlayerAction(action.Action))
                    .With(new ActionKeyboardKey(key));

            foreach (var button in action.MouseButtons)
                EntityBuilder.NewEntity()
                    .With(new PlayerAction(action.Action))
                    .With(new ActionMouseButton(button));

            foreach (var button in action.GamepadButtons)
                EntityBuilder.NewEntity()
                    .With(new PlayerAction(action.Action))
                    .With(new ActionGamepadButton(button));
        }
    }

    protected override void Update(GameTime gameTime)
    {
        GamepadController.Update();
        KeyboardController.Update();
        MouseController.Update();

        _updateSystems.Run();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DimGray);

        _drawSystems.Run();
    }
}
