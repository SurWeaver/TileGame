using Microsoft.Xna.Framework;

namespace Core.Tweening;

public static class LerpFunctions
{
    public static Vector2 LerpVector2(Vector2 start, Vector2 end, float percent) => start + (end - start) * percent;
    public static Vector3 LerpVector3(Vector3 start, Vector3 end, float percent) => start + (end - start) * percent;
    public static Vector4 LerpVector4(Vector4 start, Vector4 end, float percent) => start + (end - start) * percent;

    public static float LerpFloat(float start, float end, float percent) => start + (end - start) * percent;
    public static Color LerpColor(Color start, Color end, float percent) => new(LerpVector4(start.ToVector4(), end.ToVector4(), percent));
}
