using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] LevelDataSO[] _levelData;
    [SerializeField] GameData _gameData;

    [SerializeField] Joystick _joystick;
    [SerializeField] CameraController _camera;

    [SerializeField] HpBarController _hpBarPrefab;
    [SerializeField] Transform _hpBarCanvas;

    [SerializeField] GameObject _portal;

    BaseState _gameState;
    MonsterManager _monsterManager;

    GameInitState _gameInitState;
    GameWinState _gameWinState;
    GameLoseState _gameLoseState;
    GameRunState _gameRunState;

    void Awake()
    {
        _portal.SetActive(false);
        _monsterManager = new MonsterManager();
        _monsterManager.OnClearMonster += WinGame;
    }

    void OnDestroy()
    {
        _monsterManager.OnClearMonster -= WinGame;        
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        _gameInitState = new(_levelData, _joystick, _camera, _gameData, _monsterManager, _hpBarCanvas, _hpBarPrefab);
        _gameWinState = new();
        _gameLoseState = new();
        _gameRunState = new(_portal);

        _gameState = _gameInitState;
        _gameState.EnterState();

        ChangeState(_gameRunState);
    }

    public void WinGame()
    {
        // điều kiện để vào win game = tiêu diệt hết quái vật
        // mở cồng next state
        
        _portal.SetActive(true);
    }
    
    public void LoseGame()
    {
        // tự reset lại màn chơi
        ChangeState(_gameInitState);
    }

    public void NextGame()
    {
        _gameInitState.UpdLvl();
        _gameInitState.Release();

        ChangeState(_gameInitState);
        ChangeState(_gameRunState);
    }

    public void ChangeState(BaseState state)
    {
        if (state.ConditionCheck())
        {
            _gameState.ExitState();

            _gameState = state;
            _gameState.EnterState();
        }
    }

    public void ContinueGame()
    {

    }

    public void ResetGame()
    {

    }
    
    public void ExitGame()
    {
        
    }
}
