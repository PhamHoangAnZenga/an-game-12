using UnityEngine;

public class PlayerMoveState : BaseState
{
    Joystick _joystick;
    Rigidbody _rigidbody;
    Transform _transform;
    PlayerStats _stats;
    Animator _animator;
    Vector3 _moveDirection;

    public PlayerMoveState(PlayerStats stats)
    {
        _stats = stats;
        _joystick = stats.Joystick;
        _rigidbody = stats.Rigidbody;
        _transform = stats.Transform;
        _animator = stats.Animator;
    }

    public override bool ConditionCheck()
    {
        return _joystick.Horizontal != 0 || _joystick.Vertical != 0;
    }

    public override void EnterState()
    {
        _animator.SetBool(PlayerStats.ISMOVE, true);
    }

    public override void Update()
    {
        _moveDirection = new(_joystick.Horizontal, 0, _joystick.Vertical);

        if (_moveDirection.sqrMagnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _stats.RotateSpeed * Time.deltaTime);
        }
    }

    public override void FixedUpdate()
    {
        _rigidbody.MovePosition(_transform.position + _moveDirection * _stats.MoveSpeed * Time.fixedDeltaTime);
    }

    public override void ExitState()
    {
        _animator.SetBool(PlayerStats.ISMOVE, false);
        // Debug.Log("exit move state");
    }
    public override string GetName()
    {
        return "move";
    }
}
