using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [NonSerialized] public int ID;

    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _moveSpeed;
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] AudioClip _bulletHitSound;
    [SerializeField] AudioClip _bulletFireSound;

    float _lifeTime;
    float _damage;

    public virtual void Fired(Vector3 direction, float damage)
    {
        BulletManager.Instance.Add(this, out ID);

        _damage = damage;
        direction.y = 0;

        _rigidbody.linearVelocity = direction.normalized * _moveSpeed;
        transform.rotation = Quaternion.LookRotation(direction);

        _lifeTime = Time.time + 5f;
        gameObject.SetActive(true);
        AudioManager.Instance.PlayShotAudio(_bulletFireSound);
    }

    public void Release()
    {
        BulletManager.Instance.Remove(ID);
        Destroy(gameObject);
    }

    void LateUpdate()
    {
        if (_lifeTime < Time.time)
        {
            Release();
        }
    }

    protected virtual void Awake()
    {
        _rigidbody.includeLayers = _targetLayer;
        _rigidbody.excludeLayers = ~_targetLayer;
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IDmgAble dmgAble))
        {
            CallAudioShot();
            dmgAble.TakeDmg(_damage);
        }
        Release();
    }

    protected virtual void CallAudioShot()
    {
        AudioManager.Instance.PlayShotAudio(_bulletHitSound);
    }

}
