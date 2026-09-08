using UnityEngine;

public class Player : MonoBehaviour, IDmgAble
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _hpBarPosition;
    HpBarController _hpBar;

    MonsterManager _monsterManager;
    PlayerStats _stats;
    BaseState _currentState;

    float _maxHealth = 100f;
    float _health = 100f;

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

    public void Init(Joystick joystick, PlayerData data, MonsterManager monsterManager, HpBarController hpBar)
    {
        _monsterManager = monsterManager;

        _stats = new PlayerStats
        {
            Rigidbody = _rigidbody,
            Transform = transform,
            Joystick = joystick,
            MoveSpeed = data.MoveSpeed,
            AttackPerSecond = data.AttackPerSecond,
            Weapon = data.Weapon,
            MonsterManager = _monsterManager
        };

        PlayerIdleState idleState = new(_stats);
        PlayerAttackState attackState = new(_stats);
        PlayerMoveState moveState = new(_stats);

        idleState.AddTransitions(moveState, attackState);
        attackState.AddTransitions(moveState, attackState, idleState);
        moveState.AddTransitions(idleState);

        _currentState = idleState;
        _currentState.EnterState();

        // INIT HP BAR
        _hpBar = hpBar;
        _hpBar.Init(_hpBarPosition);

        gameObject.SetActive(true);
    }

    public virtual void TakeDmg(float dmg)
    {
        _health -= dmg;
        if (_health < 0) _health = 0;
        _hpBar.UpdateBar(_health / _maxHealth);
    }
}
