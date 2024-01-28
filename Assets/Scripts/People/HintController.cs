using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    [SerializeField] private AudioController _audioController;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _hintImage;

    [SerializeField] private Image _hintImage1;
    [SerializeField] private Image _hintImage2;
    [SerializeField] private Image _hintImage3;

    [SerializeField] private float _duration;
    [SerializeField] private float _showDelay;
    [SerializeField] private float _hideDelay;

    public float showDelay => _showDelay;

    private Coroutine _coroutine;

    protected void Awake()
    {
        _canvasGroup.alpha = 0.0f;
    }

    private void OnEnable()
    {
        _canvasGroup.alpha = 0.0f;
    }

    private void OnDisable()
    {
        _coroutine = null;
    }

    public void Show(Item item)
    {
        if (_coroutine != null)
        {
            return;
        }

        _coroutine = StartCoroutine(ShowAndHideHint(
            () =>
            {
                if (_hintImage1 != null)
                {
                    _hintImage1.gameObject.SetActive(false);
                    _hintImage2.gameObject.SetActive(false);
                    _hintImage3.gameObject.SetActive(false);
                }

                _hintImage.gameObject.SetActive(true);

                _hintImage.sprite = item.sprite;
            }
        ));
    }

    public void Show(Item item1, Item item2, Item item3)
    {
        if (_coroutine != null)
        {
            return;
        }

        _coroutine = StartCoroutine(ShowAndHideHint(
            () =>
            {
                if (_hintImage1 != null)
                {
                    _hintImage1.gameObject.SetActive(true);
                    _hintImage2.gameObject.SetActive(true);
                    _hintImage3.gameObject.SetActive(true);
                }

                _hintImage.gameObject.SetActive(false);

                _hintImage1.sprite = item1.sprite;
                _hintImage2.sprite = item2.sprite;
                _hintImage3.sprite = item3.sprite;
            }
        ));
    }

    private IEnumerator ShowAndHideHint(Action action)
    {
        yield return new WaitForSeconds(_showDelay);

        _audioController.PlayHint();

        _canvasGroup.alpha = 0.0f;

        action?.Invoke();

        for (var d = 0.0f; d < _duration; d += Time.deltaTime)
        {
            _canvasGroup.alpha = d / _duration;
            yield return null;
        }

        _canvasGroup.alpha = 1.0f;

        _audioController.PlayRequirements();

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
