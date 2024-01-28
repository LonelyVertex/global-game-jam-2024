using System.Collections;
using UnityEngine;

public class StrawberryPatchScene : GameScene
{
    [Header("Leaf")]
    [SerializeField] private ItemClickable _leafClickable;

    [Header("Bush")]
    [SerializeField] private GameObject _bush;
    [SerializeField] private Item _scythe;
    [SerializeField] private Animation _bushAnimation;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

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
        StartCoroutine(PickItem(leaf, _leafClickable.gameObject));
    }

    private void HandleStrawberriesClickSuccess(Item strawberry)
    {
        gameState.SetState(GameStateProperties.StateNoStrawberries);

        StartCoroutine(PickItem(strawberry, _strawberries.gameObject));
    }

    private void HandleStrawberriesClickFail()
    {
        FailAndShowHintIfNeeded(janek, GameStateProperties.ItemStrawberries, _scythe);
    }

    protected override void HandleInventoryItemClicked(Item item)
    {
        if (item.itemProperty.name == GameStateProperties.ItemScythe)
        {
            StartCoroutine(ThrowScytheAndCutBush());
        }
        else
        {
            FailAndShowHintIfNeeded(janek, GameStateProperties.StateNoStrawberries, _scythe);
        }
    }

    private IEnumerator ThrowScytheAndCutBush()
    {
        eventSystem.enabled = false;

        yield return janek.throwingController.ThrowItem(_scythe, _bush.transform.position);

        gameState.SetState(GameStateProperties.StateBushCut);
        gameState.UnsetState(GameStateProperties.ItemScythe);

        _audioSource.PlayOneShot(_audioClip);
        _bushAnimation.Play();
        yield return new WaitForSeconds(_bushAnimation.clip.length);

        _bush.SetActive(false);

        eventSystem.enabled = true;
    }
}
