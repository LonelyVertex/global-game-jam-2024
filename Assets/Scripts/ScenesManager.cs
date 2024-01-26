using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private SceneReference[] _scenes;

    protected void Start()
    {
        var activeScenesCount = SceneManager.loadedSceneCount;
        var activeScenes = new HashSet<string>();
        for (var i = 0; i < activeScenesCount; i++)
        {
            activeScenes.Add(SceneManager.GetSceneAt(i).path);
        }

        foreach (var scene in _scenes)
        {
            if (activeScenes.Contains(scene.ScenePath))
            {
                continue;
            }

            SceneManager.LoadScene(scene.ScenePath, LoadSceneMode.Additive);
        }
    }
}
