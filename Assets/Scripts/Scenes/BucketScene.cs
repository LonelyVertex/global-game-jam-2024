using UnityEngine;

public class BucketScene : GameScene
{
    [SerializeField] private ItemClickable _bucket;

    protected void OnEnable()
    {
        _bucket.gameObject.SetActive(!gameState.IsStateOn(GameStateProperties.ItemBucket));

        _bucket.clickSuccessfulEvent += HandleBucketClicked;
    }

    protected void OnDisable()
    {
        _bucket.clickSuccessfulEvent -= HandleBucketClicked;
    }

    private void HandleBucketClicked(Item bucket)
    {
        _bucket.gameObject.SetActive(false);

        gameState.SetState(bucket.itemProperty.name);
    }
}
