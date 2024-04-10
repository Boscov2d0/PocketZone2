public class SaveLoadController : ObjectsDisposer
{
    private readonly PlayerManager _playerManager;
    private const string _path = "/JSONGameData.json";

    public SaveLoadController(PlayerManager playerManager)
    {
        _playerManager = playerManager;
    }

    public void Load()
    {
        GameData gameData = new GameData();
        gameData = JSONDataLoadSaver<GameData>.Load(_path);
        _playerManager.HP.Value = gameData.HP;
        _playerManager.CurrentWeapon = gameData.CurrentWeapon;
        _playerManager.CurrentHelmet = gameData.CurrentHelmet;
        _playerManager.CurrentJacket = gameData.CurrentJacket;
        _playerManager.CurrentPants = gameData.CurrentPants;
        _playerManager.Items = gameData.Items;
    }
    public void Save()
    {
        GameData gameData = new GameData();
        gameData.HP = _playerManager.HP.Value;
        gameData.CurrentWeapon = _playerManager.CurrentWeapon;
        gameData.CurrentHelmet = _playerManager.CurrentHelmet;
        gameData.CurrentJacket = _playerManager.CurrentJacket;
        gameData.CurrentPants = _playerManager.CurrentPants;
        gameData.Items = _playerManager.Items;
        JSONDataLoadSaver<GameData>.SaveData(gameData, _path);
    }
    public void DeleteSave() 
    {
        _playerManager.HP.Value = 100;
        _playerManager.CurrentWeapon = null;
        _playerManager.CurrentHelmet = null;
        _playerManager.CurrentJacket = null;
        _playerManager.CurrentPants = null;
        _playerManager.Items.Clear();
        Save();
    }
}