
public interface IStateMachine
{
    bool ConditionCheck();
    void EnterState();
    void Update();
    void FixedUpdate();
    bool CheckTransitions(out IStateMachine nextState);
    void ExitState();
}
