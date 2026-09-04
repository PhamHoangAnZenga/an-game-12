using UnityEngine;

public class PlayerMoveState : BaseState
{
    Joystick _joystick;
    Rigidbody _rigidbody;
    Transform _transform;
    PlayerStats _stats;

    public PlayerMoveState(PlayerStats stats)
    {
        _stats = stats;
        _joystick = stats.Joystick;
        _rigidbody = stats.Rigidbody;
        _transform = stats.Transform;
    }

    public override bool ConditionCheck()
    {
        return _joystick.Horizontal != 0 || _joystick.Vertical != 0;
    }

    public override void EnterState()
    {
        Debug.Log("enter move state");
    }

    public override void FixedUpdate()
    {
        Vector3 moveDirection = new(_joystick.Horizontal, 0, _joystick.Vertical);
        _rigidbody.MovePosition(_transform.position + moveDirection * _stats.MoveSpeed * Time.fixedDeltaTime);
    }

    public override void ExitState()
    {
        Debug.Log("exit move state");
    }
    public override string GetName()
    {
        return "move";
    }
}
