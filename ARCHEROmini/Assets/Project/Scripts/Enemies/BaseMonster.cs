using System;
using System.Collections;
using UnityEngine;

public enum EnemyType
{
    Dummy,
    DevilTree
}

public class BaseMonster : MonoBehaviour, IDmgAble
{
    static readonly int ISDIE = Animator.StringToHash("isDie");

    public event Action<BaseMonster> OnDeath;

    public int ID;

    [SerializeField] Transform _hpBarPosition;
    [SerializeField] Animator _animator;
    [SerializeField] Collider _colider;

    string Name;

    protected HpBarController _hpBar;
    protected Transform _target;

    protected float _maxHealth = 50;
    protected float _health = 50;

    public bool IsDeath;

    public virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void Init(HpBarController hpBar, Transform target)
    {
        _hpBar = hpBar;
        _hpBar.Init(_hpBarPosition);

        _health = _maxHealth;
        _target = target;

        IsDeath = false;
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

    protected virtual void Death()
    {
        IsDeath = true;
        OnDeath.Invoke(this);
        OnDeath = null;
        Destroy(_hpBar.gameObject);
        _colider.enabled = false;
        _animator.SetTrigger(ISDIE);
        
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        OnDeath = null;
    }
}
