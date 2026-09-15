using UnityEngine;

public class DevilTree : BaseMonster
{
    [SerializeField] float _attackDelay;

    [SerializeField] Bullet _bulletPrefabs;

    float _timer;

    public override void Awake()
    {
        base.Awake();
        _timer = _attackDelay * Random.Range(0.8f, 1.2f);
    }

    protected override void Update()
    {
        if (IsDeath) return;
        
        base.Update();
        if (_timer < Time.time)
        {
            Launch(_target.position);
            _timer = Time.time + _attackDelay * Random.Range(0.8f, 1.2f);
        }
    }

    void Launch(Vector3 target)
    {
        Bullet bullet = Instantiate(_bulletPrefabs, transform.position, Quaternion.identity);

        Vector3 direction = target - transform.position;
        bullet.Fired(direction);
    }
}
