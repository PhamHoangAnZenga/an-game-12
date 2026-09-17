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
    protected static readonly int ISDIE = Animator.StringToHash("isDie");
    protected static readonly int ISATTACK = Animator.StringToHash("isAttack");
    protected static readonly int ISDAMAGE = Animator.StringToHash("isDamage");

    public event Action<BaseMonster> OnClear;

    public int ID;

    [SerializeField] Transform _hpBarPosition;
    [SerializeField] Collider _colider;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected AnimationClip _dieAnimClip;
    [SerializeField] protected EnemyDataSO _data;

    string Name;

    protected HpBarController _hpBar;
    protected Transform _target;

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
    
    public void Release()
    {
        Destroy(gameObject);
        if (IsDeath) return;
        Destroy(_hpBar.gameObject);
    }

    public virtual void Init(HpBarController hpBar, Transform target)
    {
        _hpBar = hpBar;
        _hpBar.Init(_hpBarPosition);

        _health = _data.MaxHealthPoint;
        _target = target;

        IsDeath = false;
        gameObject.SetActive(true);
    }

    public virtual void TakeDmg(float dmg)
    {
        _health -= dmg;
        _hpBar.CreateDamageText(dmg);

        _animator.SetTrigger(ISDAMAGE);
        
        if (_health <= 0.001f)
        {
            _health = 0;
            Death();
        }
        _hpBar.UpdateBar(_health / _data.MaxHealthPoint);
    }

    protected virtual void Death()
    {
        IsDeath = true;

        Destroy(_hpBar.gameObject);
        _colider.enabled = false;
        _animator.SetTrigger(ISDIE);

        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(_dieAnimClip.length);
         
        OnClear.Invoke(this);
        Destroy(gameObject);
    }

    void OnDestroy()
    { 
        OnClear = null;
    }
}
