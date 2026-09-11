using UnityEngine;

public class PlayerStats
{
    public static readonly int ISMOVE = Animator.StringToHash("isMove");
    public static readonly int ISATTACK = Animator.StringToHash("isAttack");
    public static readonly int ATTACKSPEED = Animator.StringToHash("attackSpeed");

    public Transform Transform;
    public Rigidbody Rigidbody;
    public Animator Animator;
    public Joystick Joystick;
    public AttackInfo AttackInfo;
    public Weapon Weapon;
    public MonsterManager MonsterManager;

    public float MaxHealth;
    public float CurrentHealth;
    public float BaseMoveSpeed;
    public float RotateSpeed = 10f;
    public float BuffSpeed = 1f;
    public float AttackPerSecond;
    
    public float AttackTime => Mathf.Max(1 / AttackPerSecond, 0.2f);
    public float MoveSpeed => BaseMoveSpeed * BuffSpeed;
}
