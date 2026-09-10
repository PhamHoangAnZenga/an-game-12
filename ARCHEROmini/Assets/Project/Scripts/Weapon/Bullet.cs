using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _moveSpeed;
    [SerializeField] LayerMask _targetLayer;

    float _lifeTime;

    public virtual void Fired(Vector3 direction)
    {
        _rigidbody.linearVelocity = direction * _moveSpeed;
        _lifeTime = Time.time + 5f;
        gameObject.SetActive(true);
    }

    void LateUpdate()
    {
        if(_lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Awake()
    {
        _rigidbody.includeLayers = _targetLayer;
        _rigidbody.excludeLayers = ~_targetLayer;
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IDmgAble dmgAble))
        {
            dmgAble.TakeDmg(36);
        }
        Destroy(gameObject);
    }
}
