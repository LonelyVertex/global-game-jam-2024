using System.Collections;
using UnityEngine;

public class HomeScene : GameScene
{
    [SerializeField] private Person _wife;
    [SerializeField] private Item _pie;
    [SerializeField] private Item _water;
    [SerializeField] private Item _flour;
    [SerializeField] private Item _strawberries;
    [SerializeField] private Item _map;

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

        yield return janek.throwingController.ThrowItem(_water, pos);
        gameState.UnsetState(_water.itemProperty.name);

        yield return janek.throwingController.ThrowItem(_flour, pos);
        gameState.UnsetState(_flour.itemProperty.name);

        yield return janek.throwingController.ThrowItem(_strawberries, pos);
        gameState.UnsetState(_strawberries.itemProperty.name);

        yield return new WaitForSeconds(1.0f);

        yield return _wife.throwingController.ThrowItem(_pie, janek.target.position);

        janek.characterController.Success();
        _wife.characterController.Success();

        gameState.SetState(_pie.itemProperty.name);

        // GAME END!

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
