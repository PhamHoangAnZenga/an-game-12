using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;

    MonsterManager _monsterManager;
    PlayerStats _stats;
    BaseState _currentState;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        _currentState.Update();
    }

    void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    void LateUpdate()
    {
        if (_currentState.CheckTransitions(out BaseState nextState))
        {
            Debug.Log("change " + _currentState.GetName() + " to " + nextState.GetName());
            _currentState.ExitState();
            nextState.EnterState();

            _currentState = nextState;
        }
    }

    public void Init(Joystick joystick, PlayerData data, MonsterManager monsterManager)
    {
        _monsterManager = monsterManager;

        _stats = new PlayerStats
        {
            Rigidbody = _rigidbody,
            Transform = transform,
            Joystick = joystick,
            MoveSpeed = data.MoveSpeed,
            AttackInfo = new AttackTargetInfo()
        };

        PlayerIdleState idleState = new(_stats);
        PlayerAttackState attackState = new(_stats);
        PlayerMoveState moveState = new(_stats);

        idleState.AddTransitions(moveState, attackState);
        attackState.AddTransitions(moveState, idleState);
        moveState.AddTransitions(idleState);

        _currentState = idleState;

        gameObject.SetActive(true);
    }
}
