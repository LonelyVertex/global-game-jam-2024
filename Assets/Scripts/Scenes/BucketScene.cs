using UnityEngine;

public class BucketScene : GameScene
{
    [SerializeField] private ItemClickable _bucket;
    [SerializeField] private Item _bucketItem;

    protected override void OnEnable()
    {
        base.OnEnable();

        _bucket.clickSuccessfulEvent += HandleBucketClicked;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _bucket.clickSuccessfulEvent -= HandleBucketClicked;
    }

    protected override void HandleBackgroundOnClicked()
    {
        FailAndShowHintIfNeeded(janek, GameStateProperties.ItemBucket, _bucketItem);
    }

    private void HandleBucketClicked(Item bucket)
    {
        StartCoroutine(PickItem(bucket, _bucket.gameObject));
    }
}
