using UnityEngine;

public class BucketScene : GameScene
{
    [SerializeField] private ItemClickable _bucket;

    protected override void OnEnable()
    {
        base.OnEnable();

        _bucket.gameObject.SetActive(!gameState.IsStateOn(GameStateProperties.ItemBucket));

        _bucket.clickSuccessfulEvent += HandleBucketClicked;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _bucket.clickSuccessfulEvent -= HandleBucketClicked;
    }

    private void HandleBucketClicked(Item bucket)
    {
        _bucket.gameObject.SetActive(false);

        gameState.SetState(bucket.itemProperty.name);
    }
}
