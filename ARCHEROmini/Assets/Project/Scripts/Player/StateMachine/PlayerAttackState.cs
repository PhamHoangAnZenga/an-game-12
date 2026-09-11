using UnityEngine;

public class PlayerAttackState : BaseState
{
    PlayerStats _stats;
    float _timer = 0;
    Vector3 _direction;

    public PlayerAttackState(PlayerStats stats)
    {
        Debug.Log("WHYYYYYYYYYYY");
        _stats = stats;
    }

    public override bool ConditionCheck()
    {
        return _stats.MonsterManager.HasMonster();
    }

    public override void EnterState()
    {
        FindTarget();
        _stats.Animator.SetFloat(PlayerStats.ATTACKSPEED, 2f/3f / _stats.AttackTime);

        _stats.Animator.SetBool(PlayerStats.ISATTACK, true);
    }

    public override void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_direction);
        _stats.Transform.rotation = Quaternion.Slerp(_stats.Transform.rotation, targetRotation, _stats.RotateSpeed * Time.deltaTime);

        if(_timer < Time.time)
        Attack();
    }

    public override void ExitState()
    {
        _stats.Animator.SetBool(PlayerStats.ISATTACK, false);
        // Debug.Log("exit attack state");
    }

    public override string GetName()
    {
        return "attack";
    }

    void Attack()
    {
        Bullet bullet = Object.Instantiate(_stats.Weapon.BulletPrefab);

        bullet.transform.position = _stats.Transform.position;
        bullet.Fired(_direction);

        FindTarget();
    }

    void FindTarget()
    {
        Vector3 target = _stats.MonsterManager.FindTarget(_stats.Transform.position);

        _direction = target - _stats.Transform.position;
        _direction.y = 0;    
        
        _timer = Time.time + _stats.AttackTime;    
    }
}
