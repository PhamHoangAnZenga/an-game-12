using DG.Tweening;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public event System.Action<int> OnClear;
    public int ID;

    public void Init(float cloudTime, float endLine)
    {
        transform.DOMoveZ(endLine, cloudTime * Random.Range(0.8f, 1.2f)).OnComplete(() => Clear());
    }    
    
    void Clear()
    {
        OnClear?.Invoke(ID);
        OnClear = null;
        Destroy(gameObject);        
    }
}
