using UnityEngine;

public class PlayerAttackState : BaseState
{
    PlayerStats _stats;
    float timer = 0;
    BaseMonster _target;

    public PlayerAttackState(PlayerStats stats)
    {
        _stats = stats;
    }

    public override bool ConditionCheck()
    {
        Debug.Log(timer + " " + Time.time);   
        if (timer < Time.time)
        {
            return _stats.MonsterManager.FindTarget(_stats.Transform.position, out _target);
        }else
        {
            return false;
        }
    }

    public override void EnterState()
    {
        Debug.Log("enter attack state");
    }

    public override void Update()
    {
        Attack();
        timer = Time.time + _stats.AttackTime;
    }

    public override void ExitState()
    {
        Debug.Log("exit attack state");
    }

    void Attack()
    {
        Debug.Log("ATTACK!");
        Bullet bullet = Object.Instantiate(_stats.Weapon.BulletPrefab);

        Vector3 direction = _target.transform.position - _stats.Transform.position;
        direction.y = 0;

        bullet.transform.position = _stats.Transform.position;
        bullet.Fired(direction);
    }

    public override string GetName()
    {
        return "attack";
    }
}
