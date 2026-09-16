using UnityEngine;

public class Beez : BaseMonster
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] AnimationClip _attackClip;

    float _moveSpeed;
    float _attackTimer;
    public bool IsAttack { get; protected set; }

    public override void Awake()
    {
        base.Awake();
        _moveSpeed = _data.MoveSpeed * Random.Range(0.8f, 1.2f);
    }
    
    protected override void Update()
    {
        base.Update();
        if (_attackTimer < Time.time)
        {
            IsAttack = false;
        }        
    }

    void FixedUpdate()
    {
        if (IsDeath) return;
        if (IsAttack) return;

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(nextPosition);
    }

    protected override void Death()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        base.Death();
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsAttack) return;

        if (((1 << other.gameObject.layer) & _targetLayer.value) != 0)
        {
            IsAttack = true;
            Attack(other.gameObject.GetComponent<IDmgAble>());
        }        
    }

    void Attack(IDmgAble dmgAble)
    {
        _animator.SetTrigger(ISATTACK);
        dmgAble.TakeDmg(36f);
        _attackTimer = Time.time + _attackClip.length;
    }
}
