using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _hintImage;
    [SerializeField] private float _duration;
    [SerializeField] private float _showDelay;
    [SerializeField] private float _hideDelay;

    private Coroutine _coroutine;

    protected void Awake()
    {
        _canvasGroup.alpha = 0.0f;
    }

    public void Show(Item item)
    {
        if (_coroutine != null)
        {
            return;
        }

        _coroutine = StartCoroutine(ShowAndHideHint(item));
    }

    private IEnumerator ShowAndHideHint(Item item)
    {
        yield return new WaitForSeconds(_showDelay);

        _canvasGroup.alpha = 0.0f;

        _hintImage.sprite = item.sprite;

        for (var d = 0.0f; d < _duration; d += Time.deltaTime)
        {
            _canvasGroup.alpha = d / _duration;
            yield return null;
        }

        _canvasGroup.alpha = 1.0f;

        yield return new WaitForSeconds(_hideDelay);

        for (var d = 0.0f; d < _duration; d += Time.deltaTime)
        {
            _canvasGroup.alpha = 1.0f - d / _duration;
            yield return null;
        }

        _canvasGroup.alpha = 0.0f;

        _coroutine = null;
    }
}
