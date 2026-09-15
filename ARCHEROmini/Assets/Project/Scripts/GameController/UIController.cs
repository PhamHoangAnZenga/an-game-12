using SS.UI;
using UnityEngine;

public class UIController : MySingleton<UIController>
{
    [SerializeField] GameController _gameController;

    protected override void Awake()
    {
        base.Awake();
        Core.Init();
    }
    
    public void PauseGame()
    {
        OpenPauseUI();
    }

    public void OpenPauseUI()
    {
        Time.timeScale = 0f;
        Core.Add<PauseScreenController>(screenName: "PauseScreen", onScreenLoad: (screen) => screen.Init(_gameController));
    }
    
    public void OpenLoseUI()
    {
        Time.timeScale = 0f;
        Core.Add<LoseScreenController>(screenName: "PauseScreen", onScreenLoad: (screen) => screen.Init(_gameController));        
    }
}
