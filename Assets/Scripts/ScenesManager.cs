using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private SceneReference[] _scenes;

    private Dictionary<string, GameScene> _propToScene = new();

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
            var loadedScene = SceneManager.GetSceneByPath(scene.ScenePath);

            Assert.AreEqual(loadedScene.rootCount, 1);

            var root = loadedScene.GetRootGameObjects()[0];
            var gameScene = root.GetComponent<GameScene>();

            _propToScene.Add(gameScene.gameSceneProperty.name, gameScene);
        }
    }

    public void EnableScene(string sceneProperty)
    {
        if (!_propToScene.TryGetValue(sceneProperty, out var gameScene))
        {
            Debug.LogWarning($"Couldn't find scene with name {sceneProperty}");
            return;
        }

        
    }
}
