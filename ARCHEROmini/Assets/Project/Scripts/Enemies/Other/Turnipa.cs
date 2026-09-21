using UnityEngine;

public class Turnipa : BaseMonster
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] int _numberOfBullets;

    LayerMask _playerLayer;

    float _moveSpeed;

    public override void Awake()
    {
        base.Awake();
        _moveSpeed = _data.MoveSpeed * Random.Range(0.8f, 1.2f);
        _playerLayer = LayerMask.NameToLayer("Player");
    }

    void FixedUpdate()
    {
        if (IsDeath) return;
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(nextPosition);
    }

    void Attack()
    {
        float angleStep = 360f / _numberOfBullets;
        float currentAngle = 0f;

        for (int i = 0; i < _numberOfBullets; i++)
        {
            float dirX = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            float dirY = Mathf.Cos(currentAngle * Mathf.Deg2Rad);

            Vector3 moveDirection = new(dirX, 0, dirY);

            Bullet bullet = BulletManager.Instance.GetBullet(_bulletPrefab);
            bullet.Fired(transform.position, moveDirection, _data.AttackDamage);

            currentAngle += angleStep;
        }
    }

    protected override void Death()
    {
        Attack();
        _rigidbody.linearVelocity = Vector3.zero;
        base.Death();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            Death();
        }
    }
}
