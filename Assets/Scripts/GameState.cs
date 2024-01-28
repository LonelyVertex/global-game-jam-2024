using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public event System.Action gameStateUpdatedEvent;

    public GameStateProperty[] _defaultProperties;

    private readonly HashSet<string> _currentGameState = new();

    protected void Awake()
    {
        foreach (var d in _defaultProperties)
        {
            _currentGameState.Add(d.name);
        }
    }

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
