using UnityEngine;

public class DevilTree : BaseMonster
{
    [SerializeField] float _attackDelay;

    [SerializeField] Bullet _bulletPrefabs;

    float _timer;

    Transform _target;

    public void Init(Transform target)
    {
        _target = target;
    }

    void Update()
    {
        if (_timer < _attackDelay)
        {
            Launch(_target.position);
            _timer = Time.time + _attackDelay;
        }
    }

    void Launch(Vector3 target)
    {
        Bullet bullet = Instantiate(_bulletPrefabs, transform.position, Quaternion.identity);

        Vector3 direction = target - transform.position;
        bullet.Fired(direction);
    }
}
