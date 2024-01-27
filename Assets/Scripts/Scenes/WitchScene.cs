using System.Collections;
using UnityEngine;

public class WitchScene : GameScene
{
    [SerializeField] private Janek _janek;
    [SerializeField] private Witch _witch;
    [SerializeField] private Transform _witchTarget;
    [SerializeField] private Transform _janekTarget;
    [SerializeField] private Item _potion;

    protected override void HandleInventoryItemClicked(Item item)
    {
        if (item.itemProperty.name != GameStateProperties.ItemFrog)
        {
            _witch.characterController.Fail();
            return;
        }

        StartCoroutine(TransferItem(item));
    }

    private IEnumerator TransferItem(Item item)
    {
        gameState.UnsetState(item.itemProperty.name);

        yield return _janek.throwingController.ThrowItem(item, _witchTarget.position);

        _witch.characterController.Success();

        yield return new WaitForSeconds(CharacterController.SuccessDelay);

        StartCoroutine(_witch.throwingController.ThrowItem(_potion, _janekTarget.position));

        yield return new WaitForSeconds(_witch.throwingController.duration - CharacterController.CatchDelay);

        _janek.characterController.Catch();

        gameState.SetState(_potion.itemProperty.name);
    }
}
