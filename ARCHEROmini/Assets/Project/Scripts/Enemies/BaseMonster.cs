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
    [SerializeField] Collider _colider;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected AnimationClip _dieAnimClip;

    string Name;

    protected HpBarController _hpBar;
    protected Transform _target;

    protected float _maxHealth = 50;
    protected float _health = 50;

    public bool IsDeath { get; protected set; }

    public virtual void Awake()
    {
        gameObject.SetActive(false);
    }
    
    protected virtual void Update()
    {
        if (IsDeath) return;

        Vector3 direction = transform.position - _target.position;
        transform.rotation = Quaternion.LookRotation(direction);
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
        yield return new WaitForSeconds(_dieAnimClip.length);
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        OnDeath = null;
    }
}
