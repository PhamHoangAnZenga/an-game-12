
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IStateMachine
{
    Joystick _joystick;
    Rigidbody _rigidbody;
    Transform _transform;
    PlayerStats _stats;

    List<IStateMachine> _nextState;

    public PlayerMoveState(PlayerStats stats)
    {
        _stats = stats;
        _joystick = stats.Joystick;
        _rigidbody = stats.Rigidbody;
        _transform = stats.Transform;
    }

    public void AddTransitions(params IStateMachine[] nextState)
    {
        _nextState = new List<IStateMachine>(nextState);
    }

    public void CheckTransitions()
    {
        throw new System.NotImplementedException();
    }

    public void CheckTransitions(out IStateMachine nextState)
    {
        throw new System.NotImplementedException();
    }

    public bool ConditionCheck()
    {
        return _joystick.Horizontal > 0 || _joystick.Vertical > 0;
    }

    public void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdate()
    {
        Vector3 moveDirection = new(_joystick.Horizontal, 0, _joystick.Vertical);
        _rigidbody.MovePosition(_transform.position + moveDirection * _stats.MoveSpeed * Time.fixedDeltaTime);
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    bool IStateMachine.CheckTransitions(out IStateMachine nextState)
    {
        throw new System.NotImplementedException();
    }
}
