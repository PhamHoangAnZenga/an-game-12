using UnityEngine;

public class GameLoseState : BaseState
{
    public override bool ConditionCheck()
    {
        return true;
    }

    public override void EnterState()
    {
        Time.timeScale = 0f;
    }
}
