using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemImage;

    public event System.Action<Item> inventoryItemClicked;

    public GameStateProperty property => _item.itemProperty;

    private Item _item;

    public void SetItem(Item item)
    {
        _item = item;

        _itemImage.sprite = item.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryItemClicked?.Invoke(_item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
