using System.Linq;
using UnityEngine;

public class ItemClickable : Clickable
{
    [SerializeField] private Item _item;

    public event System.Action<Item> clickSuccessfulEvent;
    public event System.Action clickFailedEvent;

    private GameState _gameState;

    protected void OnEnable()
    {
        _gameState = FindObjectOfType<GameState>();
    }

    public override void Click(Vector2 hitPoint)
    {
        if (!_item.onClickConditions.All(c => _gameState.IsStateOn(c.name)))
        {
            clickFailedEvent?.Invoke();

            return;
        }

        clickSuccessfulEvent?.Invoke(_item);
    }
}
