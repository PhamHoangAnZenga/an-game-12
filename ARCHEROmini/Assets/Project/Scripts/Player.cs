using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;


    Joystick _joystick;
    float _moveSpeed;

    public void Init(PlayerData data)
    {
        _moveSpeed = data.MoveSpeed;
    }
    
    public void SetInput(Joystick joystick)
    {
        _joystick = joystick;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new(_joystick.Horizontal, 0, _joystick.Vertical);
        _rigidbody.MovePosition(transform.position + moveDirection * _moveSpeed * Time.fixedDeltaTime);
    }
}
