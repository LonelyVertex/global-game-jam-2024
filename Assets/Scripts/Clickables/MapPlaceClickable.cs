using UnityEngine;

public class MapPlaceClickable : Clickable
{
    [SerializeField] private GameStateProperty _gameStateProperty;

    public event System.Action<GameStateProperty, Vector2> mapPlaceClickedEvent;

    public override void Click(Vector2 hitPoint)
    {
        mapPlaceClickedEvent?.Invoke(_gameStateProperty, hitPoint);
    }
}
