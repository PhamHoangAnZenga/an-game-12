using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;

    MonsterManager _monsterManager;
    Joystick _joystick;
    PlayerStats _stats;

    IStateMachine _stateMachine;


    void Awake()
    {
        _stats = new PlayerStats(_rigidbody, transform, _joystick);
    }
    
    void Update()
    {
        _stateMachine.Update();
    }

    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    void LateUpdate()
    {
        if( _stateMachine.CheckTransitions(out IStateMachine nextState))
        {
            _stateMachine.ExitState();
            _stateMachine = nextState;
        }
    }

    public void Init(Joystick joystick, PlayerData data, MonsterManager monsterManager)
    {
        _stats.MoveSpeed = data.MoveSpeed;
        _monsterManager = monsterManager;
        _joystick = joystick;

        PlayerIdleState _idleState;
        PlayerAttackState _attackState;
        PlayerMoveState _moveState;

        _idleState = new PlayerIdleState(_stats);                        
        _attackState = new PlayerAttackState(_stats);
        _moveState = new PlayerMoveState(_stats);

        _idleState.AddTransitions(_attackState, _moveState);
        _attackState.AddTransitions(_idleState);
        _moveState.AddTransitions(_idleState, _attackState);

        _stateMachine = _idleState;
    }
}
