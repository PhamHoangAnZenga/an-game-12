using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameController _gameController;
    [SerializeField] AudioSource _audioSource;

    void OnEnable()
    {
        _audioSource.Play();
    }

    void OnDisable()
    {
        _audioSource.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        _gameController.NextGame();
    }
}
