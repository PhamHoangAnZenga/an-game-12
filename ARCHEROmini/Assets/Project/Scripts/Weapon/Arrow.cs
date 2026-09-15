
using UnityEngine;

public class Arrow : Bullet
{
    [SerializeField] GameObject _vfx;

    protected override void OnTriggerEnter(Collider collider)
    {
        Vector3 position = transform.position;
        Instantiate(_vfx, position, Quaternion.identity);
        base.OnTriggerEnter(collider);
    }
}
