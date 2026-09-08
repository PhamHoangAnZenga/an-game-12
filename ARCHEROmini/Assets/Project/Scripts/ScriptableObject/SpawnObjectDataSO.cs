using UnityEngine;

[CreateAssetMenu(fileName = "SpawnObjectDataSO", menuName = "Scriptable Objects/SpawnObjectDataSO")]
public class SpawnObjectDataSO : ScriptableObject
{
    public GameObject Prefab;
    public int X;
    public int Y;
    public int Z;
}
