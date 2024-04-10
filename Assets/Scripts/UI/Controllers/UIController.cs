using Zenject;

public class UIController : ObjectsDisposer
{
    private readonly GameMyManager _gameManager;
    private readonly UIManager _uiManager;
    private readonly PlayerManager _playerManager;

    private PlayerCanvasController _playerCanvasController;
    private InventoryCanvasController _inventoryCanvasController;
    private ItemCanvasController _itemCanvasController;
    private GameOverCanvasController _gameOverCanvasController;

    [Inject]
    public UIController(GameMyManager gameManager, UIManager uiManager, PlayerManager playerManager)
    {
        _gameManager = gameManager;
        _uiManager = uiManager;
        _playerManager = playerManager;

        _gameManager.GameStates.SubscribeOnChange(OnStateChange);

        _gameManager.GameStates.Value = UIStates.Player;
    }
    protected override void OnDispose()
    {
        _gameManager.GameStates.UnSubscribeOnChange(OnStateChange);
        ControllersDisposer();

        base.OnDispose();
    }
    private void OnStateChange()
    {
        ControllersDisposer();

        switch (_gameManager.GameStates.Value)
        {
            case UIStates.Player:
                _playerCanvasController = new PlayerCanvasController(_gameManager, _uiManager);
                break;
            case UIStates.Inventory:
                _inventoryCanvasController = new InventoryCanvasController(_gameManager, _uiManager);
                break;
            case UIStates.Item:
                _itemCanvasController = new ItemCanvasController(_gameManager, _uiManager, _playerManager);
                break;
            case UIStates.GameOver:
                _gameOverCanvasController = new GameOverCanvasController(_uiManager);
                break;
        }
    }
    private void ControllersDisposer()
    {
        _playerCanvasController?.Dispose();
        _inventoryCanvasController?.Dispose();
        _itemCanvasController?.Dispose();
        _gameOverCanvasController?.Dispose();
    }
    public void FixedExecute() 
    {
        _playerCanvasController?.FixedExecute();
    }
}