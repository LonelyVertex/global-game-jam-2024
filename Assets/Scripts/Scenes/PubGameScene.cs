using System.Collections;
using UnityEngine;

public class PubGameScene : GameScene
{
    [Header("Miller")]
    [SerializeField] private Person _miller;
    [SerializeField] private Item _potion;
    [SerializeField] private Item _flour;

    [Header("Innkeeper")]
    [SerializeField] private Person _innkeeper;
    [SerializeField] private Item _money;
    [SerializeField] private Item _beer;

    protected override void HandleInventoryItemClicked(Item item)
    {
        switch (item.itemProperty.name)
        {
            case GameStateProperties.ItemPotion:
                TransferMillerItem();
                return;
            case GameStateProperties.ItemMoney:
                TransferInnkeeperItem();
                return;
            default:
                StartCoroutine(Fail());
                return;
        }
    }

    protected override void HandleBackgroundOnClicked()
    {
        StartCoroutine(FailOnly());
    }

    private IEnumerator Fail()
    {
        eventSystem.enabled = false;

        yield return FailOnly();

        eventSystem.enabled = true;
    }

    private IEnumerator FailOnly()
    {
        FailAndShowHintIfNeeded(_innkeeper, GameStateProperties.PersonInnkeeperHappy, _money);

        yield return new WaitForSeconds(1.0f);

        FailAndShowHintIfNeeded(_miller, GameStateProperties.PersonMillerHappy, _potion);
    }

    private void TransferMillerItem()
    {
        StartCoroutine(
            TransferItem(_potion, _miller, GameStateProperties.PersonMillerHappy, _flour)
        );
    }

    private void TransferInnkeeperItem()
    {
        StartCoroutine(
            TransferItem(_money, _innkeeper, GameStateProperties.PersonInnkeeperHappy, _beer)
        );
    }
}
