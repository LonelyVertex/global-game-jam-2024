using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PointerManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputActionAsset _inputActions;

    private EventSystem _eventSystem;

    private InputAction _clickAction;
    private InputAction _mousePosition;

    private RaycastHit2D[] _hits = new RaycastHit2D[2];

    protected void Awake()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
    }

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
        if (
            !_eventSystem.enabled ||
            !_clickAction.WasReleasedThisFrame()
        )
        {
            return;
        }

        var worldPosition = _camera.ScreenToWorldPoint(_mousePosition.ReadValue<Vector2>());
        var size = Physics2D.RaycastNonAlloc(worldPosition, Vector2.zero, _hits);
        var maxDepth = float.MinValue;
        RaycastHit2D hit = default;
        for (var i = 0; i < size; i++)
        {
            if (!(_hits[i].transform.position.z > maxDepth)) continue;

            maxDepth = _hits[i].transform.position.z;
            hit = _hits[i];
        }

        if (hit.collider == null)
        {
            return;
        }

        var clickable = hit.transform.GetComponent<Clickable>();
        if (clickable == null)
        {
            return;
        }

        clickable.Click(hit.point);
    }
}
