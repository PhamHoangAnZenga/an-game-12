using DG.Tweening;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public void Init(float cloudTime, float endLine)
    {
        Debug.Log(cloudTime);
        transform.DOMoveZ(endLine, cloudTime * Random.Range(0.8f, 1.2f)).OnComplete(() => Destroy(gameObject));
    }    
}
