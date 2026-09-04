using UnityEngine;

public class PlayerStats
{
    public Transform Transform;
    public Rigidbody Rigidbody;
    public Joystick Joystick;

    public float MoveSpeed;

    public PlayerStats(Rigidbody rigidbody, Transform transform, Joystick joystick)
    {
        Transform = transform;
        Rigidbody = rigidbody;
        Joystick = joystick;
    }
}
