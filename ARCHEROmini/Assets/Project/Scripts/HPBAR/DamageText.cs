using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] AnimationClip _liveTime;

    float _timer;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        EventBus<ResetGameEvent>.Add(Clear);
        _timer = Time.time + _liveTime.length;
    }

    void Update()
    {
        if(_timer < Time.time)
        {
            Clear();
        }
    }

    public void SetText(float value)
    {
        _text.text = $"-{((int)value).ToString()}";
        gameObject.SetActive(true);
    }

    void Clear(IEvent evt = null)
    {
        EventBus<ResetGameEvent>.Remove(Clear);
        Destroy(gameObject);
    }   
}
