using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGTools.Input;

public static class GamepadController
{
    private static GamePadState _previous;
    private static GamePadState _current;

    public static void Update()
    {
        _previous = _current;
        _current = GamePad.GetState(playerIndex: 0);
    }

    public static bool IsConnected => _current.IsConnected;
    public static bool IsJustConnected => !_previous.IsConnected && _current.IsConnected;
    public static bool IsJustDisconnected => _previous.IsConnected && !_current.IsConnected;

    public static bool IsJustPressed(Buttons button) => _previous.IsButtonUp(button) && _current.IsButtonDown(button);
    public static bool IsJustReleased(Buttons button) => _previous.IsButtonDown(button) && _current.IsButtonUp(button);

    public static bool IsPressed(Buttons button) => _current.IsButtonDown(button);
    public static bool IsReleased(Buttons button) => _current.IsButtonUp(button);

    public static Vector2 GetStickValue(Side side) => side switch
    {
        Side.Right => _current.ThumbSticks.Right,
        Side.Left => _current.ThumbSticks.Left,
        _ => throw new InvalidEnumArgumentException(),
    };

    public static float GetTriggerValue(Side side) => side switch
    {
        Side.Right => _current.Triggers.Right,
        Side.Left => _current.Triggers.Left,
        _ => throw new InvalidEnumArgumentException(),
    };
}

public enum Side
{
    Left,
    Right,
}
