using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameController _gameController;

    void OnTriggerEnter(Collider other)
    {
        _gameController.NextGame();
    }
}
