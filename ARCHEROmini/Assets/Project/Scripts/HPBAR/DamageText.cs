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
        _timer = Time.time + _liveTime.length;
    }

    void Update()
    {
        if(_timer < Time.time)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetText(float value)
    {
        _text.text = $"-{((int)value).ToString()}";
        gameObject.SetActive(true);
    }
}
