using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemCanvasView : MonoBehaviour
{
    [SerializeField] private GameMyManager _gameManager;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _closeButton;

    private UnityAction _apply;
    private UnityAction _delete;
    private UnityAction _close;

    public void Initialize(UnityAction apply,  UnityAction delete, UnityAction close) 
    {
        _apply = apply;
        _delete = delete;
        _close = close;

        _nameText.text = _gameManager.SelectedItem.Name;
        _descriptionText.text = _gameManager.SelectedItem.Description;
        _iconImage.sprite = _gameManager.SelectedItem.IconSprite;

        _applyButton.onClick.AddListener(_apply);
        _deleteButton.onClick.AddListener(_delete);
        _closeButton.onClick.AddListener(_close);
    }
    private void OnDestroy()
    {
        _applyButton.onClick.RemoveListener(_apply);
        _deleteButton.onClick.RemoveListener(_delete);
        _closeButton.onClick.RemoveListener(_close);
    }
}