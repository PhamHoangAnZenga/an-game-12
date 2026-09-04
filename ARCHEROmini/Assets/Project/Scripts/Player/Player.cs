using UnityEngine;

public enum PLayerState
{
    Idle,
    Move,
    Attack
}

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;


    Joystick _joystick;
    float _moveSpeed;

    bool _moveFlag = false;
    
    PLayerState _state = PLayerState.Idle;

    void Start()
    {
        _state = PLayerState.Idle;
    }

    void Update()
    {
        if (!_moveFlag)
        {
            
        }
    }

    void FixedUpdate()
    {
        if (_joystick.Horizontal > 0 || _joystick.Vertical > 0)
        {
            _moveFlag = true;
            Vector3 moveDirection = new(_joystick.Horizontal, 0, _joystick.Vertical);
            _rigidbody.MovePosition(transform.position + moveDirection * _moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _moveFlag = false;
        }
    }

    public void Init(PlayerData data)
    {
        _moveSpeed = data.MoveSpeed;
    }

    public void SetInput(Joystick joystick)
    {
        _joystick = joystick;
    }

}
