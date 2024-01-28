using System;
using UnityEngine;
using UnityEngine.Audio;

public class AmbienceAudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private AudioMixerSnapshot _mapSnapshot;
    private AudioMixerSnapshot _gameSceneSnapshot;

    protected void Awake()
    {
        var previous = FindObjectOfType<AmbienceAudioController>();
        if (previous != null && previous != this)
        {
            gameObject.SetActive(false);

            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

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
