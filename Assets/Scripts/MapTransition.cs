using System.Collections;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private Animation _animation;

    [Space]
    [SerializeField] private AnimationClip _mapToSceneFadeIn;
    [SerializeField] private AnimationClip _mapToSceneFadeOut;
    [SerializeField] private AnimationClip _sceneToMapFadeIn;
    [SerializeField] private AnimationClip _sceneToMapFadeOut;

    [Space]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _cloudsIn;
    [SerializeField] private AudioClip _cloudsOut;

    public void Hide()
    {
        _mapToSceneFadeOut.SampleAnimation(gameObject, _mapToSceneFadeOut.length);
    }

    public IEnumerator MapToSceneFadeIn()
    {
        _animation.clip = _mapToSceneFadeIn;

        _audioSource.PlayOneShot(_cloudsIn);
        yield return Play();
    }

    public IEnumerator MapToSceneFadeOut()
    {
        _animation.clip = _mapToSceneFadeOut;

        _audioSource.PlayOneShot(_cloudsOut);
        yield return Play();
    }

    public IEnumerator SceneToMapFadeIn()
    {
        _animation.clip = _sceneToMapFadeIn;

        _audioSource.PlayOneShot(_cloudsIn);
        yield return Play();
    }

    public IEnumerator SceneToMapFadeOut()
    {
        _animation.clip = _sceneToMapFadeOut;

        _audioSource.PlayOneShot(_cloudsOut);
        yield return Play();
    }

    private IEnumerator Play()
    {
        _animation.Play();

        for (var duration = 0.0f; duration < _animation.clip.length; duration += Time.deltaTime)
        {
            yield return null;
        }
    }
}
