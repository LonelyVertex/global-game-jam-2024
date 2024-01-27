using UnityEngine;

public class WitchScene : GameScene
{
    [Header("Witch")]
    [SerializeField] private Person _witch;
    [SerializeField] private Item _potion;
    [SerializeField] private Item _frog;

    protected override void HandleInventoryItemClicked(Item item)
    {
        if (item.itemProperty.name != GameStateProperties.ItemFrog)
        {
            FailAndShowHintIfNeeded(_witch, GameStateProperties.PersonInnkeeperHappy, _frog);
            return;
        }

        StartCoroutine(
            TransferItem(_frog, _witch, GameStateProperties.PersonWitchHappy, _potion)
        );
    }
}
