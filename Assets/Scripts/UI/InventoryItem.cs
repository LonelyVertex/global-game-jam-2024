using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _itemImage;

    public event System.Action<Item> inventoryItemClicked;

    private Item _item;

    public void SetItem(Item item)
    {
        _item = item;

        _itemImage.sprite = item.uiSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryItemClicked?.Invoke(_item);
    }
}
