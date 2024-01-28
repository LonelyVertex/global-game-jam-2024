using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickAudioClip;

    [Space]
    [SerializeField] private InventorySO _inventory;

    [Space]
    [SerializeField] private Transform _inventoryContainer;
    [SerializeField] private InventoryItem _inventoryItemPrefab;

    public event System.Action mapClickedEvent;
    public event System.Action<Item> itemClickedEvent;

    private GameState _gameState;
    private InventoryItem[] _inventoryItems;

    protected void Start()
    {
        _gameState = FindObjectOfType<GameState>();
        _gameState.gameStateUpdatedEvent += HandleGameStateUpdated;

        _inventoryItems = new InventoryItem[_inventory.items.Length];
        for (var i = 0; i < _inventory.items.Length; i++)
        {
            var item = Instantiate(_inventoryItemPrefab, parent: _inventoryContainer);

            item.SetItem(_inventory.items[i]);
            item.inventoryItemClicked += HandleInventoryItemClicked;
            item.gameObject.SetActive(_gameState.IsStateOn(_inventory.items[i].itemProperty.name));

            _inventoryItems[i] = item;
        }
    }

    private void HandleInventoryItemClicked(Item item)
    {
        _audioSource.PlayOneShot(_clickAudioClip);

        if (item.itemProperty.name == GameStateProperties.ItemMap)
        {
            mapClickedEvent?.Invoke();
            return;
        }

        itemClickedEvent?.Invoke(item);
    }

    private void HandleGameStateUpdated()
    {
        foreach (var i in _inventoryItems)
        {
            i.gameObject.SetActive(_gameState.IsStateOn(i.property.name));
        }
    }
}
