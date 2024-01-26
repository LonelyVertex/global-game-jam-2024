using UnityEngine;
using UnityEngine.InputSystem;

public class PointerManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputActionAsset _inputActions;

    private InputAction _clickAction;
    private InputAction _mousePosition;

    protected void OnEnable()
    {
        _inputActions.Enable();

        _clickAction = _inputActions["Click"];
        _mousePosition = _inputActions["MousePosition"];
    }

    protected void OnDisable()
    {
        _inputActions.Disable();
    }

    protected void Update()
    {
        if (!_clickAction.WasReleasedThisFrame())
        {
            return;
        }

        var worldPosition = _camera.ScreenToWorldPoint(_mousePosition.ReadValue<Vector2>());
        var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hit.collider == null)
        {
            return;
        }

        var clickable = hit.transform.GetComponent<Clickable>();
        if (clickable == null)
        {
            return;
        }

        clickable.Click();
    }
}
