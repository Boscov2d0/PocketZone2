public class ItemCanvasController : ObjectsDisposer
{
    private readonly GameMyManager _gameManager;
    private readonly PlayerManager _playerManager;

    public ItemCanvasController(GameMyManager gameManager, UIManager uiManager, PlayerManager playerManager)
    {
        _gameManager = gameManager;
        _playerManager = playerManager;

        ItemCanvasView itemCanvasView = ResourcesLoader.InstantiateAndGetObject<ItemCanvasView>(uiManager.ItemCanvasPath);
        itemCanvasView.Initialize(Apply, Delete, Close);
        AddGameObject(itemCanvasView.gameObject);
    }
    private void Apply()
    {
        switch (_gameManager.SelectedItem.Class)
        {
            case ItemClasses.Null:
                break;
            case ItemClasses.Automat:
                _playerManager.CurrentWeapon = (WeaponManager)_gameManager.SelectedItem;
                break;
            case ItemClasses.Pistol:
                _playerManager.CurrentWeapon = (WeaponManager)_gameManager.SelectedItem;
                break;
            case ItemClasses.Helmet:
                _playerManager.CurrentHelmet = (ItemManager)_gameManager.SelectedItem;
                break;
            case ItemClasses.Jacket:
                _playerManager.CurrentJacket = (ItemManager)_gameManager.SelectedItem;
                break;
            case ItemClasses.Pants:
                _playerManager.CurrentPants = (ItemManager)_gameManager.SelectedItem;
                break;
        }
        _playerManager.PutOnItem?.Invoke();
        Close();
    }
    private void Delete()
    {
        for (int i = 0; i < _playerManager.Items.Count; i++)
        {
            if (_playerManager.Items[i].Name == _gameManager.SelectedItem.Name)
            {
                _playerManager.Items[i].Count -= _gameManager.SelectedItem.Count;
                if (_playerManager.Items[i].Count <= 0)
                    _playerManager.Items.Remove(_playerManager.Items[i]);

                switch (_gameManager.SelectedItem.Class)
                {
                    case ItemClasses.Automat:
                        _playerManager.CurrentWeapon = null;
                        break;
                    case ItemClasses.Pistol:
                        _playerManager.CurrentWeapon = null;
                        break;
                    case ItemClasses.Helmet:
                        _playerManager.CurrentHelmet = null;
                        break;
                    case ItemClasses.Jacket:
                        _playerManager.CurrentJacket = null;
                        break;
                    case ItemClasses.Pants:
                        _playerManager.CurrentPants = null;
                        break;
                }
                _playerManager.PutOnItem?.Invoke();
                Close();
                return;
            }
        }
    }
    private void Close() =>
        _gameManager.GameStates.Value = UIStates.Inventory;
}