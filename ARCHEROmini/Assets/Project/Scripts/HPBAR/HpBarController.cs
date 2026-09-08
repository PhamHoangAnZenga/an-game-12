using UnityEngine;

public class HpBarController : MonoBehaviour
{
    [SerializeField] RectTransform _background;
    [SerializeField] RectTransform _bar;

    Transform _target;

    public void Init(Transform target)
    {
        _target = target;
    }

    public void LateUpdate()
    {
        transform.position = _target.position;
    }

    public void UpdateBar(float value)
    {
        _bar.sizeDelta = new Vector2(_background.sizeDelta.x * value, _background.sizeDelta.y);
    }
}
