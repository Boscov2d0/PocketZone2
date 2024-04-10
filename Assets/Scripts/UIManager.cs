using UnityEngine;

[CreateAssetMenu(menuName = "Managers/" + nameof(UIManager), fileName = nameof(UIManager))]
public class UIManager : ScriptableObject
{
    [field: SerializeField] public string PlayerCanvasPath { get; private set; }
    [field: SerializeField] public string InventoryCanvasPath { get; private set; }
    [field: SerializeField] public string ItemCanvasPath { get; private set; }
    [field: SerializeField] public string GameOverCanvasPath { get; private set; }
}