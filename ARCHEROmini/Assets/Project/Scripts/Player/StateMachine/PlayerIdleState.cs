using UnityEngine;

public class PlayerIdleState : BaseState
{
    Joystick _joystick;
    Animator _animator;
    PlayerStats _stats;

    public PlayerIdleState(PlayerStats stats)
    {
        _stats = stats;
        _joystick = stats.Joystick;
        _animator = stats.Animator;
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
