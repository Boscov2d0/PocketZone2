using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCanvasView : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Button _fireButton;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Scrollbar _hpScrollbar;
    [SerializeField] private FixedJoystick _fixedJoystick;

    private UnityAction _fire;
    private UnityAction _inventory;
    private UnityAction _exit;

    public void Initialize(UnityAction fire, UnityAction inventory, UnityAction exit) 
    {
        _fire = fire;
        _inventory = inventory;
        _exit = exit;

        _fireButton.onClick.AddListener(_fire);
        _inventoryButton.onClick.AddListener(_inventory);
        _exitButton.onClick.AddListener(_exit);

        _playerManager.HP.SubscribeOnChange(ShowUI);
        ShowUI();
    }
    public PlayerManager GetManager() 
    {
        return _playerManager;
    }
    public FixedJoystick GetJoystick()
    {
        return _fixedJoystick;
    }
    private void OnDestroy()
    {
        _playerManager.HP.UnSubscribeOnChange(ShowUI);
        _fireButton.onClick.RemoveListener(_fire);
        _inventoryButton.onClick.RemoveListener(_inventory);
        _exitButton.onClick.RemoveListener(_exit);
    }
    private void ShowUI() =>
        _hpScrollbar.size = _playerManager.HP.Value / 100;
}