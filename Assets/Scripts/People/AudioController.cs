using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [Space]
    [SerializeField] private AudioClip _hintAudioClip;
    [SerializeField] private AudioClip _requirementsAudioClip;
    [SerializeField] private AudioClip _successAudioClip;
    [SerializeField] private AudioClip _failAudioClip;
    [SerializeField] private AudioClip _throwSfx;

    public void PlayHint()
    {
        Play(_hintAudioClip);
    }

    public void PlayRequirements()
    {
        Play(_requirementsAudioClip);
    }

    public void PlaySuccess()
    {
        Play(_successAudioClip);
    }

    public void PlayFail()
    {
        Play(_failAudioClip);
    }

    public void PlayThrow()
    {
        Play(_throwSfx);
    }

    public void Play(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            Debug.Log("Missing audio clip.");
            return;
        }

        _audioSource.PlayOneShot(audioClip);
    }
}
