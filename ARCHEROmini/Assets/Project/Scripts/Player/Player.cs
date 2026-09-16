using UnityEngine;

public class Player : MonoBehaviour, IDmgAble
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _hpBarPosition;
    [SerializeField] Animator _animator;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Color _hpBarColor;

    HpBarController _hpBar;

    MonsterManager _monsterManager;
    PlayerStats _stats;
    BaseState _currentState;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        CheckTransitions();
        _currentState.Update();
    }

    void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    void CheckTransitions()
    {
        if (_currentState.CheckTransitions(out BaseState nextState))
        {
            if (_currentState == nextState) return;
            // Debug.Log("change " + _currentState.GetName() + " to " + nextState.GetName());
            _currentState.ExitState();
            nextState.EnterState();

            _currentState = nextState;
        }
    }

    public void SetSpeedBuff(float value)
    {
        _stats.BuffSpeed = value;
    }

    public void Init(Joystick joystick, PlayerData data, MonsterManager monsterManager, HpBarController hpBar)
    {
        _monsterManager = monsterManager;

        _stats = new PlayerStats
        {
            Rigidbody = _rigidbody,
            Transform = transform,
            Animator = _animator,
            Joystick = joystick,

            AttackDamage = data.AttackDamage,
            BaseMoveSpeed = data.MoveSpeed,
            AttackPerSecond = data.AttackPerSecond,
            MaxHealth = data.MaxHealthPoint,
            CurrentHealth = data.MaxHealthPoint,

            Weapon = data.Weapon,
            MonsterManager = _monsterManager,
            AudioSource = _audioSource
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
        _hpBar.SetColor(_hpBarColor);
        _hpBar.SetText(_stats.CurrentHealth);
            
        gameObject.SetActive(true);
    }

    public virtual void TakeDmg(float dmg)
    {
        if (_stats.CurrentHealth <= 0.001f) return;

        _stats.CurrentHealth -= dmg;
        _hpBar.CreateDamageText(dmg);
        
        if (_stats.CurrentHealth <= 0.001f)
        {
            _stats.CurrentHealth = 0;
            _hpBar.UpdateText(_stats.CurrentHealth);
            _hpBar.UpdateBar(_stats.CurrentHealth / _stats.MaxHealth);
            Death();
        }

        _hpBar.UpdateText(_stats.CurrentHealth);
        _hpBar.UpdateBar(_stats.CurrentHealth / _stats.MaxHealth);
    }

    public void Release()
    {
        Destroy(_hpBar.gameObject);
        Destroy(gameObject);
    }

    void Death()
    {
        Debug.Log("call");
        UIController.Instance.OpenLoseUI();
    }
}
