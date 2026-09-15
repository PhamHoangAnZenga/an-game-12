using UnityEngine;

public class Beez : BaseMonster
{
    [SerializeField] Rigidbody _rigidbody;

    float _moveSpeed;

    public override void Awake()
    {
        base.Awake();
        _moveSpeed = _data.MoveSpeed * Random.Range(0.8f, 1.2f);    
    }

    void FixedUpdate()
    {
        if (IsDeath) return;

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(nextPosition);
    }

    protected override void Death()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        base.Death();
    }
}
