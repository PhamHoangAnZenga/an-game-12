using UnityEngine;

[System.Serializable]
public class SpawnPos
{
    public float X;
    public float Y;
    public float Z;

    public Vector3 Get()
    {
        return new Vector3(X, Y, Z);
    }
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public SpawnPos PlayerSpawnPos;
}
