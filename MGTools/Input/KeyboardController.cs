using Microsoft.Xna.Framework.Input;

namespace MGTools.Input;

public static class KeyboardController
{
    private static KeyboardState _previous = new();
    private static KeyboardState _current = new();

    public static void Update()
    {
        _previous = _current;
        _current = Keyboard.GetState();
    }

    public static bool IsJustPressed(Keys key) => _previous.IsKeyUp(key) && _current.IsKeyDown(key);
    public static bool IsJustReleased(Keys key) => _previous.IsKeyDown(key) && _current.IsKeyUp(key);

    public static bool IsPressed(Keys key) => _current.IsKeyDown(key);
    public static bool IsReleased(Keys key) => _current.IsKeyUp(key);
}
