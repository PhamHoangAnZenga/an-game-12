using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public string Name;
    
    public float AttackDamage;
    public float MaxHealthPoint;
    public float MoveSpeed;
    public float AttackPerSecond;
}
