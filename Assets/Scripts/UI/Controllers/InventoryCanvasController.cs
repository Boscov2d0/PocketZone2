public class InventoryCanvasController : ObjectsDisposer
{
    private readonly GameMyManager _manager;
    public InventoryCanvasController(GameMyManager manager, UIManager uiManager) 
    {
        _manager = manager;

        InventoryCanvasView inventoryCanvasView = ResourcesLoader.InstantiateAndGetObject<InventoryCanvasView>(uiManager.InventoryCanvasPath);
        inventoryCanvasView.Initialize(Close);
        AddGameObject(inventoryCanvasView.gameObject);
    }
    private void Close() =>
        _manager.GameStates.Value = UIStates.Player;
}