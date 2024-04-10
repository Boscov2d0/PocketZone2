using UnityEngine.SceneManagement;

public class GameOverCanvasController : ObjectsDisposer
{
    public GameOverCanvasController(UIManager uiManager) 
    {
        GameOverCanvasView gameOverCanvasView = ResourcesLoader.InstantiateAndGetObject<GameOverCanvasView>(uiManager.GameOverCanvasPath);
        gameOverCanvasView.Initialize(Restart);
        AddGameObject(gameOverCanvasView.gameObject);
    }
    private void Restart() => SceneManager.LoadScene(0);
}