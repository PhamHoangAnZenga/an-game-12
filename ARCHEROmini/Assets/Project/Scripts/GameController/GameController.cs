using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static WaitForSecondsRealtime _waitForSecondsRealtime1 = new WaitForSecondsRealtime(1f);
    [SerializeField] LevelDataSO[] _levelData;
    [SerializeField] GameData _gameData;

    [SerializeField] Joystick _joystick;
    [SerializeField] CameraController _camera;

    [SerializeField] HpBarController _hpBarPrefab;
    [SerializeField] Transform _hpBarCanvas;

    [SerializeField] GameObject _portal;
    [SerializeField] float _endGameSpeedBuff = 3.6f;

    BaseState _gameState;
    MonsterManager _monsterManager;

    GameInitState _gameInitState;
    GameWinState _gameWinState;
    GameLoseState _gameLoseState;
    GameRunState _gameRunState;

    // Player _player;

    void Awake()
    {
        Time.timeScale = 0f;
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
    }

    public void PlayGame()
    {
        _gameState = _gameInitState;
        _gameState.EnterState();

        ChangeState(_gameRunState);
    }

    public void WinGame()
    {
        // điều kiện để vào win game = tiêu diệt hết quái vật
        // mở cồng next state
        _gameInitState.GetPlayer().SetSpeedBuff(_endGameSpeedBuff);        
        _portal.SetActive(true);
    }
    
    public void LoseGame()
    {
    }

    //thêm fade effect trong 1 giây khi next và reset game
    public void NextGame()
    {
        Time.timeScale = 0f;
        
        _gameInitState.UpdLvl();
        _gameInitState.Release();
        BulletManager.Instance.Release();

        StartCoroutine(RunGame());
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
        Time.timeScale = 1f;
    }

    public void ResetGame()
    {
        Time.timeScale = 0f;

        _gameInitState.ResetLvl();
        _gameInitState.Release();
        BulletManager.Instance.Release();

        StartCoroutine(RunGame());
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
    }

    IEnumerator RunGame()
    {
        UIController.Instance.FadeIn();
        yield return new WaitForSecondsRealtime(0.5f);

        ChangeState(_gameInitState);

        UIController.Instance.FadeOut();
        yield return new WaitForSecondsRealtime(0.5f);

        ChangeState(_gameRunState);
        
    }
}
