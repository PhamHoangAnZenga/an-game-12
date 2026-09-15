using UnityEngine;

public class Dummy : BaseMonster
{
    public void Start()
    {
        _health = 999999;
    }

    protected override void Update()
    {
        _health = 999999;
    }
}
