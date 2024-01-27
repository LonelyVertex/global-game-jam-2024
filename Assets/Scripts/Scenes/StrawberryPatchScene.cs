using System.Collections;
using UnityEngine;

public class StrawberryPatchScene : GameScene
{
    [Header("Leaf")]
    [SerializeField] private ItemClickable _leafClickable;

    [Header("Bush")]
    [SerializeField] private GameObject _bush;
    [SerializeField] private Item _scythe;

    [Header("Strawberry")]
    [SerializeField] private ItemClickable _strawberries;

    protected override void OnEnable()
    {
        base.OnEnable();

        _leafClickable.clickSuccessfulEvent += HandleLeafClicked;

        _strawberries.clickSuccessfulEvent += HandleStrawberriesClickSuccess;
        _strawberries.clickFailedEvent += HandleStrawberriesClickFail;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _leafClickable.clickSuccessfulEvent -= HandleLeafClicked;

        _strawberries.clickSuccessfulEvent -= HandleStrawberriesClickSuccess;
        _strawberries.clickFailedEvent -= HandleStrawberriesClickFail;
    }

    private void HandleLeafClicked(Item leaf)
    {
        _leafClickable.gameObject.SetActive(false);

        gameState.SetState(leaf.itemProperty.name);
    }

    private void HandleStrawberriesClickSuccess(Item strawberry)
    {
        _strawberries.gameObject.SetActive(false);
        gameState.SetState(GameStateProperties.ItemStrawberries);
    }

    private void HandleStrawberriesClickFail()
    {
        // Shake
    }

    protected override void HandleInventoryItemClicked(Item item)
    {
        if (item.itemProperty.name == GameStateProperties.ItemScythe)
        {
            StartCoroutine(ThrowScytheAndCutBush());
        }
    }

    private IEnumerator ThrowScytheAndCutBush()
    {
        eventSystem.enabled = false;

        yield return janek.throwingController.ThrowItem(_scythe, _bush.transform.position);

        _bush.SetActive(false);

        gameState.SetState(GameStateProperties.StateBushCut);
        gameState.UnsetState(GameStateProperties.ItemScythe);

        eventSystem.enabled = true;
    }
}
