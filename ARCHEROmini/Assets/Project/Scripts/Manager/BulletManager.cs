using System.Collections.Generic;
using UnityEngine.Pool;

public enum BulletType
{
    playerBullet,
    enemiesBullet
}

public class BulletManager : MySingleton<BulletManager>
{
    Dictionary<BulletType, ObjectPool<Bullet>> _pools = new Dictionary<BulletType, ObjectPool<Bullet>>();
    List<Bullet> _bullets = new();

    protected override void Awake()
    {
        base.Awake();
    }

    public Bullet GetBullet(Bullet prefab)
    {
        BulletType type = prefab.Type;

        if (!_pools.ContainsKey(type))
        {
            _pools[type] = new ObjectPool<Bullet>(
                createFunc: () =>
                {
                    Bullet bullet = Instantiate(prefab);
                    _bullets.Add(bullet);
                    // obj.SetPool(pools[key]); // Tiêm Pool vào object để nó biết đường về
                    return bullet;
                },
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject)
            );
        }

        Bullet bullet = _pools[type].Get();

        return bullet;
    }

    public void RemoveBullet(Bullet bullet)
    {
        _pools[bullet.Type].Release(bullet);
    }

    public void RemoveAllBullet()
    {
        foreach (Bullet bullet in _bullets)
        {
            if (bullet.enabled) RemoveBullet(bullet);
        }
    }
}
