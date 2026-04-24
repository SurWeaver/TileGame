using Core;

namespace EcsLib.Input.Components;

public struct PlayerAction(ActionType action)
{
    public ActionType Action = action;
}
