using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public event System.Action gameStateUpdatedEvent;

    private HashSet<string> _currentGameState = new()
    {
        GameStateProperties.ItemMap,
        GameStateProperties.ItemFrog,
        GameStateProperties.ItemScythe
    };

    public bool IsStateOn(string state)
    {
        return _currentGameState.Contains(state);
    }

    public void SetState(string state)
    {
        if (_currentGameState.Add(state))
        {
            gameStateUpdatedEvent?.Invoke();
        }
    }

    public void UnsetState(string state)
    {
        if (_currentGameState.Remove(state))
        {
            gameStateUpdatedEvent?.Invoke();
        }
    }
}
