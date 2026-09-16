using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    [SerializeField] RectTransform _background;
    [SerializeField] RectTransform _barBack;
    [SerializeField] RectTransform _barFront;
    [SerializeField] float _hpDropTime;
    [SerializeField] Image _bgImage;

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
        _barFront.sizeDelta = new Vector2(_background.sizeDelta.x * value, _background.sizeDelta.y);
        _barBack.DOSizeDelta(new Vector2(_background.sizeDelta.x * value, _background.sizeDelta.y), _hpDropTime);
    }

    public void SetColor(Color color)
    {
        _bgImage.color = color;
    }
}
