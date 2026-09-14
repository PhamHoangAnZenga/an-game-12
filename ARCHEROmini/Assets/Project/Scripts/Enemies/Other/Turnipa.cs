using UnityEngine;

public class Turnipa : BaseMonster
{
    [SerializeField] float _moveSpeed;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] int _numberOfBullets;

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
            
            // float rotationAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            // Quaternion bulletRotation = Quaternion.Euler(0, 0, rotationAngle);

            // 4. Sinh ra đạn
            // GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);

            Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.Fired(moveDirection);

            currentAngle += angleStep;
        }
    }

    protected override void Death()
    {
        Attack();
        _rigidbody.linearVelocity = Vector3.zero;
        base.Death();
    }
}
