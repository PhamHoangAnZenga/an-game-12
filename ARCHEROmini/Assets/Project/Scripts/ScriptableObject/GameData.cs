using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float AttackDamage;
    public float HealthPoint;
    public float MoveSpeed;
}

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public PlayerData PlayerData;
}
