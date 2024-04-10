using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private GameMyManager _gameManager;
    [SerializeField] private Image _spriteImage;
    [SerializeField] private Text _countText;
    [SerializeField] private Button _button;

    private ItemManager _item;

    public void Initialize(ItemManager item)
    {
        _item = item;

        _spriteImage.sprite = _item.IconSprite;
        if (_item.Count > 1)
        {
            _countText.text = _item.Count.ToString();
            _countText.enabled = true;
        }

        _button.onClick.AddListener(Select);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Select);
    }
    private void Select()
    {
        _gameManager.SelectedItem = _item;
        _gameManager.GameStates.Value = UIStates.Item;
    }
}