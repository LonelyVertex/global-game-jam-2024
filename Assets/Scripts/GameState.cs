using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static readonly List<string> availableGameStates = new()
    {
        "state_gran_has_beer",
        "state_frog_cooked",
        "state_well_without_frog",

        "item_beer",
        "item_money",
        "item_scythe",
        "item_strawberries",

        "item_frog",
        "item_potion",
        "item_flour",

        "item_bucket",
        "item_water"
    };

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
