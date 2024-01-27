using System.Collections;
using UnityEngine;

public class ThrowingController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    [Space]
    [SerializeField] private AnimationCurve _throwAnimationCurve;
    [SerializeField] private float _duration;

    [Space]
    [SerializeField] private MockItem _mockItem;

    public IEnumerator ThrowItem(Item item, Vector3 target)
    {
        _mockItem.transform.SetParent(_characterController.ObjectPointTransform);
        _mockItem.SetItem(item);

        _characterController.Throw();

        yield return new WaitForSeconds(CharacterController.ThrowDelay);

        var tr = _mockItem.transform;
        tr.SetParent(null, worldPositionStays: true);

        var startPosition = tr.position;
        for (var duration = 0.0f; duration < _duration; duration += Time.deltaTime)
        {
            var xDelta = duration / _duration;

            tr.position = Vector3.Lerp(startPosition, target, xDelta);
            yield return null;
        }
    }
}
