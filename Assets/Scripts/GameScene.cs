using UnityEngine;

public abstract class GameScene : MonoBehaviour
{
    public GameStateProperty gameSceneProperty;

    private GameState _gameState;
    private Inventory _inventory;

    protected void Start()
    {
        _gameState = FindObjectOfType<GameState>();
        _inventory = FindObjectOfType<Inventory>();

        _inventory.itemClickedEvent += HandleItemClicked;
        _inventory.mapClickedEvent += HandleMapClicked;
    }

    private void HandleItemClicked(Item item)
    {
        // Do some item handling
    }

    private void HandleMapClicked()
    {
        // Close current scene
    }
}
