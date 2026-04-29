namespace EcsLib.Timers.Components;

public struct Timer(double seconds)
{
    public double Duration = seconds;
    public double SecondsPassed = 0f;
}
