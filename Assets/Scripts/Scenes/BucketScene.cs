using UnityEngine;

public class BucketScene : GameScene
{
    [SerializeField] private ItemClickable _bucket;

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

    private void HandleBucketClicked(Item bucket)
    {
        StartCoroutine(PickItem(bucket, _bucket.gameObject));
    }
}
