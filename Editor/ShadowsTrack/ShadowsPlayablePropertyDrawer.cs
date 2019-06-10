using UnityEditor;
using UnityEngine;

// IngredientDrawerUIE
[CustomPropertyDrawer(typeof(ShadowsPlayable))]
public class ShadowsPlayableDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        ParameterEditorUtilities.ParameterField(property.FindPropertyRelative("maxDistance"), new GUIContent("Max Shadow Distance"));
        ParameterEditorUtilities.ParameterField(property.FindPropertyRelative("cascadesCount"), new GUIContent("Cascades Count"));
        ParameterEditorUtilities.SliderParameterField(property.FindPropertyRelative("split0"), new GUIContent("Split 0"));
        ParameterEditorUtilities.SliderParameterField(property.FindPropertyRelative("split1"), new GUIContent("Split 1"));
        ParameterEditorUtilities.SliderParameterField(property.FindPropertyRelative("split2"), new GUIContent("Split 2"));

        EditorGUI.EndProperty();
    }
}