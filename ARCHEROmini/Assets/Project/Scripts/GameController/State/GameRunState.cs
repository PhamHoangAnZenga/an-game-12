using UnityEngine;

public class GameRunState : BaseState
{
    GameObject _portal;

    public GameRunState(GameObject portal)
    {
        _portal = portal;
    }

    public override bool ConditionCheck()
    {
        return true;
    }

    public override void EnterState()
    {
        _portal.SetActive(false);
        Time.timeScale = 1f;
    }
}
