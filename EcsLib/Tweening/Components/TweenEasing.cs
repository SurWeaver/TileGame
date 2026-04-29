using Core.Tweening;

namespace EcsLib.Tweening.Components;

public struct TweenEasing(EasingType easingType)
{
    public EasingType Type = easingType;
}
