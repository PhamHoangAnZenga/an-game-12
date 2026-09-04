using UnityEngine;

public class Dummy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("take dmg");
    }
}
