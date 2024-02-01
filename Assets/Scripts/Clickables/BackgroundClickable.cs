using UnityEngine;

public class BackgroundClickable : Clickable
{
    public event System.Action onClicked;

    public override void Click(Vector2 point)
    {
        onClicked?.Invoke();
    }
}
