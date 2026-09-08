using UnityEngine;

public enum EnemyType
{
    Dummy,
    DevilTree
}

public class BaseMonster : MonoBehaviour
{
    [SerializeField] Transform _hpBarPosition;
    HpBarController _hpBar;

    public virtual void Awake()
    {
        gameObject.SetActive(false);
    }
    
    public virtual void Init(HpBarController hpBar)
    {
        _hpBar = hpBar;
        _hpBar.Init(_hpBarPosition);
        gameObject.SetActive(true);
    }
}
