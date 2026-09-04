using UnityEngine;

public class PlayerStats
{
    public Transform Transform;
    public Rigidbody Rigidbody;
    public Joystick Joystick;
    public AttackTargetInfo AttackInfo;
    public MonsterManager MonsterManager;

    public float MoveSpeed;
    public float AttackPerSecond;
    public float AttackTime => 1 / AttackPerSecond;
}
