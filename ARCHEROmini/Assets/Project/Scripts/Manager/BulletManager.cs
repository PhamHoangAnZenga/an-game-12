using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MySingleton<BulletManager>
{
    List<Bullet> _bullets;

    protected override void Awake()
    {
        base.Awake();
        _bullets = new();
    }

    public void Add(Bullet bullet, out int id)
    {
        _bullets.Add(bullet);
        id = _bullets.Count;
    }

    public void Remove(int id)
    {
        _bullets[id - 1] = _bullets[_bullets.Count - 1];
        _bullets[id - 1].ID = id;
        _bullets.RemoveAt(_bullets.Count-1);
    }
    
    public void Release()
    {
        while(_bullets.Count > 0)
        {
            _bullets[_bullets.Count-1].Release();
        }
    }
}
