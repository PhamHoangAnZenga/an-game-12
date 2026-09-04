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
        //current state == idle && enemy in range
        return base.ConditionCheck();
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
            timer = Time.time + _stats.AttackTime;
        }
    }

    public override void ExitState()
    {
        Debug.Log("exit attack state");
    }
}
