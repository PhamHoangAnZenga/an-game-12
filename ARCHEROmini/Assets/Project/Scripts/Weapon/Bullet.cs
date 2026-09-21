using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletType Type;

    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _moveSpeed;
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] AudioClip _bulletHitSound;
    [SerializeField] AudioClip _bulletFireSound;

    float _lifeTime;
    float _damage;

    public virtual void Fired(Vector3 startPos, Vector3 direction, float damage)
    {
        transform.position = startPos;
         
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
        BulletManager.Instance.RemoveBullet(this);
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
