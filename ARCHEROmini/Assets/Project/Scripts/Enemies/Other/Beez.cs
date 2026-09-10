using UnityEngine;

public class Beez : BaseMonster
{
    [SerializeField] float _moveSpeed;
    [SerializeField] Rigidbody _rigidbody;

    void FixedUpdate()
    {
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(nextPosition);
    }
}
