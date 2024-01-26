using UnityEngine;

[CreateAssetMenu(menuName = "Pie/Item")]
public class Item : ScriptableObject
{
    [Header("Data")]
    public Sprite gameSprite;
    public Sprite uiSprite;

    [Header("Conditions")]
    public Condition[] onClickConditions;
}
