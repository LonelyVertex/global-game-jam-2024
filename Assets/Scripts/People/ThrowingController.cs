using System.Collections;
using UnityEngine;

public class ThrowingController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private AudioController _audioController;

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
        _characterController.Throw();
        _audioController.PlayThrow();

        yield return new WaitForSeconds(CharacterController.ThrowDelay);

        _mockItem.SetItem(item);
        _mockItem.gameObject.SetActive(true);

        var tr = _mockItem.transform;

        var startPosition = _characterController.ObjectPointTransform.position;
        for (var d = 0.0f; d < _duration; d += Time.deltaTime)
        {
            var xDelta = d / _duration;
            var yDelta = _throwAnimationCurve.Evaluate(xDelta);

            tr.position = Vector3.Lerp(startPosition, target, xDelta) + Vector3.up * (_multiplier * yDelta);
            yield return null;
        }

        _mockItem.gameObject.SetActive(false);
    }

    public IEnumerator ReverseThrowItem(Item item, Vector3 origin)
    {
        _mockItem.SetItem(item);
        _mockItem.gameObject.SetActive(true);

        _audioController.PlayThrow();

        var tr = _mockItem.transform;
        for (var d = 0.0f; d < _duration; d += Time.deltaTime)
        {
            var xDelta = d / _duration;
            var yDelta = _throwAnimationCurve.Evaluate(xDelta);

            tr.position = Vector3.Lerp(
                origin,
                _characterController.ObjectPointTransform.position,
                xDelta
            ) + Vector3.up * (_multiplier * yDelta);

            yield return null;
        }

        _mockItem.gameObject.SetActive(false);
    }
}
