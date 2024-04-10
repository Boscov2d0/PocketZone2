using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameMyManager _gameMyManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private AssetLabelReference _assetLabelReference;
    [Inject] private UIController _uiController;
    private SaveLoadController _saveLoadController;

    private void Start()
    {
        _gameMyManager.GameStates.SubscribeOnChange(DeleteSave);

        Time.timeScale = 1;
        _saveLoadController = new SaveLoadController(_playerManager);
        _saveLoadController.Load();

        LoadSceneObjects();
    }
    private void OnDestroy()
    {
        _gameMyManager.GameStates.UnSubscribeOnChange(DeleteSave);
        _uiController?.Dispose();
    }
    private void DeleteSave()
    {
        if (_gameMyManager.GameStates.Value == UIStates.GameOver)
            _saveLoadController.DeleteSave();
    }

    private void LoadSceneObjects()
    {
        Addressables.LoadAssetsAsync<GameObject>(_assetLabelReference, obj =>
        {
            Instantiate(obj);
        });
    }
    private void FixedUpdate()
    {
        _uiController?.FixedExecute();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            _saveLoadController.Save();
    }
    private void OnApplicationQuit() =>
        _saveLoadController.Save();
}