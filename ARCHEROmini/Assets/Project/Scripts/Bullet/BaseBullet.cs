using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _moveSpeed;

    public virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void Init(Vector3 direction)
    {
        _rigidbody.linearVelocity = direction * _moveSpeed;
        gameObject.SetActive(true);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
