using SS.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameController _gameController;

    void Awake()
    {
        Core.Init();
    }
    
    public void PauseGame()
    {
        OpenPauseUI();
    }

    public void OpenPauseUI()
    {
        Time.timeScale = 0f;
        Core.Add<PauseScreenController>(screenName: "PauseScreen", onScreenLoad: (screen) => screen.Init(_gameController) );
    }
}
