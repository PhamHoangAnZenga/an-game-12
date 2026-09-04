using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    void LateUpdate()
    {
        Vector3 newPos = transform.position;
        newPos.z = _target.position.z;
        transform.position = newPos;
    }
}
