using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GamePrefabs _prefabs;
    [SerializeField] LevelData _levelData;
    [SerializeField] GameData _gameData;

    [SerializeField] Joystick _joystick;
    [SerializeField] CameraController _camera;

    Player _player;

    void Start()
    {
        StartGame();
    }
    
    void StartGame()
    {
        _player = Instantiate(_prefabs.PlayerPrefab, _levelData.PlayerSpawnPos.Get(), Quaternion.identity);
        _player.Init(_gameData.PlayerData);
        _player.SetInput(_joystick);

        _camera.SetTarget(_player.transform);
    }
}
