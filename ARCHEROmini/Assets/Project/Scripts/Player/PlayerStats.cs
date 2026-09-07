using UnityEngine;

public class PlayerStats
{
    public Transform Transform;
    public Rigidbody Rigidbody;
    public Joystick Joystick;
    public AttackInfo AttackInfo;
    public Weapon Weapon;
    public MonsterManager MonsterManager;

    public float MoveSpeed;
    public float AttackPerSecond;
    public float AttackTime => Mathf.Max(1 / AttackPerSecond, 0.2f);
}
