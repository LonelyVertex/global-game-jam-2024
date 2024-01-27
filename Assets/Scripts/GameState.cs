using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private HashSet<string> _currentGameState;

    public bool IsStateOn(string state)
    {
        return _currentGameState.Contains(state);
    }

    public void SetState(string state)
    {
        _currentGameState.Add(state);
    }
}
