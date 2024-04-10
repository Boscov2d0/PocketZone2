using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryCanvasView : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;

    [SerializeField] private Image _wearAutomatImage;
    [SerializeField] private Image _wearPistolImage;
    [SerializeField] private Image _wearHelmetImage;
    [SerializeField] private Image _wearJacketImage;
    [SerializeField] private Image _wearPantsLeftImage;
    [SerializeField] private Image _wearPantsRightImage;

    [SerializeField] private Image _wearWeaponIconImage;
    [SerializeField] private Text _wearWeaponText;
    [SerializeField] private Image _wearHelmetIconImage;
    [SerializeField] private Text _wearHelmetText;
    [SerializeField] private Image _wearJacketIconImage;
    [SerializeField] private Text _wearJacketText;
    [SerializeField] private Image _wearPantsIconImage;
    [SerializeField] private Text _wearPantsText;

    [SerializeField] private Transform _itemButtonsPanel;
    [SerializeField] private Button _closeButton;

    private List<ItemButton> _itemButtons = new List<ItemButton>();
    private UnityAction _close;

    public void Initialize(UnityAction close)
    {
        _close = close;

        _closeButton.onClick.AddListener(_close);

        SetUI();
    }
    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(_close);
        _itemButtons.Clear();
    }
    private void SetUI()
    {
        if (_playerManager.CurrentWeapon)
        {
            if (_playerManager.CurrentWeapon.Class == ItemClasses.Pistol)
            {
                _wearPistolImage.sprite = _playerManager.CurrentWeapon.IconSprite;
                _wearAutomatImage.enabled = false;
            }
            else if (_playerManager.CurrentWeapon.Class == ItemClasses.Automat)
            {
                _wearAutomatImage.sprite = _playerManager.CurrentWeapon.IconSprite;
                _wearPistolImage.enabled = false;
            }
            _wearWeaponIconImage.sprite = _playerManager.CurrentWeapon.IconSprite;
            _wearWeaponText.text = _playerManager.CurrentWeapon.Name;
        }
        else
        {
            _wearAutomatImage.enabled = false;
            _wearPistolImage.enabled = false;
            _wearWeaponIconImage.enabled = false;
        }
        if (_playerManager.CurrentHelmet)
        {
            _wearHelmetImage.sprite = _playerManager.CurrentHelmet.IconSprite;
            _wearHelmetIconImage.sprite = _playerManager.CurrentHelmet.IconSprite;
            _wearHelmetText.text = _playerManager.CurrentHelmet.Name;
        }
        else
        {
            _wearHelmetImage.enabled = false;
            _wearHelmetIconImage.enabled = false;
        }
        if (_playerManager.CurrentJacket)
        {
            _wearJacketImage.sprite = _playerManager.CurrentJacket.IconSprite;
            _wearJacketIconImage.sprite = _playerManager.CurrentJacket.IconSprite;
            _wearJacketText.text = _playerManager.CurrentJacket.Name;
        }
        else
        {
            _wearJacketImage.enabled = false;
            _wearJacketIconImage.enabled = false;
        }
        if (_playerManager.CurrentPants)
        {
            _wearPantsLeftImage.sprite = _playerManager.CurrentPants.IconSprite;
            _wearPantsRightImage.sprite = _playerManager.CurrentPants.IconSprite;
            _wearPantsIconImage.sprite = _playerManager.CurrentPants.IconSprite;
            _wearPantsText.text = _playerManager.CurrentPants.Name;
        }
        else
        {
            _wearPantsLeftImage.enabled = false;
            _wearPantsRightImage.enabled = false;
            _wearPantsIconImage.enabled = false;
        }
        for (int i = 0; i < _playerManager.BagSize; i++)
        {
            ItemButton itemButton = ResourcesLoader.InstantiateAndGetObject<ItemButton>("UI/ItemButton");
            itemButton.transform.SetParent(_itemButtonsPanel, false);
            _itemButtons.Add(itemButton);
        }

        for (int i = 0; i < _playerManager.Items.Count; i++)
            _itemButtons[i].Initialize(_playerManager.Items[i]);
    }
}