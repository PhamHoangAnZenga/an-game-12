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
        _animator.SetBool(PlayerStats.ISMOVE, false);
    }

    public override void Update()
    {
        if (_stats.MonsterManager.HasMonster())
        {
            _stats.Target = _stats.MonsterManager.FindTarget(_stats.Transform.position);

            Vector3 direction = _stats.Target.transform.position - _stats.Transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _stats.Transform.rotation = Quaternion.Slerp(_stats.Transform.rotation, targetRotation, _stats.RotateSpeed * Time.deltaTime);

        }
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
