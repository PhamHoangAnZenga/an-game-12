using UnityEngine;

public class Dummy : BaseMonster
{
    public void Start()
    {
        _health = 999999;
        _maxHealth = 999999;
    }

    void Update()
    {
        _health = _maxHealth;
    }
}
