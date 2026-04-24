namespace EcsLib.Input.Components;

public struct Happened(bool now)
{
    public bool Now = now;
}
