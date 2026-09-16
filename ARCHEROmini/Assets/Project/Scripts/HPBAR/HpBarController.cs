using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class HpBarController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textValue;
    [SerializeField] RectTransform _barBack;
    [SerializeField] RectTransform _barFront;
    [SerializeField] float _hpDropTime;
    [SerializeField] Image _bgImage;
    [SerializeField] DamageText _damageTextPrefab;

    Transform _target;
    Vector2 _startSizeDelta;
    Transform _hpCanvas;

    public void Init(Transform target)
    {
        _target = target;
        _startSizeDelta = _barBack.sizeDelta;
        _textValue.gameObject.SetActive(false);
        
        _hpCanvas = transform.parent.transform;
    }

    public void LateUpdate()
    {
        transform.position = _target.position;
    }

    public void UpdateBar(float value)
    {
        _barFront.sizeDelta = new Vector2(_startSizeDelta.x * value, _startSizeDelta.y);
        _barBack.DOSizeDelta(new Vector2(_startSizeDelta.x * value, _startSizeDelta.y), _hpDropTime);
    }

    public void SetColor(Color color)
    {
        _bgImage.color = color;
    }

    public void SetText(float value)
    {
        _textValue.gameObject.SetActive(true);
        _textValue.text = ((int)value).ToString();    
    }

    public void UpdateText(float value)
    {
        _textValue.text = ((int)value).ToString();
    }
    
    public void CreateDamageText(float value)
    {
        DamageText text = Instantiate(_damageTextPrefab, transform.position, Quaternion.identity, _hpCanvas);
        text.SetText(value);
    }
}
