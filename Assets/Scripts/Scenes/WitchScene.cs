using UnityEngine;

public class WitchScene : GameScene
{
    [SerializeField] private Janek _janek;
    [SerializeField] private Witch _witch;
    [SerializeField] private Transform _target;

    protected override void HandleInventoryItemClicked(Item item)
    {
        if (item.itemProperty.name != GameStateProperties.ItemFrog)
        {
            _witch.characterController.Fail();
            return;
        }

        StartCoroutine(_janek.throwingController.ThrowItem(item, _target.position));
    }
}
