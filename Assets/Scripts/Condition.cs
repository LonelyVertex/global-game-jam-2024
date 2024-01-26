using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct Condition
{
    public string stateName;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Condition))]
public class ConditionEditor : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 1.0f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var stateName = property.FindPropertyRelative("stateName");

        var rect = new Rect(position.x, 0.0f, position.width, EditorGUIUtility.singleLineHeight);
        var index = EditorGUI.Popup(
            rect,
            "State Name",
            GameState.availableGameStates.IndexOf(stateName.stringValue),
            GameState.availableGameStates.ToArray()
        );
        stateName.stringValue = GameState.availableGameStates[index < 0 ? 0 : index];
    }
}
#endif
