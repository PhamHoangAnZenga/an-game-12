using UnityEngine;

public class PlayerIdleState : BaseState
{
    Joystick _joystick;

    public PlayerIdleState(PlayerStats stats)
    {
        _joystick = stats.Joystick;
    }

    public override bool ConditionCheck()
    {
        return !(_joystick.Horizontal != 0 || _joystick.Vertical != 0);
    }

    public override void EnterState()
    {
        // Debug.Log("enter idle state");
    }

    public override void ExitState()
    {
        // Debug.Log("exit idle state");
    }

    public override string GetName()
    {
        return "idle";
    }
}
