using System;
using System.Collections;
using UnityEngine;

public class ThrowingController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    [Space]
    [SerializeField] private AnimationCurve _throwAnimationCurve;
    [SerializeField] private float _duration;
    [SerializeField] private float _multiplier;

    [Space]
    [SerializeField] private MockItem _mockItem;

    public float duration => _duration;

    protected void Awake()
    {
        _mockItem.gameObject.SetActive(false);
    }

    public IEnumerator ThrowItem(Item item, Vector3 target)
    {

        _mockItem.transform.SetParent(_characterController.ObjectPointTransform, worldPositionStays: false);
        _mockItem.SetItem(item);

        _mockItem.gameObject.SetActive(true);

        _characterController.Throw();

        yield return new WaitForSeconds(CharacterController.ThrowDelay);

        var tr = _mockItem.transform;
        tr.SetParent(null, worldPositionStays: true);

        var startPosition = tr.position;
        for (var d = 0.0f; d < _duration; d += Time.deltaTime)
        {
            var xDelta = d / _duration;
            var yDelta = _throwAnimationCurve.Evaluate(xDelta);

            tr.position = Vector3.Lerp(startPosition, target, xDelta) + Vector3.up * (_multiplier * yDelta);
            yield return null;
        }

        _mockItem.gameObject.SetActive(false);
    }
}
