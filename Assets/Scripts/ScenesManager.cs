using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private SceneReference[] _scenes;

    private readonly Dictionary<string, GameScene> _propToScene = new();

    protected IEnumerator Start()
    {
        var activeScenesCount = SceneManager.loadedSceneCount;
        var activeScenes = new HashSet<string>();
        for (var i = 0; i < activeScenesCount; i++)
        {
            activeScenes.Add(SceneManager.GetSceneAt(i).path);
        }

        foreach (var scene in _scenes)
        {
            if (!activeScenes.Contains(scene.ScenePath))
            {
                SceneManager.LoadScene(scene.ScenePath, LoadSceneMode.Additive);
            }
        }

        yield return null;

        foreach (var scene in _scenes)
        {
            var loadedScene = SceneManager.GetSceneByPath(scene.ScenePath);

            Assert.AreEqual(loadedScene.rootCount, 1);

            var root = loadedScene.GetRootGameObjects()[0];
            root.SetActive(true);

            var gameScene = root.GetComponent<GameScene>();
            if (gameScene == null)
            {
                continue;
            }

            gameScene.gameObject.SetActive(false);
            _propToScene.Add(gameScene.gameSceneProperty.name, gameScene);
        }
    }

    public GameScene GetScene(string sceneProperty)
    {
        if (!_propToScene.TryGetValue(sceneProperty, out var gameScene))
        {
            Debug.LogWarning($"Couldn't find scene with name {sceneProperty}");
            return null;
        }

        return gameScene;
    }
}
