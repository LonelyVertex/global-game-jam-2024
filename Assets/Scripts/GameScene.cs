using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GameScene : MonoBehaviour
{
    [Header("Janek")]
    [SerializeField] protected Person janek;
    [SerializeField] protected Transform janekTarget;

    public GameStateProperty gameSceneProperty;

    public event Action gameSceneFinishedEvent;

    protected EventSystem eventSystem;
    protected GameState gameState;
    protected Inventory inventory;

    protected void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        gameState = FindObjectOfType<GameState>();
        inventory = FindObjectOfType<Inventory>();
    }

    protected virtual void OnEnable()
    {
        inventory.itemClickedEvent += HandleInventoryItemClicked;
        inventory.mapClickedEvent += HandleInventoryMapClicked;
    }

    protected virtual void OnDisable()
    {
        inventory.itemClickedEvent -= HandleInventoryItemClicked;
        inventory.mapClickedEvent -= HandleInventoryMapClicked;
    }

    protected virtual void HandleInventoryItemClicked(Item item) { }

    protected void FailAndShowHintIfNeeded(Person person, string state, Item item)
    {
        person.characterController.Fail();

        if (!gameState.IsStateOn(state))
        {
            person.hintController.Show(item);
        }
    }

    protected IEnumerator TransferItem(
        Item itemToTransfer,
        Person recipient,
        string stateToSet,
        Item itemToGive
    )
    {
        eventSystem.enabled = false;

        gameState.UnsetState(itemToTransfer.itemProperty.name);

        yield return janek.throwingController.ThrowItem(itemToTransfer, recipient.target.position);

        recipient.characterController.Success();
        gameState.SetState(stateToSet);

        yield return new WaitForSeconds(CharacterController.SuccessDelay);

        if (itemToGive == null)
        {
            eventSystem.enabled = true;
            yield break;
        }

        StartCoroutine(recipient.throwingController.ThrowItem(itemToGive, janekTarget.position));

        yield return new WaitForSeconds(recipient.throwingController.duration - CharacterController.CatchDelay);

        janek.characterController.Catch();

        gameState.SetState(itemToGive.itemProperty.name);

        eventSystem.enabled = true;
    }

    private void HandleInventoryMapClicked()
    {
        gameSceneFinishedEvent?.Invoke();
    }
}
