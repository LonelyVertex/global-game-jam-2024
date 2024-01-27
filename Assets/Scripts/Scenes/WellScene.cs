using System.Collections;
using UnityEngine;

public class WellScene : GameScene
{
    [SerializeField] private Item _leaf;
    [SerializeField] private Item _frog;
    [SerializeField] private Item _bucket;
    [SerializeField] private Item _water;
    [SerializeField] private GameObject _frogGameObject;
    [SerializeField] private Transform _frogTarget;

    protected override void HandleInventoryItemClicked(Item item)
    {
        if (gameState.IsStateOn(GameStateProperties.StateWellEmpty))
        {
            janek.characterController.Fail();
            return;
        }

        if (item.itemProperty.name == GameStateProperties.ItemLeaf)
        {
            StartCoroutine(ThrowLeafAndPickUpFrog());
            return;
        }

        if (item.itemProperty.name == GameStateProperties.ItemBucket &&
            gameState.IsStateOn(GameStateProperties.StateWellWithoutFrog))
        {
            StartCoroutine(ThrowBucketAndPickUpWater());
            return;
        }

        if (!gameState.IsStateOn(GameStateProperties.StateWellWithoutFrog))
        {
            FailAndShowHintIfNeeded(janek, GameStateProperties.StateWellWithoutFrog, _leaf);
        }
        else if (!gameState.IsStateOn(GameStateProperties.ItemWater))
        {
            FailAndShowHintIfNeeded(janek, "", _bucket);
        }
        else
        {
            janek.characterController.Fail();
        }
    }

    private IEnumerator ThrowLeafAndPickUpFrog()
    {
        eventSystem.enabled = false;

        yield return janek.throwingController.ThrowItem(_leaf, _frogTarget.position);

        _frogGameObject.SetActive(false);

        yield return janek.throwingController.ReverseThrowItem(_frog, _frogGameObject.transform.position);

        gameState.SetState(GameStateProperties.StateWellWithoutFrog);
        gameState.SetState(_frog.itemProperty.name);
        gameState.UnsetState(_leaf.itemProperty.name);

        eventSystem.enabled = true;
    }

    private IEnumerator ThrowBucketAndPickUpWater()
    {
        eventSystem.enabled = false;

        var pos = _frogTarget.position;

        yield return janek.throwingController.ThrowItem(_bucket, pos);
        yield return janek.throwingController.ReverseThrowItem(_water, pos);

        gameState.SetState(_water.itemProperty.name);
        gameState.UnsetState(_bucket.itemProperty.name);

        eventSystem.enabled = true;
    }
}
