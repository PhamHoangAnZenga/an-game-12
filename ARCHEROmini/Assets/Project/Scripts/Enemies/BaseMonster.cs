using System;
using UnityEngine;

public enum EnemyType
{
    Dummy,
    DevilTree
}

public class BaseMonster : MonoBehaviour, IDmgAble
{
    public event Action<BaseMonster> OnDeath;

    public int ID;

    [SerializeField] Transform _hpBarPosition;

    string Name;

    HpBarController _hpBar;

    float _maxHealth = 100f;
    float _health = 100f;

    public virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void Init(HpBarController hpBar)
    {
        _hpBar = hpBar;
        _hpBar.Init(_hpBarPosition);

        _health = _maxHealth;

        gameObject.SetActive(true);
    }

    public virtual void TakeDmg(float dmg)
    {
        _health -= dmg;
        if (_health < 0)
        {
            _health = 0;
            Death();
        }
        _hpBar.UpdateBar(_health / _maxHealth);
    }

    void Death()
    {
        OnDeath.Invoke(this);
        Destroy(_hpBar.gameObject);
        Destroy(gameObject);
    }
}
