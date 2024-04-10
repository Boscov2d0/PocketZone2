using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverCanvasView : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private UnityAction _restart;

    public void Initialize(UnityAction restart) 
    {
        _restart = restart;
        _restartButton.onClick.AddListener(_restart);
    }
    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(_restart);
    }
}