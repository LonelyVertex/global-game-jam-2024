using UnityEngine;

[CreateAssetMenu(menuName = "Pie/Item")]
public class Item : ScriptableObject
{
    [Header("Data")]
    public Sprite sprite;

    [Header("ID")]
    public GameStateProperty itemProperty;

    [Header("Conditions")]
    public GameStateProperty[] onClickConditions;
}
