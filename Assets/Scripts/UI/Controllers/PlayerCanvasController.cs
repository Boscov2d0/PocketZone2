using UnityEngine;

public class PlayerCanvasController : ObjectsDisposer
{
    private readonly GameMyManager _gameMyManager;
    private readonly PlayerManager _playerManager;
    private readonly FixedJoystick _fixedJoystick;

    private PlayerCanvasView _playerCanvasView;

    public PlayerCanvasController(GameMyManager gameMyManager, UIManager uiManager) 
    {
        _gameMyManager = gameMyManager;
        _playerCanvasView = ResourcesLoader.InstantiateAndGetObject<PlayerCanvasView>(uiManager.PlayerCanvasPath);
        _playerCanvasView.Initialize(Fire, OpenInventory, Exit);
        AddGameObject(_playerCanvasView.gameObject);

        _fixedJoystick = _playerCanvasView.GetJoystick();
        _playerManager = _playerCanvasView.GetManager();
    }
    public void FixedExecute() 
    {
        _playerManager.Horizontal.Value = _fixedJoystick.Horizontal;
        _playerManager.Vertical.Value = _fixedJoystick.Vertical;
    }
    private void Fire() => _playerManager.States.Value = PlayerStates.Attack;
    private void OpenInventory() => _gameMyManager.GameStates.Value = UIStates.Inventory;
    private void Exit() => Application.Quit();
}