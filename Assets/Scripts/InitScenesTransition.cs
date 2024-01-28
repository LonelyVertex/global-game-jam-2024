using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScenesTransition : MonoBehaviour
{
    [SerializeField] private MapTransition _mapTransition;
    [SerializeField] private SceneReference _mainMenu;

    IEnumerator Start()
    {
        yield return _mapTransition.SceneToMapFadeIn();

        SceneManager.LoadScene(_mainMenu.ScenePath);
    }
}
