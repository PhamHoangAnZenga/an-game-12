using UnityEngine;
using SS.UI;

public class PauseScreenController : MonoBehaviour, IKeyBack
{
    public const string NAME = "PauseScreen";

    GameController _gameController;

    public void OnKeyBack()
    {
        Core.Close();
    }

    public void Init(GameController gameController)
    {
        _gameController = gameController;
    }

    public void ContinueGame()
    {
        _gameController.ContinueGame();
        OnKeyBack();
    }

    public void ResetGame()
    {
        _gameController.ResetGame();
        OnKeyBack();
    }
    
    public void ExitGame()
    {
        _gameController.ExitGame(); 
        OnKeyBack();
    }
}