using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameFinishedScene : MonoBehaviour
{
    [SerializeField] private MapTransition _mapTransition;
    [SerializeField] private AudioSource _inventoryClickAudioSource;
    [SerializeField] private AudioClip _click;
    [SerializeField] private InventoryItem _mapItem;
    [SerializeField] private SceneReference _mainMenu;
    [SerializeField] private EventSystem _eventSystem;

    [SerializeField] private Person[] _persons;

    protected IEnumerator Start()
    {
        _mapItem.inventoryItemClicked += HandleGoToMenu;

        _eventSystem.enabled = false;

        yield return _mapTransition.MapToSceneFadeOut();

        _eventSystem.enabled = true;

        foreach (var p in _persons)
        {
            p.characterController.Dance();
        }
    }

    private void HandleGoToMenu(Item obj)
    {
        _inventoryClickAudioSource.PlayOneShot(_click);
        StartCoroutine(GoToMenu());
    }

    private IEnumerator GoToMenu()
    {
        _eventSystem.enabled = false;

        yield return _mapTransition.SceneToMapFadeIn();

        SceneManager.LoadScene(_mainMenu);
    }
}
