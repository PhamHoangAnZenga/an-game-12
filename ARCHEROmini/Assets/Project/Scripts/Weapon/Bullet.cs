using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _moveSpeed;
    [SerializeField] LayerMask _targetLayer;

    public virtual void Fired(Vector3 direction)
    {
        _rigidbody.linearVelocity = direction * _moveSpeed;
        gameObject.SetActive(true);
    }

    protected virtual void Awake()
    {
        _rigidbody.includeLayers = _targetLayer;
        _rigidbody.excludeLayers = ~_targetLayer;
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
