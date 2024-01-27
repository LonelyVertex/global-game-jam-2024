using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Transform _mapPlayer;

    private MapPlaceClickable[] _mapPlaces;

    private bool _movingPlayer;
    private bool _shouldOpenScene;
    private Vector2 _targetPosition;
    
    protected void Start()
    {
        _mapPlaces = GetComponentsInChildren<MapPlaceClickable>();
        foreach (var p in _mapPlaces)
        {
            p.mapPlaceClickedEvent += HandleMapPlaceClicked;
        }
    }

    protected void Update()
    {
        if (!_movingPlayer)
        {
            return;
        }

        _mapPlayer.position =Vector2.MoveTowards(
            _mapPlayer.position,
            _targetPosition,
            1.0f
        );

        if (!Mathf.Approximately(_mapPlayer.position.x, _targetPosition.x) ||
            !Mathf.Approximately(_mapPlayer.position.y, _targetPosition.y))
        {
            return;
        }

        _movingPlayer = false;

        if (!_shouldOpenScene)
        {
            return;
        }

        // Open Scene
    }

    private void HandleMapPlaceClicked(GameStateProperty gameStateProperty, Vector2 point)
    {
        if (_movingPlayer)
        {
            return;
        }
        _movingPlayer = true;

        _targetPosition = point;
        _shouldOpenScene = gameStateProperty.name != GameStateProperties.PlaceMap;
    }
}
