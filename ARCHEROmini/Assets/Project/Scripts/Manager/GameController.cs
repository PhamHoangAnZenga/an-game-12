using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] LevelDataSO _levelData;
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
     
    void StartGame()
    {
        SpawnEnemies();
        SpawnPlayer();

        _camera.SetTarget(_player.transform);
    }

    void SpawnEnemies()
    {        
        foreach(var data in _levelData.MonsterPos)
        {            
            BaseMonster monsterPrefabs = Resources.Load<BaseMonster>("Prefabs/Enemies/"+data.Name) ?? throw new System.Exception("cannot load " + "monstername");
            _monsterManager.Monsters.Add(Instantiate(monsterPrefabs, data.Get(), Quaternion.identity));
        }
    }

    void SpawnPlayer()
    {
        Player playerPrefabs = Resources.Load<Player>("Prefabs/Player") ?? throw new System.Exception("cannot load player");

        _player = Instantiate(playerPrefabs, _levelData.PlayerPos.Get(), Quaternion.identity);

        _player.Init(_joystick, _gameData.PlayerData, _monsterManager);
    }
}
