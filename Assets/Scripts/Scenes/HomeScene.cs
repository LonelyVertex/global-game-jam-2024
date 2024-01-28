using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : GameScene
{
    [SerializeField] private Person _wife;
    [SerializeField] private Item _pie;
    [SerializeField] private Item _water;
    [SerializeField] private Item _flour;
    [SerializeField] private Item _strawberries;
    [SerializeField] private Item _map;

    [SerializeField] private SceneReference _gameEndScene;

    private MapTransition _mapTransition;

    protected override void Awake()
    {
        base.Awake();

        _mapTransition = FindObjectOfType<MapTransition>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (!gameState.IsStateOn(GameStateProperties.ItemMap))
        {
            StartCoroutine(ShowPie());
        }
        else if (!gameState.IsStateOn(_water.itemProperty.name) ||
                 !gameState.IsStateOn(_flour.itemProperty.name) ||
                 !gameState.IsStateOn(_strawberries.itemProperty.name)
        )
        {
            _wife.hintController.Show(_water, _flour, _strawberries);
        }
        else if (gameState.IsStateOn(_water.itemProperty.name) &&
                 gameState.IsStateOn(_flour.itemProperty.name) &&
                 gameState.IsStateOn(_strawberries.itemProperty.name))
        {
             StartCoroutine(GivePie());
        }
    }

    private IEnumerator GivePie()
    {
        eventSystem.enabled = false;

        var pos = _wife.target.position;

        yield return new WaitForSeconds(0.5f);

        eventSystem.enabled = false;

        yield return janek.throwingController.ThrowItem(_water, pos);
        gameState.UnsetState(_water.itemProperty.name);

        eventSystem.enabled = false;

        yield return janek.throwingController.ThrowItem(_flour, pos);
        gameState.UnsetState(_flour.itemProperty.name);

        eventSystem.enabled = false;

        yield return janek.throwingController.ThrowItem(_strawberries, pos);
        gameState.UnsetState(_strawberries.itemProperty.name);

        eventSystem.enabled = false;

        yield return new WaitForSeconds(1.0f);

        eventSystem.enabled = false;

        yield return _wife.throwingController.ThrowItem(_pie, janek.target.position);

        janek.audioController.PlaySuccess();
        janek.characterController.Success();
        _wife.audioController.PlaySuccess();
        _wife.characterController.Success();

        gameState.SetState(_pie.itemProperty.name);

        yield return new WaitForSeconds(1.5f);

        yield return _mapTransition.SceneToMapFadeIn();

        SceneManager.LoadScene(_gameEndScene);

        eventSystem.enabled = true;
    }

    private IEnumerator ShowPie()
    {
        eventSystem.enabled = false;

        yield return new WaitForSeconds(1.0f);

        janek.hintController.Show(_pie);

        yield return new WaitForSeconds(3.0f);

        _wife.hintController.Show(_water, _flour, _strawberries);

        yield return new WaitForSeconds(3.0f);

        yield return _wife.throwingController.ThrowItem(_map, janek.target.position);

        gameState.SetState(GameStateProperties.ItemMap);

        eventSystem.enabled = true;
    }
}
