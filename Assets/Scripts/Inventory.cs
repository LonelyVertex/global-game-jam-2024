using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Item[] _items;

    [Space]
    [SerializeField] private Transform _inventoryContainer;
    [SerializeField] private InventoryItem _inventoryItemPrefab;

    public event System.Action mapClickedEvent;
    public event System.Action<Item> itemClickedEvent;

    private InventoryItem[] _inventoryItems;

    protected void Start()
    {
        _inventoryItems = new InventoryItem[_items.Length];
        for (var i = 0; i < _items.Length; i++)
        {
            var item = Instantiate(_inventoryItemPrefab, parent: _inventoryContainer);

            item.SetItem(_items[i]);
            item.inventoryItemClicked += HandleInventoryItemClicked;

            _inventoryItems[i] = item;
        }
    }

    private void HandleInventoryItemClicked(Item item)
    {
        if (item.itemProperty.name == GameStateProperties.ItemMap)
        {
            mapClickedEvent?.Invoke();
            return;
        }

        itemClickedEvent?.Invoke(item);
    }
}
