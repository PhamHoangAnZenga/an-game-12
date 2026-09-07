
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    List<BaseState> _nextState;

    public virtual void AddTransitions(params BaseState[] nextState)
    {
        _nextState = new List<BaseState>(nextState);
    }

    public virtual bool ConditionCheck()
    {
        return false;
    }

    public virtual void EnterState()
    {
        Debug.Log("Enter Void State");
    }

    public virtual void Update(){}

    public virtual void FixedUpdate(){}

    public virtual bool CheckTransitions(out BaseState nextState)
    {
        foreach (var state in _nextState)
        {
            if (state.ConditionCheck())
            {
                nextState = state;
                return true;
            }
        }
        nextState = new BaseState();
        return false;
    }

    public virtual void ExitState() { }
    
    public virtual string GetName()
    {
        return "base";
    }
}
