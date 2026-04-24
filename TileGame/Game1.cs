using DataModels;
using EcsLib.Drawing.Systems;
using Leopotam.EcsLite;
using MGTools.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TileGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private EcsWorld _world;

    private EcsSystems _updateSystems;
    private EcsSystems _drawSystems;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _world = new EcsWorld();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        InitializeSystems();
    }

    private void InitializeSystems()
    {
        _updateSystems = new EcsSystems(_world);
        _updateSystems
            .Init();

        _drawSystems = new EcsSystems(_world);
        _drawSystems
            .Add(new BeginDrawSystem(_spriteBatch, samplerState: SamplerState.PointClamp))
            .Add(new EndDrawSystem(_spriteBatch))
            .Init();
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
