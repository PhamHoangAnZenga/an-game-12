using UnityEngine;

public class Rabby : BaseMonster
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] GameObject _arlarm;

    [Header("timing")]
    [SerializeField] float _idleTime;
    [SerializeField] float _alarmTime;
    [SerializeField] float _moveTime;

    Vector3 _moveDirection;
    float _moveSpeed;

    enum RabbyState
    {
        Idle, Alarm, Move
    };

    RabbyState _state = RabbyState.Idle;

    float _idleTimer;
    float _alarmTimer;
    float _moveTimer;

    public override void Awake()
    {
        base.Awake();
        _moveSpeed = _data.MoveSpeed * Random.Range(0.8f, 1.2f);
        _arlarm.SetActive(false);
        _rigidbody.mass = Random.Range(1, 2);
    }

    void Start()
    {
        _state = RabbyState.Idle;
        _idleTimer = Time.time + _idleTime * Random.Range(0.8f, 1.2f);
    }

    protected override void Update()
    {
        base.Update();
        if (IsDeath) return;
        switch (_state)
        {
            case RabbyState.Idle:
                {
                    if (_idleTimer < Time.time)
                    {
                        _moveDirection = (_target.position - transform.position).normalized;
                        _alarmTimer = Time.time + _alarmTime * Random.Range(0.8f, 1.2f);
                        _arlarm.SetActive(true);

                        _state = RabbyState.Alarm;
                    }
                    break;
                }
            case RabbyState.Alarm:
                {
                    if (_alarmTimer < Time.time)
                    {
                        _moveTimer = Time.time + _moveTime * Random.Range(0.8f, 1.2f);
                        _arlarm.SetActive(false);

                        _state = RabbyState.Move;
                    }
                    break;
                }
            case RabbyState.Move:
                {
                    if (_moveTimer < Time.time)
                    {
                        _idleTimer = Time.time + _idleTime * Random.Range(0.8f, 1.2f);

                        _state = RabbyState.Idle;
                    }
                    break;
                }
        }
    }

    void FixedUpdate()
    {
        if (IsDeath) return;
        if (_state != RabbyState.Move) return;

        _rigidbody.MovePosition(transform.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime);
    }
}
