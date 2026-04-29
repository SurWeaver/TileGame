namespace EcsLib.Tweening.Components;

public struct TweenValuePair<T>(T start, T end)
{
    public T Start = start;
    public T End = end;
}
