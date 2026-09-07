using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GamePrefabs _prefabs;
    [SerializeField] LevelData _levelData;
    [SerializeField] GameData _gameData;

    [SerializeField] Joystick _joystick;
    [SerializeField] CameraController _camera;

    MonsterManager _monsterManager;
    Player _player;

    void Awake()
    {
        _monsterManager = new MonsterManager();
    }
    
    void Start()
    {
        StartGame();
    }
    
    void Update()
    {
        
    }

    void StartGame()
    {
        SpawnEnemies();
        SpawnPlayer();

        _camera.SetTarget(_player.transform);
    }

    void SpawnEnemies()
    {
    }

    void SpawnPlayer()
    {
        _player = Instantiate(_prefabs.PlayerPrefab, Vector3.up, Quaternion.identity);
        _player.Init(_joystick, _gameData.PlayerData, _monsterManager);        
    }
}
