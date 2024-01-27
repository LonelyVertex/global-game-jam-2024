using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private SceneReference _gameScene;
    [SerializeField] private MapTransition _mapTransition;

    protected void Start()
    {
        _mapTransition.Hide();

        _playButton.onClick.AddListener(HandleOnPlayClick);
    }

    private void HandleOnPlayClick()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        FindObjectOfType<EventSystem>().enabled = false;

        yield return _mapTransition.SceneToMapFadeIn();

        SceneManager.LoadScene(_gameScene.ScenePath);
    }
}
