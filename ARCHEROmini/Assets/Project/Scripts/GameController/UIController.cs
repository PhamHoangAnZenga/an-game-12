using SS.UI;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MySingleton<UIController>
{
    [SerializeField] GameController _gameController;
    [SerializeField] Image _fadeImage;

    protected override void Awake()
    {
        base.Awake();
        Core.Init();
    }

    void Start()
    {
        Core.Add<HomeScreenController>(screenName: "HomeScreen", onScreenLoad: (screen) => screen.Init(_gameController));
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
        Core.Add<LoseScreenController>(screenName: "LoseScreen", onScreenLoad: (screen) => screen.Init(_gameController));
    }

    public void FadeIn()
    {
        _fadeImage.DOFade(1f, 0.5f).SetUpdate(true);
    }
    
    public void FadeOut()
    {
        _fadeImage.DOFade(0f, 0.5f).SetUpdate(true);
    }
}
