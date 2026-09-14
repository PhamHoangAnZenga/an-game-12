using UnityEngine;

public class Beez : BaseMonster
{
    [SerializeField] float _moveSpeed;
    [SerializeField] Rigidbody _rigidbody;


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
