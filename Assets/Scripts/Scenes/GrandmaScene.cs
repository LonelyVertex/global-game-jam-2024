using System.Collections;
using UnityEngine;

public class GrandmaScene : GameScene
{
    [Header("Scythe")]
    [SerializeField] private ItemClickable _scytheClickable;
    [SerializeField] private Item _scythe;
    [SerializeField] private Item _money;

    [Header("Grandma")]
    [SerializeField] private Person _grandma;
    [SerializeField] private Item _beer;

    protected override void OnEnable()
    {
        base.OnEnable();

        _scytheClickable.clickSuccessfulEvent += HandleScytheClickSuccessful;
        _scytheClickable.clickFailedEvent += HandleScytheClickFailed;
    }

    protected override void HandleBackgroundOnClicked()
    {
        FailAndShowHintIfNeeded(janek, "", _scythe);
    }

    protected override void HandleInventoryItemClicked(Item item)
    {
        switch (item.itemProperty.name)
        {
            case GameStateProperties.ItemBeer:
                StartCoroutine(
                    TransferItem(_beer, _grandma, GameStateProperties.PersonGranHappy, null)
                );
                return;
            default:
                FailAndShowHintIfNeeded(_grandma, GameStateProperties.PersonGranHappy, _beer);
                return;
        }
    }

    private void HandleScytheClickFailed()
    {
        if (
            gameState.IsStateOn(_money.itemProperty.name) ||
            gameState.IsStateOn(_beer.itemProperty.name)
        ) {
            FailAndShowHintIfNeeded(_grandma, "", _beer);

            return;
        }

        StartCoroutine(WantBeer());
    }

    private void HandleScytheClickSuccessful(Item scythe)
    {
        StartCoroutine(PickItem(scythe, _scytheClickable.gameObject));
    }

    private IEnumerator WantBeer()
    {
        eventSystem.enabled = false;

        _grandma.characterController.Fail();

        yield return new WaitForSeconds(CharacterController.FailDelay);

        _grandma.hintController.Show(_beer);

        yield return new WaitForSeconds(_grandma.hintController.showDelay);

        yield return TransferItemOneWay(_grandma, _money);

        eventSystem.enabled = true;
    }

    private IEnumerator TransferItemOneWay(Person recipient, Item itemToGive)
    {
        eventSystem.enabled = false;

        StartCoroutine(recipient.throwingController.ThrowItem(itemToGive, janek.target.position));

        yield return new WaitForSeconds(recipient.throwingController.duration - CharacterController.CatchDelay);

        janek.characterController.Catch();

        gameState.SetState(itemToGive.itemProperty.name);

        eventSystem.enabled = true;
    }
}
