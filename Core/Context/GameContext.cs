using System;
using Microsoft.Xna.Framework;

namespace Core.Context;

public static class GameContext
{
    public static TimeSpan DeltaTime => GameTime.ElapsedGameTime;

    private static GameTime GameTime;

    public static void UpdateGameTime(GameTime gameTime)
    {
        GameTime = gameTime;
    }
}
