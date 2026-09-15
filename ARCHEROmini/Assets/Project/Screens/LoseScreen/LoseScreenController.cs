using UnityEngine;
using SS.UI;

public class LoseScreenController : MonoBehaviour, IKeyBack
{
    public const string NAME = "LoseScreen";
    GameController _gameController;

    public void OnKeyBack()
    {
        Core.Close();
    }

    public void Init(GameController gameController)
    {
        _gameController = gameController;
    }

    public void ResetGame()
    {
        _gameController.ResetGame();
        Core.Close();
    }
}