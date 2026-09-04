using UnityEngine;

public class Player : MonoBehaviour
{
    Joystick _joystick;

    public void SetInput(Joystick joystick)
    {
        _joystick = joystick;
    }
}
