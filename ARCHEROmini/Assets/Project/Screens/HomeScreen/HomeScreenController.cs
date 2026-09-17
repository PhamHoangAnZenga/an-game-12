using UnityEngine;
using SS.UI;

public class HomeScreenController : MonoBehaviour, IKeyBack
{
    public const string NAME = "HomeScreen";

    GameController _gameController;

    public void Init(GameController gameController)
    {
        _gameController = gameController;
    }    

    public void OnKeyBack()
    {
    }

    public void StartGame()
    {
        _gameController.PlayGame();
        Core.Close();
    }
}