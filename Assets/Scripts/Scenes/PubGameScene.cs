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
                FailAndShowHintIfNeeded(_miller, GameStateProperties.PersonMillerHappy, _potion);
                FailAndShowHintIfNeeded(_innkeeper, GameStateProperties.PersonInnkeeperHappy, _money);
                return;
        }
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
