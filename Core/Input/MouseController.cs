using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Core.Input;

public static class MouseController
{
    private static MouseState _previous = new();
    private static MouseState _current = new();

    public static void Update()
    {
        _previous = _current;
        _current = Mouse.GetState();
    }

    public static bool IsJustPressed(MouseButton button) => button switch
    {
        MouseButton.Left => _previous.LeftButton is ButtonState.Released && _current.LeftButton is ButtonState.Pressed,
        MouseButton.Right => _previous.RightButton is ButtonState.Released && _current.RightButton is ButtonState.Pressed,
        MouseButton.Middle => _previous.MiddleButton is ButtonState.Released && _current.MiddleButton is ButtonState.Pressed,
        MouseButton.X1 => _previous.XButton1 is ButtonState.Released && _current.XButton1 is ButtonState.Pressed,
        MouseButton.X2 => _previous.XButton2 is ButtonState.Released && _current.XButton2 is ButtonState.Pressed,
        _ => false,
    };

    public static bool IsJustReleased(MouseButton button) => button switch
    {
        MouseButton.Left => _previous.LeftButton is ButtonState.Pressed && _current.LeftButton is ButtonState.Released,
        MouseButton.Right => _previous.RightButton is ButtonState.Pressed && _current.RightButton is ButtonState.Released,
        MouseButton.Middle => _previous.MiddleButton is ButtonState.Pressed && _current.MiddleButton is ButtonState.Released,
        MouseButton.X1 => _previous.XButton1 is ButtonState.Pressed && _current.XButton1 is ButtonState.Released,
        MouseButton.X2 => _previous.XButton2 is ButtonState.Pressed && _current.XButton2 is ButtonState.Released,
        _ => false,
    };

    public static bool IsPressed(MouseButton button) => button switch
    {
        MouseButton.Left => _current.LeftButton is ButtonState.Pressed,
        MouseButton.Right => _current.RightButton is ButtonState.Pressed,
        MouseButton.Middle => _current.MiddleButton is ButtonState.Pressed,
        MouseButton.X1 => _current.XButton1 is ButtonState.Pressed,
        MouseButton.X2 => _current.XButton2 is ButtonState.Pressed,
        _ => false,
    };

    public static bool IsReleased(MouseButton button) => button switch
    {
        MouseButton.Left => _current.LeftButton is ButtonState.Released,
        MouseButton.Right => _current.RightButton is ButtonState.Released,
        MouseButton.Middle => _current.MiddleButton is ButtonState.Released,
        MouseButton.X1 => _current.XButton1 is ButtonState.Released,
        MouseButton.X2 => _current.XButton2 is ButtonState.Released,
        _ => false,
    };

    public static Point Position => _current.Position;
    public static Point DeltaPosition => _current.Position - _previous.Position;
    public static int Scroll => _current.ScrollWheelValue;
    public static int DeltaScroll => _current.ScrollWheelValue - _previous.ScrollWheelValue;
}
