using UnityEngine;

public class PlayerAttackState : BaseState
{
    PlayerStats _stats;
    float timer;

    public PlayerAttackState(PlayerStats stats)
    {
        _stats = stats;
    }

    public override bool ConditionCheck()
    {
        return _stats.MonsterManager.Check();
    }

    public override void EnterState()
    {
        Debug.Log("enter attack state");
        timer = Time.time + _stats.AttackTime;
    }

    public override void Update()
    {
        if(timer < Time.time)
        {
            Attack(_stats.AttackInfo);
            timer = Time.time + _stats.AttackTime;
        }
    }

    public override void ExitState()
    {
        Debug.Log("exit attack state");
    }

    void Attack(AttackTargetInfo attack)
    {
        GameObject.Instantiate(attack.BulletPrefab);        
    }
}
