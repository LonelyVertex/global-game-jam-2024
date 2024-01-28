using UnityEngine;
using UnityEngine.Audio;

public class AmbienceAudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private AudioMixerSnapshot _mapSnapshot;
    private AudioMixerSnapshot _gameSceneSnapshot;

    protected void Start()
    {
        _mapSnapshot = _audioMixer.FindSnapshot("Map");
        _gameSceneSnapshot = _audioMixer.FindSnapshot("GameScene");
    }

    public void TransitionToMap()
    {
        _mapSnapshot.TransitionTo(0.5f);
    }

    public void TransitionToGameScene()
    {
        _gameSceneSnapshot.TransitionTo(0.5f);
    }
}
