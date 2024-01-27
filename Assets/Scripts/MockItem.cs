using UnityEngine;

public class MockItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetItem(Item item)
    {
        _spriteRenderer.sprite = item.sprite;
    }
}
