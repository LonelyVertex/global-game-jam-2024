using UnityEngine;
using UnityEngine.EventSystems;

public class MapPlaceClickable : Clickable, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameStateProperty _gameStateProperty;

    [Space]
    [SerializeField] private Animation _animation;

    public event System.Action<GameStateProperty, Vector2> mapPlaceClickedEvent;

    public override void Click(Vector2 hitPoint)
    {
        mapPlaceClickedEvent?.Invoke(_gameStateProperty, hitPoint);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_animation == null)
        {
            return;
        }

        _animation.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_animation == null)
        {
            return;
        }

        _animation.Stop();
    }
}
