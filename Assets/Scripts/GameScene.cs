using UnityEngine;

public abstract class GameScene : MonoBehaviour
{
    public GameStateProperty gameSceneProperty;

    public event System.Action gameSceneFinishedEvent;

    protected GameState gameState;
    protected Inventory inventory;

    protected void Awake()
    {
        gameState = FindObjectOfType<GameState>();
        inventory = FindObjectOfType<Inventory>();

        inventory.itemClickedEvent += HandleInventoryItemClicked;
        inventory.mapClickedEvent += HandleInventoryMapClicked;
    }

    protected virtual void HandleInventoryItemClicked(Item item) { }

    private void HandleInventoryMapClicked()
    {
        gameSceneFinishedEvent?.Invoke();
    }
}
