using Unity.Mathematics;
using UnityEngine;

public class Planta : BaseMonster
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _moveTime;
    [SerializeField] float _moveDelay;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Bullet _bulletPrefabs;

    Vector3 _stopSpot;

    enum PlantaState
    {
        Idle, Move
    }

    PlantaState _state;
    float _timer;

    public override void Awake()
    {
        base.Awake();
        _state = PlantaState.Idle;
        _timer = 0f;
    }

    void Update()
    {
        if (IsDeath) return;
        switch (_state)
        {
            case PlantaState.Idle:
                {
                    if (_timer < Time.time)
                    {
                        // di chuyển về hướng theo trục nào mà ngắn hơn so với mục tiêu tấn công
                        float distanceX = Mathf.Abs(_target.position.x - transform.position.x);
                        float distanceZ = Mathf.Abs(_target.position.z - transform.position.z);

                        _stopSpot = _target.position;

                        if (distanceX < distanceZ)
                        {
                            _stopSpot.z = transform.position.z;  
                        }
                        else
                        {
                            _stopSpot.x = transform.position.x;
                        }

                        _timer = Time.time + _moveTime;
                        _state = PlantaState.Move;
                    }
                    break;
                }
            case PlantaState.Move:
                {
                    if (_timer < Time.time)
                    {
                        Attack();
                        _timer = Time.time + _moveDelay;
                        _state = PlantaState.Idle;
                    }
                    break;
                }
        }
    }

    void FixedUpdate()
    {
        if (IsDeath) return;
        if (_state != PlantaState.Move) return;

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, _stopSpot, _moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(nextPosition);

    }

    void Attack()
    {
        Bullet bulletT = Instantiate(_bulletPrefabs, transform.position, Quaternion.identity);
        bulletT.Fired(Vector3.forward);

        Bullet bulletD = Instantiate(_bulletPrefabs, transform.position, Quaternion.identity);
        bulletD.Fired(-Vector3.forward);

        Bullet bulletL = Instantiate(_bulletPrefabs, transform.position, Quaternion.identity);
        bulletL.Fired(Vector3.left);

        Bullet bulletR = Instantiate(_bulletPrefabs, transform.position, Quaternion.identity);
        bulletR.Fired(Vector3.right);
    }
    protected override void Death()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        base.Death();
    }
}
