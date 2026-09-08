using UnityEngine;

[System.Serializable]
public class SpawnPos
{
    public string Name;
    public float X;
    public float Y;
    public float Z;

    public Vector3 Get()
    {
        return new Vector3(X, Y, Z);
    }
}

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public int LevelID;
    public SpawnPos PlayerPos;
    public SpawnPos[] MonsterPos;
}
