using UnityEngine;

public class Rabby : BaseMonster
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _delay;
    [SerializeField] Rigidbody _rigidbody;

    Vector3 _landingSpot;

    enum RabbyState
    {
        Idle, Move
    };

    RabbyState _state = RabbyState.Idle;

    float _delayTimer;

    public override void Awake()
    {
        base.Awake();
        _state = RabbyState.Idle;
        _delayTimer = 0f;
    }

    protected override void Update()
    {
        base.Update();
        if (IsDeath) return;
        switch (_state)
        {
            case RabbyState.Idle:
                {
                    if (_delayTimer < Time.time)
                    {
                        _landingSpot = _target.position;
                        _state = RabbyState.Move;
                    }
                    break;
                }
            case RabbyState.Move:
                {
                    float distance = (_landingSpot - transform.position).sqrMagnitude;
                    if (distance < 0.01f)
                    {
                        _delayTimer = Time.time + _delay;
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

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, _landingSpot, _moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(nextPosition);
    }
}
