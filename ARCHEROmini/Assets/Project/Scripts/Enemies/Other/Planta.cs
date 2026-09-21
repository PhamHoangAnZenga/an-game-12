using UnityEngine;

public class Planta : BaseMonster
{
    [SerializeField] float _moveTime;
    [SerializeField] float _moveDelay;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Bullet _bulletPrefabs;

    Vector3 _stopSpot;

    float _moveSpeed;

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
        _moveSpeed = _data.MoveSpeed * Random.Range(0.8f, 1.2f);
    }

    protected override void Update()
    {
        base.Update();
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

                        _timer = Time.time + _moveTime * Random.Range(0.8f, 1.2f);
                        _state = PlantaState.Move;
                    }
                    break;
                }
            case PlantaState.Move:
                {
                    if (_timer < Time.time)
                    {
                        Attack();
                        _timer = Time.time + _moveDelay * Random.Range(0.8f, 1.2f);
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
        Bullet bulletT = BulletManager.Instance.GetBullet(_bulletPrefabs);
        bulletT.Fired(transform.position, Vector3.forward, _data.AttackDamage);

        Bullet bulletD = BulletManager.Instance.GetBullet(_bulletPrefabs);
        bulletD.Fired(transform.position, -Vector3.forward, _data.AttackDamage);

        Bullet bulletL = BulletManager.Instance.GetBullet(_bulletPrefabs);
        bulletL.Fired(transform.position, Vector3.left, _data.AttackDamage);

        Bullet bulletR = BulletManager.Instance.GetBullet(_bulletPrefabs);
        bulletR.Fired(transform.position, Vector3.right, _data.AttackDamage);
    }
    protected override void Death()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        base.Death();
    }
}
