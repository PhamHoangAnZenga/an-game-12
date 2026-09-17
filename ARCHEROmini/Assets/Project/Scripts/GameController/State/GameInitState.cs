using UnityEngine;

public class GameInitState : BaseState
{
    LevelDataSO[] _levelData;
    MonsterManager _monsterManager;
    Joystick _joystick;

    CameraController _camera;

    HpBarController _hpBarPrefab;
    Transform _hpBarCanvas;

    Player _player;

    GameData _gameData;

    int _lvl;

    public GameInitState(LevelDataSO[] levelData, Joystick joystick, CameraController camera, GameData gameData, MonsterManager monsterManager, Transform hpBarCanvas, HpBarController hpBarPrefab)
    {
        _monsterManager = monsterManager;
        _levelData = levelData;
        _joystick = joystick;
        _camera = camera;
        _gameData = gameData;

        _hpBarCanvas = hpBarCanvas;
        _hpBarPrefab = hpBarPrefab;
    }

    public Player GetPlayer()
    {
        return _player;
    }

    public void ResetLvl()
    {        
        _lvl = 0;
    }

    public void UpdLvl()
    {
        _lvl += 1;
    }

    public void Release()
    {
        _player.Release();
        _monsterManager.Release();
    }

    public override bool ConditionCheck()
    {
        return true;
    }

    public override void EnterState()
    {
        Time.timeScale = 0f;

        EventBus<ResetGameEvent>.Call(new ResetGameEvent());        
        MapController.Instance.MapInit();
        SpawnPlayer();
        SpawnEnemies();

        _camera.SetTarget(_player.transform);
    }

    void SpawnEnemies()
    {
        foreach (var data in _levelData[_lvl].MonsterPos)
        {
            BaseMonster monsterPrefabs = Resources.Load<BaseMonster>("Prefabs/Enemies/" + data.Name) ?? throw new System.Exception("cannot load " + "monstername");
            BaseMonster monster = Object.Instantiate(monsterPrefabs, data.Get(), Quaternion.identity);
            _monsterManager.Add(monster);

            HpBarController hpBar = Object.Instantiate(_hpBarPrefab, _hpBarCanvas);
            monster.Init(hpBar, _player.transform);

        }
    }

    void SpawnPlayer()
    {
        Player playerPrefabs = Resources.Load<Player>("Prefabs/Player") ?? throw new System.Exception("cannot load player");

        _player = Object.Instantiate(playerPrefabs, _levelData[_lvl].PlayerPos.Get(), Quaternion.identity);

        HpBarController hpBar = Object.Instantiate(_hpBarPrefab, _hpBarCanvas);
        _player.Init(_joystick, _gameData.PlayerData, _monsterManager, hpBar);
    }
}
