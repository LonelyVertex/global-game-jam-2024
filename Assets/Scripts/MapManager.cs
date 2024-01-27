using System.Collections;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Transform _mapPlayer;
    [SerializeField] private GameObject _map;

    private MapTransition _mapTransition;
    private ScenesManager _scenesManager;

    private MapPlaceClickable[] _mapPlaces;

    private bool _movingPlayer;
    private Vector2 _targetPosition;

    private bool _shouldOpenScene;
    private string _sceneName;
    private GameScene _gameScene;

    protected void Start()
    {
        _mapTransition = FindObjectOfType<MapTransition>();
        _scenesManager = FindObjectOfType<ScenesManager>();

        _mapPlaces = GetComponentsInChildren<MapPlaceClickable>();
        foreach (var p in _mapPlaces)
        {
            p.mapPlaceClickedEvent += HandleMapPlaceClicked;
        }

        StartCoroutine(_mapTransition.SceneToMapFadeOut());
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

        StartCoroutine(MapOutTransition());
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
        _sceneName = gameStateProperty.name;
    }

    private IEnumerator MapOutTransition()
    {
        yield return _mapTransition.MapToSceneFadeIn();

        _map.SetActive(false);
        _gameScene = _scenesManager.GetScene(_sceneName);
        _gameScene.gameObject.SetActive(true);

        _gameScene.gameSceneFinishedEvent += HandleGameSceneFinished;

        yield return _mapTransition.MapToSceneFadeOut();
    }

    private IEnumerator MapInTransition()
    {
        _gameScene.gameSceneFinishedEvent -= HandleGameSceneFinished;

        yield return _mapTransition.SceneToMapFadeIn();

        _map.SetActive(true);
        _gameScene.gameObject.SetActive(false);

        yield return _mapTransition.SceneToMapFadeOut();
    }

    private void HandleGameSceneFinished()
    {
        StartCoroutine(MapInTransition());
    }
}
