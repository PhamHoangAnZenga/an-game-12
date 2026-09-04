using UnityEngine;

public class Dummy : BaseMonster
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("take dmg");
    }
}
