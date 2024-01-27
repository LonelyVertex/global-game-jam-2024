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

    public IEnumerator MapToSceneFadeIn()
    {
        _animation.clip = _mapToSceneFadeIn;

        yield return Play();
    }

    public IEnumerator MapToSceneFadeOut()
    {
        _animation.clip = _mapToSceneFadeOut;

        yield return Play();
    }

    public IEnumerator SceneToMapFadeIn()
    {
        _animation.clip = _sceneToMapFadeIn;

        yield return Play();
    }

    public IEnumerator SceneToMapFadeOut()
    {
        _animation.clip = _sceneToMapFadeOut;

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
