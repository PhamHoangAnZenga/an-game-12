using UnityEngine;

public class PlayerIdleState : IStateMachine
{
    PlayerStats _stats;
    
    public PlayerIdleState(PlayerStats stats)
    {
        _stats = stats;
    }

    public void AddTransitions(params IStateMachine[] nextState)
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        if (_stats.Joystick.Horizontal > 0 || _stats.Joystick.Vertical > 0)
        {
        }
    }

    bool IStateMachine.CheckTransitions(out IStateMachine nextState)
    {
        throw new System.NotImplementedException();
    }
}
